// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyRenderComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using System;
using VRageMath;
using VRageRender;

namespace Sandbox.Common.Components
{
  public abstract class MyRenderComponentBase : MyComponentBase
  {
    public static readonly Vector3 OldRedToHSV = new Vector3(0.0f, 0.0f, 0.05f);
    public static readonly Vector3 OldYellowToHSV = new Vector3(0.1222222f, -0.1f, 0.26f);
    public static readonly Vector3 OldBlueToHSV = new Vector3(0.575f, 0.0f, 0.0f);
    public static readonly Vector3 OldGreenToHSV = new Vector3(0.3333333f, -0.48f, -0.25f);
    public static readonly Vector3 OldBlackToHSV = new Vector3(0.0f, -0.96f, -0.5f);
    public static readonly Vector3 OldWhiteToHSV = new Vector3(0.0f, -0.95f, 0.4f);
    public static readonly Vector3 OldGrayToHSV = new Vector3(0.0f, -1f, 0.0f);
    protected Vector3 m_colorMaskHsv = MyRenderComponentBase.OldGrayToHSV;
    protected Color m_diffuseColor = Color.White;
    protected uint[] m_renderObjectIDs = new uint[1]
    {
      MyRenderProxy.RENDER_ID_UNASSIGNED
    };
    protected bool m_enableColorMaskHsv;
    public float Transparency;

    public abstract object ModelStorage { get; set; }

    public bool EnableColorMaskHsv
    {
      get
      {
        return this.m_enableColorMaskHsv;
      }
      set
      {
        this.m_enableColorMaskHsv = value;
        if (!this.EnableColorMaskHsv)
          return;
        this.UpdateRenderEntity(this.m_colorMaskHsv);
        this.Entity.EnableColorMaskForSubparts(value);
      }
    }

    public Vector3 ColorMaskHsv
    {
      get
      {
        return this.m_colorMaskHsv;
      }
      set
      {
        this.m_colorMaskHsv = value;
        if (!this.EnableColorMaskHsv)
          return;
        this.UpdateRenderEntity(this.m_colorMaskHsv);
        this.Entity.SetColorMaskForSubparts(value);
      }
    }

    public MyPersistentEntityFlags2 PersistentFlags { get; set; }

    public uint[] RenderObjectIDs
    {
      get
      {
        return this.m_renderObjectIDs;
      }
    }

