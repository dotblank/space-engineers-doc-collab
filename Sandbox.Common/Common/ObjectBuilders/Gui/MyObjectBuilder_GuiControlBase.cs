// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRage;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public abstract class MyObjectBuilder_GuiControlBase : MyObjectBuilder_Base
    {
        [ProtoMember(4)] public Vector4 BackgroundColor = Vector4.One;
        [ProtoMember(1)] public Vector2 Position;
        [ProtoMember(2)] public Vector2 Size;
        [ProtoMember(3)] public string Name;
        [ProtoMember(5)] public string ControlTexture;
        [ProtoMember(6)] public MyGuiDrawAlignEnum OriginAlign;

        public int ControlAlign
        {
            get { return (int) this.OriginAlign; }
            set { this.OriginAlign = (MyGuiDrawAlignEnum) value; }
        }

        public bool ShouldSerializeControlAlign()
        {
            return false;
        }
    }
}