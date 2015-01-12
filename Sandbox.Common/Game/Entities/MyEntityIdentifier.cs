// Decompiled with JetBrains decompiler
// Type: Sandbox.Game.Entities.MyEntityIdentifier
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VRage;

namespace Sandbox.Game.Entities
{
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct MyEntityIdentifier
    {
        private static Dictionary<long, IMyEntity> m_entityList = new Dictionary<long, IMyEntity>(32768);
        private static bool m_allocationSuspended = false;
        private const int DEFAULT_DICTIONARY_SIZE = 32768;

        public static bool AllocationSuspended
        {
            get { return MyEntityIdentifier.m_allocationSuspended; }
            set { MyEntityIdentifier.m_allocationSuspended = value; }
        }

        public static void AddEntityWithId(IMyEntity entity)
        {
            MyEntityIdentifier.m_entityList.Add(entity.EntityId, entity);
        }

        public static long AllocateId(
            MyEntityIdentifier.ID_OBJECT_TYPE objectType = MyEntityIdentifier.ID_OBJECT_TYPE.ENTITY)
        {
            return MyRandom.Instance.NextLong() & 72057594037927935L | (long) objectType << 56;
        }

        public static MyEntityIdentifier.ID_OBJECT_TYPE GetIdObjectType(long id)
        {
            return (MyEntityIdentifier.ID_OBJECT_TYPE) (id >> 56);
        }

        public static bool IsIdentityObjectType(MyEntityIdentifier.ID_OBJECT_TYPE identityType)
        {
            if (identityType != MyEntityIdentifier.ID_OBJECT_TYPE.PLAYER &&
                identityType != MyEntityIdentifier.ID_OBJECT_TYPE.NPC)
                return identityType == MyEntityIdentifier.ID_OBJECT_TYPE.SPAWN_GROUP;
            else
                return true;
        }

        public static void RemoveEntity(long entityId)
        {
            MyEntityIdentifier.m_entityList.Remove(entityId);
        }

        public static bool TryGetEntity(long entityId, out IMyEntity entity)
        {
            return MyEntityIdentifier.m_entityList.TryGetValue(entityId, out entity);
        }

        public static bool TryGetEntity<T>(long entityId, out T entity) where T : class, IMyEntity
        {
            IMyEntity entity1;
            bool entity2 = MyEntityIdentifier.TryGetEntity(entityId, out entity1);
            entity = entity1 as T;
            if (entity2)
                return (object) entity != null;
            else
                return false;
        }

        public static IMyEntity GetEntityById(long entityId)
        {
            return MyEntityIdentifier.m_entityList[entityId];
        }

        public static bool ExistsById(long entityId)
        {
            return MyEntityIdentifier.m_entityList.ContainsKey(entityId);
        }

        public static void SwapRegisteredEntityId(IMyEntity entity, long oldId, long newId)
        {
            MyEntityIdentifier.RemoveEntity(oldId);
            MyEntityIdentifier.m_entityList[newId] = entity;
        }

        public static void Clear()
        {
            MyEntityIdentifier.m_entityList.Clear();
        }

        public enum ID_OBJECT_TYPE : byte
        {
            UNKNOWN,
            ENTITY,
            PLAYER,
            FACTION,
            NPC,
            SPAWN_GROUP,
            ASTEROID,
        }
    }
}