    public bool Visible
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.Visible) != (EntityFlags) 0;
      }
      set
      {
        EntityFlags flags = this.Entity.Flags;
        if (value)
          this.Entity.Flags = this.Entity.Flags | EntityFlags.Visible;
        else
          this.Entity.Flags = this.Entity.Flags & ~EntityFlags.Visible;
        if (flags == this.Entity.Flags)
          return;
        this.UpdateRenderObject(value);
      }
    }

    public virtual bool NearFlag
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.Near) != (EntityFlags) 0;
      }
      set
      {
        bool flag = value != this.NearFlag;
        if (value)
          this.Entity.Flags |= EntityFlags.Near;
        else
          this.Entity.Flags &= ~EntityFlags.Near;
        if (flag)
          MyRenderProxy.UpdateRenderObjectVisibility(this.m_renderObjectIDs[0], this.Visible, this.NearFlag);
        foreach (MyHierarchyComponentBase hierarchyComponentBase in this.Entity.Hierarchy.Children)
        {
          MyRenderComponentBase component = (MyRenderComponentBase) null;
          if (hierarchyComponentBase.Entity.InScene && hierarchyComponentBase.CurrentContainer.TryGet<MyRenderComponentBase>(out component))
            component.NearFlag = value;
        }
      }
    }

    public bool NeedsDrawFromParent
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.NeedsDrawFromParent) != (EntityFlags) 0;
      }
      set
      {
        if (value == this.NeedsDrawFromParent)
          return;
        this.Entity.Flags &= ~EntityFlags.NeedsDrawFromParent;
        if (!value)
          return;
        this.Entity.Flags |= EntityFlags.NeedsDrawFromParent;
      }
    }

    public bool CastShadows
    {
      get
      {
        return (this.PersistentFlags & MyPersistentEntityFlags2.CastShadows) != MyPersistentEntityFlags2.None;
      }
      set
      {
        if (value)
          this.PersistentFlags |= MyPersistentEntityFlags2.CastShadows;
        else
          this.PersistentFlags &= ~MyPersistentEntityFlags2.CastShadows;
      }
    }

    public bool NeedsResolveCastShadow
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.NeedsResolveCastShadow) != (EntityFlags) 0;
      }
      set
      {
        if (value)
          this.Entity.Flags |= EntityFlags.NeedsResolveCastShadow;
        else
          this.Entity.Flags &= ~EntityFlags.NeedsResolveCastShadow;
      }
    }

    public bool FastCastShadowResolve
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.FastCastShadowResolve) != (EntityFlags) 0;
      }
      set
      {
        if (value)
          this.Entity.Flags |= EntityFlags.FastCastShadowResolve;
        else
          this.Entity.Flags &= ~EntityFlags.FastCastShadowResolve;
      }
    }

    public bool SkipIfTooSmall
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.SkipIfTooSmall) != (EntityFlags) 0;
      }
      set
      {
        if (value)
          this.Entity.Flags |= EntityFlags.SkipIfTooSmall;
        else
          this.Entity.Flags &= ~EntityFlags.SkipIfTooSmall;
      }
    }

    public bool ShadowBoxLod
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.ShadowBoxLod) != (EntityFlags) 0;
      }
      set
      {
        if (value)
          this.Entity.Flags |= EntityFlags.ShadowBoxLod;
        else
          this.Entity.Flags &= ~EntityFlags.ShadowBoxLod;
      }
    }

    public bool NeedsDraw
    {
      get
      {
        return (this.Entity.Flags & EntityFlags.NeedsDraw) != (EntityFlags) 0;
      }
      set
      {
        if (value == this.NeedsDraw)
          return;
        this.Entity.Flags &= ~EntityFlags.NeedsDraw;
        if (value)
          this.Entity.Flags |= EntityFlags.NeedsDraw;
        int num = this.Entity.InScene ? 1 : 0;
      }
    }

    public abstract void SetRenderObjectID(int index, uint ID);

    public int GetRenderObjectID()
    {
      if (this.m_renderObjectIDs.Length > 0)
        return (int) this.m_renderObjectIDs[0];
      else
        return -1;
    }

    public virtual void RemoveRenderObjects()
    {
      for (int index = 0; index < this.m_renderObjectIDs.Length; ++index)
        this.ReleaseRenderObjectID(index);
    }

    public abstract void ReleaseRenderObjectID(int index);

    public void ResizeRenderObjectArray(int newSize)
    {
      int length = this.m_renderObjectIDs.Length;
      Array.Resize<uint>(ref this.m_renderObjectIDs, newSize);
      for (int index = length; index < newSize; ++index)
        this.m_renderObjectIDs[index] = MyRenderProxy.RENDER_ID_UNASSIGNED;
    }

    public bool IsRenderObjectAssigned(int index)
    {
      return (int) this.m_renderObjectIDs[index] != (int) MyRenderProxy.RENDER_ID_UNASSIGNED;
    }

    public virtual void InvalidateRenderObjects(bool sortIntoCullobjects = false)
    {
      MatrixD worldMatrix = this.Entity.PositionComp.WorldMatrix;
      if (!this.Entity.Visible && !this.Entity.CastShadows || (!this.Entity.InScene || !this.Entity.InvalidateOnMove))
        return;
      foreach (uint id in this.m_renderObjectIDs)
        MyRenderProxy.UpdateRenderObject(id, ref worldMatrix, sortIntoCullobjects, new BoundingBoxD?());
    }

    public void UpdateRenderEntity(Vector3 colorMaskHSV)
    {
      this.m_colorMaskHsv = colorMaskHSV;
      MyRenderProxy.UpdateRenderEntity(this.m_renderObjectIDs[0], new Color?(this.m_diffuseColor), new Vector3?(this.m_colorMaskHsv), 0.0f);
    }

    protected virtual bool CanBeAddedToRender()
    {
      return true;
    }

    public void UpdateRenderObject(bool visible)
    {
      if (!this.Visible || !this.Entity.InScene && visible)
        return;
      if (visible)
      {
        MyHierarchyComponentBase hierarchy = this.Entity.Hierarchy;
        if (this.Visible && (hierarchy.Parent == null || hierarchy.Parent.Entity.Visible) && this.CanBeAddedToRender())
        {
          if (!this.IsRenderObjectAssigned(0))
            this.AddRenderObjects();
          else
            this.UpdateRenderObjectVisibility(visible);
        }
      }
      else
      {
        if ((int) this.m_renderObjectIDs[0] != (int) MyRenderProxy.RENDER_ID_UNASSIGNED)
          this.UpdateRenderObjectVisibility(visible);
        this.RemoveRenderObjects();
      }
      foreach (MyHierarchyComponentBase hierarchyComponentBase in this.Entity.Hierarchy.Children)
      {
        MyRenderComponentBase component = (MyRenderComponentBase) null;
        if (hierarchyComponentBase.CurrentContainer.TryGet<MyRenderComponentBase>(out component))
          component.UpdateRenderObject(visible);
      }
    }

    protected void UpdateRenderObjectVisibility(bool visible)
    {
      foreach (uint id in this.m_renderObjectIDs)
        MyRenderProxy.UpdateRenderObjectVisibility(id, visible, this.Entity.NearFlag);
    }

    public Color GetDiffuseColor()
    {
      return this.m_diffuseColor;
    }

    protected void SetDiffuseColor(Color vctColor)
    {
      this.m_diffuseColor = vctColor;
      MyRenderProxy.UpdateRenderEntity(this.m_renderObjectIDs[0], new Color?(this.m_diffuseColor), new Vector3?(this.m_colorMaskHsv), 0.0f);
    }

    public virtual RenderFlags GetRenderFlags()
    {
      RenderFlags renderFlags = (RenderFlags) 0;
      if (this.NearFlag)
        renderFlags |= RenderFlags.Near;
      if (this.CastShadows)
        renderFlags |= RenderFlags.CastShadows;
      if (this.Visible)
        renderFlags |= RenderFlags.Visible;
      if (this.NeedsResolveCastShadow)
        renderFlags |= RenderFlags.NeedsResolveCastShadow;
      if (this.FastCastShadowResolve)
        renderFlags |= RenderFlags.FastCastShadowResolve;
      if (this.SkipIfTooSmall)
        renderFlags |= RenderFlags.SkipIfTooSmall;
      if (this.ShadowBoxLod)
        renderFlags |= RenderFlags.ShadowLodBox;
      return renderFlags;
    }

    public virtual CullingOptions GetRenderCullingOptions()
    {
      return CullingOptions.Default;
    }

    public abstract void AddRenderObjects();

    public abstract void Draw();

    public abstract bool IsVisible();
  }
}
