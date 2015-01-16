// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyEntity
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Common.Components;
using Sandbox.Common.ObjectBuilders;
using System;
using System.Collections.Generic;
using VRage.Common;
using VRageMath;

namespace Sandbox.ModAPI
{
    public interface IMyEntity
    {
        EntityFlags Flags { get; set; }

        long EntityId { get; set; }

        string Name { get; set; }

        string DisplayName { get; set; }

        bool MarkedForClose { get; }

        bool Closed { get; }

        MyEntityUpdateEnum NeedsUpdate { get; set; }

        IMyEntity Parent { get; }

        bool NearFlag { get; set; }

        bool CastShadows { get; set; }

        bool FastCastShadowResolve { get; set; }

        bool NeedsResolveCastShadow { get; set; }

        float MaxGlassDistSq { get; }

        bool NeedsDraw { get; set; }

        bool NeedsDrawFromParent { get; set; }

        bool Transparent { get; set; }

        bool ShadowBoxLod { get; set; }

        bool SkipIfTooSmall { get; set; }

        bool Visible { get; set; }

        bool Save { get; set; }

        MyPersistentEntityFlags2 PersistentFlags { get; set; }

        bool InScene { get; set; }

        bool InvalidateOnMove { get; }

        bool IsCCDForProjectiles { get; }

        bool IsVolumetric { get; }

        BoundingBox LocalAABB { get; set; }

        BoundingBox LocalAABBHr { get; }

        Matrix LocalMatrix { get; set; }

        BoundingSphere LocalVolume { get; set; }

        Vector3 LocalVolumeOffset { get; set; }

        Vector3 LocationForHudMarker { get; }

        BoundingBoxD WorldAABB { get; }

        BoundingBoxD WorldAABBHr { get; }

        MatrixD WorldMatrix { get; set; }

        MatrixD WorldMatrixInvScaled { get; }

        MatrixD WorldMatrixNormalizedInv { get; }

        BoundingSphereD WorldVolume { get; }

        BoundingSphereD WorldVolumeHr { get; }

        MyComponentContainer Components { get; }

        MyPhysicsComponentBase Physics { get; set; }

        MyPositionComponentBase PositionComp { get; set; }

        MyRenderComponentBase Render { get; set; }

        MyGameLogicComponent GameLogic { get; set; }

        MyHierarchyComponentBase Hierarchy { get; set; }

        MySyncComponentBase SyncObject { get; }

        event Action<IMyEntity> OnClose;

        event Action<IMyEntity> OnClosing;

        event Action<IMyEntity> OnMarkForClose;

        event Action<IMyEntity> OnPhysicsChanged;

        string GetFriendlyName();

        void Close();

        void Delete();

        IMyEntity GetTopMostParent(Type type = null);

        Vector3 GetDiffuseColor();

        float GetDistanceBetweenCameraAndBoundingSphere();

        float GetDistanceBetweenCameraAndPosition();

        float GetLargestDistanceBetweenCameraAndBoundingSphere();

        float GetSmallestDistanceBetweenCameraAndBoundingSphere();

        Vector3? GetIntersectionWithLineAndBoundingSphere(ref LineD line, float boundingSphereRadiusMultiplier);

        bool GetIntersectionWithSphere(ref BoundingSphereD sphere);

        void GetTrianglesIntersectingSphere(ref BoundingSphereD sphere, Vector3? referenceNormalVector, float? maxAngle,
            List<MyTriangle_Vertex_Normals> retTriangles, int maxNeighbourTriangles);

        bool DoOverlapSphereTest(float sphereRadius, Vector3D spherePos);

        MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false);

        bool IsSelectable();

        bool IsSelectableAsChild();

        bool IsSelectableParentOnly();

        bool IsVisible();

        MatrixD GetViewMatrix();

        MatrixD GetWorldMatrixNormalizedInv();

        void SetLocalMatrix(Matrix localMatrix, object source = null);

        void SetWorldMatrix(MatrixD worldMatrix, object source = null);

        Vector3D GetPosition();

        void SetPosition(Vector3D pos);

        void GetChildren(List<IMyEntity> children, Func<IMyEntity, bool> collect = null);

        void OnRemovedFromScene(object source);

        void OnAddedToScene(object source);

        void BeforeSave();

        void AddToGamePruningStructure();

        void RemoveFromGamePruningStructure();

        void UpdateGamePruningStructure();

        void DebugDraw();

        void DebugDrawInvalidTriangles();

        void EnableColorMaskForSubparts(bool enable);

        void SetColorMaskForSubparts(Vector3 colorMaskHsv);
    }
}