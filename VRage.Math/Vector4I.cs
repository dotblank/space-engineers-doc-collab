// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector4I
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;

namespace VRageMath
{
  [ProtoContract]
  public struct Vector4I
  {
    [ProtoMember(1)]
    public int X;
    [ProtoMember(2)]
    public int Y;
    [ProtoMember(3)]
    public int Z;
    [ProtoMember(4)]
    public int W;

    public Vector4I(int x, int y, int z, int w)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
      this.W = w;
    }

    public override string ToString()
    {
      return (string) (object) this.X + (object) ", " + (string) (object) this.Y + ", " + (string) (object) this.Z + ", " + (string) (object) this.W;
    }
  }
}
