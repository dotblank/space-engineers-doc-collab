﻿// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.IDvdInfo2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using DShowNET;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("34151510-EEC0-11D2-8201-00A0C9D74842")]
  [ComVisible(true)]
  [ComImport]
  public interface IDvdInfo2
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentDomain(out DvdDomain pDomain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentLocation(out DvdPlayLocation pLocation);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTotalTitleTime(out DvdTimeCode pTotalTime, out int ulTimeCodeFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentButton(out int pulButtonsAvailable, out int pulCurrentButton);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentAngle(out int pulAnglesAvailable, out int pulCurrentAngle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentAudio(out int pulStreamsAvailable, out int pulCurrentStream);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentSubpicture(out int pulStreamsAvailable, out int pulCurrentStream, [MarshalAs(UnmanagedType.Bool)] out bool pbIsDisabled);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentUOPS(out int pulUOPs);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAllSPRMs(out IntPtr pRegisterArray);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAllGPRMs(out IntPtr pRegisterArray);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAudioLanguage(int ulStream, out int pLanguage);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSubpictureLanguage(int ulStream, out int pLanguage);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTitleAttributes(int ulTitle, out DvdMenuAttr pMenu, IntPtr pTitle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetVMGAttributes(out DvdMenuAttr pATR);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentVideoAttributes(out DvdVideoAttr pATR);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAudioAttributes(int ulStream, out DvdAudioAttr pATR);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetKaraokeAttributes(int ulStream, IntPtr pATR);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSubpictureAttributes(int ulStream, out DvdSubPicAttr pATR);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDVDVolumeInfo(out int pulNumOfVolumes, out int pulVolume, out DvdDiscSide pSide, out int pulNumOfTitles);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDVDTextNumberOfLanguages(out int pulNumOfLangs);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDVDTextLanguageInfo(int ulLangIndex, out int pulNumOfStrings, out int pLangCode, out DvdCharSet pbCharacterSet);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDVDTextStringAsNative(int ulLangIndex, int ulStringIndex, IntPtr pbBuffer, int ulMaxBufferSize, out int pulActualSize, out int pType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDVDTextStringAsUnicode(int ulLangIndex, int ulStringIndex, IntPtr pchwBuffer, int ulMaxBufferSize, out int pulActualSize, out int pType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPlayerParentalLevel(out int pulParentalLevel, [Out] byte[] pbCountryCode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetNumberOfChapters(int ulTitle, out int pulNumOfChapters);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTitleParentalLevels(int ulTitle, out int pulParentalLevels);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDVDDirectory(IntPtr pszwPath, int ulMaxSize, out int pulActualSize);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsAudioStreamEnabled(int ulStreamNum, [MarshalAs(UnmanagedType.Bool)] out bool pbEnabled);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDiscID([MarshalAs(UnmanagedType.LPWStr), In] string pszwPath, out long pullDiscID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetState(out IDvdState pStateData);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMenuLanguages([Out] int[] pLanguages, int ulMaxLanguages, out int pulActualLanguages);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetButtonAtPosition(DsPOINT point, out int pulButtonIndex);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCmdFromEvent(int lParam1, out IDvdCmd pCmdObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDefaultMenuLanguage(out int pLanguage);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDefaultAudioLanguage(out int pLanguage, out DvdAudioLangExt pAudioExtension);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDefaultSubpictureLanguage(out int pLanguage, out DvdSubPicLangExt pSubpictureExtension);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDecoderCaps(ref DvdDecoderCaps pCaps);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetButtonRect(int ulButton, out DsRECT pRect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsSubpictureStreamEnabled(int ulStreamNum, [MarshalAs(UnmanagedType.Bool)] out bool pbEnabled);
  }
}