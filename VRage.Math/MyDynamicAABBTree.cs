// Decompiled with JetBrains decompiler
// Type: VRageMath.MyDynamicAABBTree
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Collections.Generic;
using VRage;

namespace VRageMath
{
    public class MyDynamicAABBTree
    {
        private static List<Stack<int>> m_StackCacheCollection = new List<Stack<int>>();
        private FastResourceLock m_rwLock = new FastResourceLock();
        public const int NullNode = -1;
        private int _freeList;
        private int _insertionCount;
        private int _nodeCapacity;
        private int _nodeCount;
        private MyDynamicAABBTree.DynamicTreeNode[] _nodes;
        private Dictionary<int, MyDynamicAABBTree.DynamicTreeNode> _leafElementCache;
        private int _root;
        [ThreadStatic] private static Stack<int> m_queryStack;
        private Vector3 m_extension;
        private float m_aabbMultiplier;

        private Stack<int> CurrentThreadStack
        {
            get
            {
                if (MyDynamicAABBTree.m_queryStack == null)
                {
                    MyDynamicAABBTree.m_queryStack = new Stack<int>(32);
                    lock (MyDynamicAABBTree.m_StackCacheCollection)
                        MyDynamicAABBTree.m_StackCacheCollection.Add(MyDynamicAABBTree.m_queryStack);
                }
                return MyDynamicAABBTree.m_queryStack;
            }
        }

        public MyDynamicAABBTree(Vector3 extension, float aabbMultiplier = 1f)
        {
            this._nodeCapacity = 256;
            this.m_extension = extension;
            this.m_aabbMultiplier = aabbMultiplier;
            this._nodes = new MyDynamicAABBTree.DynamicTreeNode[this._nodeCapacity];
            this._leafElementCache = new Dictionary<int, MyDynamicAABBTree.DynamicTreeNode>(this._nodeCapacity/4);
            for (int index = 0; index < this._nodeCapacity; ++index)
                this._nodes[index] = new MyDynamicAABBTree.DynamicTreeNode();
            Stack<int> currentThreadStack = this.CurrentThreadStack;
            this.ResetNodes();
        }

        private Stack<int> GetStack()
        {
            Stack<int> currentThreadStack = this.CurrentThreadStack;
            currentThreadStack.Clear();
            return currentThreadStack;
        }

        private void PushStack(Stack<int> stack)
        {
        }

