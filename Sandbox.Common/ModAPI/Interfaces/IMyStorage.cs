// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyStorage
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRage.Common.Voxels;
using VRageMath;

namespace Sandbox.ModAPI.Interfaces
{
    public interface IMyStorage
    {
        Vector3I Size { get; }

        void OverwriteAllMaterials(byte materialIndex);

        void ReadRange(MyStorageDataCache target, MyStorageDataTypeFlags dataToRead, int lodIndex,
            Vector3I lodVoxelRangeMin, Vector3I lodVoxelRangeMax);

        void WriteRange(MyStorageDataCache source, MyStorageDataTypeFlags dataToWrite, Vector3I voxelRangeMin,
            Vector3I voxelRangeMax);
    }
}