﻿// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoException
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf
{
  public class ProtoException : Exception
  {
    public ProtoException()
    {
    }

    public ProtoException(string message)
      : base(message)
    {
    }

    public ProtoException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
