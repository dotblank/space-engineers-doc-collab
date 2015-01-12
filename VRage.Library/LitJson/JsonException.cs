// Decompiled with JetBrains decompiler
// Type: LitJson.JsonException
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace LitJson
{
    public class JsonException : ApplicationException
    {
        public JsonException()
        {
        }

        internal JsonException(ParserToken token)
            : base(string.Format("Invalid token '{0}' in input string", (object) token))
        {
        }

        internal JsonException(ParserToken token, Exception inner_exception)
            : base(string.Format("Invalid token '{0}' in input string", (object) token), inner_exception)
        {
        }

        internal JsonException(int c)
            : base(string.Format("Invalid character '{0}' in input string", (object) (char) c))
        {
        }

        internal JsonException(int c, Exception inner_exception)
            : base(string.Format("Invalid character '{0}' in input string", (object) (char) c), inner_exception)
        {
        }

        public JsonException(string message)
            : base(message)
        {
        }

        public JsonException(string message, Exception inner_exception)
            : base(message, inner_exception)
        {
        }
    }
}