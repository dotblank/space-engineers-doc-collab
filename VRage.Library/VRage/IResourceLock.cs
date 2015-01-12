// Decompiled with JetBrains decompiler
// Type: VRage.IResourceLock
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace VRage
{
    public interface IResourceLock
    {
        void AcquireExclusive();

        void AcquireShared();

        void ReleaseExclusive();

        void ReleaseShared();

        bool TryAcquireExclusive();

        bool TryAcquireShared();
    }
}