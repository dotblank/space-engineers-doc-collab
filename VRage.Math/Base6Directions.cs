// Decompiled with JetBrains decompiler
// Type: VRageMath.Base6Directions
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
  public static class Base6Directions
  {
    public static readonly Base6Directions.Direction[] EnumDirections = new Base6Directions.Direction[6]
    {
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Backward,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Right,
      Base6Directions.Direction.Up,
      Base6Directions.Direction.Down
    };
    public static readonly Vector3[] Directions = new Vector3[6]
    {
      Vector3.Forward,
      Vector3.Backward,
      Vector3.Left,
      Vector3.Right,
      Vector3.Up,
      Vector3.Down
    };
    public static readonly Vector3I[] IntDirections = new Vector3I[6]
    {
      Vector3I.Forward,
      Vector3I.Backward,
      Vector3I.Left,
      Vector3I.Right,
      Vector3I.Up,
      Vector3I.Down
    };
    private static readonly Base6Directions.Direction[] LeftDirections = new Base6Directions.Direction[36]
    {
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Down,
      Base6Directions.Direction.Up,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Right,
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Up,
      Base6Directions.Direction.Down,
      Base6Directions.Direction.Right,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Up,
      Base6Directions.Direction.Down,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Backward,
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Down,
      Base6Directions.Direction.Up,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Backward,
      Base6Directions.Direction.Right,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Backward,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Right,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Right,
      Base6Directions.Direction.Backward,
      Base6Directions.Direction.Forward,
      Base6Directions.Direction.Left,
      Base6Directions.Direction.Right
    };
    private const float DIRECTION_EPSILON = 1E-05f;
    private static readonly int[] ForwardBackward;
    private static readonly int[] LeftRight;
    private static readonly int[] UpDown;

    static Base6Directions()
    {
      int[] numArray = new int[3];
      numArray[2] = 1;
      Base6Directions.ForwardBackward = numArray;
      Base6Directions.LeftRight = new int[3]
      {
        2,
        0,
        3
      };
      Base6Directions.UpDown = new int[3]
      {
        5,
        0,
        4
      };
    }

    public static bool IsBaseDirection(ref Vector3 vec)
    {
      return (double) vec.X * (double) vec.X + (double) vec.Y * (double) vec.Y + (double) vec.Z * (double) vec.Z - 1.0 < 9.99999974737875E-06;
    }

    public static bool IsBaseDirection(Vector3 vec)
    {
      return Base6Directions.IsBaseDirection(ref vec);
    }

    public static bool IsBaseDirection(ref Vector3I vec)
    {
      return vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z - 1 == 0;
    }

    public static Vector3 GetVector(int direction)
    {
      return Base6Directions.Directions[direction];
    }

    public static Vector3 GetVector(Base6Directions.Direction dir)
    {
      return Base6Directions.Directions[(int) dir];
    }

    public static Vector3I GetIntVector(int direction)
    {
      return Base6Directions.IntDirections[direction];
    }

    public static Vector3I GetIntVector(Base6Directions.Direction dir)
    {
      return Base6Directions.IntDirections[(int) dir];
    }

    public static void GetVector(Base6Directions.Direction dir, out Vector3 result)
    {
      result = Base6Directions.Directions[(int) dir];
    }

    public static Base6Directions.Direction GetPerpendicular(Base6Directions.Direction dir)
    {
      return Base6Directions.GetAxis(dir) == Base6Directions.Axis.UpDown ? Base6Directions.Direction.Right : Base6Directions.Direction.Up;
    }

    public static Base6Directions.Direction GetDirection(Vector3 vec)
    {
      return Base6Directions.GetDirection(ref vec);
    }

    public static Base6Directions.Direction GetDirection(ref Vector3 vec)
    {
      return (Base6Directions.Direction) (0 + Base6Directions.ForwardBackward[(int) Math.Round((double) vec.Z + 1.0)] + Base6Directions.LeftRight[(int) Math.Round((double) vec.X + 1.0)] + Base6Directions.UpDown[(int) Math.Round((double) vec.Y + 1.0)]);
    }

    public static Base6Directions.Direction GetDirection(Vector3I vec)
    {
      return Base6Directions.GetDirection(ref vec);
    }

    public static Base6Directions.Direction GetDirection(ref Vector3I vec)
    {
      return (Base6Directions.Direction) (0 + Base6Directions.ForwardBackward[vec.Z + 1] + Base6Directions.LeftRight[vec.X + 1] + Base6Directions.UpDown[vec.Y + 1]);
    }

    public static Base6Directions.Direction GetClosestDirection(Vector3 vec)
    {
      return Base6Directions.GetClosestDirection(ref vec);
    }

    public static Base6Directions.Direction GetClosestDirection(ref Vector3 vec)
    {
      Vector3 vec1 = Vector3.Sign(Vector3.DominantAxisProjection(vec));
      return Base6Directions.GetDirection(ref vec1);
    }

    public static Base6Directions.Direction GetDirectionInAxis(Vector3 vec, Base6Directions.Axis axis)
    {
      return Base6Directions.GetDirectionInAxis(ref vec, axis);
    }

    public static Base6Directions.Direction GetDirectionInAxis(ref Vector3 vec, Base6Directions.Axis axis)
    {
      Base6Directions.Direction baseAxisDirection = Base6Directions.GetBaseAxisDirection(axis);
      Vector3 vector3 = (Vector3) Base6Directions.IntDirections[(int) baseAxisDirection] * vec;
      if ((double) vector3.X + (double) vector3.Y + (double) vector3.Z >= 1.0)
        return baseAxisDirection;
      else
        return Base6Directions.GetFlippedDirection(baseAxisDirection);
    }

    public static Base6Directions.Direction GetForward(Quaternion rot)
    {
      Vector3 result;
      Vector3.Transform(ref Vector3.Forward, ref rot, out result);
      return Base6Directions.GetDirection(ref result);
    }

    public static Base6Directions.Direction GetForward(ref Quaternion rot)
    {
      Vector3 result;
      Vector3.Transform(ref Vector3.Forward, ref rot, out result);
      return Base6Directions.GetDirection(ref result);
    }

    public static Base6Directions.Direction GetForward(ref Matrix rotation)
    {
      Vector3 result;
      Vector3.TransformNormal(ref Vector3.Forward, ref rotation, out result);
      return Base6Directions.GetDirection(ref result);
    }

    public static Base6Directions.Direction GetUp(Quaternion rot)
    {
      Vector3 result;
      Vector3.Transform(ref Vector3.Up, ref rot, out result);
      return Base6Directions.GetDirection(ref result);
    }

    public static Base6Directions.Direction GetUp(ref Quaternion rot)
    {
      Vector3 result;
      Vector3.Transform(ref Vector3.Up, ref rot, out result);
      return Base6Directions.GetDirection(ref result);
    }

    public static Base6Directions.Direction GetUp(ref Matrix rotation)
    {
      Vector3 result;
      Vector3.TransformNormal(ref Vector3.Up, ref rotation, out result);
      return Base6Directions.GetDirection(ref result);
    }

    public static Base6Directions.Axis GetAxis(Base6Directions.Direction direction)
    {
      return (Base6Directions.Axis) ((uint) direction >> 1);
    }

    public static Base6Directions.Direction GetBaseAxisDirection(Base6Directions.Axis axis)
    {
      return (Base6Directions.Direction) ((uint) axis << 1);
    }

    public static Base6Directions.Direction GetFlippedDirection(Base6Directions.Direction toFlip)
    {
      return toFlip ^ Base6Directions.Direction.Backward;
    }

    public static Base6Directions.Direction GetCross(Base6Directions.Direction dir1, Base6Directions.Direction dir2)
    {
      return Base6Directions.GetLeft(dir1, dir2);
    }

    public static Base6Directions.Direction GetLeft(Base6Directions.Direction up, Base6Directions.Direction forward)
    {
      return Base6Directions.LeftDirections[(int) ((byte) ((int) forward * 6) + up)];
    }

    public static Quaternion GetOrientation(Base6Directions.Direction forward, Base6Directions.Direction up)
    {
      return Quaternion.CreateFromForwardUp(Base6Directions.GetVector(forward), Base6Directions.GetVector(up));
    }

    public static bool IsValidBlockOrientation(Base6Directions.Direction forward, Base6Directions.Direction up)
    {
      return (double) Vector3.Dot(Base6Directions.GetVector(forward), Base6Directions.GetVector(up)) == 0.0;
    }

    public enum Direction : byte
    {
      Forward,
      Backward,
      Left,
      Right,
      Up,
      Down,
    }

    public enum Axis : byte
    {
      ForwardBackward,
      LeftRight,
      UpDown,
    }
  }
}
