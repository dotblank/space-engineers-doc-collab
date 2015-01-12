// Decompiled with JetBrains decompiler
// Type: VRage.Compression.MyZipFileInfo
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;

namespace VRage.Compression
{
    public struct MyZipFileInfo
    {
        internal object m_fileInfo;

        public bool IsValid
        {
            get { return this.m_fileInfo != null; }
        }

        public CompressionMethodEnum CompressionMethod
        {
            get { return (CompressionMethodEnum) MyZipFileInfoReflection.CompressionMethod(this.m_fileInfo); }
        }

        public DeflateOptionEnum DeflateOption
        {
            get { return (DeflateOptionEnum) MyZipFileInfoReflection.DeflateOption(this.m_fileInfo); }
        }

        public bool FolderFlag
        {
            get { return MyZipFileInfoReflection.FolderFlag(this.m_fileInfo); }
        }

        public DateTime LastModFileDateTime
        {
            get { return MyZipFileInfoReflection.LastModFileDateTime(this.m_fileInfo); }
        }

        public string Name
        {
            get { return MyZipFileInfoReflection.Name(this.m_fileInfo); }
        }

        public bool VolumeLabelFlag
        {
            get { return MyZipFileInfoReflection.VolumeLabelFlag(this.m_fileInfo); }
        }

        internal MyZipFileInfo(object fileInfo)
        {
            this.m_fileInfo = fileInfo;
        }

        public Stream GetStream(FileMode mode = FileMode.Open, FileAccess access = FileAccess.Read)
        {
            return MyZipFileInfoReflection.GetStream(this.m_fileInfo, mode, access);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}