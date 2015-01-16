// Decompiled with JetBrains decompiler
// Type: System.Reflection.Obfuscator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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