// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.ISerializer`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using VRage;

namespace VRage.Serialization
{
  public interface ISerializer<T>
  {
    void Serialize(ByteStream destination, ref T data);

    void Deserialize(ByteStream source, out T data);
  }
}
