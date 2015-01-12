// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Player
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Collections.Generic;
using System.Xml.Serialization;
using VRage.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_Player : MyObjectBuilder_Base
    {
        public ulong SteamID;
        private SerializableDictionary<long, MyObjectBuilder_Player.CameraControllerSettings> m_cameraData;
        public long PlayerEntity;
        public string PlayerModel;
        public long PlayerId;
        [ProtoMember(1)] public string DisplayName;
        [ProtoMember(2)] public long IdentityId;
        [ProtoMember(3)] public bool Connected;
        [ProtoMember(4)] public MyObjectBuilder_Toolbar Toolbar;
        [ProtoMember(5)] public long LastActivity;
        [ProtoMember(6)] public MyObjectBuilder_Player.CameraControllerSettings CharacterCameraData;
        [ProtoMember(7)] public List<MyObjectBuilder_Player.CameraControllerSettings> EntityCameraData;

        public SerializableDictionary<long, MyObjectBuilder_Player.CameraControllerSettings> CameraData
        {
            get { return this.m_cameraData; }
            set { this.m_cameraData = value; }
        }

        public bool ShouldSerializeSteamID()
        {
            return false;
        }

        public bool ShouldSerializeCameraData()
        {
            return false;
        }

        public bool ShouldSerializePlayerEntity()
        {
            return false;
        }

        public bool ShouldSerializePlayerModel()
        {
            return false;
        }

        public bool ShouldSerializePlayerId()
        {
            return false;
        }

        [ProtoContract]
        public class CameraControllerSettings
        {
            [ProtoMember(1)] public bool IsFirstPerson;
            [ProtoMember(2)] public double Distance;
            [ProtoMember(3)] public SerializableVector2? HeadAngle;
            [XmlAttribute] public long EntityId;
        }
    }
}