        public int AddProxy(ref BoundingBox aabb, object userData, uint userFlags, bool rebalance = true)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_rwLock))
            {
                int leaf = this.AllocateNode();
                this._nodes[leaf].Aabb = aabb;
                this._nodes[leaf].Aabb.Min -= this.m_extension;
                this._nodes[leaf].Aabb.Max += this.m_extension;
                this._nodes[leaf].UserData = userData;
                this._nodes[leaf].UserFlag = userFlags;
                this._nodes[leaf].Height = 0;
                this._leafElementCache[leaf] = this._nodes[leaf];
                this.InsertLeaf(leaf, rebalance);
                return leaf;
            }
        }

        public void RemoveProxy(int proxyId)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_rwLock))
            {
                this._leafElementCache.Remove(proxyId);
                this.RemoveLeaf(proxyId);
                this.FreeNode(proxyId);
            }
        }

        public bool MoveProxy(int proxyId, ref BoundingBox aabb, Vector3 displacement)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_rwLock))
            {
                if (this._nodes[proxyId].Aabb.Contains(aabb) == ContainmentType.Contains)
                    return false;
                this.RemoveLeaf(proxyId);
                BoundingBox boundingBox = aabb;
                Vector3 vector3_1 = this.m_extension;
                boundingBox.Min -= vector3_1;
                boundingBox.Max += vector3_1;
                Vector3 vector3_2 = this.m_aabbMultiplier*displacement;
                if ((double) vector3_2.X < 0.0)
                    boundingBox.Min.X += vector3_2.X;
                else
                    boundingBox.Max.X += vector3_2.X;
                if ((double) vector3_2.Y < 0.0)
                    boundingBox.Min.Y += vector3_2.Y;
                else
                    boundingBox.Max.Y += vector3_2.Y;
                if ((double) vector3_2.Z < 0.0)
                    boundingBox.Min.Z += vector3_2.Z;
                else
                    boundingBox.Max.Z += vector3_2.Z;
                this._nodes[proxyId].Aabb = boundingBox;
                this.InsertLeaf(proxyId, true);
            }
            return true;
        }

        public T GetUserData<T>(int proxyId)
        {
            return (T) this._nodes[proxyId].UserData;
        }

        public int GetRoot()
        {
            return this._root;
        }

        public int GetLeafCount(int proxyId)
        {
            int num = 0;
            Stack<int> stack = this.GetStack();
            stack.Push(proxyId);
            while (stack.Count > 0)
            {
                int index = stack.Pop();
                if (index != -1)
                {
                    MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode = this._nodes[index];
                    if (dynamicTreeNode.IsLeaf())
                    {
                        ++num;
                    }
                    else
                    {
                        stack.Push(dynamicTreeNode.Child1);
                        stack.Push(dynamicTreeNode.Child2);
                    }
                }
            }
            this.PushStack(stack);
            return num;
        }

        public void GetNodeLeaves(int proxyId, List<int> children)
        {
            Stack<int> stack = this.GetStack();
            stack.Push(proxyId);
            while (stack.Count > 0)
            {
                int index = stack.Pop();
                if (index != -1)
                {
                    MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode = this._nodes[index];
                    if (dynamicTreeNode.IsLeaf())
                    {
                        children.Add(index);
                    }
                    else
                    {
                        stack.Push(dynamicTreeNode.Child1);
                        stack.Push(dynamicTreeNode.Child2);
                    }
                }
            }
            this.PushStack(stack);
        }

        public BoundingBox GetAabb(int proxyId)
        {
            return this._nodes[proxyId].Aabb;
        }

        public void GetChildren(int proxyId, out int left, out int right)
        {
            left = this._nodes[proxyId].Child1;
            right = this._nodes[proxyId].Child2;
        }

        private uint GetUserFlag(int proxyId)
        {
            return this._nodes[proxyId].UserFlag;
        }

        public void GetFatAABB(int proxyId, out BoundingBox fatAABB)
        {
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
                fatAABB = this._nodes[proxyId].Aabb;
        }

        public void Query(Func<int, bool> callback, ref BoundingBox aabb)
        {
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = new Stack<int>(256);
                stack.Push(this._root);
                while (stack.Count > 0)
                {
                    int index = stack.Pop();
                    if (index != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode = this._nodes[index];
                        if (dynamicTreeNode.Aabb.Intersects(aabb))
                        {
                            if (dynamicTreeNode.IsLeaf())
                            {
                                if (!callback(index))
                                    break;
                            }
                            else
                            {
                                stack.Push(dynamicTreeNode.Child1);
                                stack.Push(dynamicTreeNode.Child2);
                            }
                        }
                    }
                }
            }
        }

        public int CountLeaves(int nodeId)
        {
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                if (nodeId == -1)
                    return 0;
                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode = this._nodes[nodeId];
                if (dynamicTreeNode.IsLeaf())
                    return 1;
                else
                    return this.CountLeaves(dynamicTreeNode.Child1) + this.CountLeaves(dynamicTreeNode.Child2);
            }
        }

        private int AllocateNode()
        {
            if (this._freeList == -1)
            {
                MyDynamicAABBTree.DynamicTreeNode[] dynamicTreeNodeArray = this._nodes;
                this._nodeCapacity *= 2;
                this._nodes = new MyDynamicAABBTree.DynamicTreeNode[this._nodeCapacity];
                Array.Copy((Array) dynamicTreeNodeArray, (Array) this._nodes, this._nodeCount);
                for (int index = this._nodeCount; index < this._nodeCapacity - 1; ++index)
                {
                    this._nodes[index] = new MyDynamicAABBTree.DynamicTreeNode();
                    this._nodes[index].ParentOrNext = index + 1;
                    this._nodes[index].Height = 1;
                }
                this._nodes[this._nodeCapacity - 1] = new MyDynamicAABBTree.DynamicTreeNode();
                this._nodes[this._nodeCapacity - 1].ParentOrNext = -1;
                this._nodes[this._nodeCapacity - 1].Height = 1;
                this._freeList = this._nodeCount;
            }
            int index1 = this._freeList;
            this._freeList = this._nodes[index1].ParentOrNext;
            this._nodes[index1].ParentOrNext = -1;
            this._nodes[index1].Child1 = -1;
            this._nodes[index1].Child2 = -1;
            this._nodes[index1].Height = 0;
            this._nodes[index1].UserData = (object) null;
            ++this._nodeCount;
            return index1;
        }

        private void FreeNode(int nodeId)
        {
            this._nodes[nodeId].ParentOrNext = this._freeList;
            this._nodes[nodeId].Height = -1;
            this._nodes[nodeId].UserData = (object) null;
            this._freeList = nodeId;
            --this._nodeCount;
        }

        private void InsertLeaf(int leaf, bool rebalance)
        {
            ++this._insertionCount;
            if (this._root == -1)
            {
                this._root = leaf;
                this._nodes[this._root].ParentOrNext = -1;
            }
            else
            {
                BoundingBox original = this._nodes[leaf].Aabb;
                int index1 = this._root;
                while (!this._nodes[index1].IsLeaf())
                {
                    int index2 = this._nodes[index1].Child1;
                    int index3 = this._nodes[index1].Child2;
                    if (rebalance)
                    {
                        float num1 = this._nodes[index1].Aabb.Perimeter();
                        float num2 = BoundingBox.CreateMerged(this._nodes[index1].Aabb, original).Perimeter();
                        float num3 = 2f*num2;
                        float num4 = (float) (2.0*((double) num2 - (double) num1));
                        float num5;
                        if (this._nodes[index2].IsLeaf())
                        {
                            BoundingBox result;
                            BoundingBox.CreateMerged(ref original, ref this._nodes[index2].Aabb, out result);
                            num5 = result.Perimeter() + num4;
                        }
                        else
                        {
                            BoundingBox result;
                            BoundingBox.CreateMerged(ref original, ref this._nodes[index2].Aabb, out result);
                            float num6 = this._nodes[index2].Aabb.Perimeter();
                            num5 = result.Perimeter() - num6 + num4;
                        }
                        float num7;
                        if (this._nodes[index3].IsLeaf())
                        {
                            BoundingBox result;
                            BoundingBox.CreateMerged(ref original, ref this._nodes[index3].Aabb, out result);
                            num7 = result.Perimeter() + num4;
                        }
                        else
                        {
                            BoundingBox result;
                            BoundingBox.CreateMerged(ref original, ref this._nodes[index3].Aabb, out result);
                            float num6 = this._nodes[index3].Aabb.Perimeter();
                            num7 = result.Perimeter() - num6 + num4;
                        }
                        if ((double) num3 >= (double) num5 || (double) num5 >= (double) num7)
                            index1 = (double) num5 >= (double) num7 ? index3 : index2;
                        else
                            break;
                    }
                    else
                    {
                        BoundingBox result1;
                        BoundingBox.CreateMerged(ref original, ref this._nodes[index2].Aabb, out result1);
                        BoundingBox result2;
                        BoundingBox.CreateMerged(ref original, ref this._nodes[index3].Aabb, out result2);
                        index1 = (double) ((float) (this._nodes[index2].Height + 1)*result1.Perimeter()) >=
                                 (double) ((float) (this._nodes[index3].Height + 1)*result2.Perimeter())
                            ? index3
                            : index2;
                    }
                }
                int index4 = index1;
                int index5 = this._nodes[index1].ParentOrNext;
                int index6 = this.AllocateNode();
                this._nodes[index6].ParentOrNext = index5;
                this._nodes[index6].UserData = (object) null;
                this._nodes[index6].Aabb = BoundingBox.CreateMerged(original, this._nodes[index4].Aabb);
                this._nodes[index6].Height = this._nodes[index4].Height + 1;
                if (index5 != -1)
                {
                    if (this._nodes[index5].Child1 == index4)
                        this._nodes[index5].Child1 = index6;
                    else
                        this._nodes[index5].Child2 = index6;
                    this._nodes[index6].Child1 = index4;
                    this._nodes[index6].Child2 = leaf;
                    this._nodes[index1].ParentOrNext = index6;
                    this._nodes[leaf].ParentOrNext = index6;
                }
                else
                {
                    this._nodes[index6].Child1 = index4;
                    this._nodes[index6].Child2 = leaf;
                    this._nodes[index1].ParentOrNext = index6;
                    this._nodes[leaf].ParentOrNext = index6;
                    this._root = index6;
                }
                for (int iA = this._nodes[leaf].ParentOrNext; iA != -1; iA = this._nodes[iA].ParentOrNext)
                {
                    if (rebalance)
                        iA = this.Balance(iA);
                    int index2 = this._nodes[iA].Child1;
                    int index3 = this._nodes[iA].Child2;
                    this._nodes[iA].Height = 1 + Math.Max(this._nodes[index2].Height, this._nodes[index3].Height);
                    BoundingBox.CreateMerged(ref this._nodes[index2].Aabb, ref this._nodes[index3].Aabb,
                        out this._nodes[iA].Aabb);
                }
            }
        }

        private void RemoveLeaf(int leaf)
        {
        }

        public int GetHeight()
        {
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                if (this._root == -1)
                    return 0;
                else
                    return this._nodes[this._root].Height;
            }
        }

        public int Balance(int iA)
        {
            MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode1 = this._nodes[iA];
            if (dynamicTreeNode1.IsLeaf() || dynamicTreeNode1.Height < 2)
                return iA;
            int index1 = dynamicTreeNode1.Child1;
            int index2 = dynamicTreeNode1.Child2;
            MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode2 = this._nodes[index1];
            MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode3 = this._nodes[index2];
            int num = dynamicTreeNode3.Height - dynamicTreeNode2.Height;
            if (num > 1)
            {
                int index3 = dynamicTreeNode3.Child1;
                int index4 = dynamicTreeNode3.Child2;
                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode4 = this._nodes[index3];
                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode5 = this._nodes[index4];
                dynamicTreeNode3.Child1 = iA;
                dynamicTreeNode3.ParentOrNext = dynamicTreeNode1.ParentOrNext;
                dynamicTreeNode1.ParentOrNext = index2;
                if (dynamicTreeNode3.ParentOrNext != -1)
                {
                    if (this._nodes[dynamicTreeNode3.ParentOrNext].Child1 == iA)
                        this._nodes[dynamicTreeNode3.ParentOrNext].Child1 = index2;
                    else
                        this._nodes[dynamicTreeNode3.ParentOrNext].Child2 = index2;
                }
                else
                    this._root = index2;
                if (dynamicTreeNode4.Height > dynamicTreeNode5.Height)
                {
                    dynamicTreeNode3.Child2 = index3;
                    dynamicTreeNode1.Child2 = index4;
                    dynamicTreeNode5.ParentOrNext = iA;
                    BoundingBox.CreateMerged(ref dynamicTreeNode2.Aabb, ref dynamicTreeNode5.Aabb,
                        out dynamicTreeNode1.Aabb);
                    BoundingBox.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode4.Aabb,
                        out dynamicTreeNode3.Aabb);
                    dynamicTreeNode1.Height = 1 + Math.Max(dynamicTreeNode2.Height, dynamicTreeNode5.Height);
                    dynamicTreeNode3.Height = 1 + Math.Max(dynamicTreeNode1.Height, dynamicTreeNode4.Height);
                }
                else
                {
                    dynamicTreeNode3.Child2 = index4;
                    dynamicTreeNode1.Child2 = index3;
                    dynamicTreeNode4.ParentOrNext = iA;
                    BoundingBox.CreateMerged(ref dynamicTreeNode2.Aabb, ref dynamicTreeNode4.Aabb,
                        out dynamicTreeNode1.Aabb);
                    BoundingBox.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode5.Aabb,
                        out dynamicTreeNode3.Aabb);
                    dynamicTreeNode1.Height = 1 + Math.Max(dynamicTreeNode2.Height, dynamicTreeNode4.Height);
                    dynamicTreeNode3.Height = 1 + Math.Max(dynamicTreeNode1.Height, dynamicTreeNode5.Height);
                }
                return index2;
            }
            else
            {
                if (num >= -1)
                    return iA;
                int index3 = dynamicTreeNode2.Child1;
                int index4 = dynamicTreeNode2.Child2;
                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode4 = this._nodes[index3];
                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode5 = this._nodes[index4];
                dynamicTreeNode2.Child1 = iA;
                dynamicTreeNode2.ParentOrNext = dynamicTreeNode1.ParentOrNext;
                dynamicTreeNode1.ParentOrNext = index1;
                if (dynamicTreeNode2.ParentOrNext != -1)
                {
                    if (this._nodes[dynamicTreeNode2.ParentOrNext].Child1 == iA)
                        this._nodes[dynamicTreeNode2.ParentOrNext].Child1 = index1;
                    else
                        this._nodes[dynamicTreeNode2.ParentOrNext].Child2 = index1;
                }
                else
                    this._root = index1;
                if (dynamicTreeNode4.Height > dynamicTreeNode5.Height)
                {
                    dynamicTreeNode2.Child2 = index3;
                    dynamicTreeNode1.Child1 = index4;
                    dynamicTreeNode5.ParentOrNext = iA;
                    BoundingBox.CreateMerged(ref dynamicTreeNode3.Aabb, ref dynamicTreeNode5.Aabb,
                        out dynamicTreeNode1.Aabb);
                    BoundingBox.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode4.Aabb,
                        out dynamicTreeNode2.Aabb);
                    dynamicTreeNode1.Height = 1 + Math.Max(dynamicTreeNode3.Height, dynamicTreeNode5.Height);
                    dynamicTreeNode2.Height = 1 + Math.Max(dynamicTreeNode1.Height, dynamicTreeNode4.Height);
                }
                else
                {
                    dynamicTreeNode2.Child2 = index4;
                    dynamicTreeNode1.Child1 = index3;
                    dynamicTreeNode4.ParentOrNext = iA;
                    BoundingBox.CreateMerged(ref dynamicTreeNode3.Aabb, ref dynamicTreeNode4.Aabb,
                        out dynamicTreeNode1.Aabb);
                    BoundingBox.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode5.Aabb,
                        out dynamicTreeNode2.Aabb);
                    dynamicTreeNode1.Height = 1 + Math.Max(dynamicTreeNode3.Height, dynamicTreeNode4.Height);
                    dynamicTreeNode2.Height = 1 + Math.Max(dynamicTreeNode1.Height, dynamicTreeNode5.Height);
                }
                return index1;
            }
        }

        public void OverlapAllFrustum<T>(ref BoundingFrustum frustum, List<T> elementsList, bool clear = true)
        {
            this.OverlapAllFrustum<T>(ref frustum, elementsList, 0U, clear);
        }

        public void OverlapAllFrustum<T>(ref BoundingFrustum frustum, List<T> elementsList, uint requiredFlags,
            bool clear = true)
        {
            if (clear)
                elementsList.Clear();
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                while (stack.Count > 0)
                {
                    int proxyId1 = stack.Pop();
                    if (proxyId1 != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode1 = this._nodes[proxyId1];
                        ContainmentType result;
                        frustum.Contains(ref dynamicTreeNode1.Aabb, out result);
                        if (result == ContainmentType.Contains)
                        {
                            int count = stack.Count;
                            stack.Push(proxyId1);
                            while (stack.Count > count)
                            {
                                int proxyId2 = stack.Pop();
                                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode2 = this._nodes[proxyId2];
                                if (dynamicTreeNode2.IsLeaf())
                                {
                                    if (((int) this.GetUserFlag(proxyId2) & (int) requiredFlags) == (int) requiredFlags)
                                        elementsList.Add(this.GetUserData<T>(proxyId2));
                                }
                                else
                                {
                                    if (dynamicTreeNode2.Child1 != -1)
                                        stack.Push(dynamicTreeNode2.Child1);
                                    if (dynamicTreeNode2.Child2 != -1)
                                        stack.Push(dynamicTreeNode2.Child2);
                                }
                            }
                        }
                        else if (result == ContainmentType.Intersects)
                        {
                            if (dynamicTreeNode1.IsLeaf())
                            {
                                if (((int) this.GetUserFlag(proxyId1) & (int) requiredFlags) == (int) requiredFlags)
                                    elementsList.Add(this.GetUserData<T>(proxyId1));
                            }
                            else
                            {
                                stack.Push(dynamicTreeNode1.Child1);
                                stack.Push(dynamicTreeNode1.Child2);
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void OverlapAllFrustum<T>(ref BoundingFrustum frustum, List<T> elementsList, List<bool> isInsideList,
            uint requiredFlags, bool clear = true)
        {
            if (clear)
            {
                elementsList.Clear();
                isInsideList.Clear();
            }
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                while (stack.Count > 0)
                {
                    int proxyId1 = stack.Pop();
                    if (proxyId1 != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode1 = this._nodes[proxyId1];
                        ContainmentType result;
                        frustum.Contains(ref dynamicTreeNode1.Aabb, out result);
                        if (result == ContainmentType.Contains)
                        {
                            int count = stack.Count;
                            stack.Push(proxyId1);
                            while (stack.Count > count)
                            {
                                int proxyId2 = stack.Pop();
                                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode2 = this._nodes[proxyId2];
                                if (dynamicTreeNode2.IsLeaf())
                                {
                                    if (((int) this.GetUserFlag(proxyId2) & (int) requiredFlags) == (int) requiredFlags)
                                    {
                                        elementsList.Add(this.GetUserData<T>(proxyId2));
                                        isInsideList.Add(true);
                                    }
                                }
                                else
                                {
                                    if (dynamicTreeNode2.Child1 != -1)
                                        stack.Push(dynamicTreeNode2.Child1);
                                    if (dynamicTreeNode2.Child2 != -1)
                                        stack.Push(dynamicTreeNode2.Child2);
                                }
                            }
                        }
                        else if (result == ContainmentType.Intersects)
                        {
                            if (dynamicTreeNode1.IsLeaf())
                            {
                                if (((int) this.GetUserFlag(proxyId1) & (int) requiredFlags) == (int) requiredFlags)
                                {
                                    elementsList.Add(this.GetUserData<T>(proxyId1));
                                    isInsideList.Add(false);
                                }
                            }
                            else
                            {
                                stack.Push(dynamicTreeNode1.Child1);
                                stack.Push(dynamicTreeNode1.Child2);
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void OverlapAllFrustum<T>(ref BoundingFrustum frustum, List<T> elementsList, List<bool> isInsideList,
            Vector3 projectionDir, float projectionFactor, float ignoreThr, uint requiredFlags, bool clear = true)
        {
            if (clear)
            {
                elementsList.Clear();
                isInsideList.Clear();
            }
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                while (stack.Count > 0)
                {
                    int proxyId1 = stack.Pop();
                    if (proxyId1 != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode1 = this._nodes[proxyId1];
                        ContainmentType result;
                        frustum.Contains(ref dynamicTreeNode1.Aabb, out result);
                        if (result == ContainmentType.Contains)
                        {
                            int count = stack.Count;
                            stack.Push(proxyId1);
                            while (stack.Count > count)
                            {
                                int proxyId2 = stack.Pop();
                                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode2 = this._nodes[proxyId2];
                                if (dynamicTreeNode2.IsLeaf())
                                {
                                    if ((double) dynamicTreeNode2.Aabb.ProjectedArea(projectionDir)*
                                        (double) projectionFactor > (double) ignoreThr &&
                                        ((int) this.GetUserFlag(proxyId2) & (int) requiredFlags) == (int) requiredFlags)
                                    {
                                        elementsList.Add(this.GetUserData<T>(proxyId2));
                                        isInsideList.Add(true);
                                    }
                                }
                                else
                                {
                                    if (dynamicTreeNode2.Child1 != -1)
                                        stack.Push(dynamicTreeNode2.Child1);
                                    if (dynamicTreeNode2.Child2 != -1)
                                        stack.Push(dynamicTreeNode2.Child2);
                                }
                            }
                        }
                        else if (result == ContainmentType.Intersects)
                        {
                            if (dynamicTreeNode1.IsLeaf())
                            {
                                if ((double) dynamicTreeNode1.Aabb.ProjectedArea(projectionDir)*
                                    (double) projectionFactor > (double) ignoreThr &&
                                    ((int) this.GetUserFlag(proxyId1) & (int) requiredFlags) == (int) requiredFlags)
                                {
                                    elementsList.Add(this.GetUserData<T>(proxyId1));
                                    isInsideList.Add(false);
                                }
                            }
                            else
                            {
                                stack.Push(dynamicTreeNode1.Child1);
                                stack.Push(dynamicTreeNode1.Child2);
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void OverlapAllFrustumConservative<T>(ref BoundingFrustum frustum, List<T> elementsList,
            uint requiredFlags, bool clear = true)
        {
            if (clear)
                elementsList.Clear();
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                BoundingBox invalid = BoundingBox.CreateInvalid();
                invalid.Include(ref frustum);
                while (stack.Count > 0)
                {
                    int proxyId1 = stack.Pop();
                    if (proxyId1 != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode1 = this._nodes[proxyId1];
                        if (dynamicTreeNode1.Aabb.Intersects(invalid))
                        {
                            ContainmentType result;
                            frustum.Contains(ref dynamicTreeNode1.Aabb, out result);
                            if (result == ContainmentType.Contains)
                            {
                                int count = stack.Count;
                                stack.Push(proxyId1);
                                while (stack.Count > count)
                                {
                                    int proxyId2 = stack.Pop();
                                    MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode2 = this._nodes[proxyId2];
                                    if (dynamicTreeNode2.IsLeaf())
                                    {
                                        if (((int) this.GetUserFlag(proxyId2) & (int) requiredFlags) ==
                                            (int) requiredFlags)
                                            elementsList.Add(this.GetUserData<T>(proxyId2));
                                    }
                                    else
                                    {
                                        if (dynamicTreeNode2.Child1 != -1)
                                            stack.Push(dynamicTreeNode2.Child1);
                                        if (dynamicTreeNode2.Child2 != -1)
                                            stack.Push(dynamicTreeNode2.Child2);
                                    }
                                }
                            }
                            else if (result == ContainmentType.Intersects)
                            {
                                if (dynamicTreeNode1.IsLeaf())
                                {
                                    if (((int) this.GetUserFlag(proxyId1) & (int) requiredFlags) == (int) requiredFlags)
                                        elementsList.Add(this.GetUserData<T>(proxyId1));
                                }
                                else
                                {
                                    stack.Push(dynamicTreeNode1.Child1);
                                    stack.Push(dynamicTreeNode1.Child2);
                                }
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void OverlapAllFrustumAny<T>(ref BoundingFrustum frustum, List<T> elementsList, bool clear = true)
        {
            if (clear)
                elementsList.Clear();
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                while (stack.Count > 0)
                {
                    int proxyId1 = stack.Pop();
                    if (proxyId1 != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode1 = this._nodes[proxyId1];
                        ContainmentType result;
                        frustum.Contains(ref dynamicTreeNode1.Aabb, out result);
                        if (result == ContainmentType.Contains)
                        {
                            int count = stack.Count;
                            stack.Push(proxyId1);
                            while (stack.Count > count)
                            {
                                int proxyId2 = stack.Pop();
                                MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode2 = this._nodes[proxyId2];
                                if (dynamicTreeNode2.IsLeaf())
                                {
                                    T userData = this.GetUserData<T>(proxyId2);
                                    elementsList.Add(userData);
                                }
                                else
                                {
                                    if (dynamicTreeNode2.Child1 != -1)
                                        stack.Push(dynamicTreeNode2.Child1);
                                    if (dynamicTreeNode2.Child2 != -1)
                                        stack.Push(dynamicTreeNode2.Child2);
                                }
                            }
                        }
                        else if (result == ContainmentType.Intersects)
                        {
                            if (dynamicTreeNode1.IsLeaf())
                            {
                                T userData = this.GetUserData<T>(proxyId1);
                                elementsList.Add(userData);
                            }
                            else
                            {
                                stack.Push(dynamicTreeNode1.Child1);
                                stack.Push(dynamicTreeNode1.Child2);
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void OverlapAllLineSegment<T>(ref Line line, List<MyLineSegmentOverlapResult<T>> elementsList)
        {
            this.OverlapAllLineSegment<T>(ref line, elementsList, 0U);
        }

        public void OverlapAllLineSegment<T>(ref Line line, List<MyLineSegmentOverlapResult<T>> elementsList,
            uint requiredFlags)
        {
            elementsList.Clear();
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                BoundingBox invalid = BoundingBox.CreateInvalid();
                invalid.Include(ref line);
                Ray ray = new Ray(line.From, line.Direction);
                while (stack.Count > 0)
                {
                    int proxyId = stack.Pop();
                    if (proxyId != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode = this._nodes[proxyId];
                        if (dynamicTreeNode.Aabb.Intersects(invalid))
                        {
                            float? nullable = dynamicTreeNode.Aabb.Intersects(ray);
                            if (nullable.HasValue && (double) nullable.Value <= (double) line.Length &&
                                (double) nullable.Value >= 0.0)
                            {
                                if (dynamicTreeNode.IsLeaf())
                                {
                                    if (((int) this.GetUserFlag(proxyId) & (int) requiredFlags) == (int) requiredFlags)
                                        elementsList.Add(new MyLineSegmentOverlapResult<T>()
                                        {
                                            Element = this.GetUserData<T>(proxyId),
                                            Distance = (double) nullable.Value
                                        });
                                }
                                else
                                {
                                    stack.Push(dynamicTreeNode.Child1);
                                    stack.Push(dynamicTreeNode.Child2);
                                }
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void OverlapAllBoundingBox<T>(ref BoundingBox bbox, List<T> elementsList, uint requiredFlags = 0U,
            bool clear = true)
        {
            if (clear)
                elementsList.Clear();
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                while (stack.Count > 0)
                {
                    int proxyId = stack.Pop();
                    if (proxyId != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode = this._nodes[proxyId];
                        if (dynamicTreeNode.Aabb.Intersects(bbox))
                        {
                            if (dynamicTreeNode.IsLeaf())
                            {
                                if (((int) this.GetUserFlag(proxyId) & (int) requiredFlags) == (int) requiredFlags)
                                    elementsList.Add(this.GetUserData<T>(proxyId));
                            }
                            else
                            {
                                stack.Push(dynamicTreeNode.Child1);
                                stack.Push(dynamicTreeNode.Child2);
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void OverlapAllBoundingSphere<T>(ref BoundingSphere sphere, List<T> overlapElementsList,
            bool clear = true)
        {
            if (clear)
                overlapElementsList.Clear();
            if (this._root == -1)
                return;
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                Stack<int> stack = this.GetStack();
                stack.Push(this._root);
                while (stack.Count > 0)
                {
                    int proxyId = stack.Pop();
                    if (proxyId != -1)
                    {
                        MyDynamicAABBTree.DynamicTreeNode dynamicTreeNode = this._nodes[proxyId];
                        if (dynamicTreeNode.Aabb.Intersects(sphere))
                        {
                            if (dynamicTreeNode.IsLeaf())
                            {
                                overlapElementsList.Add(this.GetUserData<T>(proxyId));
                            }
                            else
                            {
                                stack.Push(dynamicTreeNode.Child1);
                                stack.Push(dynamicTreeNode.Child2);
                            }
                        }
                    }
                }
                this.PushStack(stack);
            }
        }

        public void GetAll<T>(List<T> elementsList, bool clear, List<BoundingBox> boxsList = null)
        {
            if (clear)
            {
                elementsList.Clear();
                if (boxsList != null)
                    boxsList.Clear();
            }
            using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
            {
                foreach (KeyValuePair<int, MyDynamicAABBTree.DynamicTreeNode> keyValuePair in this._leafElementCache)
                    elementsList.Add((T) keyValuePair.Value.UserData);
                if (boxsList == null)
                    return;
                foreach (KeyValuePair<int, MyDynamicAABBTree.DynamicTreeNode> keyValuePair in this._leafElementCache)
                    boxsList.Add(keyValuePair.Value.Aabb);
            }
        }

        private void ResetNodes()
        {
            this._leafElementCache.Clear();
            this._root = -1;
            this._nodeCount = 0;
            this._insertionCount = 0;
            for (int index = 0; index < this._nodeCapacity - 1; ++index)
            {
                this._nodes[index].ParentOrNext = index + 1;
                this._nodes[index].Height = 1;
                this._nodes[index].UserData = (object) null;
            }
            this._nodes[this._nodeCapacity - 1].ParentOrNext = -1;
            this._nodes[this._nodeCapacity - 1].Height = 1;
            this._freeList = 0;
        }

        public void Clear()
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_rwLock))
            {
                lock (MyDynamicAABBTree.m_StackCacheCollection)
                {
                    foreach (Stack<int> item_0 in MyDynamicAABBTree.m_StackCacheCollection)
                        item_0.Clear();
                    MyDynamicAABBTree.m_StackCacheCollection.Clear();
                }
                this.ResetNodes();
            }
        }

        internal class DynamicTreeNode
        {
            internal BoundingBox Aabb;
            internal int Child1;
            internal int Child2;
            internal int Height;
            internal int ParentOrNext;
            internal object UserData;
            internal uint UserFlag;

            internal bool IsLeaf()
            {
                return this.Child1 == -1;
            }
        }
    }
}