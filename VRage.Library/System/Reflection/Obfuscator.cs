// Decompiled with JetBrains decompiler
// Type: System.Reflection.Obfuscator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Linq;

namespace System.Reflection
{
    public static class Obfuscator
    {
        public const bool EnableAttributeCheck = true;
        public const string NoRename = "cw symbol renaming";

        public static bool CheckAttribute(this MemberInfo member)
        {
            foreach (
                ObfuscationAttribute obfuscationAttribute in
                    Enumerable.OfType<ObfuscationAttribute>(
                        (IEnumerable) member.GetCustomAttributes(typeof (ObfuscationAttribute), false)))
            {
                if (obfuscationAttribute.Feature == "cw symbol renaming" && obfuscationAttribute.Exclude)
                    return true;
            }
            return false;
        }
    }
}