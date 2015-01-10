// Decompiled with JetBrains decompiler
// Type: VRageMath.Curve
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
  [Serializable]
  public class Curve
  {
    private CurveKeyCollection keys = new CurveKeyCollection();
    private CurveLoopType preLoop;
    private CurveLoopType postLoop;

    public CurveLoopType PreLoop
    {
      get
      {
        return this.preLoop;
      }
      set
      {
        this.preLoop = value;
      }
    }

    public CurveLoopType PostLoop
    {
      get
      {
        return this.postLoop;
      }
      set
      {
        this.postLoop = value;
      }
    }

    public CurveKeyCollection Keys
    {
      get
      {
        return this.keys;
      }
    }

    public bool IsConstant
    {
      get
      {
        return this.keys.Count <= 1;
      }
    }

    public Curve Clone()
    {
      return new Curve()
      {
        preLoop = this.preLoop,
        postLoop = this.postLoop,
        keys = this.keys.Clone()
      };
    }

    public void ComputeTangent(int keyIndex, CurveTangent tangentType)
    {
      this.ComputeTangent(keyIndex, tangentType, tangentType);
    }

    public void ComputeTangent(int keyIndex, CurveTangent tangentInType, CurveTangent tangentOutType)
    {
      if (this.keys.Count <= keyIndex || keyIndex < 0)
        throw new ArgumentOutOfRangeException("keyIndex");
      CurveKey curveKey = this.Keys[keyIndex];
      double num1;
      float num2 = (float) (num1 = (double) curveKey.Position);
      float num3 = (float) num1;
      float num4 = (float) num1;
      double num5;
      float num6 = (float) (num5 = (double) curveKey.Value);
      float num7 = (float) num5;
      float num8 = (float) num5;
      if (keyIndex > 0)
      {
        num4 = this.Keys[keyIndex - 1].Position;
        num8 = this.Keys[keyIndex - 1].Value;
      }
      if (keyIndex + 1 < this.keys.Count)
      {
        num2 = this.Keys[keyIndex + 1].Position;
        num6 = this.Keys[keyIndex + 1].Value;
      }
      if (tangentInType == CurveTangent.Smooth)
      {
        float num9 = num2 - num4;
        float num10 = num6 - num8;
        curveKey.TangentIn = (double) Math.Abs(num10) >= 1.19209289550781E-07 ? num10 * Math.Abs(num4 - num3) / num9 : 0.0f;
      }
      else
        curveKey.TangentIn = tangentInType != CurveTangent.Linear ? 0.0f : num7 - num8;
      if (tangentOutType == CurveTangent.Smooth)
      {
        float num9 = num2 - num4;
        float num10 = num6 - num8;
        if ((double) Math.Abs(num10) < 1.19209289550781E-07)
          curveKey.TangentOut = 0.0f;
        else
          curveKey.TangentOut = num10 * Math.Abs(num2 - num3) / num9;
      }
      else if (tangentOutType == CurveTangent.Linear)
        curveKey.TangentOut = num6 - num7;
      else
        curveKey.TangentOut = 0.0f;
    }

    public void ComputeTangents(CurveTangent tangentType)
    {
      this.ComputeTangents(tangentType, tangentType);
    }

    public void ComputeTangents(CurveTangent tangentInType, CurveTangent tangentOutType)
    {
      for (int keyIndex = 0; keyIndex < this.Keys.Count; ++keyIndex)
        this.ComputeTangent(keyIndex, tangentInType, tangentOutType);
    }

    public float Evaluate(float position)
    {
      if (this.keys.Count == 0)
        return 0.0f;
      if (this.keys.Count == 1)
        return this.keys[0].internalValue;
      CurveKey curveKey1 = this.keys[0];
      CurveKey curveKey2 = this.keys[this.keys.Count - 1];
      float t = position;
      float num1 = 0.0f;
      if ((double) t < (double) curveKey1.position)
      {
        if (this.preLoop == CurveLoopType.Constant)
          return curveKey1.internalValue;
        if (this.preLoop == CurveLoopType.Linear)
          return curveKey1.internalValue - curveKey1.tangentIn * (curveKey1.position - t);
        if (!this.keys.IsCacheAvailable)
          this.keys.ComputeCacheValues();
        float num2 = this.CalcCycle(t);
        float num3 = t - (curveKey1.position + num2 * this.keys.TimeRange);
        if (this.preLoop == CurveLoopType.Cycle)
          t = curveKey1.position + num3;
        else if (this.preLoop == CurveLoopType.CycleOffset)
        {
          t = curveKey1.position + num3;
          num1 = (curveKey2.internalValue - curveKey1.internalValue) * num2;
        }
        else
          t = ((int) num2 & 1) != 0 ? curveKey2.position - num3 : curveKey1.position + num3;
      }
      else if ((double) curveKey2.position < (double) t)
      {
        if (this.postLoop == CurveLoopType.Constant)
          return curveKey2.internalValue;
        if (this.postLoop == CurveLoopType.Linear)
          return curveKey2.internalValue - curveKey2.tangentOut * (curveKey2.position - t);
        if (!this.keys.IsCacheAvailable)
          this.keys.ComputeCacheValues();
        float num2 = this.CalcCycle(t);
        float num3 = t - (curveKey1.position + num2 * this.keys.TimeRange);
        if (this.postLoop == CurveLoopType.Cycle)
          t = curveKey1.position + num3;
        else if (this.postLoop == CurveLoopType.CycleOffset)
        {
          t = curveKey1.position + num3;
          num1 = (curveKey2.internalValue - curveKey1.internalValue) * num2;
        }
        else
          t = ((int) num2 & 1) != 0 ? curveKey2.position - num3 : curveKey1.position + num3;
      }
      CurveKey k0 = (CurveKey) null;
      CurveKey k1 = (CurveKey) null;
      float segment = this.FindSegment(t, ref k0, ref k1);
      return num1 + Curve.Hermite(k0, k1, segment);
    }

    private float CalcCycle(float t)
    {
      float num = (t - this.keys[0].position) * this.keys.InvTimeRange;
      if ((double) num < 0.0)
        --num;
      return (float) (int) num;
    }

    private float FindSegment(float t, ref CurveKey k0, ref CurveKey k1)
    {
      float num1 = t;
      k0 = this.keys[0];
      for (int index = 1; index < this.keys.Count; ++index)
      {
        k1 = this.keys[index];
        if ((double) k1.position >= (double) t)
        {
          double num2 = (double) k0.position;
          double num3 = (double) k1.position;
          double num4 = (double) t;
          double num5 = num3 - num2;
          num1 = 0.0f;
          if (num5 > 0.0)
          {
            num1 = (float) ((num4 - num2) / num5);
            break;
          }
          else
            break;
        }
        else
          k0 = k1;
      }
      return num1;
    }

    private static float Hermite(CurveKey k0, CurveKey k1, float t)
    {
      if (k0.Continuity == CurveContinuity.Step)
      {
        if ((double) t >= 1.0)
          return k1.internalValue;
        else
          return k0.internalValue;
      }
      else
      {
        float num1 = t * t;
        float num2 = num1 * t;
        float num3 = k0.internalValue;
        float num4 = k1.internalValue;
        float num5 = k0.tangentOut;
        float num6 = k1.tangentIn;
        return (float) ((double) num3 * (2.0 * (double) num2 - 3.0 * (double) num1 + 1.0) + (double) num4 * (-2.0 * (double) num2 + 3.0 * (double) num1) + (double) num5 * ((double) num2 - 2.0 * (double) num1 + (double) t) + (double) num6 * ((double) num2 - (double) num1));
      }
    }
  }
}
