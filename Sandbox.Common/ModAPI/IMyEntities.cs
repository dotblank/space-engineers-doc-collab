// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyEntities
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using System;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.ModAPI
{
    public interface IMyEntities
    {
        event Action<IMyEntity> OnEntityRemove;

        event Action<IMyEntity> OnEntityAdd;

        event Action OnCloseAll;

        event Action<IMyEntity, string, string> OnEntityNameSet;

        bool TryGetEntityById(long id, out IMyEntity entity);

        bool TryGetEntityByName(string name, out IMyEntity entity);

        bool EntityExists(string name);

        void AddEntity(IMyEntity entity, bool insertIntoScene = true);

        IMyEntity CreateFromObjectBuilder(MyObjectBuilder_EntityBase objectBuilder);

        IMyEntity CreateFromObjectBuilderAndAdd(MyObjectBuilder_EntityBase objectBuilder);

        void RemoveEntity(IMyEntity entity);

        bool IsSpherePenetrating(ref BoundingSphereD bs);

        Vector3D? FindFreePlace(Vector3D basePos, float radius, int maxTestCount = 20, int testsPerDistance = 5,
            float stepSize = 1f);

        void GetInflatedPlayerBoundingBox(ref BoundingBox playerBox, float inflation);

        bool IsInsideVoxel(Vector3 pos, Vector3 hintPosition, out Vector3 lastOutsidePos);

        bool IsWorldLimited();

        float WorldHalfExtent();

        float WorldSafeHalfExtent();

        bool IsInsideWorld(Vector3D pos);

        bool IsRaycastBlocked(Vector3D pos, Vector3D target);

        void SetEntityName(IMyEntity IMyEntity, bool possibleRename = true);

        bool IsNameExists(IMyEntity entity, string name);

        void RemoveFromClosedEntities(IMyEntity entity);

        void RemoveName(IMyEntity entity);

        bool Exist(IMyEntity entity);

        void MarkForClose(IMyEntity entity);

        void RegisterForUpdate(IMyEntity entity);

        void RegisterForDraw(IMyEntity entity);

        void UnregisterForUpdate(IMyEntity entity, bool immediate = false);

        void UnregisterForDraw(IMyEntity entity);

        IMyEntity GetIntersectionWithSphere(ref BoundingSphereD sphere);

        IMyEntity GetIntersectionWithSphere(ref BoundingSphereD sphere, IMyEntity ignoreEntity0, IMyEntity ignoreEntity1);

        IMyEntity GetIntersectionWithSphere(ref BoundingSphereD sphere, IMyEntity ignoreEntity0, IMyEntity ignoreEntity1,
            bool ignoreVoxelMaps, bool volumetricTest, bool excludeEntitiesWithDisabledPhysics = false,
            bool ignoreFloatingObjects = true, bool ignoreHandWeapons = true);

        IMyEntity GetEntityById(long entityId);

        bool ExistsById(long entityId);

        IMyEntity GetEntityByName(string name);

        void SetTypeSelectable(Type type, bool selectable);

        bool IsTypeSelectable(Type type);

        bool IsSelectable(IMyEntity entity);

        void SetTypeHidden(Type type, bool hidden);

        bool IsTypeHidden(Type type);

        bool IsVisible(IMyEntity entity);

        void UnhideAllTypes();

        void RemapObjectBuilderCollection(IEnumerable<MyObjectBuilder_EntityBase> objectBuilders);

        void RemapObjectBuilder(MyObjectBuilder_EntityBase objectBuilder);

        IMyEntity CreateFromObjectBuilderNoinit(MyObjectBuilder_EntityBase objectBuilder);

        void EnableEntityBoundingBoxDraw(IMyEntity entity, bool enable, Vector4? color = null, float lineWidth = 0.01f,
            Vector3? inflateAmount = null);

        IMyEntity GetEntity(Func<IMyEntity, bool> match);

        void GetEntities(HashSet<IMyEntity> entities, Func<IMyEntity, bool> collect = null);

        List<IMyEntity> GetIntersectionWithSphere(ref BoundingSphereD sphere, IMyEntity ignoreEntity0,
            IMyEntity ignoreEntity1, bool ignoreVoxelMaps, bool volumetricTest);

        List<IMyEntity> GetEntitiesInAABB(ref BoundingBoxD boundingBox);

        List<IMyEntity> GetEntitiesInSphere(ref BoundingSphereD boundingSphere);

        List<IMyEntity> GetElementsInBox(ref BoundingBoxD boundingBox);
    }
}