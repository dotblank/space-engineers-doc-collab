// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace System.Reflection
{
    public static class AssemblyExtensions
    {
        public static ProcessorArchitecture ToProcessorArchitecture(this PortableExecutableKinds peKind)
        {
            switch (peKind & ~PortableExecutableKinds.ILOnly)
            {
                case PortableExecutableKinds.Required32Bit:
                    return ProcessorArchitecture.X86;
                case PortableExecutableKinds.PE32Plus:
                    return ProcessorArchitecture.Amd64;
                case PortableExecutableKinds.Unmanaged32Bit:
                    return ProcessorArchitecture.X86;
                default:
                    return (peKind & PortableExecutableKinds.ILOnly) ==
                           PortableExecutableKinds.NotAPortableExecutableImage
                        ? ProcessorArchitecture.None
                        : ProcessorArchitecture.MSIL;
            }
        }

        public static PortableExecutableKinds GetPeKind(this Assembly assembly)
        {
            PortableExecutableKinds peKind;
            ImageFileMachine machine;
            assembly.ManifestModule.GetPEKind(out peKind, out machine);
            return peKind;
        }

        public static ProcessorArchitecture GetArchitecture(this Assembly assembly)
        {
            return AssemblyExtensions.ToProcessorArchitecture(AssemblyExtensions.GetPeKind(assembly));
        }

        public static ProcessorArchitecture TryGetArchitecture(string assemblyName)
        {
            try
            {
                return AssemblyName.GetAssemblyName(assemblyName).ProcessorArchitecture;
            }
            catch
            {
                return ProcessorArchitecture.None;
            }
        }

        public static ProcessorArchitecture TryGetArchitecture(this Assembly assembly)
        {
            try
            {
                return AssemblyExtensions.GetArchitecture(assembly);
            }
            catch
            {
                return ProcessorArchitecture.None;
            }
        }
    }
}