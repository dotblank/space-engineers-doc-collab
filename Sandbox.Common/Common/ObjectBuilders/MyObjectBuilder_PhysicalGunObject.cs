// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_PhysicalGunObject
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using System.Xml.Serialization;
using VRage.Common.Utils;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_PhysicalGunObject : MyObjectBuilder_PhysicalObject
    {
        [XmlElement("GunEntity", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_EntityBase>))] [ProtoMember(3)] public MyObjectBuilder_EntityBase GunEntity;

        public MyObjectBuilder_PhysicalGunObject()
            : this((MyObjectBuilder_EntityBase) null)
        {
        }

        public MyObjectBuilder_PhysicalGunObject(MyObjectBuilder_EntityBase gunEntity)
        {
            this.GunEntity = gunEntity;
        }

        public override bool CanStack(MyObjectBuilderType type, MyStringId subtypeId, MyItemFlags flags)
        {
            return false;
        }
    }
}