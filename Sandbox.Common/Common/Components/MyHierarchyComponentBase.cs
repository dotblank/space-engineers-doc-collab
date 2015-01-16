// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyHierarchyComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;
using System;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.Common.Components
{
    public class MyHierarchyComponentBase : MyComponentBase
    {
        private List<MyHierarchyComponentBase> m_children = new List<MyHierarchyComponentBase>();

        public List<MyHierarchyComponentBase> Children
        {
            get { return this.m_children; }
        }

        public MyHierarchyComponentBase Parent { get; set; }

        public MyHierarchyComponentBase GetTopMostParent(Type type = null)
        {
            MyHierarchyComponentBase hierarchyComponentBase = this;
            while (hierarchyComponentBase.Parent != null &&
                   (type == (Type) null || !hierarchyComponentBase.CurrentContainer.Contains(type)))
                hierarchyComponentBase = hierarchyComponentBase.Parent;
            return hierarchyComponentBase;
        }

        public void AddChild(IMyEntity child, bool preserveWorldPos = false, bool insertIntoSceneIfNeeded = true)
        {
            child.Hierarchy.Parent = this;
            if (preserveWorldPos)
            {
                MatrixD worldMatrix = child.WorldMatrix;
                this.Children.Add(child.Hierarchy);
                child.WorldMatrix = worldMatrix;
            }
            else
            {
                this.Children.Add(child.Hierarchy);
                MatrixD worldMatrix = this.Entity.PositionComp.WorldMatrix;
                child.PositionComp.UpdateWorldMatrix(ref worldMatrix, (object) null);
            }
            if (!this.Entity.InScene || child.InScene || !insertIntoSceneIfNeeded)
                return;
            child.OnAddedToScene((object) this.Entity);
        }

        public void AddChildWithMatrix(IMyEntity child, ref Matrix childLocalMatrix, bool insertIntoSceneIfNeeded = true)
        {
            child.Hierarchy.Parent = this;
            this.Children.Add(child.Hierarchy);
            child.WorldMatrix = (MatrixD) childLocalMatrix*this.Entity.PositionComp.WorldMatrix;
            if (!this.Entity.InScene || child.InScene || !insertIntoSceneIfNeeded)
                return;
            child.OnAddedToScene((object) this);
        }

        public void RemoveChild(IMyEntity child, bool preserveWorldPos = false)
        {
            if (preserveWorldPos)
            {
                MatrixD worldMatrix = child.WorldMatrix;
                this.Children.Remove(child.Hierarchy);
                child.WorldMatrix = worldMatrix;
            }
            else
                this.Children.Remove(child.Hierarchy);
            child.Hierarchy.Parent = (MyHierarchyComponentBase) null;
            if (!child.InScene)
                return;
            child.OnRemovedFromScene((object) this);
        }

        public void GetChildrenRecursive(HashSet<IMyEntity> result)
        {
            for (int index = 0; index < this.Children.Count; ++index)
            {
                MyHierarchyComponentBase hierarchyComponentBase = this.Children[index];
                result.Add(hierarchyComponentBase.Entity);
                hierarchyComponentBase.GetChildrenRecursive(result);
            }
        }
    }
}