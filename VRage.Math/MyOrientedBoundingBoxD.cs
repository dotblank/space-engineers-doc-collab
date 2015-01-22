// Decompiled with JetBrains decompiler
// Type: VRageMath.MyOrientedBoundingBoxD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
  public struct MyOrientedBoundingBoxD : IEquatable<MyOrientedBoundingBox>
  {
    public static readonly int[] StartVertices = new int[12]
    {
      0,
      1,
      5,
      4,
      3,
      2,
      6,
      7,
      0,
      1,
      5,
      4
    };
    public static readonly int[] EndVertices = new int[12]
    {
      1,
      5,
      4,
      0,
      2,
      6,
      7,
      3,
      3,
      2,
      6,
      7
    };
    public static readonly int[] StartXVertices = new int[4]
    {
      0,
      4,
      7,
      3
    };
    public static readonly int[] EndXVertices = new int[4]
    {
      1,
      5,
      6,
      2
    };
    public static readonly int[] StartYVertices = new int[4]
    {
      0,
      1,
      5,
      4
    };
    public static readonly int[] EndYVertices = new int[4]
    {
      3,
      2,
      6,
      7
    };
    public static readonly int[] StartZVertices = new int[4]
    {
      0,
      3,
      2,
      1
    };
    public static readonly int[] EndZVertices = new int[4]
    {
      4,
      7,
      6,
      5
    };
    public static readonly Vector3[] XNeighbourVectorsBack = new Vector3[4]
    {
      new Vector3(0.0f, 0.0f, 1f),
      new Vector3(0.0f, 1f, 0.0f),
      new Vector3(0.0f, 0.0f, -1f),
      new Vector3(0.0f, -1f, 0.0f)
    };
    public static readonly Vector3[] XNeighbourVectorsForw = new Vector3[4]
    {
      new Vector3(0.0f, 0.0f, -1f),
      new Vector3(0.0f, -1f, 0.0f),
      new Vector3(0.0f, 0.0f, 1f),
      new Vector3(0.0f, 1f, 0.0f)
    };
    public static readonly Vector3[] YNeighbourVectorsBack = new Vector3[4]
    {
      new Vector3(1f, 0.0f, 0.0f),
      new Vector3(0.0f, 0.0f, 1f),
      new Vector3(-1f, 0.0f, 0.0f),
      new Vector3(0.0f, 0.0f, -1f)
    };
    public static readonly Vector3[] YNeighbourVectorsForw = new Vector3[4]
    {
      new Vector3(-1f, 0.0f, 0.0f),
      new Vector3(0.0f, 0.0f, -1f),
      new Vector3(1f, 0.0f, 0.0f),
      new Vector3(0.0f, 0.0f, 1f)
    };
    public static readonly Vector3[] ZNeighbourVectorsBack = new Vector3[4]
    {
      new Vector3(0.0f, 1f, 0.0f),
      new Vector3(1f, 0.0f, 0.0f),
      new Vector3(0.0f, -1f, 0.0f),
      new Vector3(-1f, 0.0f, 0.0f)
    };
    public static readonly Vector3[] ZNeighbourVectorsForw = new Vector3[4]
    {
      new Vector3(0.0f, -1f, 0.0f),
      new Vector3(-1f, 0.0f, 0.0f),
      new Vector3(0.0f, 1f, 0.0f),
      new Vector3(1f, 0.0f, 0.0f)
    };
    public const int CornerCount = 8;
    private const float RAY_EPSILON = 1E-20f;
    public Vector3D Center;
    public Vector3D HalfExtent;
    public Quaternion Orientation;

    public MyOrientedBoundingBoxD(MatrixD matrix)
    {
      this.Center = matrix.Translation;
      Vector3D vector3D = new Vector3D(matrix.Right.Length(), matrix.Up.Length(), matrix.Forward.Length());
      this.HalfExtent = vector3D / 2.0;
      matrix.Right /= vector3D.X;
      matrix.Up /= vector3D.Y;
      matrix.Forward /= vector3D.Z;
      Quaternion.CreateFromRotationMatrix(ref matrix, out this.Orientation);
    }

    public MyOrientedBoundingBoxD(Vector3D center, Vector3D halfExtents, Quaternion orientation)
    {
      this.Center = center;
      this.HalfExtent = halfExtents;
      this.Orientation = orientation;
    }

    public MyOrientedBoundingBoxD(BoundingBoxD box, MatrixD transform)
    {
      this.Center = (box.Min + box.Max) * 0.5;
      this.HalfExtent = (box.Max - box.Min) * 0.5;
      this.Center = Vector3D.Transform(this.Center, transform);
      this.Orientation = Quaternion.CreateFromRotationMatrix(transform);
    }

    public static bool operator ==(MyOrientedBoundingBoxD a, MyOrientedBoundingBoxD b)
    {
      return object.Equals((object) a, (object) b);
    }

    public static bool operator !=(MyOrientedBoundingBoxD a, MyOrientedBoundingBoxD b)
    {
      return !object.Equals((object) a, (object) b);
    }

    public static bool GetNormalBetweenEdges(int axis, int edge0, int edge1, out Vector3 normal)
    {
      normal = Vector3.Zero;
      Vector3[] vector3Array1;
      Vector3[] vector3Array2;
      switch (axis)
      {
        case 0:
          int[] numArray1 = MyOrientedBoundingBoxD.StartXVertices;
          int[] numArray2 = MyOrientedBoundingBoxD.EndXVertices;
          vector3Array1 = MyOrientedBoundingBoxD.XNeighbourVectorsForw;
          vector3Array2 = MyOrientedBoundingBoxD.XNeighbourVectorsBack;
          break;
        case 1:
          int[] numArray3 = MyOrientedBoundingBoxD.StartYVertices;
          int[] numArray4 = MyOrientedBoundingBoxD.EndYVertices;
          vector3Array1 = MyOrientedBoundingBoxD.YNeighbourVectorsForw;
          vector3Array2 = MyOrientedBoundingBoxD.YNeighbourVectorsBack;
          break;
        case 2:
          int[] numArray5 = MyOrientedBoundingBoxD.StartZVertices;
          int[] numArray6 = MyOrientedBoundingBoxD.EndZVertices;
          vector3Array1 = MyOrientedBoundingBoxD.ZNeighbourVectorsForw;
          vector3Array2 = MyOrientedBoundingBoxD.ZNeighbourVectorsBack;
          break;
        default:
          return false;
      }
      if (edge0 == -1)
        edge0 = 3;
      if (edge0 == 4)
        edge0 = 0;
      if (edge1 == -1)
        edge1 = 3;
      if (edge1 == 4)
        edge1 = 0;
      if (edge0 == 3 && edge1 == 0)
      {
        normal = vector3Array1[3];
        return true;
      }
      else if (edge0 == 0 && edge1 == 3)
      {
        normal = vector3Array2[3];
        return true;
      }
      else if (edge0 + 1 == edge1)
      {
        normal = vector3Array1[edge0];
        return true;
      }
      else
      {
        if (edge0 != edge1 + 1)
          return false;
        normal = vector3Array2[edge1];
        return true;
      }
    }

    public static MyOrientedBoundingBoxD CreateFromBoundingBox(BoundingBoxD box)
    {
      return new MyOrientedBoundingBoxD((Vector3D) (Vector3) ((box.Min + box.Max) * 0.5), (Vector3D) (Vector3) ((box.Max - box.Min) * 0.5), Quaternion.Identity);
    }

    public MyOrientedBoundingBoxD Transform(Quaternion rotation, Vector3D translation)
    {
      return new MyOrientedBoundingBoxD(Vector3D.Transform(this.Center, rotation) + translation, this.HalfExtent, this.Orientation * rotation);
    }

    public MyOrientedBoundingBoxD Transform(float scale, Quaternion rotation, Vector3D translation)
    {
      return new MyOrientedBoundingBoxD(Vector3.Transform((Vector3) (this.Center * (double) scale), rotation) + translation, this.HalfExtent * (double) scale, this.Orientation * rotation);
    }

    public void Transform(MatrixD matrix)
    {
      this.Center = Vector3D.Transform(this.Center, matrix);
      this.Orientation = Quaternion.CreateFromRotationMatrix(MatrixD.CreateFromQuaternion(this.Orientation) * matrix);
    }

    public bool Equals(MyOrientedBoundingBox other)
    {
      if (this.Center == other.Center && this.HalfExtent == other.HalfExtent)
        return this.Orientation == other.Orientation;
      else
        return false;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is MyOrientedBoundingBox))
        return false;
      MyOrientedBoundingBox orientedBoundingBox = (MyOrientedBoundingBox) obj;
      if (this.Center == orientedBoundingBox.Center && this.HalfExtent == orientedBoundingBox.HalfExtent)
        return this.Orientation == orientedBoundingBox.Orientation;
      else
        return false;
    }

    public override int GetHashCode()
    {
      return this.Center.GetHashCode() ^ this.HalfExtent.GetHashCode() ^ this.Orientation.GetHashCode();
    }

    public override string ToString()
    {
      return "{Center:" + this.Center.ToString() + " Extents:" + this.HalfExtent.ToString() + " Orientation:" + this.Orientation.ToString() + "}";
    }

    public bool Intersects(ref BoundingBox box)
    {
      Vector3D vector3D = (Vector3D) ((box.Max + box.Min) * 0.5f);
      Vector3D hA = (Vector3D) ((box.Max - box.Min) * 0.5f);
      MatrixD fromQuaternion = MatrixD.CreateFromQuaternion(this.Orientation);
      fromQuaternion.Translation = this.Center - vector3D;
      return MyOrientedBoundingBoxD.ContainsRelativeBox(ref hA, ref this.HalfExtent, ref fromQuaternion) != ContainmentType.Disjoint;
    }

    public bool Intersects(ref BoundingBoxD box)
    {
      Vector3D vector3D = (box.Max + box.Min) * 0.5;
      Vector3D hA = (box.Max - box.Min) * 0.5;
      MatrixD fromQuaternion = MatrixD.CreateFromQuaternion(this.Orientation);
      fromQuaternion.Translation = this.Center - vector3D;
      return MyOrientedBoundingBoxD.ContainsRelativeBox(ref hA, ref this.HalfExtent, ref fromQuaternion) != ContainmentType.Disjoint;
    }

    public ContainmentType Contains(ref BoundingBox box)
    {
      Vector3D vector3D = (Vector3D) ((box.Max + box.Min) * 0.5f);
      Vector3D hB = (Vector3D) ((box.Max - box.Min) * 0.5f);
      Quaternion result;
      Quaternion.Conjugate(ref this.Orientation, out result);
      MatrixD fromQuaternion = MatrixD.CreateFromQuaternion(result);
      fromQuaternion.Translation = Vector3D.TransformNormal(vector3D - this.Center, fromQuaternion);
      return MyOrientedBoundingBoxD.ContainsRelativeBox(ref this.HalfExtent, ref hB, ref fromQuaternion);
    }

    public static ContainmentType Contains(ref BoundingBox boxA, ref MyOrientedBoundingBox oboxB)
    {
      Vector3 hA = (boxA.Max - boxA.Min) * 0.5f;
      Vector3 vector3 = (boxA.Max + boxA.Min) * 0.5f;
      Matrix fromQuaternion = Matrix.CreateFromQuaternion(oboxB.Orientation);
      fromQuaternion.Translation = oboxB.Center - vector3;
      return MyOrientedBoundingBox.ContainsRelativeBox(ref hA, ref oboxB.HalfExtent, ref fromQuaternion);
    }

    public bool Intersects(ref MyOrientedBoundingBoxD other)
    {
      return this.Contains(ref other) != ContainmentType.Disjoint;
    }

    public ContainmentType Contains(ref MyOrientedBoundingBoxD other)
    {
      Quaternion result1;
      Quaternion.Conjugate(ref this.Orientation, out result1);
      Quaternion result2;
      Quaternion.Multiply(ref result1, ref other.Orientation, out result2);
      MatrixD fromQuaternion = MatrixD.CreateFromQuaternion(result2);
      fromQuaternion.Translation = Vector3D.Transform(other.Center - this.Center, result1);
      return MyOrientedBoundingBoxD.ContainsRelativeBox(ref this.HalfExtent, ref other.HalfExtent, ref fromQuaternion);
    }

    public ContainmentType Contains(BoundingFrustumD frustum)
    {
      return this.ConvertToFrustum().Contains(frustum);
    }

    public bool Intersects(BoundingFrustumD frustum)
    {
      return this.Contains(frustum) != ContainmentType.Disjoint;
    }

    public static ContainmentType Contains(BoundingFrustumD frustum, ref MyOrientedBoundingBoxD obox)
    {
      return frustum.Contains(obox.ConvertToFrustum());
    }

    public ContainmentType Contains(ref BoundingSphereD sphere)
    {
      Quaternion rotation = Quaternion.Conjugate(this.Orientation);
      Vector3 vector3 = Vector3.Transform((Vector3) (sphere.Center - this.Center), rotation);
      double val1_1 = (double) Math.Abs(vector3.X) - this.HalfExtent.X;
      double val1_2 = (double) Math.Abs(vector3.Y) - this.HalfExtent.Y;
      double val1_3 = (double) Math.Abs(vector3.Z) - this.HalfExtent.Z;
      double num1 = sphere.Radius;
      if (val1_1 <= -num1 && val1_2 <= -num1 && val1_3 <= -num1)
        return ContainmentType.Contains;
      double num2 = Math.Max(val1_1, 0.0);
      double num3 = Math.Max(val1_2, 0.0);
      double num4 = Math.Max(val1_3, 0.0);
      return num2 * num2 + num3 * num3 + num4 * num4 >= num1 * num1 ? ContainmentType.Disjoint : ContainmentType.Intersects;
    }

    public bool Intersects(ref BoundingSphereD sphere)
    {
      Quaternion rotation = Quaternion.Conjugate(this.Orientation);
      Vector3 vector3 = Vector3.Transform((Vector3) (sphere.Center - this.Center), rotation);
      double val1_1 = (double) Math.Abs(vector3.X) - this.HalfExtent.X;
      double val1_2 = (double) Math.Abs(vector3.Y) - this.HalfExtent.Y;
      double val1_3 = (double) Math.Abs(vector3.Z) - this.HalfExtent.Z;
      double num1 = Math.Max(val1_1, 0.0);
      double num2 = Math.Max(val1_2, 0.0);
      double num3 = Math.Max(val1_3, 0.0);
      double num4 = sphere.Radius;
      return num1 * num1 + num2 * num2 + num3 * num3 < num4 * num4;
    }

    public static ContainmentType Contains(ref BoundingSphere sphere, ref MyOrientedBoundingBox box)
    {
      Quaternion rotation = Quaternion.Conjugate(box.Orientation);
      Vector3 vector3_1 = Vector3.Transform(sphere.Center - box.Center, rotation);
      vector3_1.X = Math.Abs(vector3_1.X);
      vector3_1.Y = Math.Abs(vector3_1.Y);
      vector3_1.Z = Math.Abs(vector3_1.Z);
      float num = sphere.Radius * sphere.Radius;
      if ((double) (vector3_1 + box.HalfExtent).LengthSquared() <= (double) num)
        return ContainmentType.Contains;
      Vector3 vector3_2 = vector3_1 - box.HalfExtent;
      vector3_2.X = Math.Max(vector3_2.X, 0.0f);
      vector3_2.Y = Math.Max(vector3_2.Y, 0.0f);
      vector3_2.Z = Math.Max(vector3_2.Z, 0.0f);
      return (double) vector3_2.LengthSquared() >= (double) num ? ContainmentType.Disjoint : ContainmentType.Intersects;
    }

    public bool Contains(ref Vector3 point)
    {
      Quaternion rotation = Quaternion.Conjugate(this.Orientation);
      Vector3 vector3 = Vector3.Transform((Vector3) (point - this.Center), rotation);
      if ((double) Math.Abs(vector3.X) <= this.HalfExtent.X && (double) Math.Abs(vector3.Y) <= this.HalfExtent.Y)
        return (double) Math.Abs(vector3.Z) <= this.HalfExtent.Z;
      else
        return false;
    }

    public bool Contains(ref Vector3D point)
    {
      Quaternion rotation = Quaternion.Conjugate(this.Orientation);
      Vector3D vector3D = Vector3D.Transform(point - this.Center, rotation);
      if (Math.Abs(vector3D.X) <= this.HalfExtent.X && Math.Abs(vector3D.Y) <= this.HalfExtent.Y)
        return Math.Abs(vector3D.Z) <= this.HalfExtent.Z;
      else
        return false;
    }

    public double? Intersects(ref RayD ray)
    {
      MatrixD matrixD = (MatrixD) Matrix.CreateFromQuaternion(this.Orientation);
      Vector3D vector2 = this.Center - ray.Position;
      double num1 = double.MinValue;
      double num2 = double.MaxValue;
      double num3 = Vector3D.Dot(matrixD.Right, vector2);
      double num4 = Vector3D.Dot(matrixD.Right, ray.Direction);
      if (num4 >= -9.99999968265523E-21 && num4 <= 9.99999968265523E-21)
      {
        if (-num3 - this.HalfExtent.X > 0.0 || -num3 + this.HalfExtent.X < 0.0)
          return new double?();
      }
      else
      {
        double num5 = (num3 - this.HalfExtent.X) / num4;
        double num6 = (num3 + this.HalfExtent.X) / num4;
        if (num5 > num6)
        {
          double num7 = num5;
          num5 = num6;
          num6 = num7;
        }
        if (num5 > num1)
          num1 = num5;
        if (num6 < num2)
          num2 = num6;
        if (num2 < 0.0 || num1 > num2)
          return new double?();
      }
      double num8 = (double) Vector3.Dot((Vector3) matrixD.Up, (Vector3) vector2);
      double num9 = (double) Vector3.Dot((Vector3) matrixD.Up, (Vector3) ray.Direction);
      if (num9 >= -9.99999968265523E-21 && num9 <= 9.99999968265523E-21)
      {
        if (-num8 - this.HalfExtent.Y > 0.0 || -num8 + this.HalfExtent.Y < 0.0)
          return new double?();
      }
      else
      {
        double num5 = (num8 - this.HalfExtent.Y) / num9;
        double num6 = (num8 + this.HalfExtent.Y) / num9;
        if (num5 > num6)
        {
          double num7 = num5;
          num5 = num6;
          num6 = num7;
        }
        if (num5 > num1)
          num1 = num5;
        if (num6 < num2)
          num2 = num6;
        if (num2 < 0.0 || num1 > num2)
          return new double?();
      }
      double num10 = (double) Vector3.Dot((Vector3) matrixD.Forward, (Vector3) vector2);
      double num11 = (double) Vector3.Dot((Vector3) matrixD.Forward, (Vector3) ray.Direction);
      if (num11 >= -9.99999968265523E-21 && num11 <= 9.99999968265523E-21)
      {
        if (-num10 - this.HalfExtent.Z > 0.0 || -num10 + this.HalfExtent.Z < 0.0)
          return new double?();
      }
      else
      {
        double num5 = (num10 - this.HalfExtent.Z) / num11;
        double num6 = (num10 + this.HalfExtent.Z) / num11;
        if (num5 > num6)
        {
          double num7 = num5;
          num5 = num6;
          num6 = num7;
        }
        if (num5 > num1)
          num1 = num5;
        if (num6 < num2)
          num2 = num6;
        if (num2 < 0.0 || num1 > num2)
          return new double?();
      }
      return new double?(num1);
    }

    public double? Intersects(ref LineD line)
    {
      if (this.Contains(ref line.From))
      {
        RayD ray = new RayD(line.To, -line.Direction);
        double? nullable = this.Intersects(ref ray);
        if (!nullable.HasValue)
          return new double?();
        double num = line.Length - nullable.Value;
        if (num < 0.0)
          return new double?();
        if (num > line.Length)
          return new double?();
        else
          return new double?(num);
      }
      else
      {
        RayD ray = new RayD(line.From, line.Direction);
        double? nullable = this.Intersects(ref ray);
        if (!nullable.HasValue)
          return new double?();
        if (nullable.Value < 0.0)
          return new double?();
        if (nullable.Value > line.Length)
          return new double?();
        else
          return new double?(nullable.Value);
      }
    }

    public PlaneIntersectionType Intersects(ref PlaneD plane)
    {
      double num1 = plane.DotCoordinate(this.Center);
      Vector3D vector3D = Vector3D.Transform(plane.Normal, Quaternion.Conjugate(this.Orientation));
      double num2 = Math.Abs(this.HalfExtent.X * vector3D.X) + Math.Abs(this.HalfExtent.Y * vector3D.Y) + Math.Abs(this.HalfExtent.Z * vector3D.Z);
      if (num1 > num2)
        return PlaneIntersectionType.Front;
      return num1 < -num2 ? PlaneIntersectionType.Back : PlaneIntersectionType.Intersecting;
    }

    public void GetCorners(Vector3D[] corners, int startIndex)
    {
      MatrixD fromQuaternion = MatrixD.CreateFromQuaternion(this.Orientation);
      Vector3D vector3D1 = fromQuaternion.Left * this.HalfExtent.X;
      Vector3D vector3D2 = fromQuaternion.Up * this.HalfExtent.Y;
      Vector3D vector3D3 = fromQuaternion.Backward * this.HalfExtent.Z;
      int num1 = startIndex;
      Vector3D[] vector3DArray1 = corners;
      int index1 = num1;
      int num2 = 1;
      int num3 = index1 + num2;
      vector3DArray1[index1] = this.Center - vector3D1 + vector3D2 + vector3D3;
      Vector3D[] vector3DArray2 = corners;
      int index2 = num3;
      int num4 = 1;
      int num5 = index2 + num4;
      vector3DArray2[index2] = this.Center + vector3D1 + vector3D2 + vector3D3;
      Vector3D[] vector3DArray3 = corners;
      int index3 = num5;
      int num6 = 1;
      int num7 = index3 + num6;
      vector3DArray3[index3] = this.Center + vector3D1 - vector3D2 + vector3D3;
      Vector3D[] vector3DArray4 = corners;
      int index4 = num7;
      int num8 = 1;
      int num9 = index4 + num8;
      vector3DArray4[index4] = this.Center - vector3D1 - vector3D2 + vector3D3;
      Vector3D[] vector3DArray5 = corners;
      int index5 = num9;
      int num10 = 1;
      int num11 = index5 + num10;
      vector3DArray5[index5] = this.Center - vector3D1 + vector3D2 - vector3D3;
      Vector3D[] vector3DArray6 = corners;
      int index6 = num11;
      int num12 = 1;
      int num13 = index6 + num12;
      vector3DArray6[index6] = this.Center + vector3D1 + vector3D2 - vector3D3;
      Vector3D[] vector3DArray7 = corners;
      int index7 = num13;
      int num14 = 1;
      int num15 = index7 + num14;
      vector3DArray7[index7] = this.Center + vector3D1 - vector3D2 - vector3D3;
      Vector3D[] vector3DArray8 = corners;
      int index8 = num15;
      int num16 = 1;
      int num17 = index8 + num16;
      vector3DArray8[index8] = this.Center - vector3D1 - vector3D2 - vector3D3;
    }

    public static ContainmentType ContainsRelativeBox(ref Vector3D hA, ref Vector3D hB, ref MatrixD mB)
    {
      Vector3D translation = mB.Translation;
      Vector3D vector3D1 = new Vector3D(Math.Abs(translation.X), Math.Abs(translation.Y), Math.Abs(translation.Z));
      Vector3D right = mB.Right;
      Vector3D up = mB.Up;
      Vector3D backward = mB.Backward;
      Vector3D vector3D2 = right * hB.X;
      Vector3D vector3D3 = up * hB.Y;
      Vector3D vector3D4 = backward * hB.Z;
      double num1 = Math.Abs(vector3D2.X) + Math.Abs(vector3D3.X) + Math.Abs(vector3D4.X);
      double num2 = Math.Abs(vector3D2.Y) + Math.Abs(vector3D3.Y) + Math.Abs(vector3D4.Y);
      double num3 = Math.Abs(vector3D2.Z) + Math.Abs(vector3D3.Z) + Math.Abs(vector3D4.Z);
      if (vector3D1.X + num1 <= hA.X && vector3D1.Y + num2 <= hA.Y && vector3D1.Z + num3 <= hA.Z)
        return ContainmentType.Contains;
      if (vector3D1.X >= hA.X + Math.Abs(vector3D2.X) + Math.Abs(vector3D3.X) + Math.Abs(vector3D4.X) || vector3D1.Y >= hA.Y + Math.Abs(vector3D2.Y) + Math.Abs(vector3D3.Y) + Math.Abs(vector3D4.Y) || (vector3D1.Z >= hA.Z + Math.Abs(vector3D2.Z) + Math.Abs(vector3D3.Z) + Math.Abs(vector3D4.Z) || (double) Math.Abs(Vector3.Dot((Vector3) translation, (Vector3) right)) >= Math.Abs(hA.X * right.X) + Math.Abs(hA.Y * right.Y) + Math.Abs(hA.Z * right.Z) + hB.X) || ((double) Math.Abs(Vector3.Dot((Vector3) translation, (Vector3) up)) >= Math.Abs(hA.X * up.X) + Math.Abs(hA.Y * up.Y) + Math.Abs(hA.Z * up.Z) + hB.Y || (double) Math.Abs(Vector3.Dot((Vector3) translation, (Vector3) backward)) >= Math.Abs(hA.X * backward.X) + Math.Abs(hA.Y * backward.Y) + Math.Abs(hA.Z * backward.Z) + hB.Z))
        return ContainmentType.Disjoint;
      Vector3 vector3_1 = (Vector3) new Vector3D(0.0, -right.Z, right.Y);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_1)) >= Math.Abs(hA.Y * (double) vector3_1.Y) + Math.Abs(hA.Z * (double) vector3_1.Z) + (double) Math.Abs(Vector3.Dot(vector3_1, (Vector3) vector3D3)) + (double) Math.Abs(Vector3.Dot(vector3_1, (Vector3) vector3D4)))
        return ContainmentType.Disjoint;
      Vector3 vector3_2 = (Vector3) new Vector3D(0.0, -up.Z, up.Y);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_2)) >= Math.Abs(hA.Y * (double) vector3_2.Y) + Math.Abs(hA.Z * (double) vector3_2.Z) + (double) Math.Abs(Vector3.Dot(vector3_2, (Vector3) vector3D4)) + (double) Math.Abs(Vector3.Dot(vector3_2, (Vector3) vector3D2)))
        return ContainmentType.Disjoint;
      Vector3 vector3_3 = (Vector3) new Vector3D(0.0, -backward.Z, backward.Y);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_3)) >= Math.Abs(hA.Y * (double) vector3_3.Y) + Math.Abs(hA.Z * (double) vector3_3.Z) + (double) Math.Abs(Vector3.Dot(vector3_3, (Vector3) vector3D2)) + (double) Math.Abs(Vector3.Dot(vector3_3, (Vector3) vector3D3)))
        return ContainmentType.Disjoint;
      Vector3 vector3_4 = (Vector3) new Vector3D(right.Z, 0.0, -right.X);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_4)) >= Math.Abs(hA.Z * (double) vector3_4.Z) + Math.Abs(hA.X * (double) vector3_4.X) + (double) Math.Abs(Vector3.Dot(vector3_4, (Vector3) vector3D3)) + (double) Math.Abs(Vector3.Dot(vector3_4, (Vector3) vector3D4)))
        return ContainmentType.Disjoint;
      Vector3 vector3_5 = (Vector3) new Vector3D(up.Z, 0.0, -up.X);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_5)) >= Math.Abs(hA.Z * (double) vector3_5.Z) + Math.Abs(hA.X * (double) vector3_5.X) + (double) Math.Abs(Vector3.Dot(vector3_5, (Vector3) vector3D4)) + (double) Math.Abs(Vector3.Dot(vector3_5, (Vector3) vector3D2)))
        return ContainmentType.Disjoint;
      Vector3 vector3_6 = (Vector3) new Vector3D(backward.Z, 0.0, -backward.X);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_6)) >= Math.Abs(hA.Z * (double) vector3_6.Z) + Math.Abs(hA.X * (double) vector3_6.X) + (double) Math.Abs(Vector3.Dot(vector3_6, (Vector3) vector3D2)) + (double) Math.Abs(Vector3.Dot(vector3_6, (Vector3) vector3D3)))
        return ContainmentType.Disjoint;
      Vector3 vector3_7 = (Vector3) new Vector3D(-right.Y, right.X, 0.0);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_7)) >= Math.Abs(hA.X * (double) vector3_7.X) + Math.Abs(hA.Y * (double) vector3_7.Y) + (double) Math.Abs(Vector3.Dot(vector3_7, (Vector3) vector3D3)) + (double) Math.Abs(Vector3.Dot(vector3_7, (Vector3) vector3D4)))
        return ContainmentType.Disjoint;
      Vector3 vector3_8 = (Vector3) new Vector3D(-up.Y, up.X, 0.0);
      if ((double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_8)) >= Math.Abs(hA.X * (double) vector3_8.X) + Math.Abs(hA.Y * (double) vector3_8.Y) + (double) Math.Abs(Vector3.Dot(vector3_8, (Vector3) vector3D4)) + (double) Math.Abs(Vector3.Dot(vector3_8, (Vector3) vector3D2)))
        return ContainmentType.Disjoint;
      Vector3 vector3_9 = (Vector3) new Vector3D(-backward.Y, backward.X, 0.0);
      return (double) Math.Abs(Vector3.Dot((Vector3) translation, vector3_9)) >= Math.Abs(hA.X * (double) vector3_9.X) + Math.Abs(hA.Y * (double) vector3_9.Y) + (double) Math.Abs(Vector3.Dot(vector3_9, (Vector3) vector3D2)) + (double) Math.Abs(Vector3.Dot(vector3_9, (Vector3) vector3D3)) ? ContainmentType.Disjoint : ContainmentType.Intersects;
    }

    public BoundingFrustumD ConvertToFrustum()
    {
      Quaternion result1;
      Quaternion.Conjugate(ref this.Orientation, out result1);
      double num1 = 1.0 / this.HalfExtent.X;
      double num2 = 1.0 / this.HalfExtent.Y;
      double num3 = 0.5 / this.HalfExtent.Z;
      MatrixD result2;
      MatrixD.CreateFromQuaternion(ref result1, out result2);
      result2.M11 *= num1;
      result2.M21 *= num1;
      result2.M31 *= num1;
      result2.M12 *= num2;
      result2.M22 *= num2;
      result2.M32 *= num2;
      result2.M13 *= num3;
      result2.M23 *= num3;
      result2.M33 *= num3;
      result2.Translation = Vector3.UnitZ * 0.5f + Vector3D.TransformNormal(-this.Center, result2);
      return new BoundingFrustumD(result2);
    }

    public BoundingBoxD GetAABB()
    {
      BoundingBoxD invalid = BoundingBoxD.CreateInvalid();
      BoundingFrustumD frustum = this.ConvertToFrustum();
      invalid.Include(ref frustum);
      return invalid;
    }

    public static MyOrientedBoundingBoxD Create(BoundingBoxD boundingBox, MatrixD matrix)
    {
      MyOrientedBoundingBoxD fromBoundingBox = MyOrientedBoundingBoxD.CreateFromBoundingBox(new BoundingBoxD(-boundingBox.Size() / 2.0, boundingBox.Size() / 2.0));
      fromBoundingBox.Transform(matrix);
      return fromBoundingBox;
    }
  }
}
