// Decompiled with JetBrains decompiler
// Type: LitJson.IJsonWrapper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Specialized;

namespace LitJson
{
  public interface IJsonWrapper : IList, IOrderedDictionary, IDictionary, ICollection, IEnumerable
  {
    bool IsArray { get; }

    bool IsBoolean { get; }

    bool IsDouble { get; }

    bool IsInt { get; }

    bool IsLong { get; }

    bool IsObject { get; }

    bool IsString { get; }

    bool GetBoolean();

    double GetDouble();

    int GetInt();

    JsonType GetJsonType();

    long GetLong();

    string GetString();

    void SetBoolean(bool val);

    void SetDouble(double val);

    void SetInt(int val);

    void SetJsonType(JsonType type);

    void SetLong(long val);

    void SetString(string val);

    string ToJson();

    void ToJson(JsonWriter writer);
  }
}
