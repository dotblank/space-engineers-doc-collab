// Decompiled with JetBrains decompiler
// Type: VRageMath.MyDynamicAABBTreeD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Collections.Generic;
using VRage;

namespace VRageMath
{
  public class MyDynamicAABBTreeD
  {
    private static List<Stack<int>> m_StackCacheCollection = new List<Stack<int>>();
    private FastResourceLock m_rwLock = new FastResourceLock();
    public const int NullNode = -1;
    private int _freeList;
    private int _insertionCount;
    private int _nodeCapacity;
    private int _nodeCount;
    private MyDynamicAABBTreeD.DynamicTreeNode[] _nodes;
    private Dictionary<int, MyDynamicAABBTreeD.DynamicTreeNode> _leafElementCache;
    private int _root;
    [ThreadStatic]
    private static Stack<int> m_queryStack;
    private Vector3D m_extension;
    private double m_aabbMultiplier;

    private Stack<int> CurrentThreadStack
    {
      get
      {
        if (MyDynamicAABBTreeD.m_queryStack == null)
        {
          MyDynamicAABBTreeD.m_queryStack = new Stack<int>(32);
          lock (MyDynamicAABBTreeD.m_StackCacheCollection)
            MyDynamicAABBTreeD.m_StackCacheCollection.Add(MyDynamicAABBTreeD.m_queryStack);
        }
        return MyDynamicAABBTreeD.m_queryStack;
      }
    }

