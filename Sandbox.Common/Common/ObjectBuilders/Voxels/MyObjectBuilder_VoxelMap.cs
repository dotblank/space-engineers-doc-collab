// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Voxels.MyObjectBuilder_VoxelMap
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.IO;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Voxels
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_VoxelMap : MyObjectBuilder_EntityBase
    {
        public bool MutableStorage = true;
        private string m_storageName;

        public string Filename
        {
            get { return (string) null; }
            set { this.StorageName = Path.GetFileNameWithoutExtension(value); }
        }

        public string Name
        {
            get { return this.Name; }
            set { this.m_storageName = value; }
        }

        [ProtoMember(1)]
        public string StorageName
        {
            get { return this.m_storageName ?? this.Name; }
            set { this.m_storageName = value; }
        }

        public MyObjectBuilder_VoxelMap()
        {
            this.PositionAndOrientation =
                new MyPositionAndOrientation?(new MyPositionAndOrientation((Vector3D) Vector3.Zero, Vector3.Forward,
                    Vector3.Up));
        }

        public MyObjectBuilder_VoxelMap(Vector3 position, string storageName)
        {
            this.PositionAndOrientation =
                new MyPositionAndOrientation?(new MyPositionAndOrientation((Vector3D) position, Vector3.Forward,
                    Vector3.Up));
            this.StorageName = storageName;
        }

        public bool ShouldSerializeFilename()
        {
            return false;
        }

        public bool ShouldSerializeName()
        {
            return false;
        }

        public bool ShouldSerializeMutableStorage()
        {
            return !this.MutableStorage;
        }
    }
}