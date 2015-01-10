// Decompiled with JetBrains decompiler
// Type: VRageMath.CurveKey
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.ComponentModel;

namespace VRageMath
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  [Serializable]
  public class CurveKey : IEquatable<CurveKey>, IComparable<CurveKey>
  {
    internal float position;
    internal float internalValue;
    internal float tangentOut;
    internal float tangentIn;
    internal CurveContinuity continuity;

    public float Position
    {
      get
      {
        return this.position;
      }
    }

    public float Value
    {
      get
      {
        return this.internalValue;
      }
      set
      {
        this.internalValue = value;
      }
    }

    public float TangentIn
    {
      get
      {
        return this.tangentIn;
      }
      set
      {
        this.tangentIn = value;
      }
    }

    public float TangentOut
    {
      get
      {
        return this.tangentOut;
      }
      set
      {
        this.tangentOut = value;
      }
    }

    public CurveContinuity Continuity
    {
      get
      {
        return this.continuity;
      }
      set
      {
        this.continuity = value;
      }
    }

    public CurveKey(float position, float value)
    {
      this.position = position;
      this.internalValue = value;
    }

    public CurveKey(float position, float value, float tangentIn, float tangentOut)
    {
      this.position = position;
      this.internalValue = value;
      this.tangentIn = tangentIn;
      this.tangentOut = tangentOut;
    }

    public CurveKey(float position, float value, float tangentIn, float tangentOut, CurveContinuity continuity)
    {
      this.position = position;
      this.internalValue = value;
      this.tangentIn = tangentIn;
      this.tangentOut = tangentOut;
      this.continuity = continuity;
    }

    public static bool operator ==(CurveKey a, CurveKey b)
    {
      bool flag1 = null == a;
      bool flag2 = null == b;
      if (!flag1 && !flag2)
        return a.Equals(b);
      else
        return flag1 == flag2;
    }

    public static bool operator !=(CurveKey a, CurveKey b)
    {
      bool flag1 = a == (CurveKey) null;
      bool flag2 = b == (CurveKey) null;
      if (flag1 || flag2)
        return flag1 != flag2;
      if ((double) a.position == (double) b.position && (double) a.internalValue == (double) b.internalValue && ((double) a.tangentIn == (double) b.tangentIn && (double) a.tangentOut == (double) b.tangentOut))
        return a.continuity != b.continuity;
      else
        return true;
    }

    public CurveKey Clone()
    {
      return new CurveKey(this.position, this.internalValue, this.tangentIn, this.tangentOut, this.continuity);
    }

    public bool Equals(CurveKey other)
    {
      if (other != (CurveKey) null && (double) other.position == (double) this.position && ((double) other.internalValue == (double) this.internalValue && (double) other.tangentIn == (double) this.tangentIn) && (double) other.tangentOut == (double) this.tangentOut)
        return other.continuity == this.continuity;
      else
        return false;
    }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as CurveKey);
    }

    public override int GetHashCode()
    {
      return this.position.GetHashCode() + this.internalValue.GetHashCode() + this.tangentIn.GetHashCode() + this.tangentOut.GetHashCode() + this.continuity.GetHashCode();
    }

    public int CompareTo(CurveKey other)
    {
      if ((double) this.position == (double) other.position)
        return 0;
      return (double) this.position < (double) other.position ? -1 : 1;
    }
  }
}