    public MyDynamicAABBTreeD(Vector3D extension, double aabbMultiplier = 1.0)
    {
      this._nodeCapacity = 256;
      this.m_extension = extension;
      this.m_aabbMultiplier = aabbMultiplier;
      this._nodes = new MyDynamicAABBTreeD.DynamicTreeNode[this._nodeCapacity];
      this._leafElementCache = new Dictionary<int, MyDynamicAABBTreeD.DynamicTreeNode>(this._nodeCapacity / 4);
      for (int index = 0; index < this._nodeCapacity; ++index)
        this._nodes[index] = new MyDynamicAABBTreeD.DynamicTreeNode();
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

    public int AddProxy(ref BoundingBoxD aabb, object userData, uint userFlags, bool rebalance = true)
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

    public bool MoveProxy(int proxyId, ref BoundingBoxD aabb, Vector3D displacement)
    {
      using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_rwLock))
      {
        if (this._nodes[proxyId].Aabb.Contains(aabb) == ContainmentType.Contains)
          return false;
        this.RemoveLeaf(proxyId);
        BoundingBoxD boundingBoxD = aabb;
        Vector3D vector3D1 = this.m_extension;
        boundingBoxD.Min -= vector3D1;
        boundingBoxD.Max += vector3D1;
        Vector3D vector3D2 = this.m_aabbMultiplier * displacement;
        if (vector3D2.X < 0.0)
          boundingBoxD.Min.X += vector3D2.X;
        else
          boundingBoxD.Max.X += vector3D2.X;
        if (vector3D2.Y < 0.0)
          boundingBoxD.Min.Y += vector3D2.Y;
        else
          boundingBoxD.Max.Y += vector3D2.Y;
        if (vector3D2.Z < 0.0)
          boundingBoxD.Min.Z += vector3D2.Z;
        else
          boundingBoxD.Max.Z += vector3D2.Z;
        this._nodes[proxyId].Aabb = boundingBoxD;
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
          MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode = this._nodes[index];
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
          MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode = this._nodes[index];
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

    public BoundingBoxD GetAabb(int proxyId)
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

    public void GetFatAABB(int proxyId, out BoundingBoxD fatAABB)
    {
      using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
        fatAABB = this._nodes[proxyId].Aabb;
    }

    public void Query(Func<int, bool> callback, ref BoundingBoxD aabb)
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
            MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode = this._nodes[index];
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
        MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode = this._nodes[nodeId];
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
        MyDynamicAABBTreeD.DynamicTreeNode[] dynamicTreeNodeArray = this._nodes;
        this._nodeCapacity *= 2;
        this._nodes = new MyDynamicAABBTreeD.DynamicTreeNode[this._nodeCapacity];
        Array.Copy((Array) dynamicTreeNodeArray, (Array) this._nodes, this._nodeCount);
        for (int index = this._nodeCount; index < this._nodeCapacity - 1; ++index)
        {
          this._nodes[index] = new MyDynamicAABBTreeD.DynamicTreeNode();
          this._nodes[index].ParentOrNext = index + 1;
          this._nodes[index].Height = 1;
        }
        this._nodes[this._nodeCapacity - 1] = new MyDynamicAABBTreeD.DynamicTreeNode();
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
        BoundingBoxD original = this._nodes[leaf].Aabb;
        int index1 = this._root;
        while (!this._nodes[index1].IsLeaf())
        {
          int index2 = this._nodes[index1].Child1;
          int index3 = this._nodes[index1].Child2;
          if (rebalance)
          {
            double num1 = this._nodes[index1].Aabb.Perimeter();
            double num2 = BoundingBoxD.CreateMerged(this._nodes[index1].Aabb, original).Perimeter();
            double num3 = 2.0 * num2;
            double num4 = 2.0 * (num2 - num1);
            double num5;
            if (this._nodes[index2].IsLeaf())
            {
              BoundingBoxD result;
              BoundingBoxD.CreateMerged(ref original, ref this._nodes[index2].Aabb, out result);
              num5 = result.Perimeter() + num4;
            }
            else
            {
              BoundingBoxD result;
              BoundingBoxD.CreateMerged(ref original, ref this._nodes[index2].Aabb, out result);
              double num6 = this._nodes[index2].Aabb.Perimeter();
              num5 = result.Perimeter() - num6 + num4;
            }
            double num7;
            if (this._nodes[index3].IsLeaf())
            {
              BoundingBoxD result;
              BoundingBoxD.CreateMerged(ref original, ref this._nodes[index3].Aabb, out result);
              num7 = result.Perimeter() + num4;
            }
            else
            {
              BoundingBoxD result;
              BoundingBoxD.CreateMerged(ref original, ref this._nodes[index3].Aabb, out result);
              double num6 = this._nodes[index3].Aabb.Perimeter();
              num7 = result.Perimeter() - num6 + num4;
            }
            if (num3 >= num5 || num5 >= num7)
              index1 = num5 >= num7 ? index3 : index2;
            else
              break;
          }
          else
          {
            BoundingBoxD result1;
            BoundingBoxD.CreateMerged(ref original, ref this._nodes[index2].Aabb, out result1);
            BoundingBoxD result2;
            BoundingBoxD.CreateMerged(ref original, ref this._nodes[index3].Aabb, out result2);
            index1 = (double) (this._nodes[index2].Height + 1) * result1.Perimeter() >= (double) (this._nodes[index3].Height + 1) * result2.Perimeter() ? index3 : index2;
          }
        }
        int index4 = index1;
        int index5 = this._nodes[index1].ParentOrNext;
        int index6 = this.AllocateNode();
        this._nodes[index6].ParentOrNext = index5;
        this._nodes[index6].UserData = (object) null;
        this._nodes[index6].Aabb = BoundingBoxD.CreateMerged(original, this._nodes[index4].Aabb);
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
          BoundingBoxD.CreateMerged(ref this._nodes[index2].Aabb, ref this._nodes[index3].Aabb, out this._nodes[iA].Aabb);
        }
      }
    }

    private void RemoveLeaf(int leaf)
    {
        throw new System.NotImplementedException();
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
      MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode1 = this._nodes[iA];
      if (dynamicTreeNode1.IsLeaf() || dynamicTreeNode1.Height < 2)
        return iA;
      int index1 = dynamicTreeNode1.Child1;
      int index2 = dynamicTreeNode1.Child2;
      MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode2 = this._nodes[index1];
      MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode3 = this._nodes[index2];
      int num = dynamicTreeNode3.Height - dynamicTreeNode2.Height;
      if (num > 1)
      {
        int index3 = dynamicTreeNode3.Child1;
        int index4 = dynamicTreeNode3.Child2;
        MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode4 = this._nodes[index3];
        MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode5 = this._nodes[index4];
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
          BoundingBoxD.CreateMerged(ref dynamicTreeNode2.Aabb, ref dynamicTreeNode5.Aabb, out dynamicTreeNode1.Aabb);
          BoundingBoxD.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode4.Aabb, out dynamicTreeNode3.Aabb);
          dynamicTreeNode1.Height = 1 + Math.Max(dynamicTreeNode2.Height, dynamicTreeNode5.Height);
          dynamicTreeNode3.Height = 1 + Math.Max(dynamicTreeNode1.Height, dynamicTreeNode4.Height);
        }
        else
        {
          dynamicTreeNode3.Child2 = index4;
          dynamicTreeNode1.Child2 = index3;
          dynamicTreeNode4.ParentOrNext = iA;
          BoundingBoxD.CreateMerged(ref dynamicTreeNode2.Aabb, ref dynamicTreeNode4.Aabb, out dynamicTreeNode1.Aabb);
          BoundingBoxD.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode5.Aabb, out dynamicTreeNode3.Aabb);
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
        MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode4 = this._nodes[index3];
        MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode5 = this._nodes[index4];
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
          BoundingBoxD.CreateMerged(ref dynamicTreeNode3.Aabb, ref dynamicTreeNode5.Aabb, out dynamicTreeNode1.Aabb);
          BoundingBoxD.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode4.Aabb, out dynamicTreeNode2.Aabb);
          dynamicTreeNode1.Height = 1 + Math.Max(dynamicTreeNode3.Height, dynamicTreeNode5.Height);
          dynamicTreeNode2.Height = 1 + Math.Max(dynamicTreeNode1.Height, dynamicTreeNode4.Height);
        }
        else
        {
          dynamicTreeNode2.Child2 = index4;
          dynamicTreeNode1.Child1 = index3;
          dynamicTreeNode4.ParentOrNext = iA;
          BoundingBoxD.CreateMerged(ref dynamicTreeNode3.Aabb, ref dynamicTreeNode4.Aabb, out dynamicTreeNode1.Aabb);
          BoundingBoxD.CreateMerged(ref dynamicTreeNode1.Aabb, ref dynamicTreeNode5.Aabb, out dynamicTreeNode2.Aabb);
          dynamicTreeNode1.Height = 1 + Math.Max(dynamicTreeNode3.Height, dynamicTreeNode4.Height);
          dynamicTreeNode2.Height = 1 + Math.Max(dynamicTreeNode1.Height, dynamicTreeNode5.Height);
        }
        return index1;
      }
    }

    public void OverlapAllFrustum<T>(ref BoundingFrustumD frustum, List<T> elementsList, bool clear = true)
    {
      this.OverlapAllFrustum<T>(ref frustum, elementsList, 0U, clear);
    }

    public void OverlapAllFrustum<T>(ref BoundingFrustumD frustum, List<T> elementsList, uint requiredFlags, bool clear = true)
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
            MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode1 = this._nodes[proxyId1];
            ContainmentType result;
            frustum.Contains(ref dynamicTreeNode1.Aabb, out result);
            if (result == ContainmentType.Contains)
            {
              int count = stack.Count;
              stack.Push(proxyId1);
              while (stack.Count > count)
              {
                int proxyId2 = stack.Pop();
                MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode2 = this._nodes[proxyId2];
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

    public void OverlapAllFrustumAny<T>(ref BoundingFrustumD frustum, List<T> elementsList, bool clear = true)
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
            MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode1 = this._nodes[proxyId1];
            ContainmentType result;
            frustum.Contains(ref dynamicTreeNode1.Aabb, out result);
            if (result == ContainmentType.Contains)
            {
              int count = stack.Count;
              stack.Push(proxyId1);
              while (stack.Count > count)
              {
                int proxyId2 = stack.Pop();
                MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode2 = this._nodes[proxyId2];
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

    public void OverlapAllLineSegment<T>(ref LineD line, List<MyLineSegmentOverlapResult<T>> elementsList)
    {
      this.OverlapAllLineSegment<T>(ref line, elementsList, 0U);
    }

    public void OverlapAllLineSegment<T>(ref LineD line, List<MyLineSegmentOverlapResult<T>> elementsList, uint requiredFlags)
    {
      elementsList.Clear();
      if (this._root == -1)
        return;
      using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
      {
        Stack<int> stack = this.GetStack();
        stack.Push(this._root);
        BoundingBoxD invalid = BoundingBoxD.CreateInvalid();
        invalid.Include(ref line);
        RayD ray = new RayD(line.From, line.Direction);
        while (stack.Count > 0)
        {
          int proxyId = stack.Pop();
          if (proxyId != -1)
          {
            MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode = this._nodes[proxyId];
            if (dynamicTreeNode.Aabb.Intersects(invalid))
            {
              double? nullable = dynamicTreeNode.Aabb.Intersects(ray);
              if (nullable.HasValue && nullable.Value <= line.Length && nullable.Value >= 0.0)
              {
                if (dynamicTreeNode.IsLeaf())
                {
                  if (((int) this.GetUserFlag(proxyId) & (int) requiredFlags) == (int) requiredFlags)
                    elementsList.Add(new MyLineSegmentOverlapResult<T>()
                    {
                      Element = this.GetUserData<T>(proxyId),
                      Distance = nullable.Value
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

    public void OverlapAllBoundingBox<T>(ref BoundingBoxD bbox, List<T> elementsList, uint requiredFlags = 0U, bool clear = true)
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
            MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode = this._nodes[proxyId];
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

    public void OverlapAllBoundingSphere<T>(ref BoundingSphereD sphere, List<T> overlapElementsList, bool clear = true)
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
            MyDynamicAABBTreeD.DynamicTreeNode dynamicTreeNode = this._nodes[proxyId];
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

    public void GetAll<T>(List<T> elementsList, bool clear, List<BoundingBoxD> boxsList = null)
    {
      if (clear)
      {
        elementsList.Clear();
        if (boxsList != null)
          boxsList.Clear();
      }
      using (FastResourceLockExtensions.AcquireSharedUsing(this.m_rwLock))
      {
        foreach (KeyValuePair<int, MyDynamicAABBTreeD.DynamicTreeNode> keyValuePair in this._leafElementCache)
          elementsList.Add((T) keyValuePair.Value.UserData);
        if (boxsList == null)
          return;
        foreach (KeyValuePair<int, MyDynamicAABBTreeD.DynamicTreeNode> keyValuePair in this._leafElementCache)
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
        lock (MyDynamicAABBTreeD.m_StackCacheCollection)
        {
          foreach (Stack<int> item_0 in MyDynamicAABBTreeD.m_StackCacheCollection)
            item_0.Clear();
          MyDynamicAABBTreeD.m_StackCacheCollection.Clear();
        }
        this.ResetNodes();
      }
    }

    internal class DynamicTreeNode
    {
      internal BoundingBoxD Aabb;
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
