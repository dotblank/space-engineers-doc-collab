// Decompiled with JetBrains decompiler
// Type: VRageMath.MyBlockOrientation
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

namespace VRageMath
{
  public struct MyBlockOrientation
  {
    public static readonly MyBlockOrientation Identity = new MyBlockOrientation(Base6Directions.Direction.Forward, Base6Directions.Direction.Up);
    public Base6Directions.Direction Forward;
    public Base6Directions.Direction Up;

    public Base6Directions.Direction Left
    {
      get
      {
        return Base6Directions.GetLeft(this.Up, this.Forward);
      }
    }

    public bool IsValid
    {
      get
      {
        return Base6Directions.IsValidBlockOrientation(this.Forward, this.Up);
      }
    }

    public MyBlockOrientation(Base6Directions.Direction forward, Base6Directions.Direction up)
    {
      this.Forward = forward;
      this.Up = up;
    }

    public MyBlockOrientation(ref Quaternion q)
    {
      this.Forward = Base6Directions.GetForward(q);
      this.Up = Base6Directions.GetUp(q);
    }

    public MyBlockOrientation(ref Matrix m)
    {
      this.Forward = Base6Directions.GetForward(ref m);
      this.Up = Base6Directions.GetUp(ref m);
    }

    public static bool operator ==(MyBlockOrientation orientation1, MyBlockOrientation orientation2)
    {
      if (orientation1.Forward == orientation2.Forward)
        return orientation1.Up == orientation2.Up;
      else
        return false;
    }

    public static bool operator !=(MyBlockOrientation orientation1, MyBlockOrientation orientation2)
    {
      if (orientation1.Forward == orientation2.Forward)
        return orientation1.Up != orientation2.Up;
      else
        return true;
    }

    public void GetQuaternion(out Quaternion result)
    {
      Matrix result1;
      this.GetMatrix(out result1);
      Quaternion.CreateFromRotationMatrix(ref result1, out result);
    }

    public void GetMatrix(out Matrix result)
    {
      Vector3 result1;
      Base6Directions.GetVector(this.Forward, out result1);
      Vector3 result2;
      Base6Directions.GetVector(this.Up, out result2);
      Matrix.CreateWorld(ref Vector3.Zero, ref result1, ref result2, out result);
    }

    public Base6Directions.Direction TransformDirection(Base6Directions.Direction baseDirection)
    {
      Base6Directions.Axis axis = Base6Directions.GetAxis(baseDirection);
      int num = (int) baseDirection % 2;
      if (axis == Base6Directions.Axis.ForwardBackward)
      {
        if (num != 1)
          return this.Forward;
        else
          return Base6Directions.GetFlippedDirection(this.Forward);
      }
      else if (axis == Base6Directions.Axis.LeftRight)
      {
        if (num != 1)
          return this.Left;
        else
          return Base6Directions.GetFlippedDirection(this.Left);
      }
      else if (num != 1)
        return this.Up;
      else
        return Base6Directions.GetFlippedDirection(this.Up);
    }

    public Base6Directions.Direction TransformDirectionInverse(Base6Directions.Direction baseDirection)
    {
      Base6Directions.Axis axis = Base6Directions.GetAxis(baseDirection);
      if (axis == Base6Directions.GetAxis(this.Forward))
        return baseDirection != this.Forward ? Base6Directions.Direction.Backward : Base6Directions.Direction.Forward;
      else if (axis == Base6Directions.GetAxis(this.Left))
        return baseDirection != this.Left ? Base6Directions.Direction.Right : Base6Directions.Direction.Left;
      else
        return baseDirection != this.Up ? Base6Directions.Direction.Down : Base6Directions.Direction.Up;
    }
  }
}
