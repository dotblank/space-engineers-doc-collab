// Decompiled with JetBrains decompiler
// Type: VRage.Partition
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Linq;

namespace VRage
{
  public static class Partition
  {
    private static readonly string[] m_letters = Enumerable.ToArray<string>(Enumerable.Select<int, string>(Enumerable.Range(65, 26), (Func<int, string>) (s => new string((char) s, 1))));

    public static T Select<T>(int num, T a, T b)
    {
      if (num % 2 != 0)
        return b;
      else
        return a;
    }

    public static T Select<T>(int num, T a, T b, T c)
    {
      switch ((uint) num % 3U)
      {
        case 0U:
          return a;
        case 1U:
          return b;
        default:
          return c;
      }
    }

    public static T Select<T>(int num, T a, T b, T c, T d)
    {
      switch ((uint) num % 4U)
      {
        case 0U:
          return a;
        case 1U:
          return b;
        case 2U:
          return c;
        default:
          return d;
      }
    }

    public static T Select<T>(int num, T a, T b, T c, T d, T e)
    {
      switch ((uint) num % 5U)
      {
        case 0U:
          return a;
        case 1U:
          return b;
        case 2U:
          return c;
        case 3U:
          return d;
        default:
          return e;
      }
    }

    public static T Select<T>(int num, T a, T b, T c, T d, T e, T f)
    {
      switch ((uint) num % 6U)
      {
        case 0U:
          return a;
        case 1U:
          return b;
        case 2U:
          return c;
        case 3U:
          return d;
        case 4U:
          return e;
        default:
          return f;
      }
    }

    public static T Select<T>(int num, T a, T b, T c, T d, T e, T f, T g)
    {
      switch ((uint) num % 7U)
      {
        case 0U:
          return a;
        case 1U:
          return b;
        case 2U:
          return c;
        case 3U:
          return d;
        case 4U:
          return e;
        case 5U:
          return f;
        default:
          return g;
      }
    }

    public static T Select<T>(int num, T a, T b, T c, T d, T e, T f, T g, T h)
    {
      switch ((uint) num % 8U)
      {
        case 0U:
          return a;
        case 1U:
          return b;
        case 2U:
          return c;
        case 3U:
          return d;
        case 4U:
          return e;
        case 5U:
          return f;
        case 6U:
          return g;
        default:
          return h;
      }
    }

    public static T Select<T>(int num, T a, T b, T c, T d, T e, T f, T g, T h, T i)
    {
      switch ((uint) num % 9U)
      {
        case 0U:
          return a;
        case 1U:
          return b;
        case 2U:
          return c;
        case 3U:
          return d;
        case 4U:
          return e;
        case 5U:
          return f;
        case 6U:
          return g;
        case 7U:
          return h;
        default:
          return i;
      }
    }

    public static string SelectStringByLetter(char c)
    {
      if ((int) c >= 97 && (int) c <= 122 || (int) c >= 65 && (int) c <= 90)
      {
        c = char.ToUpperInvariant(c);
        return Partition.m_letters[(int) c - 65];
      }
      else
        return (int) c >= 48 && (int) c <= 57 ? "0-9" : "Non-letter";
    }

    public static string SelectStringGroupOfTenByLetter(char c)
    {
      c = char.ToUpperInvariant(c);
      if ((int) c >= 48 && (int) c <= 57)
        return "0-9";
      if ((int) c == 65 || (int) c == 66 || (int) c == 67)
        return "A-C";
      if ((int) c == 68 || (int) c == 69 || (int) c == 70)
        return "D-F";
      if ((int) c == 71 || (int) c == 72 || (int) c == 73)
        return "G-I";
      if ((int) c == 74 || (int) c == 75 || (int) c == 76)
        return "J-L";
      if ((int) c == 77 || (int) c == 78 || (int) c == 79)
        return "M-O";
      if ((int) c == 80 || (int) c == 81 || (int) c == 82)
        return "P-R";
      if ((int) c == 83 || (int) c == 84 || ((int) c == 85 || (int) c == 86))
        return "S-V";
      return (int) c == 87 || (int) c == 88 || ((int) c == 89 || (int) c == 90) ? "W-Z" : "Non-letter";
    }
  }
}
