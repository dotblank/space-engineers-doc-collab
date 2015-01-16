// Decompiled with JetBrains decompiler
// Type: LitJson.JsonException
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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