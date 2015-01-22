// Decompiled with JetBrains decompiler
// Type: DShowNET.IEnumPins
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [Guid("56a86892-0ad4-11ce-b03a-0020af0ba770")]
  [ComImport]
  public interface IEnumPins
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next([In] int cPins, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out IPin[] ppPins, out int pcFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip([In] int cPins);

    void Reset();

    void Clone(out IEnumPins ppEnum);
  }
}
