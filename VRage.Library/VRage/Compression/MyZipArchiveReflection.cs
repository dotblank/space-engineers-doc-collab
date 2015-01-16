// Decompiled with JetBrains decompiler
// Type: VRage.Compression.MyZipArchiveReflection
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.IO.Packaging;
using System.Linq.Expressions;
using System.Reflection;

namespace VRage.Compression
{
    public class MyZipArchiveReflection
    {
        public static readonly BindingFlags StaticBind = BindingFlags.Static | BindingFlags.Public |
                                                         BindingFlags.NonPublic;

        public static readonly BindingFlags InstanceBind = BindingFlags.Instance | BindingFlags.Public |
                                                           BindingFlags.NonPublic;

        public static readonly Assembly ZipAssembly = typeof (Package).Assembly;

        public static readonly Type ZipArchiveType =
            MyZipArchiveReflection.ZipAssembly.GetType("MS.Internal.IO.Zip.ZipArchive");

        public static readonly Type CompressionMethodType =
            MyZipArchiveReflection.ZipAssembly.GetType("MS.Internal.IO.Zip.CompressionMethodEnum");

        public static readonly Type DeflateOptionType =
            MyZipArchiveReflection.ZipAssembly.GetType("MS.Internal.IO.Zip.DeflateOptionEnum");

        public static readonly MethodInfo OpenOnFileMethod =
            MyZipArchiveReflection.ZipArchiveType.GetMethod("OpenOnFile", MyZipArchiveReflection.StaticBind);

        public static readonly MethodInfo OpenOnStreamMethod =
            MyZipArchiveReflection.ZipArchiveType.GetMethod("OpenOnStream", MyZipArchiveReflection.StaticBind);

        public static readonly MethodInfo GetFilesMethod = MyZipArchiveReflection.ZipArchiveType.GetMethod("GetFiles",
            MyZipArchiveReflection.InstanceBind);

        public static readonly MethodInfo GetFileMethod = MyZipArchiveReflection.ZipArchiveType.GetMethod("GetFile",
            MyZipArchiveReflection.InstanceBind);

        public static readonly MethodInfo FileExistsMethod =
            MyZipArchiveReflection.ZipArchiveType.GetMethod("FileExists", MyZipArchiveReflection.InstanceBind);

        public static readonly MethodInfo AddFileMethod = MyZipArchiveReflection.ZipArchiveType.GetMethod("AddFile",
            MyZipArchiveReflection.InstanceBind);

        public static readonly MethodInfo DeleteFileMethod =
            MyZipArchiveReflection.ZipArchiveType.GetMethod("DeleteFile", MyZipArchiveReflection.InstanceBind);

        public static readonly Func<string, FileMode, FileAccess, FileShare, bool, object> OpenOnFile =
            ExpressionExtension.StaticCall<Func<string, FileMode, FileAccess, FileShare, bool, object>>(
                MyZipArchiveReflection.OpenOnFileMethod);

        public static readonly Func<Stream, FileMode, FileAccess, bool, object> OpenOnStream =
            ExpressionExtension.StaticCall<Func<Stream, FileMode, FileAccess, bool, object>>(
                MyZipArchiveReflection.OpenOnStreamMethod);

        public static readonly Func<object, object> GetFiles =
            ExpressionExtension.InstanceCall<Func<object, object>>(MyZipArchiveReflection.GetFilesMethod);

        public static readonly Func<object, string, object> GetFile =
            ExpressionExtension.InstanceCall<Func<object, string, object>>(MyZipArchiveReflection.GetFileMethod);

        public static readonly Func<object, string, bool> FileExists =
            ExpressionExtension.InstanceCall<Func<object, string, bool>>(MyZipArchiveReflection.FileExistsMethod);

        public static readonly Func<object, string, ushort, byte, object> AddFile =
            ExpressionExtension.InstanceCall<Func<object, string, ushort, byte, object>>(
                MyZipArchiveReflection.AddFileMethod);

        public static readonly Action<object, string> DeleteFile =
            ExpressionExtension.InstanceCall<Action<object, string>>(MyZipArchiveReflection.DeleteFileMethod);
    }
}