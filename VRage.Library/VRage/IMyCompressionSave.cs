// Decompiled with JetBrains decompiler
// Type: VRage.IMyCompressionSave
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage
{
    public interface IMyCompressionSave : IDisposable
    {
        void Add(byte[] value);

        void Add(byte[] value, int count);

        void Add(float value);

        void Add(int value);

        void Add(byte value);
    }
}