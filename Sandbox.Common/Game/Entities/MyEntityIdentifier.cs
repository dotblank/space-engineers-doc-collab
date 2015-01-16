// Decompiled with JetBrains decompiler
// Type: Sandbox.Game.Entities.MyEntityIdentifier
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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
        private static long[] m_lastGeneratedIds = (long[]) null;
        private const int DEFAULT_DICTIONARY_SIZE = 32768;

        public static bool AllocationSuspended
        {
            get { return MyEntityIdentifier.m_allocationSuspended; }
            set { MyEntityIdentifier.m_allocationSuspended = value; }
        }

        static MyEntityIdentifier()
        {
            MyEntityIdentifier.m_lastGeneratedIds =
                new long[(int) (MyEnum<MyEntityIdentifier.ID_OBJECT_TYPE>.MaxValue.Value + (byte) 1)];
        }

        public static void Reset()
        {
            for (int index = 0;
                (MyEntityIdentifier.ID_OBJECT_TYPE) index <
                MyEnum<MyEntityIdentifier.ID_OBJECT_TYPE>.MaxValue.Value + (byte) 1;
                ++index)
                MyEntityIdentifier.m_lastGeneratedIds[index] = 0L;
        }

        public static void MarkIdUsed(long id)
        {
            long idUniqueNumber = MyEntityIdentifier.GetIdUniqueNumber(id);
            MyEntityIdentifier.ID_OBJECT_TYPE idObjectType = MyEntityIdentifier.GetIdObjectType(id);
            if (MyEntityIdentifier.m_lastGeneratedIds[(int) idObjectType] >= idUniqueNumber)
                return;
            MyEntityIdentifier.m_lastGeneratedIds[(int) idObjectType] = idUniqueNumber;
        }

        public static void AddEntityWithId(IMyEntity entity)
        {
            MyEntityIdentifier.m_entityList.Add(entity.EntityId, entity);
        }

        public static long AllocateId(
            MyEntityIdentifier.ID_OBJECT_TYPE objectType = MyEntityIdentifier.ID_OBJECT_TYPE.ENTITY,
            MyEntityIdentifier.ID_ALLOCATION_METHOD generationMethod = MyEntityIdentifier.ID_ALLOCATION_METHOD.RANDOM)
        {
            long uniqueNumber;
            if (generationMethod == MyEntityIdentifier.ID_ALLOCATION_METHOD.RANDOM)
            {
                uniqueNumber = MyRandom.Instance.NextLong() & 72057594037927935L;
            }
            else
            {
                uniqueNumber = MyEntityIdentifier.m_lastGeneratedIds[(int) objectType] + 1L;
                MyEntityIdentifier.m_lastGeneratedIds[(int) objectType] = uniqueNumber;
            }
            return MyEntityIdentifier.ConstructId(objectType, uniqueNumber);
        }

        public static MyEntityIdentifier.ID_OBJECT_TYPE GetIdObjectType(long id)
        {
            return (MyEntityIdentifier.ID_OBJECT_TYPE) (id >> 56);
        }

        public static long GetIdUniqueNumber(long id)
        {
            return id & 72057594037927935L;
        }

        public static long ConstructId(MyEntityIdentifier.ID_OBJECT_TYPE type, long uniqueNumber)
        {
            return uniqueNumber & 72057594037927935L | 144115188075855872L;
        }

        public static long FixObsoleteIdentityType(long id)
        {
            if (MyEntityIdentifier.GetIdObjectType(id) == MyEntityIdentifier.ID_OBJECT_TYPE.NPC ||
                MyEntityIdentifier.GetIdObjectType(id) == MyEntityIdentifier.ID_OBJECT_TYPE.SPAWN_GROUP)
                id = MyEntityIdentifier.ConstructId(MyEntityIdentifier.ID_OBJECT_TYPE.IDENTITY,
                    MyEntityIdentifier.GetIdUniqueNumber(id));
            return id;
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
            IDENTITY,
            FACTION,
            NPC,
            SPAWN_GROUP,
            ASTEROID,
        }

        public enum ID_ALLOCATION_METHOD : byte
        {
            RANDOM,
            SERIAL_START_WITH_1,
        }
    }
}