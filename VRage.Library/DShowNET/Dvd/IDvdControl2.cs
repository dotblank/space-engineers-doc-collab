// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.IDvdControl2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using DShowNET;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [Guid("33BC7430-EEC0-11D2-8201-00A0C9D74842")]
  [ComImport]
  public interface IDvdControl2
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayTitle(int ulTitle, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayChapterInTitle(int ulTitle, int ulChapter, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayAtTimeInTitle(int ulTitle, [In] ref DvdTimeCode pStartTime, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Stop();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ReturnFromSubmenu(DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayAtTime([In] ref DvdTimeCode pTime, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayChapter(int ulChapter, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayPrevChapter(DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ReplayChapter(DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayNextChapter(DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayForwards(double dSpeed, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayBackwards(double dSpeed, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ShowMenu(DvdMenuID MenuID, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Resume(DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectRelativeButton(DvdRelButton buttonDir);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ActivateButton();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectButton(int ulButton);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectAndActivateButton(int ulButton);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int StillOff();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Pause([MarshalAs(UnmanagedType.Bool), In] bool bState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectAudioStream(int ulAudio, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectSubpictureStream(int ulSubPicture, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetSubpictureState([MarshalAs(UnmanagedType.Bool), In] bool bState, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectAngle(int ulAngle, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectParentalLevel(int ulParentalLevel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectParentalCountry(byte[] bCountry);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectKaraokeAudioPresentationMode(int ulMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectVideoModePreference(int ulPreferredDisplayMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDVDDirectory([MarshalAs(UnmanagedType.LPWStr), In] string pszwPath);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ActivateAtPosition(DsPOINT point);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectAtPosition(DsPOINT point);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayChaptersAutoStop(int ulTitle, int ulChapter, int ulChaptersToPlay, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AcceptParentalLevelChange([MarshalAs(UnmanagedType.Bool), In] bool bAccept);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetOption(DvdOptionFlag flag, [MarshalAs(UnmanagedType.Bool), In] bool fState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetState(IDvdState pState, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int PlayPeriodInTitleAutoStop(int ulTitle, [In] ref DvdTimeCode pStartTime, [In] ref DvdTimeCode pEndTime, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetGPRM(int ulIndex, short wValue, DvdCmdFlags dwFlags, [Out] OptIDvdCmd ppCmd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectDefaultMenuLanguage(int Language);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectDefaultAudioLanguage(int Language, DvdAudioLangExt audioExtension);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SelectDefaultSubpictureLanguage(int Language, DvdSubPicLangExt subpictureExtension);
  }
}
