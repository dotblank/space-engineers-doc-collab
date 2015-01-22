// Decompiled with JetBrains decompiler
// Type: VRage.Compression.MyZipFileInfoReflection
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace VRage.Compression
{
  public class MyZipFileInfoReflection
  {
    public static readonly BindingFlags Bind = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    public static readonly Type ZipFileInfoType = MyZipArchiveReflection.ZipAssembly.GetType("MS.Internal.IO.Zip.ZipFileInfo");
    public static readonly PropertyInfo CompressionMethodProperty = MyZipFileInfoReflection.ZipFileInfoType.GetProperty("CompressionMethod", MyZipFileInfoReflection.Bind);
    public static readonly PropertyInfo DeflateOptionProperty = MyZipFileInfoReflection.ZipFileInfoType.GetProperty("DeflateOption", MyZipFileInfoReflection.Bind);
    public static readonly PropertyInfo FolderFlagProperty = MyZipFileInfoReflection.ZipFileInfoType.GetProperty("FolderFlag", MyZipFileInfoReflection.Bind);
    public static readonly PropertyInfo LastModFileDateTimeProperty = MyZipFileInfoReflection.ZipFileInfoType.GetProperty("LastModFileDateTime", MyZipFileInfoReflection.Bind);
    public static readonly PropertyInfo NameProperty = MyZipFileInfoReflection.ZipFileInfoType.GetProperty("Name", MyZipFileInfoReflection.Bind);
    public static readonly PropertyInfo VolumeLabelFlagProperty = MyZipFileInfoReflection.ZipFileInfoType.GetProperty("VolumeLabelFlag", MyZipFileInfoReflection.Bind);
    public static readonly MethodInfo GetStreamMethod = MyZipFileInfoReflection.ZipFileInfoType.GetMethod("GetStream", MyZipFileInfoReflection.Bind);
    public static readonly Func<object, ushort> CompressionMethod = ExpressionExtension.CreateGetter<object, ushort>(MyZipFileInfoReflection.CompressionMethodProperty);
    public static readonly Func<object, byte> DeflateOption = ExpressionExtension.CreateGetter<object, byte>(MyZipFileInfoReflection.DeflateOptionProperty);
    public static readonly Func<object, bool> FolderFlag = ExpressionExtension.CreateGetter<object, bool>(MyZipFileInfoReflection.FolderFlagProperty);
    public static readonly Func<object, DateTime> LastModFileDateTime = ExpressionExtension.CreateGetter<object, DateTime>(MyZipFileInfoReflection.LastModFileDateTimeProperty);
    public static readonly Func<object, string> Name = ExpressionExtension.CreateGetter<object, string>(MyZipFileInfoReflection.NameProperty);
    public static readonly Func<object, bool> VolumeLabelFlag = ExpressionExtension.CreateGetter<object, bool>(MyZipFileInfoReflection.VolumeLabelFlagProperty);
    public static readonly Func<object, FileMode, FileAccess, Stream> GetStream = ExpressionExtension.InstanceCall<Func<object, FileMode, FileAccess, Stream>>(MyZipFileInfoReflection.GetStreamMethod);
  }
}
