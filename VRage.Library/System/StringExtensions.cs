// Decompiled with JetBrains decompiler
// Type: System.StringExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace System
{
    public static class StringExtensions
    {
        public static unsafe bool Equals(this string text, char* compareTo, int length)
        {
            int index1 = Math.Min(length, text.Length);
            for (int index2 = 0; index2 < index1; ++index2)
            {
                if ((int) text[index2] != (int) compareTo[index2])
                    return false;
            }
            if (length > index1)
                return (int) compareTo[index1] == 0;
            else
                return true;
        }

        public static bool Contains(this string text, string testSequence, StringComparison comparison)
        {
            return text.IndexOf(testSequence, comparison) != -1;
        }
    }
}