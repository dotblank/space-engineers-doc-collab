﻿// Decompiled with JetBrains decompiler
// Type: DShowNET.IAMTVTuner
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [Guid("211A8766-03AC-11d1-8D13-00AA00BD8339")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IAMTVTuner
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Channel(int lChannel, AMTunerSubChannel lVideoSubChannel, AMTunerSubChannel lAudioSubChannel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Channel(out int plChannel, out int plVideoSubChannel, out int plAudioSubChannel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ChannelMinMax(out int lChannelMin, out int lChannelMax);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_CountryCode(int lCountryCode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_CountryCode(out int plCountryCode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_TuningSpace(int lTuningSpace);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_TuningSpace(out int plTuningSpace);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Logon(IntPtr hCurrentUser);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Logout();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SignalPresent(out AMTunerSignalStrength plSignalStrength);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Mode(AMTunerModeType lMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Mode(out AMTunerModeType plMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAvailableModes(out AMTunerModeType plModes);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RegisterNotificationCallBack(IAMTunerNotification pNotify, AMTunerEventType lEvents);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UnRegisterNotificationCallBack(IAMTunerNotification pNotify);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_AvailableTVFormats(out AnalogVideoStandard lAnalogVideoStandard);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_TVFormat(out AnalogVideoStandard lAnalogVideoStandard);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AutoTune(int lChannel, out int plFoundSignal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int StoreAutoTune();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_NumInputConnections(out int plNumInputConnections);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_InputType(int lIndex, TunerInputType inputType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_InputType(int lIndex, out TunerInputType inputType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_ConnectInput(int lIndex);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_ConnectInput(out int lIndex);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_VideoFrequency(out int lFreq);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_AudioFrequency(out int lFreq);
  }
}
