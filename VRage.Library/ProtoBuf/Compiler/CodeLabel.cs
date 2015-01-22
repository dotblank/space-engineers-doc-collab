// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Compiler.CodeLabel
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Reflection.Emit;

namespace ProtoBuf.Compiler
{
  internal struct CodeLabel
  {
    public readonly Label Value;
    public readonly int Index;

    public CodeLabel(Label value, int index)
    {
      this.Value = value;
      this.Index = index;
    }
  }
}
