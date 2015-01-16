// Decompiled with JetBrains decompiler
// Type: VRageMath.MatrixD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace VRageMath
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct MatrixD : IEquatable<MatrixD>
    {
        public static MatrixD Identity = new MatrixD(1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0,
            0.0, 0.0, 1.0);

        public static MatrixD Zero = new MatrixD(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0,
            0.0, 0.0);

        [FieldOffset(0)] private MatrixD.F16 M;
        [FieldOffset(0)] public double M11;
        [FieldOffset(8)] public double M12;
        [FieldOffset(16)] public double M13;
        [FieldOffset(24)] public double M14;
        [FieldOffset(32)] public double M21;
        [FieldOffset(40)] public double M22;
        [FieldOffset(48)] public double M23;
        [FieldOffset(56)] public double M24;
        [FieldOffset(64)] public double M31;
        [FieldOffset(72)] public double M32;
        [FieldOffset(80)] public double M33;
        [FieldOffset(88)] public double M34;
        [FieldOffset(96)] public double M41;
        [FieldOffset(104)] public double M42;
        [FieldOffset(112)] public double M43;
        [FieldOffset(120)] public double M44;

        public Vector3D Up
        {
            get
            {
                Vector3D vector3D;
                vector3D.X = this.M21;
                vector3D.Y = this.M22;
                vector3D.Z = this.M23;
                return vector3D;
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }

        public Vector3D Down
        {
            get
            {
                Vector3D vector3D;
                vector3D.X = -this.M21;
                vector3D.Y = -this.M22;
                vector3D.Z = -this.M23;
                return vector3D;
            }
            set
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }

        public Vector3D Right
        {
            get
            {
                Vector3D vector3D;
                vector3D.X = this.M11;
                vector3D.Y = this.M12;
                vector3D.Z = this.M13;
                return vector3D;
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }

        public Vector3D Left
        {
            get
            {
                Vector3D vector3D;
                vector3D.X = -this.M11;
                vector3D.Y = -this.M12;
                vector3D.Z = -this.M13;
                return vector3D;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }

        public Vector3D Forward
        {
            get
            {
                Vector3D vector3D;
                vector3D.X = -this.M31;
                vector3D.Y = -this.M32;
                vector3D.Z = -this.M33;
                return vector3D;
            }
            set
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }

        public Vector3D Backward
        {
            get
            {
                Vector3D vector3D;
                vector3D.X = this.M31;
                vector3D.Y = this.M32;
                vector3D.Z = this.M33;
                return vector3D;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }

        public Vector3D Scale
        {
            get { return new Vector3D(this.Right.Length(), this.Up.Length(), this.Forward.Length()); }
        }

        public Vector3D Translation
        {
            get
            {
                Vector3D vector3D;
                vector3D.X = this.M41;
                vector3D.Y = this.M42;
                vector3D.Z = this.M43;
                return vector3D;
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }

        public unsafe double this[int row, int column]
        {
            get { return 0; }
            set { }
        }

        public MatrixD(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24,
            double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;
            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        public MatrixD(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32,
            double m33)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = 0.0;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = 0.0;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = 0.0;
            this.M41 = 0.0;
            this.M42 = 0.0;
            this.M43 = 0.0;
            this.M44 = 1.0;
        }

        public MatrixD(Matrix m)
        {
            this.M11 = (double) m.M11;
            this.M12 = (double) m.M12;
            this.M13 = (double) m.M13;
            this.M14 = (double) m.M14;
            this.M21 = (double) m.M21;
            this.M22 = (double) m.M22;
            this.M23 = (double) m.M23;
            this.M24 = (double) m.M24;
            this.M31 = (double) m.M31;
            this.M32 = (double) m.M32;
            this.M33 = (double) m.M33;
            this.M34 = (double) m.M34;
            this.M41 = (double) m.M41;
            this.M42 = (double) m.M42;
            this.M43 = (double) m.M43;
            this.M44 = (double) m.M44;
        }

        public static implicit operator Matrix(MatrixD m)
        {
            return new Matrix((float) m.M11, (float) m.M12, (float) m.M13, (float) m.M14, (float) m.M21, (float) m.M22,
                (float) m.M23, (float) m.M24, (float) m.M31, (float) m.M32, (float) m.M33, (float) m.M34, (float) m.M41,
                (float) m.M42, (float) m.M43, (float) m.M44);
        }

        public static implicit operator MatrixD(Matrix m)
        {
            return new MatrixD((double) m.M11, (double) m.M12, (double) m.M13, (double) m.M14, (double) m.M21,
                (double) m.M22, (double) m.M23, (double) m.M24, (double) m.M31, (double) m.M32, (double) m.M33,
                (double) m.M34, (double) m.M41, (double) m.M42, (double) m.M43, (double) m.M44);
        }

        public static MatrixD operator -(MatrixD matrix1)
        {
            MatrixD matrixD;
            matrixD.M11 = -matrix1.M11;
            matrixD.M12 = -matrix1.M12;
            matrixD.M13 = -matrix1.M13;
            matrixD.M14 = -matrix1.M14;
            matrixD.M21 = -matrix1.M21;
            matrixD.M22 = -matrix1.M22;
            matrixD.M23 = -matrix1.M23;
            matrixD.M24 = -matrix1.M24;
            matrixD.M31 = -matrix1.M31;
            matrixD.M32 = -matrix1.M32;
            matrixD.M33 = -matrix1.M33;
            matrixD.M34 = -matrix1.M34;
            matrixD.M41 = -matrix1.M41;
            matrixD.M42 = -matrix1.M42;
            matrixD.M43 = -matrix1.M43;
            matrixD.M44 = -matrix1.M44;
            return matrixD;
        }

        public static bool operator ==(MatrixD matrix1, MatrixD matrix2)
        {
            if (matrix1.M11 == matrix2.M11 && matrix1.M22 == matrix2.M22 &&
                (matrix1.M33 == matrix2.M33 && matrix1.M44 == matrix2.M44) &&
                (matrix1.M12 == matrix2.M12 && matrix1.M13 == matrix2.M13 &&
                 (matrix1.M14 == matrix2.M14 && matrix1.M21 == matrix2.M21)) &&
                (matrix1.M23 == matrix2.M23 && matrix1.M24 == matrix2.M24 &&
                 (matrix1.M31 == matrix2.M31 && matrix1.M32 == matrix2.M32) &&
                 (matrix1.M34 == matrix2.M34 && matrix1.M41 == matrix2.M41 && matrix1.M42 == matrix2.M42)))
                return matrix1.M43 == matrix2.M43;
            else
                return false;
        }

        public static bool operator !=(MatrixD matrix1, MatrixD matrix2)
        {
            if (matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 &&
                (matrix1.M13 == matrix2.M13 && matrix1.M14 == matrix2.M14) &&
                (matrix1.M21 == matrix2.M21 && matrix1.M22 == matrix2.M22 &&
                 (matrix1.M23 == matrix2.M23 && matrix1.M24 == matrix2.M24)) &&
                (matrix1.M31 == matrix2.M31 && matrix1.M32 == matrix2.M32 &&
                 (matrix1.M33 == matrix2.M33 && matrix1.M34 == matrix2.M34) &&
                 (matrix1.M41 == matrix2.M41 && matrix1.M42 == matrix2.M42 && matrix1.M43 == matrix2.M43)))
                return matrix1.M44 != matrix2.M44;
            else
                return true;
        }

        public static MatrixD operator +(MatrixD matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11 + matrix2.M11;
            matrixD.M12 = matrix1.M12 + matrix2.M12;
            matrixD.M13 = matrix1.M13 + matrix2.M13;
            matrixD.M14 = matrix1.M14 + matrix2.M14;
            matrixD.M21 = matrix1.M21 + matrix2.M21;
            matrixD.M22 = matrix1.M22 + matrix2.M22;
            matrixD.M23 = matrix1.M23 + matrix2.M23;
            matrixD.M24 = matrix1.M24 + matrix2.M24;
            matrixD.M31 = matrix1.M31 + matrix2.M31;
            matrixD.M32 = matrix1.M32 + matrix2.M32;
            matrixD.M33 = matrix1.M33 + matrix2.M33;
            matrixD.M34 = matrix1.M34 + matrix2.M34;
            matrixD.M41 = matrix1.M41 + matrix2.M41;
            matrixD.M42 = matrix1.M42 + matrix2.M42;
            matrixD.M43 = matrix1.M43 + matrix2.M43;
            matrixD.M44 = matrix1.M44 + matrix2.M44;
            return matrixD;
        }

        public static MatrixD operator -(MatrixD matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11 - matrix2.M11;
            matrixD.M12 = matrix1.M12 - matrix2.M12;
            matrixD.M13 = matrix1.M13 - matrix2.M13;
            matrixD.M14 = matrix1.M14 - matrix2.M14;
            matrixD.M21 = matrix1.M21 - matrix2.M21;
            matrixD.M22 = matrix1.M22 - matrix2.M22;
            matrixD.M23 = matrix1.M23 - matrix2.M23;
            matrixD.M24 = matrix1.M24 - matrix2.M24;
            matrixD.M31 = matrix1.M31 - matrix2.M31;
            matrixD.M32 = matrix1.M32 - matrix2.M32;
            matrixD.M33 = matrix1.M33 - matrix2.M33;
            matrixD.M34 = matrix1.M34 - matrix2.M34;
            matrixD.M41 = matrix1.M41 - matrix2.M41;
            matrixD.M42 = matrix1.M42 - matrix2.M42;
            matrixD.M43 = matrix1.M43 - matrix2.M43;
            matrixD.M44 = matrix1.M44 - matrix2.M44;
            return matrixD;
        }

        public static MatrixD operator *(MatrixD matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11*matrix2.M11 + matrix1.M12*matrix2.M21 + matrix1.M13*matrix2.M31 +
                          matrix1.M14*matrix2.M41;
            matrixD.M12 = matrix1.M11*matrix2.M12 + matrix1.M12*matrix2.M22 + matrix1.M13*matrix2.M32 +
                          matrix1.M14*matrix2.M42;
            matrixD.M13 = matrix1.M11*matrix2.M13 + matrix1.M12*matrix2.M23 + matrix1.M13*matrix2.M33 +
                          matrix1.M14*matrix2.M43;
            matrixD.M14 = matrix1.M11*matrix2.M14 + matrix1.M12*matrix2.M24 + matrix1.M13*matrix2.M34 +
                          matrix1.M14*matrix2.M44;
            matrixD.M21 = matrix1.M21*matrix2.M11 + matrix1.M22*matrix2.M21 + matrix1.M23*matrix2.M31 +
                          matrix1.M24*matrix2.M41;
            matrixD.M22 = matrix1.M21*matrix2.M12 + matrix1.M22*matrix2.M22 + matrix1.M23*matrix2.M32 +
                          matrix1.M24*matrix2.M42;
            matrixD.M23 = matrix1.M21*matrix2.M13 + matrix1.M22*matrix2.M23 + matrix1.M23*matrix2.M33 +
                          matrix1.M24*matrix2.M43;
            matrixD.M24 = matrix1.M21*matrix2.M14 + matrix1.M22*matrix2.M24 + matrix1.M23*matrix2.M34 +
                          matrix1.M24*matrix2.M44;
            matrixD.M31 = matrix1.M31*matrix2.M11 + matrix1.M32*matrix2.M21 + matrix1.M33*matrix2.M31 +
                          matrix1.M34*matrix2.M41;
            matrixD.M32 = matrix1.M31*matrix2.M12 + matrix1.M32*matrix2.M22 + matrix1.M33*matrix2.M32 +
                          matrix1.M34*matrix2.M42;
            matrixD.M33 = matrix1.M31*matrix2.M13 + matrix1.M32*matrix2.M23 + matrix1.M33*matrix2.M33 +
                          matrix1.M34*matrix2.M43;
            matrixD.M34 = matrix1.M31*matrix2.M14 + matrix1.M32*matrix2.M24 + matrix1.M33*matrix2.M34 +
                          matrix1.M34*matrix2.M44;
            matrixD.M41 = matrix1.M41*matrix2.M11 + matrix1.M42*matrix2.M21 + matrix1.M43*matrix2.M31 +
                          matrix1.M44*matrix2.M41;
            matrixD.M42 = matrix1.M41*matrix2.M12 + matrix1.M42*matrix2.M22 + matrix1.M43*matrix2.M32 +
                          matrix1.M44*matrix2.M42;
            matrixD.M43 = matrix1.M41*matrix2.M13 + matrix1.M42*matrix2.M23 + matrix1.M43*matrix2.M33 +
                          matrix1.M44*matrix2.M43;
            matrixD.M44 = matrix1.M41*matrix2.M14 + matrix1.M42*matrix2.M24 + matrix1.M43*matrix2.M34 +
                          matrix1.M44*matrix2.M44;
            return matrixD;
        }

        public static MatrixD operator *(MatrixD matrix1, Matrix matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11*(double) matrix2.M11 + matrix1.M12*(double) matrix2.M21 +
                          matrix1.M13*(double) matrix2.M31 + matrix1.M14*(double) matrix2.M41;
            matrixD.M12 = matrix1.M11*(double) matrix2.M12 + matrix1.M12*(double) matrix2.M22 +
                          matrix1.M13*(double) matrix2.M32 + matrix1.M14*(double) matrix2.M42;
            matrixD.M13 = matrix1.M11*(double) matrix2.M13 + matrix1.M12*(double) matrix2.M23 +
                          matrix1.M13*(double) matrix2.M33 + matrix1.M14*(double) matrix2.M43;
            matrixD.M14 = matrix1.M11*(double) matrix2.M14 + matrix1.M12*(double) matrix2.M24 +
                          matrix1.M13*(double) matrix2.M34 + matrix1.M14*(double) matrix2.M44;
            matrixD.M21 = matrix1.M21*(double) matrix2.M11 + matrix1.M22*(double) matrix2.M21 +
                          matrix1.M23*(double) matrix2.M31 + matrix1.M24*(double) matrix2.M41;
            matrixD.M22 = matrix1.M21*(double) matrix2.M12 + matrix1.M22*(double) matrix2.M22 +
                          matrix1.M23*(double) matrix2.M32 + matrix1.M24*(double) matrix2.M42;
            matrixD.M23 = matrix1.M21*(double) matrix2.M13 + matrix1.M22*(double) matrix2.M23 +
                          matrix1.M23*(double) matrix2.M33 + matrix1.M24*(double) matrix2.M43;
            matrixD.M24 = matrix1.M21*(double) matrix2.M14 + matrix1.M22*(double) matrix2.M24 +
                          matrix1.M23*(double) matrix2.M34 + matrix1.M24*(double) matrix2.M44;
            matrixD.M31 = matrix1.M31*(double) matrix2.M11 + matrix1.M32*(double) matrix2.M21 +
                          matrix1.M33*(double) matrix2.M31 + matrix1.M34*(double) matrix2.M41;
            matrixD.M32 = matrix1.M31*(double) matrix2.M12 + matrix1.M32*(double) matrix2.M22 +
                          matrix1.M33*(double) matrix2.M32 + matrix1.M34*(double) matrix2.M42;
            matrixD.M33 = matrix1.M31*(double) matrix2.M13 + matrix1.M32*(double) matrix2.M23 +
                          matrix1.M33*(double) matrix2.M33 + matrix1.M34*(double) matrix2.M43;
            matrixD.M34 = matrix1.M31*(double) matrix2.M14 + matrix1.M32*(double) matrix2.M24 +
                          matrix1.M33*(double) matrix2.M34 + matrix1.M34*(double) matrix2.M44;
            matrixD.M41 = matrix1.M41*(double) matrix2.M11 + matrix1.M42*(double) matrix2.M21 +
                          matrix1.M43*(double) matrix2.M31 + matrix1.M44*(double) matrix2.M41;
            matrixD.M42 = matrix1.M41*(double) matrix2.M12 + matrix1.M42*(double) matrix2.M22 +
                          matrix1.M43*(double) matrix2.M32 + matrix1.M44*(double) matrix2.M42;
            matrixD.M43 = matrix1.M41*(double) matrix2.M13 + matrix1.M42*(double) matrix2.M23 +
                          matrix1.M43*(double) matrix2.M33 + matrix1.M44*(double) matrix2.M43;
            matrixD.M44 = matrix1.M41*(double) matrix2.M14 + matrix1.M42*(double) matrix2.M24 +
                          matrix1.M43*(double) matrix2.M34 + matrix1.M44*(double) matrix2.M44;
            return matrixD;
        }

        public static MatrixD operator *(Matrix matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = (double) matrix1.M11*matrix2.M11 + (double) matrix1.M12*matrix2.M21 +
                          (double) matrix1.M13*matrix2.M31 + (double) matrix1.M14*matrix2.M41;
            matrixD.M12 = (double) matrix1.M11*matrix2.M12 + (double) matrix1.M12*matrix2.M22 +
                          (double) matrix1.M13*matrix2.M32 + (double) matrix1.M14*matrix2.M42;
            matrixD.M13 = (double) matrix1.M11*matrix2.M13 + (double) matrix1.M12*matrix2.M23 +
                          (double) matrix1.M13*matrix2.M33 + (double) matrix1.M14*matrix2.M43;
            matrixD.M14 = (double) matrix1.M11*matrix2.M14 + (double) matrix1.M12*matrix2.M24 +
                          (double) matrix1.M13*matrix2.M34 + (double) matrix1.M14*matrix2.M44;
            matrixD.M21 = (double) matrix1.M21*matrix2.M11 + (double) matrix1.M22*matrix2.M21 +
                          (double) matrix1.M23*matrix2.M31 + (double) matrix1.M24*matrix2.M41;
            matrixD.M22 = (double) matrix1.M21*matrix2.M12 + (double) matrix1.M22*matrix2.M22 +
                          (double) matrix1.M23*matrix2.M32 + (double) matrix1.M24*matrix2.M42;
            matrixD.M23 = (double) matrix1.M21*matrix2.M13 + (double) matrix1.M22*matrix2.M23 +
                          (double) matrix1.M23*matrix2.M33 + (double) matrix1.M24*matrix2.M43;
            matrixD.M24 = (double) matrix1.M21*matrix2.M14 + (double) matrix1.M22*matrix2.M24 +
                          (double) matrix1.M23*matrix2.M34 + (double) matrix1.M24*matrix2.M44;
            matrixD.M31 = (double) matrix1.M31*matrix2.M11 + (double) matrix1.M32*matrix2.M21 +
                          (double) matrix1.M33*matrix2.M31 + (double) matrix1.M34*matrix2.M41;
            matrixD.M32 = (double) matrix1.M31*matrix2.M12 + (double) matrix1.M32*matrix2.M22 +
                          (double) matrix1.M33*matrix2.M32 + (double) matrix1.M34*matrix2.M42;
            matrixD.M33 = (double) matrix1.M31*matrix2.M13 + (double) matrix1.M32*matrix2.M23 +
                          (double) matrix1.M33*matrix2.M33 + (double) matrix1.M34*matrix2.M43;
            matrixD.M34 = (double) matrix1.M31*matrix2.M14 + (double) matrix1.M32*matrix2.M24 +
                          (double) matrix1.M33*matrix2.M34 + (double) matrix1.M34*matrix2.M44;
            matrixD.M41 = (double) matrix1.M41*matrix2.M11 + (double) matrix1.M42*matrix2.M21 +
                          (double) matrix1.M43*matrix2.M31 + (double) matrix1.M44*matrix2.M41;
            matrixD.M42 = (double) matrix1.M41*matrix2.M12 + (double) matrix1.M42*matrix2.M22 +
                          (double) matrix1.M43*matrix2.M32 + (double) matrix1.M44*matrix2.M42;
            matrixD.M43 = (double) matrix1.M41*matrix2.M13 + (double) matrix1.M42*matrix2.M23 +
                          (double) matrix1.M43*matrix2.M33 + (double) matrix1.M44*matrix2.M43;
            matrixD.M44 = (double) matrix1.M41*matrix2.M14 + (double) matrix1.M42*matrix2.M24 +
                          (double) matrix1.M43*matrix2.M34 + (double) matrix1.M44*matrix2.M44;
            return matrixD;
        }

        public static MatrixD operator *(MatrixD matrix, double scaleFactor)
        {
            double num = scaleFactor;
            MatrixD matrixD;
            matrixD.M11 = matrix.M11*num;
            matrixD.M12 = matrix.M12*num;
            matrixD.M13 = matrix.M13*num;
            matrixD.M14 = matrix.M14*num;
            matrixD.M21 = matrix.M21*num;
            matrixD.M22 = matrix.M22*num;
            matrixD.M23 = matrix.M23*num;
            matrixD.M24 = matrix.M24*num;
            matrixD.M31 = matrix.M31*num;
            matrixD.M32 = matrix.M32*num;
            matrixD.M33 = matrix.M33*num;
            matrixD.M34 = matrix.M34*num;
            matrixD.M41 = matrix.M41*num;
            matrixD.M42 = matrix.M42*num;
            matrixD.M43 = matrix.M43*num;
            matrixD.M44 = matrix.M44*num;
            return matrixD;
        }

        public static MatrixD operator *(double scaleFactor, MatrixD matrix)
        {
            double num = scaleFactor;
            MatrixD matrixD;
            matrixD.M11 = matrix.M11*num;
            matrixD.M12 = matrix.M12*num;
            matrixD.M13 = matrix.M13*num;
            matrixD.M14 = matrix.M14*num;
            matrixD.M21 = matrix.M21*num;
            matrixD.M22 = matrix.M22*num;
            matrixD.M23 = matrix.M23*num;
            matrixD.M24 = matrix.M24*num;
            matrixD.M31 = matrix.M31*num;
            matrixD.M32 = matrix.M32*num;
            matrixD.M33 = matrix.M33*num;
            matrixD.M34 = matrix.M34*num;
            matrixD.M41 = matrix.M41*num;
            matrixD.M42 = matrix.M42*num;
            matrixD.M43 = matrix.M43*num;
            matrixD.M44 = matrix.M44*num;
            return matrixD;
        }

        public static MatrixD operator /(MatrixD matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11/matrix2.M11;
            matrixD.M12 = matrix1.M12/matrix2.M12;
            matrixD.M13 = matrix1.M13/matrix2.M13;
            matrixD.M14 = matrix1.M14/matrix2.M14;
            matrixD.M21 = matrix1.M21/matrix2.M21;
            matrixD.M22 = matrix1.M22/matrix2.M22;
            matrixD.M23 = matrix1.M23/matrix2.M23;
            matrixD.M24 = matrix1.M24/matrix2.M24;
            matrixD.M31 = matrix1.M31/matrix2.M31;
            matrixD.M32 = matrix1.M32/matrix2.M32;
            matrixD.M33 = matrix1.M33/matrix2.M33;
            matrixD.M34 = matrix1.M34/matrix2.M34;
            matrixD.M41 = matrix1.M41/matrix2.M41;
            matrixD.M42 = matrix1.M42/matrix2.M42;
            matrixD.M43 = matrix1.M43/matrix2.M43;
            matrixD.M44 = matrix1.M44/matrix2.M44;
            return matrixD;
        }

        public static MatrixD operator /(MatrixD matrix1, double divider)
        {
            double num = 1.0/divider;
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11*num;
            matrixD.M12 = matrix1.M12*num;
            matrixD.M13 = matrix1.M13*num;
            matrixD.M14 = matrix1.M14*num;
            matrixD.M21 = matrix1.M21*num;
            matrixD.M22 = matrix1.M22*num;
            matrixD.M23 = matrix1.M23*num;
            matrixD.M24 = matrix1.M24*num;
            matrixD.M31 = matrix1.M31*num;
            matrixD.M32 = matrix1.M32*num;
            matrixD.M33 = matrix1.M33*num;
            matrixD.M34 = matrix1.M34*num;
            matrixD.M41 = matrix1.M41*num;
            matrixD.M42 = matrix1.M42*num;
            matrixD.M43 = matrix1.M43*num;
            matrixD.M44 = matrix1.M44*num;
            return matrixD;
        }

        public Vector3D GetDirectionVector(Base6Directions.Direction direction)
        {
            switch (direction)
            {
                case Base6Directions.Direction.Forward:
                    return this.Forward;
                case Base6Directions.Direction.Backward:
                    return this.Backward;
                case Base6Directions.Direction.Left:
                    return this.Left;
                case Base6Directions.Direction.Right:
                    return this.Right;
                case Base6Directions.Direction.Up:
                    return this.Up;
                case Base6Directions.Direction.Down:
                    return this.Down;
                default:
                    return Vector3D.Zero;
            }
        }

        public void SetDirectionVector(Base6Directions.Direction direction, Vector3D newValue)
        {
            switch (direction)
            {
                case Base6Directions.Direction.Forward:
                    this.Forward = newValue;
                    break;
                case Base6Directions.Direction.Backward:
                    this.Backward = newValue;
                    break;
                case Base6Directions.Direction.Left:
                    this.Left = newValue;
                    break;
                case Base6Directions.Direction.Right:
                    this.Right = newValue;
                    break;
                case Base6Directions.Direction.Up:
                    this.Up = newValue;
                    break;
                case Base6Directions.Direction.Down:
                    this.Down = newValue;
                    break;
            }
        }

        public Base6Directions.Direction GetClosestDirection(Vector3D referenceVector)
        {
            return this.GetClosestDirection(ref referenceVector);
        }

        public Base6Directions.Direction GetClosestDirection(ref Vector3D referenceVector)
        {
            double num1 = Vector3D.Dot(referenceVector, this.Right);
            double num2 = Vector3D.Dot(referenceVector, this.Up);
            double num3 = Vector3D.Dot(referenceVector, this.Backward);
            double num4 = Math.Abs(num1);
            double num5 = Math.Abs(num2);
            double num6 = Math.Abs(num3);
            if (num4 > num5)
            {
                if (num4 > num6)
                    return num1 > 0.0 ? Base6Directions.Direction.Right : Base6Directions.Direction.Left;
                else
                    return num3 > 0.0 ? Base6Directions.Direction.Backward : Base6Directions.Direction.Forward;
            }
            else if (num5 > num6)
                return num2 > 0.0 ? Base6Directions.Direction.Up : Base6Directions.Direction.Down;
            else
                return num3 > 0.0 ? Base6Directions.Direction.Backward : Base6Directions.Direction.Forward;
        }

        public static void Rescale(ref MatrixD matrix, double scale)
        {
            matrix.M11 *= scale;
            matrix.M12 *= scale;
            matrix.M13 *= scale;
            matrix.M21 *= scale;
            matrix.M22 *= scale;
            matrix.M23 *= scale;
            matrix.M31 *= scale;
            matrix.M32 *= scale;
            matrix.M33 *= scale;
        }

        public static void Rescale(ref MatrixD matrix, float scale)
        {
            matrix.M11 *= (double) scale;
            matrix.M12 *= (double) scale;
            matrix.M13 *= (double) scale;
            matrix.M21 *= (double) scale;
            matrix.M22 *= (double) scale;
            matrix.M23 *= (double) scale;
            matrix.M31 *= (double) scale;
            matrix.M32 *= (double) scale;
            matrix.M33 *= (double) scale;
        }

        public static void Rescale(ref MatrixD matrix, ref Vector3D scale)
        {
            matrix.M11 *= scale.X;
            matrix.M12 *= scale.X;
            matrix.M13 *= scale.X;
            matrix.M21 *= scale.Y;
            matrix.M22 *= scale.Y;
            matrix.M23 *= scale.Y;
            matrix.M31 *= scale.Z;
            matrix.M32 *= scale.Z;
            matrix.M33 *= scale.Z;
        }

        public static MatrixD Rescale(MatrixD matrix, double scale)
        {
            MatrixD.Rescale(ref matrix, scale);
            return matrix;
        }

        public static MatrixD Rescale(MatrixD matrix, Vector3D scale)
        {
            MatrixD.Rescale(ref matrix, ref scale);
            return matrix;
        }

        public static MatrixD CreateBillboard(Vector3D objectPosition, Vector3D cameraPosition, Vector3D cameraUpVector,
            Vector3D? cameraForwardVector)
        {
            Vector3D result1;
            result1.X = objectPosition.X - cameraPosition.X;
            result1.Y = objectPosition.Y - cameraPosition.Y;
            result1.Z = objectPosition.Z - cameraPosition.Z;
            double d = result1.LengthSquared();
            if (d < 9.99999974737875E-05)
                result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3D.Forward;
            else
                Vector3D.Multiply(ref result1, 1.0/Math.Sqrt(d), out result1);
            Vector3D result2;
            Vector3D.Cross(ref cameraUpVector, ref result1, out result2);
            result2.Normalize();
            Vector3D result3;
            Vector3D.Cross(ref result1, ref result2, out result3);
            MatrixD matrixD;
            matrixD.M11 = result2.X;
            matrixD.M12 = result2.Y;
            matrixD.M13 = result2.Z;
            matrixD.M14 = 0.0;
            matrixD.M21 = result3.X;
            matrixD.M22 = result3.Y;
            matrixD.M23 = result3.Z;
            matrixD.M24 = 0.0;
            matrixD.M31 = result1.X;
            matrixD.M32 = result1.Y;
            matrixD.M33 = result1.Z;
            matrixD.M34 = 0.0;
            matrixD.M41 = objectPosition.X;
            matrixD.M42 = objectPosition.Y;
            matrixD.M43 = objectPosition.Z;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateBillboard(ref Vector3D objectPosition, ref Vector3D cameraPosition,
            ref Vector3D cameraUpVector, Vector3D? cameraForwardVector, out MatrixD result)
        {
            Vector3D result1;
            result1.X = objectPosition.X - cameraPosition.X;
            result1.Y = objectPosition.Y - cameraPosition.Y;
            result1.Z = objectPosition.Z - cameraPosition.Z;
            double d = result1.LengthSquared();
            if (d < 9.99999974737875E-05)
                result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3D.Forward;
            else
                Vector3D.Multiply(ref result1, 1.0/Math.Sqrt(d), out result1);
            Vector3D result2;
            Vector3D.Cross(ref cameraUpVector, ref result1, out result2);
            result2.Normalize();
            Vector3D result3;
            Vector3D.Cross(ref result1, ref result2, out result3);
            result.M11 = result2.X;
            result.M12 = result2.Y;
            result.M13 = result2.Z;
            result.M14 = 0.0;
            result.M21 = result3.X;
            result.M22 = result3.Y;
            result.M23 = result3.Z;
            result.M24 = 0.0;
            result.M31 = result1.X;
            result.M32 = result1.Y;
            result.M33 = result1.Z;
            result.M34 = 0.0;
            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1.0;
        }

        public static MatrixD CreateConstrainedBillboard(Vector3D objectPosition, Vector3D cameraPosition,
            Vector3D rotateAxis, Vector3D? cameraForwardVector, Vector3D? objectForwardVector)
        {
            Vector3D result1;
            result1.X = objectPosition.X - cameraPosition.X;
            result1.Y = objectPosition.Y - cameraPosition.Y;
            result1.Z = objectPosition.Z - cameraPosition.Z;
            double d = result1.LengthSquared();
            if (d < 9.99999974737875E-05)
                result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3D.Forward;
            else
                Vector3D.Multiply(ref result1, 1.0/Math.Sqrt(d), out result1);
            Vector3D vector2 = rotateAxis;
            double result2;
            Vector3D.Dot(ref rotateAxis, ref result1, out result2);
            Vector3D result3;
            Vector3D result4;
            if (Math.Abs(result2) > 0.998254656791687)
            {
                if (objectForwardVector.HasValue)
                {
                    result3 = objectForwardVector.Value;
                    Vector3D.Dot(ref rotateAxis, ref result3, out result2);
                    if (Math.Abs(result2) > 0.998254656791687)
                        result3 =
                            Math.Abs(rotateAxis.X*Vector3D.Forward.X + rotateAxis.Y*Vector3D.Forward.Y +
                                     rotateAxis.Z*Vector3D.Forward.Z) > 0.998254656791687
                                ? Vector3D.Right
                                : Vector3D.Forward;
                }
                else
                    result3 =
                        Math.Abs(rotateAxis.X*Vector3D.Forward.X + rotateAxis.Y*Vector3D.Forward.Y +
                                 rotateAxis.Z*Vector3D.Forward.Z) > 0.998254656791687
                            ? Vector3D.Right
                            : Vector3D.Forward;
                Vector3D.Cross(ref rotateAxis, ref result3, out result4);
                result4.Normalize();
                Vector3D.Cross(ref result4, ref rotateAxis, out result3);
                result3.Normalize();
            }
            else
            {
                Vector3D.Cross(ref rotateAxis, ref result1, out result4);
                result4.Normalize();
                Vector3D.Cross(ref result4, ref vector2, out result3);
                result3.Normalize();
            }
            MatrixD matrixD;
            matrixD.M11 = result4.X;
            matrixD.M12 = result4.Y;
            matrixD.M13 = result4.Z;
            matrixD.M14 = 0.0;
            matrixD.M21 = vector2.X;
            matrixD.M22 = vector2.Y;
            matrixD.M23 = vector2.Z;
            matrixD.M24 = 0.0;
            matrixD.M31 = result3.X;
            matrixD.M32 = result3.Y;
            matrixD.M33 = result3.Z;
            matrixD.M34 = 0.0;
            matrixD.M41 = objectPosition.X;
            matrixD.M42 = objectPosition.Y;
            matrixD.M43 = objectPosition.Z;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateConstrainedBillboard(ref Vector3D objectPosition, ref Vector3D cameraPosition,
            ref Vector3D rotateAxis, Vector3D? cameraForwardVector, Vector3D? objectForwardVector, out MatrixD result)
        {
            Vector3D result1;
            result1.X = objectPosition.X - cameraPosition.X;
            result1.Y = objectPosition.Y - cameraPosition.Y;
            result1.Z = objectPosition.Z - cameraPosition.Z;
            double d = result1.LengthSquared();
            if (d < 9.99999974737875E-05)
                result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3D.Forward;
            else
                Vector3D.Multiply(ref result1, 1.0/Math.Sqrt(d), out result1);
            Vector3D vector2 = rotateAxis;
            double result2;
            Vector3D.Dot(ref rotateAxis, ref result1, out result2);
            Vector3D result3;
            Vector3D result4;
            if (Math.Abs(result2) > 0.998254656791687)
            {
                if (objectForwardVector.HasValue)
                {
                    result3 = objectForwardVector.Value;
                    Vector3D.Dot(ref rotateAxis, ref result3, out result2);
                    if (Math.Abs(result2) > 0.998254656791687)
                        result3 =
                            Math.Abs(rotateAxis.X*Vector3D.Forward.X + rotateAxis.Y*Vector3D.Forward.Y +
                                     rotateAxis.Z*Vector3D.Forward.Z) > 0.998254656791687
                                ? Vector3D.Right
                                : Vector3D.Forward;
                }
                else
                    result3 =
                        Math.Abs(rotateAxis.X*Vector3D.Forward.X + rotateAxis.Y*Vector3D.Forward.Y +
                                 rotateAxis.Z*Vector3D.Forward.Z) > 0.998254656791687
                            ? Vector3D.Right
                            : Vector3D.Forward;
                Vector3D.Cross(ref rotateAxis, ref result3, out result4);
                result4.Normalize();
                Vector3D.Cross(ref result4, ref rotateAxis, out result3);
                result3.Normalize();
            }
            else
            {
                Vector3D.Cross(ref rotateAxis, ref result1, out result4);
                result4.Normalize();
                Vector3D.Cross(ref result4, ref vector2, out result3);
                result3.Normalize();
            }
            result.M11 = result4.X;
            result.M12 = result4.Y;
            result.M13 = result4.Z;
            result.M14 = 0.0;
            result.M21 = vector2.X;
            result.M22 = vector2.Y;
            result.M23 = vector2.Z;
            result.M24 = 0.0;
            result.M31 = result3.X;
            result.M32 = result3.Y;
            result.M33 = result3.Z;
            result.M34 = 0.0;
            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1.0;
        }

        public static MatrixD CreateTranslation(Vector3D position)
        {
            MatrixD matrixD;
            matrixD.M11 = 1.0;
            matrixD.M12 = 0.0;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = 1.0;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = 0.0;
            matrixD.M33 = 1.0;
            matrixD.M34 = 0.0;
            matrixD.M41 = position.X;
            matrixD.M42 = position.Y;
            matrixD.M43 = position.Z;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static MatrixD CreateTranslation(Vector3 position)
        {
            MatrixD matrixD;
            matrixD.M11 = 1.0;
            matrixD.M12 = 0.0;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = 1.0;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = 0.0;
            matrixD.M33 = 1.0;
            matrixD.M34 = 0.0;
            matrixD.M41 = (double) position.X;
            matrixD.M42 = (double) position.Y;
            matrixD.M43 = (double) position.Z;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateTranslation(ref Vector3D position, out MatrixD result)
        {
            result.M11 = 1.0;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = 1.0;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = 1.0;
            result.M34 = 0.0;
            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1.0;
        }

        public static MatrixD CreateTranslation(double xPosition, double yPosition, double zPosition)
        {
            MatrixD matrixD;
            matrixD.M11 = 1.0;
            matrixD.M12 = 0.0;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = 1.0;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = 0.0;
            matrixD.M33 = 1.0;
            matrixD.M34 = 0.0;
            matrixD.M41 = xPosition;
            matrixD.M42 = yPosition;
            matrixD.M43 = zPosition;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateTranslation(double xPosition, double yPosition, double zPosition, out MatrixD result)
        {
            result.M11 = 1.0;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = 1.0;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = 1.0;
            result.M34 = 0.0;
            result.M41 = xPosition;
            result.M42 = yPosition;
            result.M43 = zPosition;
            result.M44 = 1.0;
        }

        public static MatrixD CreateScale(double xScale, double yScale, double zScale)
        {
            double num1 = xScale;
            double num2 = yScale;
            double num3 = zScale;
            MatrixD matrixD;
            matrixD.M11 = num1;
            matrixD.M12 = 0.0;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = num2;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = 0.0;
            matrixD.M33 = num3;
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateScale(double xScale, double yScale, double zScale, out MatrixD result)
        {
            double num1 = xScale;
            double num2 = yScale;
            double num3 = zScale;
            result.M11 = num1;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = num2;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = num3;
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreateScale(Vector3D scales)
        {
            double num1 = scales.X;
            double num2 = scales.Y;
            double num3 = scales.Z;
            MatrixD matrixD;
            matrixD.M11 = num1;
            matrixD.M12 = 0.0;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = num2;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = 0.0;
            matrixD.M33 = num3;
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateScale(ref Vector3D scales, out MatrixD result)
        {
            double num1 = scales.X;
            double num2 = scales.Y;
            double num3 = scales.Z;
            result.M11 = num1;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = num2;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = num3;
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreateScale(double scale)
        {
            double num = scale;
            MatrixD matrixD;
            matrixD.M11 = num;
            matrixD.M12 = 0.0;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = num;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = 0.0;
            matrixD.M33 = num;
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateScale(double scale, out MatrixD result)
        {
            double num = scale;
            result.M11 = num;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = num;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = num;
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreateRotationX(double radians)
        {
            double num1 = Math.Cos(radians);
            double num2 = Math.Sin(radians);
            MatrixD matrixD;
            matrixD.M11 = 1.0;
            matrixD.M12 = 0.0;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = num1;
            matrixD.M23 = num2;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = -num2;
            matrixD.M33 = num1;
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateRotationX(double radians, out MatrixD result)
        {
            double num1 = Math.Cos(radians);
            double num2 = Math.Sin(radians);
            result.M11 = 1.0;
            result.M12 = 0.0;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = num1;
            result.M23 = num2;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = -num2;
            result.M33 = num1;
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreateRotationY(double radians)
        {
            double num1 = Math.Cos(radians);
            double num2 = Math.Sin(radians);
            MatrixD matrixD;
            matrixD.M11 = num1;
            matrixD.M12 = 0.0;
            matrixD.M13 = -num2;
            matrixD.M14 = 0.0;
            matrixD.M21 = 0.0;
            matrixD.M22 = 1.0;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = num2;
            matrixD.M32 = 0.0;
            matrixD.M33 = num1;
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateRotationY(double radians, out MatrixD result)
        {
            double num1 = Math.Cos(radians);
            double num2 = Math.Sin(radians);
            result.M11 = num1;
            result.M12 = 0.0;
            result.M13 = -num2;
            result.M14 = 0.0;
            result.M21 = 0.0;
            result.M22 = 1.0;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = num2;
            result.M32 = 0.0;
            result.M33 = num1;
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreateRotationZ(double radians)
        {
            double num1 = Math.Cos(radians);
            double num2 = Math.Sin(radians);
            MatrixD matrixD;
            matrixD.M11 = num1;
            matrixD.M12 = num2;
            matrixD.M13 = 0.0;
            matrixD.M14 = 0.0;
            matrixD.M21 = -num2;
            matrixD.M22 = num1;
            matrixD.M23 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M31 = 0.0;
            matrixD.M32 = 0.0;
            matrixD.M33 = 1.0;
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateRotationZ(double radians, out MatrixD result)
        {
            double num1 = Math.Cos(radians);
            double num2 = Math.Sin(radians);
            result.M11 = num1;
            result.M12 = num2;
            result.M13 = 0.0;
            result.M14 = 0.0;
            result.M21 = -num2;
            result.M22 = num1;
            result.M23 = 0.0;
            result.M24 = 0.0;
            result.M31 = 0.0;
            result.M32 = 0.0;
            result.M33 = 1.0;
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreateFromAxisAngle(Vector3D axis, double angle)
        {
            double num1 = axis.X;
            double num2 = axis.Y;
            double num3 = axis.Z;
            double num4 = Math.Sin(angle);
            double num5 = Math.Cos(angle);
            double num6 = num1*num1;
            double num7 = num2*num2;
            double num8 = num3*num3;
            double num9 = num1*num2;
            double num10 = num1*num3;
            double num11 = num2*num3;
            MatrixD matrixD;
            matrixD.M11 = num6 + num5*(1.0 - num6);
            matrixD.M12 = num9 - num5*num9 + num4*num3;
            matrixD.M13 = num10 - num5*num10 - num4*num2;
            matrixD.M14 = 0.0;
            matrixD.M21 = num9 - num5*num9 - num4*num3;
            matrixD.M22 = num7 + num5*(1.0 - num7);
            matrixD.M23 = num11 - num5*num11 + num4*num1;
            matrixD.M24 = 0.0;
            matrixD.M31 = num10 - num5*num10 + num4*num2;
            matrixD.M32 = num11 - num5*num11 - num4*num1;
            matrixD.M33 = num8 + num5*(1.0 - num8);
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateFromAxisAngle(ref Vector3D axis, double angle, out MatrixD result)
        {
            double num1 = axis.X;
            double num2 = axis.Y;
            double num3 = axis.Z;
            double num4 = Math.Sin(angle);
            double num5 = Math.Cos(angle);
            double num6 = num1*num1;
            double num7 = num2*num2;
            double num8 = num3*num3;
            double num9 = num1*num2;
            double num10 = num1*num3;
            double num11 = num2*num3;
            result.M11 = num6 + num5*(1.0 - num6);
            result.M12 = num9 - num5*num9 + num4*num3;
            result.M13 = num10 - num5*num10 - num4*num2;
            result.M14 = 0.0;
            result.M21 = num9 - num5*num9 - num4*num3;
            result.M22 = num7 + num5*(1.0 - num7);
            result.M23 = num11 - num5*num11 + num4*num1;
            result.M24 = 0.0;
            result.M31 = num10 - num5*num10 + num4*num2;
            result.M32 = num11 - num5*num11 - num4*num1;
            result.M33 = num8 + num5*(1.0 - num8);
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreatePerspectiveFieldOfView(double fieldOfView, double aspectRatio,
            double nearPlaneDistance, double farPlaneDistance)
        {
            if (fieldOfView <= 0.0 || fieldOfView >= 3.14159274101257)
                throw new ArgumentOutOfRangeException("fieldOfView",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OutRangeFieldOfView", new object[1]
                    {
                        (object) "fieldOfView"
                    }));
            else if (nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "nearPlaneDistance"
                    }));
            else if (farPlaneDistance <= 0.0)
            {
                throw new ArgumentOutOfRangeException("farPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "farPlaneDistance"
                    }));
            }
            else
            {
                if (nearPlaneDistance >= farPlaneDistance)
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
                double num1 = 1.0/Math.Tan(fieldOfView*0.5);
                double num2 = num1/aspectRatio;
                MatrixD matrixD;
                matrixD.M11 = num2;
                matrixD.M12 = matrixD.M13 = matrixD.M14 = 0.0;
                matrixD.M22 = num1;
                matrixD.M21 = matrixD.M23 = matrixD.M24 = 0.0;
                matrixD.M31 = matrixD.M32 = 0.0;
                matrixD.M33 = farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                matrixD.M34 = -1.0;
                matrixD.M41 = matrixD.M42 = matrixD.M44 = 0.0;
                matrixD.M43 = nearPlaneDistance*farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                return matrixD;
            }
        }

        public static void CreatePerspectiveFieldOfView(double fieldOfView, double aspectRatio, double nearPlaneDistance,
            double farPlaneDistance, out MatrixD result)
        {
            if (fieldOfView <= 0.0 || fieldOfView >= 3.14159274101257)
                throw new ArgumentOutOfRangeException("fieldOfView",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OutRangeFieldOfView", new object[1]
                    {
                        (object) "fieldOfView"
                    }));
            else if (nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "nearPlaneDistance"
                    }));
            else if (farPlaneDistance <= 0.0)
            {
                throw new ArgumentOutOfRangeException("farPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "farPlaneDistance"
                    }));
            }
            else
            {
                if (nearPlaneDistance >= farPlaneDistance)
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
                double num1 = 1.0/Math.Tan(fieldOfView*0.5);
                double num2 = num1/aspectRatio;
                result.M11 = num2;
                result.M12 = result.M13 = result.M14 = 0.0;
                result.M22 = num1;
                result.M21 = result.M23 = result.M24 = 0.0;
                result.M31 = result.M32 = 0.0;
                result.M33 = farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                result.M34 = -1.0;
                result.M41 = result.M42 = result.M44 = 0.0;
                result.M43 = nearPlaneDistance*farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
            }
        }

        public static MatrixD CreatePerspective(double width, double height, double nearPlaneDistance,
            double farPlaneDistance)
        {
            if (nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "nearPlaneDistance"
                    }));
            else if (farPlaneDistance <= 0.0)
            {
                throw new ArgumentOutOfRangeException("farPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "farPlaneDistance"
                    }));
            }
            else
            {
                if (nearPlaneDistance >= farPlaneDistance)
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
                MatrixD matrixD;
                matrixD.M11 = 2.0*nearPlaneDistance/width;
                matrixD.M12 = matrixD.M13 = matrixD.M14 = 0.0;
                matrixD.M22 = 2.0*nearPlaneDistance/height;
                matrixD.M21 = matrixD.M23 = matrixD.M24 = 0.0;
                matrixD.M33 = farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                matrixD.M31 = matrixD.M32 = 0.0;
                matrixD.M34 = -1.0;
                matrixD.M41 = matrixD.M42 = matrixD.M44 = 0.0;
                matrixD.M43 = nearPlaneDistance*farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                return matrixD;
            }
        }

        public static void CreatePerspective(double width, double height, double nearPlaneDistance,
            double farPlaneDistance, out MatrixD result)
        {
            if (nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "nearPlaneDistance"
                    }));
            else if (farPlaneDistance <= 0.0)
            {
                throw new ArgumentOutOfRangeException("farPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "farPlaneDistance"
                    }));
            }
            else
            {
                if (nearPlaneDistance >= farPlaneDistance)
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
                result.M11 = 2.0*nearPlaneDistance/width;
                result.M12 = result.M13 = result.M14 = 0.0;
                result.M22 = 2.0*nearPlaneDistance/height;
                result.M21 = result.M23 = result.M24 = 0.0;
                result.M33 = farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                result.M31 = result.M32 = 0.0;
                result.M34 = -1.0;
                result.M41 = result.M42 = result.M44 = 0.0;
                result.M43 = nearPlaneDistance*farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
            }
        }

        public static MatrixD CreatePerspectiveOffCenter(double left, double right, double bottom, double top,
            double nearPlaneDistance, double farPlaneDistance)
        {
            if (nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "nearPlaneDistance"
                    }));
            else if (farPlaneDistance <= 0.0)
            {
                throw new ArgumentOutOfRangeException("farPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "farPlaneDistance"
                    }));
            }
            else
            {
                if (nearPlaneDistance >= farPlaneDistance)
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
                MatrixD matrixD;
                matrixD.M11 = 2.0*nearPlaneDistance/(right - left);
                matrixD.M12 = matrixD.M13 = matrixD.M14 = 0.0;
                matrixD.M22 = 2.0*nearPlaneDistance/(top - bottom);
                matrixD.M21 = matrixD.M23 = matrixD.M24 = 0.0;
                matrixD.M31 = (left + right)/(right - left);
                matrixD.M32 = (top + bottom)/(top - bottom);
                matrixD.M33 = farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                matrixD.M34 = -1.0;
                matrixD.M43 = nearPlaneDistance*farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                matrixD.M41 = matrixD.M42 = matrixD.M44 = 0.0;
                return matrixD;
            }
        }

        public static void CreatePerspectiveOffCenter(double left, double right, double bottom, double top,
            double nearPlaneDistance, double farPlaneDistance, out MatrixD result)
        {
            if (nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException("nearPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "nearPlaneDistance"
                    }));
            else if (farPlaneDistance <= 0.0)
            {
                throw new ArgumentOutOfRangeException("farPlaneDistance",
                    string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
                    {
                        (object) "farPlaneDistance"
                    }));
            }
            else
            {
                if (nearPlaneDistance >= farPlaneDistance)
                    throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
                result.M11 = 2.0*nearPlaneDistance/(right - left);
                result.M12 = result.M13 = result.M14 = 0.0;
                result.M22 = 2.0*nearPlaneDistance/(top - bottom);
                result.M21 = result.M23 = result.M24 = 0.0;
                result.M31 = (left + right)/(right - left);
                result.M32 = (top + bottom)/(top - bottom);
                result.M33 = farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                result.M34 = -1.0;
                result.M43 = nearPlaneDistance*farPlaneDistance/(nearPlaneDistance - farPlaneDistance);
                result.M41 = result.M42 = result.M44 = 0.0;
            }
        }

        public static MatrixD CreateOrthographic(double width, double height, double zNearPlane, double zFarPlane)
        {
            MatrixD matrixD;
            matrixD.M11 = 2.0/width;
            matrixD.M12 = matrixD.M13 = matrixD.M14 = 0.0;
            matrixD.M22 = 2.0/height;
            matrixD.M21 = matrixD.M23 = matrixD.M24 = 0.0;
            matrixD.M33 = 1.0/(zNearPlane - zFarPlane);
            matrixD.M31 = matrixD.M32 = matrixD.M34 = 0.0;
            matrixD.M41 = matrixD.M42 = 0.0;
            matrixD.M43 = zNearPlane/(zNearPlane - zFarPlane);
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateOrthographic(double width, double height, double zNearPlane, double zFarPlane,
            out MatrixD result)
        {
            result.M11 = 2.0/width;
            result.M12 = result.M13 = result.M14 = 0.0;
            result.M22 = 2.0/height;
            result.M21 = result.M23 = result.M24 = 0.0;
            result.M33 = 1.0/(zNearPlane - zFarPlane);
            result.M31 = result.M32 = result.M34 = 0.0;
            result.M41 = result.M42 = 0.0;
            result.M43 = zNearPlane/(zNearPlane - zFarPlane);
            result.M44 = 1.0;
        }

        public static MatrixD CreateOrthographicOffCenter(double left, double right, double bottom, double top,
            double zNearPlane, double zFarPlane)
        {
            MatrixD matrixD;
            matrixD.M11 = 2.0/(right - left);
            matrixD.M12 = matrixD.M13 = matrixD.M14 = 0.0;
            matrixD.M22 = 2.0/(top - bottom);
            matrixD.M21 = matrixD.M23 = matrixD.M24 = 0.0;
            matrixD.M33 = 1.0/(zNearPlane - zFarPlane);
            matrixD.M31 = matrixD.M32 = matrixD.M34 = 0.0;
            matrixD.M41 = (left + right)/(left - right);
            matrixD.M42 = (top + bottom)/(bottom - top);
            matrixD.M43 = zNearPlane/(zNearPlane - zFarPlane);
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateOrthographicOffCenter(double left, double right, double bottom, double top,
            double zNearPlane, double zFarPlane, out MatrixD result)
        {
            result.M11 = 2.0/(right - left);
            result.M12 = result.M13 = result.M14 = 0.0;
            result.M22 = 2.0/(top - bottom);
            result.M21 = result.M23 = result.M24 = 0.0;
            result.M33 = 1.0/(zNearPlane - zFarPlane);
            result.M31 = result.M32 = result.M34 = 0.0;
            result.M41 = (left + right)/(left - right);
            result.M42 = (top + bottom)/(bottom - top);
            result.M43 = zNearPlane/(zNearPlane - zFarPlane);
            result.M44 = 1.0;
        }

        public static MatrixD CreateLookAt(Vector3D cameraPosition, Vector3D cameraTarget, Vector3 cameraUpVector)
        {
            return MatrixD.CreateLookAt(cameraPosition, cameraTarget, (Vector3D) cameraUpVector);
        }

        public static MatrixD CreateLookAt(Vector3D cameraPosition, Vector3D cameraTarget, Vector3D cameraUpVector)
        {
            Vector3D vector3D1 = Vector3D.Normalize(cameraPosition - cameraTarget);
            Vector3D vector3D2 = Vector3D.Normalize(Vector3D.Cross(cameraUpVector, vector3D1));
            Vector3D vector1 = Vector3D.Cross(vector3D1, vector3D2);
            MatrixD matrixD;
            matrixD.M11 = vector3D2.X;
            matrixD.M12 = vector1.X;
            matrixD.M13 = vector3D1.X;
            matrixD.M14 = 0.0;
            matrixD.M21 = vector3D2.Y;
            matrixD.M22 = vector1.Y;
            matrixD.M23 = vector3D1.Y;
            matrixD.M24 = 0.0;
            matrixD.M31 = vector3D2.Z;
            matrixD.M32 = vector1.Z;
            matrixD.M33 = vector3D1.Z;
            matrixD.M34 = 0.0;
            matrixD.M41 = -Vector3D.Dot(vector3D2, cameraPosition);
            matrixD.M42 = -Vector3D.Dot(vector1, cameraPosition);
            matrixD.M43 = -Vector3D.Dot(vector3D1, cameraPosition);
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateLookAt(ref Vector3D cameraPosition, ref Vector3D cameraTarget,
            ref Vector3D cameraUpVector, out MatrixD result)
        {
            Vector3D vector3D1 = Vector3D.Normalize(cameraPosition - cameraTarget);
            Vector3D vector3D2 = Vector3D.Normalize(Vector3D.Cross(cameraUpVector, vector3D1));
            Vector3D vector1 = Vector3D.Cross(vector3D1, vector3D2);
            result.M11 = vector3D2.X;
            result.M12 = vector1.X;
            result.M13 = vector3D1.X;
            result.M14 = 0.0;
            result.M21 = vector3D2.Y;
            result.M22 = vector1.Y;
            result.M23 = vector3D1.Y;
            result.M24 = 0.0;
            result.M31 = vector3D2.Z;
            result.M32 = vector1.Z;
            result.M33 = vector3D1.Z;
            result.M34 = 0.0;
            result.M41 = -Vector3D.Dot(vector3D2, cameraPosition);
            result.M42 = -Vector3D.Dot(vector1, cameraPosition);
            result.M43 = -Vector3D.Dot(vector3D1, cameraPosition);
            result.M44 = 1.0;
        }

        public static MatrixD CreateWorld(Vector3D position, Vector3 forward, Vector3 up)
        {
            return MatrixD.CreateWorld(position, (Vector3D) forward, (Vector3D) up);
        }

        public static MatrixD CreateWorld(Vector3D position, Vector3D forward, Vector3D up)
        {
            Vector3D vector3D1 = Vector3D.Normalize(-forward);
            Vector3D vector2 = Vector3D.Normalize(Vector3D.Cross(up, vector3D1));
            Vector3D vector3D2 = Vector3D.Cross(vector3D1, vector2);
            MatrixD matrixD;
            matrixD.M11 = vector2.X;
            matrixD.M12 = vector2.Y;
            matrixD.M13 = vector2.Z;
            matrixD.M14 = 0.0;
            matrixD.M21 = vector3D2.X;
            matrixD.M22 = vector3D2.Y;
            matrixD.M23 = vector3D2.Z;
            matrixD.M24 = 0.0;
            matrixD.M31 = vector3D1.X;
            matrixD.M32 = vector3D1.Y;
            matrixD.M33 = vector3D1.Z;
            matrixD.M34 = 0.0;
            matrixD.M41 = position.X;
            matrixD.M42 = position.Y;
            matrixD.M43 = position.Z;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateWorld(ref Vector3D position, ref Vector3D forward, ref Vector3D up, out MatrixD result)
        {
            Vector3D vector3D1 = Vector3D.Normalize(-forward);
            Vector3D vector2 = Vector3D.Normalize(Vector3D.Cross(up, vector3D1));
            Vector3D vector3D2 = Vector3D.Cross(vector3D1, vector2);
            result.M11 = vector2.X;
            result.M12 = vector2.Y;
            result.M13 = vector2.Z;
            result.M14 = 0.0;
            result.M21 = vector3D2.X;
            result.M22 = vector3D2.Y;
            result.M23 = vector3D2.Z;
            result.M24 = 0.0;
            result.M31 = vector3D1.X;
            result.M32 = vector3D1.Y;
            result.M33 = vector3D1.Z;
            result.M34 = 0.0;
            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1.0;
        }

        public static MatrixD CreateFromQuaternion(Quaternion quaternion)
        {
            double num1 = (double) quaternion.X*(double) quaternion.X;
            double num2 = (double) quaternion.Y*(double) quaternion.Y;
            double num3 = (double) quaternion.Z*(double) quaternion.Z;
            double num4 = (double) quaternion.X*(double) quaternion.Y;
            double num5 = (double) quaternion.Z*(double) quaternion.W;
            double num6 = (double) quaternion.Z*(double) quaternion.X;
            double num7 = (double) quaternion.Y*(double) quaternion.W;
            double num8 = (double) quaternion.Y*(double) quaternion.Z;
            double num9 = (double) quaternion.X*(double) quaternion.W;
            MatrixD matrixD;
            matrixD.M11 = 1.0 - 2.0*(num2 + num3);
            matrixD.M12 = 2.0*(num4 + num5);
            matrixD.M13 = 2.0*(num6 - num7);
            matrixD.M14 = 0.0;
            matrixD.M21 = 2.0*(num4 - num5);
            matrixD.M22 = 1.0 - 2.0*(num3 + num1);
            matrixD.M23 = 2.0*(num8 + num9);
            matrixD.M24 = 0.0;
            matrixD.M31 = 2.0*(num6 + num7);
            matrixD.M32 = 2.0*(num8 - num9);
            matrixD.M33 = 1.0 - 2.0*(num2 + num1);
            matrixD.M34 = 0.0;
            matrixD.M41 = 0.0;
            matrixD.M42 = 0.0;
            matrixD.M43 = 0.0;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateFromQuaternion(ref Quaternion quaternion, out MatrixD result)
        {
            double num1 = (double) quaternion.X*(double) quaternion.X;
            double num2 = (double) quaternion.Y*(double) quaternion.Y;
            double num3 = (double) quaternion.Z*(double) quaternion.Z;
            double num4 = (double) quaternion.X*(double) quaternion.Y;
            double num5 = (double) quaternion.Z*(double) quaternion.W;
            double num6 = (double) quaternion.Z*(double) quaternion.X;
            double num7 = (double) quaternion.Y*(double) quaternion.W;
            double num8 = (double) quaternion.Y*(double) quaternion.Z;
            double num9 = (double) quaternion.X*(double) quaternion.W;
            result.M11 = 1.0 - 2.0*(num2 + num3);
            result.M12 = 2.0*(num4 + num5);
            result.M13 = 2.0*(num6 - num7);
            result.M14 = 0.0;
            result.M21 = 2.0*(num4 - num5);
            result.M22 = 1.0 - 2.0*(num3 + num1);
            result.M23 = 2.0*(num8 + num9);
            result.M24 = 0.0;
            result.M31 = 2.0*(num6 + num7);
            result.M32 = 2.0*(num8 - num9);
            result.M33 = 1.0 - 2.0*(num2 + num1);
            result.M34 = 0.0;
            result.M41 = 0.0;
            result.M42 = 0.0;
            result.M43 = 0.0;
            result.M44 = 1.0;
        }

        public static MatrixD CreateFromYawPitchRoll(double yaw, double pitch, double roll)
        {
            Quaternion result1;
            Quaternion.CreateFromYawPitchRoll((float) yaw, (float) pitch, (float) roll, out result1);
            MatrixD result2;
            MatrixD.CreateFromQuaternion(ref result1, out result2);
            return result2;
        }

        public static void CreateFromYawPitchRoll(double yaw, double pitch, double roll, out MatrixD result)
        {
            Quaternion result1;
            Quaternion.CreateFromYawPitchRoll((float) yaw, (float) pitch, (float) roll, out result1);
            MatrixD.CreateFromQuaternion(ref result1, out result);
        }

        public static MatrixD CreateFromTransformScale(Quaternion orientation, Vector3D position, Vector3D scale)
        {
            MatrixD fromQuaternion = MatrixD.CreateFromQuaternion(orientation);
            fromQuaternion.Translation = position;
            MatrixD.Rescale(ref fromQuaternion, ref scale);
            return fromQuaternion;
        }

        public static MatrixD CreateShadow(Vector3D lightDirection, Plane plane)
        {
            Plane result;
            Plane.Normalize(ref plane, out result);
            double num1 = (double) result.Normal.X*lightDirection.X + (double) result.Normal.Y*lightDirection.Y +
                          (double) result.Normal.Z*lightDirection.Z;
            double num2 = -(double) result.Normal.X;
            double num3 = -(double) result.Normal.Y;
            double num4 = -(double) result.Normal.Z;
            double num5 = -(double) result.D;
            MatrixD matrixD;
            matrixD.M11 = num2*lightDirection.X + num1;
            matrixD.M21 = num3*lightDirection.X;
            matrixD.M31 = num4*lightDirection.X;
            matrixD.M41 = num5*lightDirection.X;
            matrixD.M12 = num2*lightDirection.Y;
            matrixD.M22 = num3*lightDirection.Y + num1;
            matrixD.M32 = num4*lightDirection.Y;
            matrixD.M42 = num5*lightDirection.Y;
            matrixD.M13 = num2*lightDirection.Z;
            matrixD.M23 = num3*lightDirection.Z;
            matrixD.M33 = num4*lightDirection.Z + num1;
            matrixD.M43 = num5*lightDirection.Z;
            matrixD.M14 = 0.0;
            matrixD.M24 = 0.0;
            matrixD.M34 = 0.0;
            matrixD.M44 = num1;
            return matrixD;
        }

        public static void CreateShadow(ref Vector3D lightDirection, ref Plane plane, out MatrixD result)
        {
            Plane result1;
            Plane.Normalize(ref plane, out result1);
            double num1 = (double) result1.Normal.X*lightDirection.X + (double) result1.Normal.Y*lightDirection.Y +
                          (double) result1.Normal.Z*lightDirection.Z;
            double num2 = -(double) result1.Normal.X;
            double num3 = -(double) result1.Normal.Y;
            double num4 = -(double) result1.Normal.Z;
            double num5 = -(double) result1.D;
            result.M11 = num2*lightDirection.X + num1;
            result.M21 = num3*lightDirection.X;
            result.M31 = num4*lightDirection.X;
            result.M41 = num5*lightDirection.X;
            result.M12 = num2*lightDirection.Y;
            result.M22 = num3*lightDirection.Y + num1;
            result.M32 = num4*lightDirection.Y;
            result.M42 = num5*lightDirection.Y;
            result.M13 = num2*lightDirection.Z;
            result.M23 = num3*lightDirection.Z;
            result.M33 = num4*lightDirection.Z + num1;
            result.M43 = num5*lightDirection.Z;
            result.M14 = 0.0;
            result.M24 = 0.0;
            result.M34 = 0.0;
            result.M44 = num1;
        }

        public static MatrixD CreateReflection(Plane value)
        {
            value.Normalize();
            double num1 = (double) value.Normal.X;
            double num2 = (double) value.Normal.Y;
            double num3 = (double) value.Normal.Z;
            double num4 = -2.0*num1;
            double num5 = -2.0*num2;
            double num6 = -2.0*num3;
            MatrixD matrixD;
            matrixD.M11 = num4*num1 + 1.0;
            matrixD.M12 = num5*num1;
            matrixD.M13 = num6*num1;
            matrixD.M14 = 0.0;
            matrixD.M21 = num4*num2;
            matrixD.M22 = num5*num2 + 1.0;
            matrixD.M23 = num6*num2;
            matrixD.M24 = 0.0;
            matrixD.M31 = num4*num3;
            matrixD.M32 = num5*num3;
            matrixD.M33 = num6*num3 + 1.0;
            matrixD.M34 = 0.0;
            matrixD.M41 = num4*(double) value.D;
            matrixD.M42 = num5*(double) value.D;
            matrixD.M43 = num6*(double) value.D;
            matrixD.M44 = 1.0;
            return matrixD;
        }

        public static void CreateReflection(ref Plane value, out MatrixD result)
        {
            Plane result1;
            Plane.Normalize(ref value, out result1);
            value.Normalize();
            double num1 = (double) result1.Normal.X;
            double num2 = (double) result1.Normal.Y;
            double num3 = (double) result1.Normal.Z;
            double num4 = -2.0*num1;
            double num5 = -2.0*num2;
            double num6 = -2.0*num3;
            result.M11 = num4*num1 + 1.0;
            result.M12 = num5*num1;
            result.M13 = num6*num1;
            result.M14 = 0.0;
            result.M21 = num4*num2;
            result.M22 = num5*num2 + 1.0;
            result.M23 = num6*num2;
            result.M24 = 0.0;
            result.M31 = num4*num3;
            result.M32 = num5*num3;
            result.M33 = num6*num3 + 1.0;
            result.M34 = 0.0;
            result.M41 = num4*(double) result1.D;
            result.M42 = num5*(double) result1.D;
            result.M43 = num6*(double) result1.D;
            result.M44 = 1.0;
        }

        public static MatrixD Transform(MatrixD value, Quaternion rotation)
        {
            double num1 = (double) rotation.X + (double) rotation.X;
            double num2 = (double) rotation.Y + (double) rotation.Y;
            double num3 = (double) rotation.Z + (double) rotation.Z;
            double num4 = (double) rotation.W*num1;
            double num5 = (double) rotation.W*num2;
            double num6 = (double) rotation.W*num3;
            double num7 = (double) rotation.X*num1;
            double num8 = (double) rotation.X*num2;
            double num9 = (double) rotation.X*num3;
            double num10 = (double) rotation.Y*num2;
            double num11 = (double) rotation.Y*num3;
            double num12 = (double) rotation.Z*num3;
            double num13 = 1.0 - num10 - num12;
            double num14 = num8 - num6;
            double num15 = num9 + num5;
            double num16 = num8 + num6;
            double num17 = 1.0 - num7 - num12;
            double num18 = num11 - num4;
            double num19 = num9 - num5;
            double num20 = num11 + num4;
            double num21 = 1.0 - num7 - num10;
            MatrixD matrixD;
            matrixD.M11 = value.M11*num13 + value.M12*num14 + value.M13*num15;
            matrixD.M12 = value.M11*num16 + value.M12*num17 + value.M13*num18;
            matrixD.M13 = value.M11*num19 + value.M12*num20 + value.M13*num21;
            matrixD.M14 = value.M14;
            matrixD.M21 = value.M21*num13 + value.M22*num14 + value.M23*num15;
            matrixD.M22 = value.M21*num16 + value.M22*num17 + value.M23*num18;
            matrixD.M23 = value.M21*num19 + value.M22*num20 + value.M23*num21;
            matrixD.M24 = value.M24;
            matrixD.M31 = value.M31*num13 + value.M32*num14 + value.M33*num15;
            matrixD.M32 = value.M31*num16 + value.M32*num17 + value.M33*num18;
            matrixD.M33 = value.M31*num19 + value.M32*num20 + value.M33*num21;
            matrixD.M34 = value.M34;
            matrixD.M41 = value.M41*num13 + value.M42*num14 + value.M43*num15;
            matrixD.M42 = value.M41*num16 + value.M42*num17 + value.M43*num18;
            matrixD.M43 = value.M41*num19 + value.M42*num20 + value.M43*num21;
            matrixD.M44 = value.M44;
            return matrixD;
        }

        public static void Transform(ref MatrixD value, ref Quaternion rotation, out MatrixD result)
        {
            double num1 = (double) rotation.X + (double) rotation.X;
            double num2 = (double) rotation.Y + (double) rotation.Y;
            double num3 = (double) rotation.Z + (double) rotation.Z;
            double num4 = (double) rotation.W*num1;
            double num5 = (double) rotation.W*num2;
            double num6 = (double) rotation.W*num3;
            double num7 = (double) rotation.X*num1;
            double num8 = (double) rotation.X*num2;
            double num9 = (double) rotation.X*num3;
            double num10 = (double) rotation.Y*num2;
            double num11 = (double) rotation.Y*num3;
            double num12 = (double) rotation.Z*num3;
            double num13 = 1.0 - num10 - num12;
            double num14 = num8 - num6;
            double num15 = num9 + num5;
            double num16 = num8 + num6;
            double num17 = 1.0 - num7 - num12;
            double num18 = num11 - num4;
            double num19 = num9 - num5;
            double num20 = num11 + num4;
            double num21 = 1.0 - num7 - num10;
            double num22 = value.M11*num13 + value.M12*num14 + value.M13*num15;
            double num23 = value.M11*num16 + value.M12*num17 + value.M13*num18;
            double num24 = value.M11*num19 + value.M12*num20 + value.M13*num21;
            double num25 = value.M14;
            double num26 = value.M21*num13 + value.M22*num14 + value.M23*num15;
            double num27 = value.M21*num16 + value.M22*num17 + value.M23*num18;
            double num28 = value.M21*num19 + value.M22*num20 + value.M23*num21;
            double num29 = value.M24;
            double num30 = value.M31*num13 + value.M32*num14 + value.M33*num15;
            double num31 = value.M31*num16 + value.M32*num17 + value.M33*num18;
            double num32 = value.M31*num19 + value.M32*num20 + value.M33*num21;
            double num33 = value.M34;
            double num34 = value.M41*num13 + value.M42*num14 + value.M43*num15;
            double num35 = value.M41*num16 + value.M42*num17 + value.M43*num18;
            double num36 = value.M41*num19 + value.M42*num20 + value.M43*num21;
            double num37 = value.M44;
            result.M11 = num22;
            result.M12 = num23;
            result.M13 = num24;
            result.M14 = num25;
            result.M21 = num26;
            result.M22 = num27;
            result.M23 = num28;
            result.M24 = num29;
            result.M31 = num30;
            result.M32 = num31;
            result.M33 = num32;
            result.M34 = num33;
            result.M41 = num34;
            result.M42 = num35;
            result.M43 = num36;
            result.M44 = num37;
        }

        public unsafe Vector4 GetRow(int row)
        {
            return default(Vector4);
        }

        public unsafe void SetRow(int row, Vector4 value)
        {
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return "{ " +
                   string.Format((IFormatProvider) currentCulture, "{{M11:{0} M12:{1} M13:{2} M14:{3}}} ",
                       (object) this.M11.ToString((IFormatProvider) currentCulture),
                       (object) this.M12.ToString((IFormatProvider) currentCulture),
                       (object) this.M13.ToString((IFormatProvider) currentCulture),
                       (object) this.M14.ToString((IFormatProvider) currentCulture)) +
                   string.Format((IFormatProvider) currentCulture, "{{M21:{0} M22:{1} M23:{2} M24:{3}}} ",
                       (object) this.M21.ToString((IFormatProvider) currentCulture),
                       (object) this.M22.ToString((IFormatProvider) currentCulture),
                       (object) this.M23.ToString((IFormatProvider) currentCulture),
                       (object) this.M24.ToString((IFormatProvider) currentCulture)) +
                   string.Format((IFormatProvider) currentCulture, "{{M31:{0} M32:{1} M33:{2} M34:{3}}} ",
                       (object) this.M31.ToString((IFormatProvider) currentCulture),
                       (object) this.M32.ToString((IFormatProvider) currentCulture),
                       (object) this.M33.ToString((IFormatProvider) currentCulture),
                       (object) this.M34.ToString((IFormatProvider) currentCulture)) +
                   string.Format((IFormatProvider) currentCulture, "{{M41:{0} M42:{1} M43:{2} M44:{3}}} ",
                       (object) this.M41.ToString((IFormatProvider) currentCulture),
                       (object) this.M42.ToString((IFormatProvider) currentCulture),
                       (object) this.M43.ToString((IFormatProvider) currentCulture),
                       (object) this.M44.ToString((IFormatProvider) currentCulture)) + "}";
        }

        public bool Equals(MatrixD other)
        {
            if (this.M11 == other.M11 && this.M22 == other.M22 && (this.M33 == other.M33 && this.M44 == other.M44) &&
                (this.M12 == other.M12 && this.M13 == other.M13 && (this.M14 == other.M14 && this.M21 == other.M21)) &&
                (this.M23 == other.M23 && this.M24 == other.M24 && (this.M31 == other.M31 && this.M32 == other.M32) &&
                 (this.M34 == other.M34 && this.M41 == other.M41 && this.M42 == other.M42)))
                return this.M43 == other.M43;
            else
                return false;
        }

        public bool EqualsFast(ref MatrixD other, double epsilon = 0.0001)
        {
            double num1 = this.M21 - other.M21;
            double num2 = this.M22 - other.M22;
            double num3 = this.M23 - other.M23;
            double num4 = this.M31 - other.M31;
            double num5 = this.M32 - other.M32;
            double num6 = this.M33 - other.M33;
            double num7 = this.M41 - other.M41;
            double num8 = this.M42 - other.M42;
            double num9 = this.M43 - other.M43;
            double num10 = epsilon*epsilon;
            return num1*num1 + num2*num2 + num3*num3 < num10 & num4*num4 + num5*num5 + num6*num6 < num10 &
                   num7*num7 + num8*num8 + num9*num9 < num10;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is MatrixD)
                flag = this.Equals((MatrixD) obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M14.GetHashCode() +
                   this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M24.GetHashCode() +
                   this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode() + this.M34.GetHashCode() +
                   this.M41.GetHashCode() + this.M42.GetHashCode() + this.M43.GetHashCode() + this.M44.GetHashCode();
        }

        public static MatrixD Transpose(MatrixD matrix)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix.M11;
            matrixD.M12 = matrix.M21;
            matrixD.M13 = matrix.M31;
            matrixD.M14 = matrix.M41;
            matrixD.M21 = matrix.M12;
            matrixD.M22 = matrix.M22;
            matrixD.M23 = matrix.M32;
            matrixD.M24 = matrix.M42;
            matrixD.M31 = matrix.M13;
            matrixD.M32 = matrix.M23;
            matrixD.M33 = matrix.M33;
            matrixD.M34 = matrix.M43;
            matrixD.M41 = matrix.M14;
            matrixD.M42 = matrix.M24;
            matrixD.M43 = matrix.M34;
            matrixD.M44 = matrix.M44;
            return matrixD;
        }

        public static void Transpose(ref MatrixD matrix, out MatrixD result)
        {
            double num1 = matrix.M11;
            double num2 = matrix.M12;
            double num3 = matrix.M13;
            double num4 = matrix.M14;
            double num5 = matrix.M21;
            double num6 = matrix.M22;
            double num7 = matrix.M23;
            double num8 = matrix.M24;
            double num9 = matrix.M31;
            double num10 = matrix.M32;
            double num11 = matrix.M33;
            double num12 = matrix.M34;
            double num13 = matrix.M41;
            double num14 = matrix.M42;
            double num15 = matrix.M43;
            double num16 = matrix.M44;
            result.M11 = num1;
            result.M12 = num5;
            result.M13 = num9;
            result.M14 = num13;
            result.M21 = num2;
            result.M22 = num6;
            result.M23 = num10;
            result.M24 = num14;
            result.M31 = num3;
            result.M32 = num7;
            result.M33 = num11;
            result.M34 = num15;
            result.M41 = num4;
            result.M42 = num8;
            result.M43 = num12;
            result.M44 = num16;
        }

        public double Determinant()
        {
            double num1 = this.M11;
            double num2 = this.M12;
            double num3 = this.M13;
            double num4 = this.M14;
            double num5 = this.M21;
            double num6 = this.M22;
            double num7 = this.M23;
            double num8 = this.M24;
            double num9 = this.M31;
            double num10 = this.M32;
            double num11 = this.M33;
            double num12 = this.M34;
            double num13 = this.M41;
            double num14 = this.M42;
            double num15 = this.M43;
            double num16 = this.M44;
            double num17 = num11*num16 - num12*num15;
            double num18 = num10*num16 - num12*num14;
            double num19 = num10*num15 - num11*num14;
            double num20 = num9*num16 - num12*num13;
            double num21 = num9*num15 - num11*num13;
            double num22 = num9*num14 - num10*num13;
            return num1*(num6*num17 - num7*num18 + num8*num19) - num2*(num5*num17 - num7*num20 + num8*num21) +
                   num3*(num5*num18 - num6*num20 + num8*num22) - num4*(num5*num19 - num6*num21 + num7*num22);
        }

        public static MatrixD Invert(MatrixD matrix)
        {
            double num1 = matrix.M11;
            double num2 = matrix.M12;
            double num3 = matrix.M13;
            double num4 = matrix.M14;
            double num5 = matrix.M21;
            double num6 = matrix.M22;
            double num7 = matrix.M23;
            double num8 = matrix.M24;
            double num9 = matrix.M31;
            double num10 = matrix.M32;
            double num11 = matrix.M33;
            double num12 = matrix.M34;
            double num13 = matrix.M41;
            double num14 = matrix.M42;
            double num15 = matrix.M43;
            double num16 = matrix.M44;
            double num17 = num11*num16 - num12*num15;
            double num18 = num10*num16 - num12*num14;
            double num19 = num10*num15 - num11*num14;
            double num20 = num9*num16 - num12*num13;
            double num21 = num9*num15 - num11*num13;
            double num22 = num9*num14 - num10*num13;
            double num23 = num6*num17 - num7*num18 + num8*num19;
            double num24 = -(num5*num17 - num7*num20 + num8*num21);
            double num25 = num5*num18 - num6*num20 + num8*num22;
            double num26 = -(num5*num19 - num6*num21 + num7*num22);
            double num27 = 1.0/(num1*num23 + num2*num24 + num3*num25 + num4*num26);
            MatrixD matrixD;
            matrixD.M11 = num23*num27;
            matrixD.M21 = num24*num27;
            matrixD.M31 = num25*num27;
            matrixD.M41 = num26*num27;
            matrixD.M12 = -(num2*num17 - num3*num18 + num4*num19)*num27;
            matrixD.M22 = (num1*num17 - num3*num20 + num4*num21)*num27;
            matrixD.M32 = -(num1*num18 - num2*num20 + num4*num22)*num27;
            matrixD.M42 = (num1*num19 - num2*num21 + num3*num22)*num27;
            double num28 = num7*num16 - num8*num15;
            double num29 = num6*num16 - num8*num14;
            double num30 = num6*num15 - num7*num14;
            double num31 = num5*num16 - num8*num13;
            double num32 = num5*num15 - num7*num13;
            double num33 = num5*num14 - num6*num13;
            matrixD.M13 = (num2*num28 - num3*num29 + num4*num30)*num27;
            matrixD.M23 = -(num1*num28 - num3*num31 + num4*num32)*num27;
            matrixD.M33 = (num1*num29 - num2*num31 + num4*num33)*num27;
            matrixD.M43 = -(num1*num30 - num2*num32 + num3*num33)*num27;
            double num34 = num7*num12 - num8*num11;
            double num35 = num6*num12 - num8*num10;
            double num36 = num6*num11 - num7*num10;
            double num37 = num5*num12 - num8*num9;
            double num38 = num5*num11 - num7*num9;
            double num39 = num5*num10 - num6*num9;
            matrixD.M14 = -(num2*num34 - num3*num35 + num4*num36)*num27;
            matrixD.M24 = (num1*num34 - num3*num37 + num4*num38)*num27;
            matrixD.M34 = -(num1*num35 - num2*num37 + num4*num39)*num27;
            matrixD.M44 = (num1*num36 - num2*num38 + num3*num39)*num27;
            return matrixD;
        }

        public static void Invert(ref MatrixD matrix, out MatrixD result)
        {
            double num1 = matrix.M11;
            double num2 = matrix.M12;
            double num3 = matrix.M13;
            double num4 = matrix.M14;
            double num5 = matrix.M21;
            double num6 = matrix.M22;
            double num7 = matrix.M23;
            double num8 = matrix.M24;
            double num9 = matrix.M31;
            double num10 = matrix.M32;
            double num11 = matrix.M33;
            double num12 = matrix.M34;
            double num13 = matrix.M41;
            double num14 = matrix.M42;
            double num15 = matrix.M43;
            double num16 = matrix.M44;
            double num17 = num11*num16 - num12*num15;
            double num18 = num10*num16 - num12*num14;
            double num19 = num10*num15 - num11*num14;
            double num20 = num9*num16 - num12*num13;
            double num21 = num9*num15 - num11*num13;
            double num22 = num9*num14 - num10*num13;
            double num23 = num6*num17 - num7*num18 + num8*num19;
            double num24 = -(num5*num17 - num7*num20 + num8*num21);
            double num25 = num5*num18 - num6*num20 + num8*num22;
            double num26 = -(num5*num19 - num6*num21 + num7*num22);
            double num27 = 1.0/(num1*num23 + num2*num24 + num3*num25 + num4*num26);
            result.M11 = num23*num27;
            result.M21 = num24*num27;
            result.M31 = num25*num27;
            result.M41 = num26*num27;
            result.M12 = -(num2*num17 - num3*num18 + num4*num19)*num27;
            result.M22 = (num1*num17 - num3*num20 + num4*num21)*num27;
            result.M32 = -(num1*num18 - num2*num20 + num4*num22)*num27;
            result.M42 = (num1*num19 - num2*num21 + num3*num22)*num27;
            double num28 = num7*num16 - num8*num15;
            double num29 = num6*num16 - num8*num14;
            double num30 = num6*num15 - num7*num14;
            double num31 = num5*num16 - num8*num13;
            double num32 = num5*num15 - num7*num13;
            double num33 = num5*num14 - num6*num13;
            result.M13 = (num2*num28 - num3*num29 + num4*num30)*num27;
            result.M23 = -(num1*num28 - num3*num31 + num4*num32)*num27;
            result.M33 = (num1*num29 - num2*num31 + num4*num33)*num27;
            result.M43 = -(num1*num30 - num2*num32 + num3*num33)*num27;
            double num34 = num7*num12 - num8*num11;
            double num35 = num6*num12 - num8*num10;
            double num36 = num6*num11 - num7*num10;
            double num37 = num5*num12 - num8*num9;
            double num38 = num5*num11 - num7*num9;
            double num39 = num5*num10 - num6*num9;
            result.M14 = -(num2*num34 - num3*num35 + num4*num36)*num27;
            result.M24 = (num1*num34 - num3*num37 + num4*num38)*num27;
            result.M34 = -(num1*num35 - num2*num37 + num4*num39)*num27;
            result.M44 = (num1*num36 - num2*num38 + num3*num39)*num27;
        }

        public static MatrixD Lerp(MatrixD matrix1, MatrixD matrix2, double amount)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11)*amount;
            matrixD.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12)*amount;
            matrixD.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13)*amount;
            matrixD.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14)*amount;
            matrixD.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21)*amount;
            matrixD.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22)*amount;
            matrixD.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23)*amount;
            matrixD.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24)*amount;
            matrixD.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31)*amount;
            matrixD.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32)*amount;
            matrixD.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33)*amount;
            matrixD.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34)*amount;
            matrixD.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41)*amount;
            matrixD.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42)*amount;
            matrixD.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43)*amount;
            matrixD.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44)*amount;
            return matrixD;
        }

        public static void Lerp(ref MatrixD matrix1, ref MatrixD matrix2, double amount, out MatrixD result)
        {
            result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11)*amount;
            result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12)*amount;
            result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13)*amount;
            result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14)*amount;
            result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21)*amount;
            result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22)*amount;
            result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23)*amount;
            result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24)*amount;
            result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31)*amount;
            result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32)*amount;
            result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33)*amount;
            result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34)*amount;
            result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41)*amount;
            result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42)*amount;
            result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43)*amount;
            result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44)*amount;
        }

        public static void Slerp(ref MatrixD matrix1, ref MatrixD matrix2, float amount, out MatrixD result)
        {
            Quaternion result1;
            Quaternion.CreateFromRotationMatrix(ref matrix1, out result1);
            Quaternion result2;
            Quaternion.CreateFromRotationMatrix(ref matrix2, out result2);
            Quaternion result3;
            Quaternion.Slerp(ref result1, ref result2, amount, out result3);
            MatrixD.CreateFromQuaternion(ref result3, out result);
            result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41)*(double) amount;
            result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42)*(double) amount;
            result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43)*(double) amount;
        }

        public static void SlerpScale(ref MatrixD matrix1, ref MatrixD matrix2, float amount, out MatrixD result)
        {
            Vector3D scale1 = matrix1.Scale;
            Vector3D scale2 = matrix2.Scale;
            if (scale1.LengthSquared() < 0.00999999977648258 || scale2.LengthSquared() < 0.00999999977648258)
            {
                result = MatrixD.Zero;
            }
            else
            {
                MatrixD matrix3 = MatrixD.Normalize(matrix1);
                MatrixD matrix4 = MatrixD.Normalize(matrix2);
                Quaternion result1;
                Quaternion.CreateFromRotationMatrix(ref matrix3, out result1);
                Quaternion result2;
                Quaternion.CreateFromRotationMatrix(ref matrix4, out result2);
                Quaternion result3;
                Quaternion.Slerp(ref result1, ref result2, amount, out result3);
                MatrixD.CreateFromQuaternion(ref result3, out result);
                Vector3D scale3 = Vector3D.Lerp(scale1, scale2, (double) amount);
                MatrixD.Rescale(ref result, ref scale3);
                result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41)*(double) amount;
                result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42)*(double) amount;
                result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43)*(double) amount;
            }
        }

        public static void Slerp(MatrixD matrix1, MatrixD matrix2, float amount, out MatrixD result)
        {
            MatrixD.Slerp(ref matrix1, ref matrix2, amount, out result);
        }

        public static MatrixD Slerp(MatrixD matrix1, MatrixD matrix2, float amount)
        {
            MatrixD result;
            MatrixD.Slerp(ref matrix1, ref matrix2, amount, out result);
            return result;
        }

        public static void SlerpScale(MatrixD matrix1, MatrixD matrix2, float amount, out MatrixD result)
        {
            MatrixD.SlerpScale(ref matrix1, ref matrix2, amount, out result);
        }

        public static MatrixD SlerpScale(MatrixD matrix1, MatrixD matrix2, float amount)
        {
            MatrixD result;
            MatrixD.SlerpScale(ref matrix1, ref matrix2, amount, out result);
            return result;
        }

        public static MatrixD Negate(MatrixD matrix)
        {
            MatrixD matrixD;
            matrixD.M11 = -matrix.M11;
            matrixD.M12 = -matrix.M12;
            matrixD.M13 = -matrix.M13;
            matrixD.M14 = -matrix.M14;
            matrixD.M21 = -matrix.M21;
            matrixD.M22 = -matrix.M22;
            matrixD.M23 = -matrix.M23;
            matrixD.M24 = -matrix.M24;
            matrixD.M31 = -matrix.M31;
            matrixD.M32 = -matrix.M32;
            matrixD.M33 = -matrix.M33;
            matrixD.M34 = -matrix.M34;
            matrixD.M41 = -matrix.M41;
            matrixD.M42 = -matrix.M42;
            matrixD.M43 = -matrix.M43;
            matrixD.M44 = -matrix.M44;
            return matrixD;
        }

        public static void Negate(ref MatrixD matrix, out MatrixD result)
        {
            result.M11 = -matrix.M11;
            result.M12 = -matrix.M12;
            result.M13 = -matrix.M13;
            result.M14 = -matrix.M14;
            result.M21 = -matrix.M21;
            result.M22 = -matrix.M22;
            result.M23 = -matrix.M23;
            result.M24 = -matrix.M24;
            result.M31 = -matrix.M31;
            result.M32 = -matrix.M32;
            result.M33 = -matrix.M33;
            result.M34 = -matrix.M34;
            result.M41 = -matrix.M41;
            result.M42 = -matrix.M42;
            result.M43 = -matrix.M43;
            result.M44 = -matrix.M44;
        }

        public static MatrixD Add(MatrixD matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11 + matrix2.M11;
            matrixD.M12 = matrix1.M12 + matrix2.M12;
            matrixD.M13 = matrix1.M13 + matrix2.M13;
            matrixD.M14 = matrix1.M14 + matrix2.M14;
            matrixD.M21 = matrix1.M21 + matrix2.M21;
            matrixD.M22 = matrix1.M22 + matrix2.M22;
            matrixD.M23 = matrix1.M23 + matrix2.M23;
            matrixD.M24 = matrix1.M24 + matrix2.M24;
            matrixD.M31 = matrix1.M31 + matrix2.M31;
            matrixD.M32 = matrix1.M32 + matrix2.M32;
            matrixD.M33 = matrix1.M33 + matrix2.M33;
            matrixD.M34 = matrix1.M34 + matrix2.M34;
            matrixD.M41 = matrix1.M41 + matrix2.M41;
            matrixD.M42 = matrix1.M42 + matrix2.M42;
            matrixD.M43 = matrix1.M43 + matrix2.M43;
            matrixD.M44 = matrix1.M44 + matrix2.M44;
            return matrixD;
        }

        public static void Add(ref MatrixD matrix1, ref MatrixD matrix2, out MatrixD result)
        {
            result.M11 = matrix1.M11 + matrix2.M11;
            result.M12 = matrix1.M12 + matrix2.M12;
            result.M13 = matrix1.M13 + matrix2.M13;
            result.M14 = matrix1.M14 + matrix2.M14;
            result.M21 = matrix1.M21 + matrix2.M21;
            result.M22 = matrix1.M22 + matrix2.M22;
            result.M23 = matrix1.M23 + matrix2.M23;
            result.M24 = matrix1.M24 + matrix2.M24;
            result.M31 = matrix1.M31 + matrix2.M31;
            result.M32 = matrix1.M32 + matrix2.M32;
            result.M33 = matrix1.M33 + matrix2.M33;
            result.M34 = matrix1.M34 + matrix2.M34;
            result.M41 = matrix1.M41 + matrix2.M41;
            result.M42 = matrix1.M42 + matrix2.M42;
            result.M43 = matrix1.M43 + matrix2.M43;
            result.M44 = matrix1.M44 + matrix2.M44;
        }

        public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix;
            matrix.M11 = matrix1.M11 - matrix2.M11;
            matrix.M12 = matrix1.M12 - matrix2.M12;
            matrix.M13 = matrix1.M13 - matrix2.M13;
            matrix.M14 = matrix1.M14 - matrix2.M14;
            matrix.M21 = matrix1.M21 - matrix2.M21;
            matrix.M22 = matrix1.M22 - matrix2.M22;
            matrix.M23 = matrix1.M23 - matrix2.M23;
            matrix.M24 = matrix1.M24 - matrix2.M24;
            matrix.M31 = matrix1.M31 - matrix2.M31;
            matrix.M32 = matrix1.M32 - matrix2.M32;
            matrix.M33 = matrix1.M33 - matrix2.M33;
            matrix.M34 = matrix1.M34 - matrix2.M34;
            matrix.M41 = matrix1.M41 - matrix2.M41;
            matrix.M42 = matrix1.M42 - matrix2.M42;
            matrix.M43 = matrix1.M43 - matrix2.M43;
            matrix.M44 = matrix1.M44 - matrix2.M44;
            return matrix;
        }

        public static void Subtract(ref MatrixD matrix1, ref MatrixD matrix2, out MatrixD result)
        {
            result.M11 = matrix1.M11 - matrix2.M11;
            result.M12 = matrix1.M12 - matrix2.M12;
            result.M13 = matrix1.M13 - matrix2.M13;
            result.M14 = matrix1.M14 - matrix2.M14;
            result.M21 = matrix1.M21 - matrix2.M21;
            result.M22 = matrix1.M22 - matrix2.M22;
            result.M23 = matrix1.M23 - matrix2.M23;
            result.M24 = matrix1.M24 - matrix2.M24;
            result.M31 = matrix1.M31 - matrix2.M31;
            result.M32 = matrix1.M32 - matrix2.M32;
            result.M33 = matrix1.M33 - matrix2.M33;
            result.M34 = matrix1.M34 - matrix2.M34;
            result.M41 = matrix1.M41 - matrix2.M41;
            result.M42 = matrix1.M42 - matrix2.M42;
            result.M43 = matrix1.M43 - matrix2.M43;
            result.M44 = matrix1.M44 - matrix2.M44;
        }

        public static MatrixD Multiply(MatrixD matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11*matrix2.M11 + matrix1.M12*matrix2.M21 + matrix1.M13*matrix2.M31 +
                          matrix1.M14*matrix2.M41;
            matrixD.M12 = matrix1.M11*matrix2.M12 + matrix1.M12*matrix2.M22 + matrix1.M13*matrix2.M32 +
                          matrix1.M14*matrix2.M42;
            matrixD.M13 = matrix1.M11*matrix2.M13 + matrix1.M12*matrix2.M23 + matrix1.M13*matrix2.M33 +
                          matrix1.M14*matrix2.M43;
            matrixD.M14 = matrix1.M11*matrix2.M14 + matrix1.M12*matrix2.M24 + matrix1.M13*matrix2.M34 +
                          matrix1.M14*matrix2.M44;
            matrixD.M21 = matrix1.M21*matrix2.M11 + matrix1.M22*matrix2.M21 + matrix1.M23*matrix2.M31 +
                          matrix1.M24*matrix2.M41;
            matrixD.M22 = matrix1.M21*matrix2.M12 + matrix1.M22*matrix2.M22 + matrix1.M23*matrix2.M32 +
                          matrix1.M24*matrix2.M42;
            matrixD.M23 = matrix1.M21*matrix2.M13 + matrix1.M22*matrix2.M23 + matrix1.M23*matrix2.M33 +
                          matrix1.M24*matrix2.M43;
            matrixD.M24 = matrix1.M21*matrix2.M14 + matrix1.M22*matrix2.M24 + matrix1.M23*matrix2.M34 +
                          matrix1.M24*matrix2.M44;
            matrixD.M31 = matrix1.M31*matrix2.M11 + matrix1.M32*matrix2.M21 + matrix1.M33*matrix2.M31 +
                          matrix1.M34*matrix2.M41;
            matrixD.M32 = matrix1.M31*matrix2.M12 + matrix1.M32*matrix2.M22 + matrix1.M33*matrix2.M32 +
                          matrix1.M34*matrix2.M42;
            matrixD.M33 = matrix1.M31*matrix2.M13 + matrix1.M32*matrix2.M23 + matrix1.M33*matrix2.M33 +
                          matrix1.M34*matrix2.M43;
            matrixD.M34 = matrix1.M31*matrix2.M14 + matrix1.M32*matrix2.M24 + matrix1.M33*matrix2.M34 +
                          matrix1.M34*matrix2.M44;
            matrixD.M41 = matrix1.M41*matrix2.M11 + matrix1.M42*matrix2.M21 + matrix1.M43*matrix2.M31 +
                          matrix1.M44*matrix2.M41;
            matrixD.M42 = matrix1.M41*matrix2.M12 + matrix1.M42*matrix2.M22 + matrix1.M43*matrix2.M32 +
                          matrix1.M44*matrix2.M42;
            matrixD.M43 = matrix1.M41*matrix2.M13 + matrix1.M42*matrix2.M23 + matrix1.M43*matrix2.M33 +
                          matrix1.M44*matrix2.M43;
            matrixD.M44 = matrix1.M41*matrix2.M14 + matrix1.M42*matrix2.M24 + matrix1.M43*matrix2.M34 +
                          matrix1.M44*matrix2.M44;
            return matrixD;
        }

        public static MatrixD Multiply(MatrixD matrix1, Matrix matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11*(double) matrix2.M11 + matrix1.M12*(double) matrix2.M21 +
                          matrix1.M13*(double) matrix2.M31 + matrix1.M14*(double) matrix2.M41;
            matrixD.M12 = matrix1.M11*(double) matrix2.M12 + matrix1.M12*(double) matrix2.M22 +
                          matrix1.M13*(double) matrix2.M32 + matrix1.M14*(double) matrix2.M42;
            matrixD.M13 = matrix1.M11*(double) matrix2.M13 + matrix1.M12*(double) matrix2.M23 +
                          matrix1.M13*(double) matrix2.M33 + matrix1.M14*(double) matrix2.M43;
            matrixD.M14 = matrix1.M11*(double) matrix2.M14 + matrix1.M12*(double) matrix2.M24 +
                          matrix1.M13*(double) matrix2.M34 + matrix1.M14*(double) matrix2.M44;
            matrixD.M21 = matrix1.M21*(double) matrix2.M11 + matrix1.M22*(double) matrix2.M21 +
                          matrix1.M23*(double) matrix2.M31 + matrix1.M24*(double) matrix2.M41;
            matrixD.M22 = matrix1.M21*(double) matrix2.M12 + matrix1.M22*(double) matrix2.M22 +
                          matrix1.M23*(double) matrix2.M32 + matrix1.M24*(double) matrix2.M42;
            matrixD.M23 = matrix1.M21*(double) matrix2.M13 + matrix1.M22*(double) matrix2.M23 +
                          matrix1.M23*(double) matrix2.M33 + matrix1.M24*(double) matrix2.M43;
            matrixD.M24 = matrix1.M21*(double) matrix2.M14 + matrix1.M22*(double) matrix2.M24 +
                          matrix1.M23*(double) matrix2.M34 + matrix1.M24*(double) matrix2.M44;
            matrixD.M31 = matrix1.M31*(double) matrix2.M11 + matrix1.M32*(double) matrix2.M21 +
                          matrix1.M33*(double) matrix2.M31 + matrix1.M34*(double) matrix2.M41;
            matrixD.M32 = matrix1.M31*(double) matrix2.M12 + matrix1.M32*(double) matrix2.M22 +
                          matrix1.M33*(double) matrix2.M32 + matrix1.M34*(double) matrix2.M42;
            matrixD.M33 = matrix1.M31*(double) matrix2.M13 + matrix1.M32*(double) matrix2.M23 +
                          matrix1.M33*(double) matrix2.M33 + matrix1.M34*(double) matrix2.M43;
            matrixD.M34 = matrix1.M31*(double) matrix2.M14 + matrix1.M32*(double) matrix2.M24 +
                          matrix1.M33*(double) matrix2.M34 + matrix1.M34*(double) matrix2.M44;
            matrixD.M41 = matrix1.M41*(double) matrix2.M11 + matrix1.M42*(double) matrix2.M21 +
                          matrix1.M43*(double) matrix2.M31 + matrix1.M44*(double) matrix2.M41;
            matrixD.M42 = matrix1.M41*(double) matrix2.M12 + matrix1.M42*(double) matrix2.M22 +
                          matrix1.M43*(double) matrix2.M32 + matrix1.M44*(double) matrix2.M42;
            matrixD.M43 = matrix1.M41*(double) matrix2.M13 + matrix1.M42*(double) matrix2.M23 +
                          matrix1.M43*(double) matrix2.M33 + matrix1.M44*(double) matrix2.M43;
            matrixD.M44 = matrix1.M41*(double) matrix2.M14 + matrix1.M42*(double) matrix2.M24 +
                          matrix1.M43*(double) matrix2.M34 + matrix1.M44*(double) matrix2.M44;
            return matrixD;
        }

        public static void Multiply(ref MatrixD matrix1, ref Matrix matrix2, out MatrixD result)
        {
            double num1 = matrix1.M11*(double) matrix2.M11 + matrix1.M12*(double) matrix2.M21 +
                          matrix1.M13*(double) matrix2.M31 + matrix1.M14*(double) matrix2.M41;
            double num2 = matrix1.M11*(double) matrix2.M12 + matrix1.M12*(double) matrix2.M22 +
                          matrix1.M13*(double) matrix2.M32 + matrix1.M14*(double) matrix2.M42;
            double num3 = matrix1.M11*(double) matrix2.M13 + matrix1.M12*(double) matrix2.M23 +
                          matrix1.M13*(double) matrix2.M33 + matrix1.M14*(double) matrix2.M43;
            double num4 = matrix1.M11*(double) matrix2.M14 + matrix1.M12*(double) matrix2.M24 +
                          matrix1.M13*(double) matrix2.M34 + matrix1.M14*(double) matrix2.M44;
            double num5 = matrix1.M21*(double) matrix2.M11 + matrix1.M22*(double) matrix2.M21 +
                          matrix1.M23*(double) matrix2.M31 + matrix1.M24*(double) matrix2.M41;
            double num6 = matrix1.M21*(double) matrix2.M12 + matrix1.M22*(double) matrix2.M22 +
                          matrix1.M23*(double) matrix2.M32 + matrix1.M24*(double) matrix2.M42;
            double num7 = matrix1.M21*(double) matrix2.M13 + matrix1.M22*(double) matrix2.M23 +
                          matrix1.M23*(double) matrix2.M33 + matrix1.M24*(double) matrix2.M43;
            double num8 = matrix1.M21*(double) matrix2.M14 + matrix1.M22*(double) matrix2.M24 +
                          matrix1.M23*(double) matrix2.M34 + matrix1.M24*(double) matrix2.M44;
            double num9 = matrix1.M31*(double) matrix2.M11 + matrix1.M32*(double) matrix2.M21 +
                          matrix1.M33*(double) matrix2.M31 + matrix1.M34*(double) matrix2.M41;
            double num10 = matrix1.M31*(double) matrix2.M12 + matrix1.M32*(double) matrix2.M22 +
                           matrix1.M33*(double) matrix2.M32 + matrix1.M34*(double) matrix2.M42;
            double num11 = matrix1.M31*(double) matrix2.M13 + matrix1.M32*(double) matrix2.M23 +
                           matrix1.M33*(double) matrix2.M33 + matrix1.M34*(double) matrix2.M43;
            double num12 = matrix1.M31*(double) matrix2.M14 + matrix1.M32*(double) matrix2.M24 +
                           matrix1.M33*(double) matrix2.M34 + matrix1.M34*(double) matrix2.M44;
            double num13 = matrix1.M41*(double) matrix2.M11 + matrix1.M42*(double) matrix2.M21 +
                           matrix1.M43*(double) matrix2.M31 + matrix1.M44*(double) matrix2.M41;
            double num14 = matrix1.M41*(double) matrix2.M12 + matrix1.M42*(double) matrix2.M22 +
                           matrix1.M43*(double) matrix2.M32 + matrix1.M44*(double) matrix2.M42;
            double num15 = matrix1.M41*(double) matrix2.M13 + matrix1.M42*(double) matrix2.M23 +
                           matrix1.M43*(double) matrix2.M33 + matrix1.M44*(double) matrix2.M43;
            double num16 = matrix1.M41*(double) matrix2.M14 + matrix1.M42*(double) matrix2.M24 +
                           matrix1.M43*(double) matrix2.M34 + matrix1.M44*(double) matrix2.M44;
            result.M11 = num1;
            result.M12 = num2;
            result.M13 = num3;
            result.M14 = num4;
            result.M21 = num5;
            result.M22 = num6;
            result.M23 = num7;
            result.M24 = num8;
            result.M31 = num9;
            result.M32 = num10;
            result.M33 = num11;
            result.M34 = num12;
            result.M41 = num13;
            result.M42 = num14;
            result.M43 = num15;
            result.M44 = num16;
        }

        public static void Multiply(ref Matrix matrix1, ref MatrixD matrix2, out MatrixD result)
        {
            double num1 = (double) matrix1.M11*matrix2.M11 + (double) matrix1.M12*matrix2.M21 +
                          (double) matrix1.M13*matrix2.M31 + (double) matrix1.M14*matrix2.M41;
            double num2 = (double) matrix1.M11*matrix2.M12 + (double) matrix1.M12*matrix2.M22 +
                          (double) matrix1.M13*matrix2.M32 + (double) matrix1.M14*matrix2.M42;
            double num3 = (double) matrix1.M11*matrix2.M13 + (double) matrix1.M12*matrix2.M23 +
                          (double) matrix1.M13*matrix2.M33 + (double) matrix1.M14*matrix2.M43;
            double num4 = (double) matrix1.M11*matrix2.M14 + (double) matrix1.M12*matrix2.M24 +
                          (double) matrix1.M13*matrix2.M34 + (double) matrix1.M14*matrix2.M44;
            double num5 = (double) matrix1.M21*matrix2.M11 + (double) matrix1.M22*matrix2.M21 +
                          (double) matrix1.M23*matrix2.M31 + (double) matrix1.M24*matrix2.M41;
            double num6 = (double) matrix1.M21*matrix2.M12 + (double) matrix1.M22*matrix2.M22 +
                          (double) matrix1.M23*matrix2.M32 + (double) matrix1.M24*matrix2.M42;
            double num7 = (double) matrix1.M21*matrix2.M13 + (double) matrix1.M22*matrix2.M23 +
                          (double) matrix1.M23*matrix2.M33 + (double) matrix1.M24*matrix2.M43;
            double num8 = (double) matrix1.M21*matrix2.M14 + (double) matrix1.M22*matrix2.M24 +
                          (double) matrix1.M23*matrix2.M34 + (double) matrix1.M24*matrix2.M44;
            double num9 = (double) matrix1.M31*matrix2.M11 + (double) matrix1.M32*matrix2.M21 +
                          (double) matrix1.M33*matrix2.M31 + (double) matrix1.M34*matrix2.M41;
            double num10 = (double) matrix1.M31*matrix2.M12 + (double) matrix1.M32*matrix2.M22 +
                           (double) matrix1.M33*matrix2.M32 + (double) matrix1.M34*matrix2.M42;
            double num11 = (double) matrix1.M31*matrix2.M13 + (double) matrix1.M32*matrix2.M23 +
                           (double) matrix1.M33*matrix2.M33 + (double) matrix1.M34*matrix2.M43;
            double num12 = (double) matrix1.M31*matrix2.M14 + (double) matrix1.M32*matrix2.M24 +
                           (double) matrix1.M33*matrix2.M34 + (double) matrix1.M34*matrix2.M44;
            double num13 = (double) matrix1.M41*matrix2.M11 + (double) matrix1.M42*matrix2.M21 +
                           (double) matrix1.M43*matrix2.M31 + (double) matrix1.M44*matrix2.M41;
            double num14 = (double) matrix1.M41*matrix2.M12 + (double) matrix1.M42*matrix2.M22 +
                           (double) matrix1.M43*matrix2.M32 + (double) matrix1.M44*matrix2.M42;
            double num15 = (double) matrix1.M41*matrix2.M13 + (double) matrix1.M42*matrix2.M23 +
                           (double) matrix1.M43*matrix2.M33 + (double) matrix1.M44*matrix2.M43;
            double num16 = (double) matrix1.M41*matrix2.M14 + (double) matrix1.M42*matrix2.M24 +
                           (double) matrix1.M43*matrix2.M34 + (double) matrix1.M44*matrix2.M44;
            result.M11 = num1;
            result.M12 = num2;
            result.M13 = num3;
            result.M14 = num4;
            result.M21 = num5;
            result.M22 = num6;
            result.M23 = num7;
            result.M24 = num8;
            result.M31 = num9;
            result.M32 = num10;
            result.M33 = num11;
            result.M34 = num12;
            result.M41 = num13;
            result.M42 = num14;
            result.M43 = num15;
            result.M44 = num16;
        }

        public static void Multiply(ref MatrixD matrix1, ref MatrixD matrix2, out MatrixD result)
        {
            double num1 = matrix1.M11*matrix2.M11 + matrix1.M12*matrix2.M21 + matrix1.M13*matrix2.M31 +
                          matrix1.M14*matrix2.M41;
            double num2 = matrix1.M11*matrix2.M12 + matrix1.M12*matrix2.M22 + matrix1.M13*matrix2.M32 +
                          matrix1.M14*matrix2.M42;
            double num3 = matrix1.M11*matrix2.M13 + matrix1.M12*matrix2.M23 + matrix1.M13*matrix2.M33 +
                          matrix1.M14*matrix2.M43;
            double num4 = matrix1.M11*matrix2.M14 + matrix1.M12*matrix2.M24 + matrix1.M13*matrix2.M34 +
                          matrix1.M14*matrix2.M44;
            double num5 = matrix1.M21*matrix2.M11 + matrix1.M22*matrix2.M21 + matrix1.M23*matrix2.M31 +
                          matrix1.M24*matrix2.M41;
            double num6 = matrix1.M21*matrix2.M12 + matrix1.M22*matrix2.M22 + matrix1.M23*matrix2.M32 +
                          matrix1.M24*matrix2.M42;
            double num7 = matrix1.M21*matrix2.M13 + matrix1.M22*matrix2.M23 + matrix1.M23*matrix2.M33 +
                          matrix1.M24*matrix2.M43;
            double num8 = matrix1.M21*matrix2.M14 + matrix1.M22*matrix2.M24 + matrix1.M23*matrix2.M34 +
                          matrix1.M24*matrix2.M44;
            double num9 = matrix1.M31*matrix2.M11 + matrix1.M32*matrix2.M21 + matrix1.M33*matrix2.M31 +
                          matrix1.M34*matrix2.M41;
            double num10 = matrix1.M31*matrix2.M12 + matrix1.M32*matrix2.M22 + matrix1.M33*matrix2.M32 +
                           matrix1.M34*matrix2.M42;
            double num11 = matrix1.M31*matrix2.M13 + matrix1.M32*matrix2.M23 + matrix1.M33*matrix2.M33 +
                           matrix1.M34*matrix2.M43;
            double num12 = matrix1.M31*matrix2.M14 + matrix1.M32*matrix2.M24 + matrix1.M33*matrix2.M34 +
                           matrix1.M34*matrix2.M44;
            double num13 = matrix1.M41*matrix2.M11 + matrix1.M42*matrix2.M21 + matrix1.M43*matrix2.M31 +
                           matrix1.M44*matrix2.M41;
            double num14 = matrix1.M41*matrix2.M12 + matrix1.M42*matrix2.M22 + matrix1.M43*matrix2.M32 +
                           matrix1.M44*matrix2.M42;
            double num15 = matrix1.M41*matrix2.M13 + matrix1.M42*matrix2.M23 + matrix1.M43*matrix2.M33 +
                           matrix1.M44*matrix2.M43;
            double num16 = matrix1.M41*matrix2.M14 + matrix1.M42*matrix2.M24 + matrix1.M43*matrix2.M34 +
                           matrix1.M44*matrix2.M44;
            result.M11 = num1;
            result.M12 = num2;
            result.M13 = num3;
            result.M14 = num4;
            result.M21 = num5;
            result.M22 = num6;
            result.M23 = num7;
            result.M24 = num8;
            result.M31 = num9;
            result.M32 = num10;
            result.M33 = num11;
            result.M34 = num12;
            result.M41 = num13;
            result.M42 = num14;
            result.M43 = num15;
            result.M44 = num16;
        }

        public static MatrixD Multiply(MatrixD matrix1, double scaleFactor)
        {
            double num = scaleFactor;
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11*num;
            matrixD.M12 = matrix1.M12*num;
            matrixD.M13 = matrix1.M13*num;
            matrixD.M14 = matrix1.M14*num;
            matrixD.M21 = matrix1.M21*num;
            matrixD.M22 = matrix1.M22*num;
            matrixD.M23 = matrix1.M23*num;
            matrixD.M24 = matrix1.M24*num;
            matrixD.M31 = matrix1.M31*num;
            matrixD.M32 = matrix1.M32*num;
            matrixD.M33 = matrix1.M33*num;
            matrixD.M34 = matrix1.M34*num;
            matrixD.M41 = matrix1.M41*num;
            matrixD.M42 = matrix1.M42*num;
            matrixD.M43 = matrix1.M43*num;
            matrixD.M44 = matrix1.M44*num;
            return matrixD;
        }

        public static void Multiply(ref MatrixD matrix1, double scaleFactor, out MatrixD result)
        {
            double num = scaleFactor;
            result.M11 = matrix1.M11*num;
            result.M12 = matrix1.M12*num;
            result.M13 = matrix1.M13*num;
            result.M14 = matrix1.M14*num;
            result.M21 = matrix1.M21*num;
            result.M22 = matrix1.M22*num;
            result.M23 = matrix1.M23*num;
            result.M24 = matrix1.M24*num;
            result.M31 = matrix1.M31*num;
            result.M32 = matrix1.M32*num;
            result.M33 = matrix1.M33*num;
            result.M34 = matrix1.M34*num;
            result.M41 = matrix1.M41*num;
            result.M42 = matrix1.M42*num;
            result.M43 = matrix1.M43*num;
            result.M44 = matrix1.M44*num;
        }

        public static MatrixD Divide(MatrixD matrix1, MatrixD matrix2)
        {
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11/matrix2.M11;
            matrixD.M12 = matrix1.M12/matrix2.M12;
            matrixD.M13 = matrix1.M13/matrix2.M13;
            matrixD.M14 = matrix1.M14/matrix2.M14;
            matrixD.M21 = matrix1.M21/matrix2.M21;
            matrixD.M22 = matrix1.M22/matrix2.M22;
            matrixD.M23 = matrix1.M23/matrix2.M23;
            matrixD.M24 = matrix1.M24/matrix2.M24;
            matrixD.M31 = matrix1.M31/matrix2.M31;
            matrixD.M32 = matrix1.M32/matrix2.M32;
            matrixD.M33 = matrix1.M33/matrix2.M33;
            matrixD.M34 = matrix1.M34/matrix2.M34;
            matrixD.M41 = matrix1.M41/matrix2.M41;
            matrixD.M42 = matrix1.M42/matrix2.M42;
            matrixD.M43 = matrix1.M43/matrix2.M43;
            matrixD.M44 = matrix1.M44/matrix2.M44;
            return matrixD;
        }

        public static void Divide(ref MatrixD matrix1, ref MatrixD matrix2, out MatrixD result)
        {
            result.M11 = matrix1.M11/matrix2.M11;
            result.M12 = matrix1.M12/matrix2.M12;
            result.M13 = matrix1.M13/matrix2.M13;
            result.M14 = matrix1.M14/matrix2.M14;
            result.M21 = matrix1.M21/matrix2.M21;
            result.M22 = matrix1.M22/matrix2.M22;
            result.M23 = matrix1.M23/matrix2.M23;
            result.M24 = matrix1.M24/matrix2.M24;
            result.M31 = matrix1.M31/matrix2.M31;
            result.M32 = matrix1.M32/matrix2.M32;
            result.M33 = matrix1.M33/matrix2.M33;
            result.M34 = matrix1.M34/matrix2.M34;
            result.M41 = matrix1.M41/matrix2.M41;
            result.M42 = matrix1.M42/matrix2.M42;
            result.M43 = matrix1.M43/matrix2.M43;
            result.M44 = matrix1.M44/matrix2.M44;
        }

        public static MatrixD Divide(MatrixD matrix1, double divider)
        {
            double num = 1.0/divider;
            MatrixD matrixD;
            matrixD.M11 = matrix1.M11*num;
            matrixD.M12 = matrix1.M12*num;
            matrixD.M13 = matrix1.M13*num;
            matrixD.M14 = matrix1.M14*num;
            matrixD.M21 = matrix1.M21*num;
            matrixD.M22 = matrix1.M22*num;
            matrixD.M23 = matrix1.M23*num;
            matrixD.M24 = matrix1.M24*num;
            matrixD.M31 = matrix1.M31*num;
            matrixD.M32 = matrix1.M32*num;
            matrixD.M33 = matrix1.M33*num;
            matrixD.M34 = matrix1.M34*num;
            matrixD.M41 = matrix1.M41*num;
            matrixD.M42 = matrix1.M42*num;
            matrixD.M43 = matrix1.M43*num;
            matrixD.M44 = matrix1.M44*num;
            return matrixD;
        }

        public static void Divide(ref MatrixD matrix1, double divider, out MatrixD result)
        {
            double num = 1.0/divider;
            result.M11 = matrix1.M11*num;
            result.M12 = matrix1.M12*num;
            result.M13 = matrix1.M13*num;
            result.M14 = matrix1.M14*num;
            result.M21 = matrix1.M21*num;
            result.M22 = matrix1.M22*num;
            result.M23 = matrix1.M23*num;
            result.M24 = matrix1.M24*num;
            result.M31 = matrix1.M31*num;
            result.M32 = matrix1.M32*num;
            result.M33 = matrix1.M33*num;
            result.M34 = matrix1.M34*num;
            result.M41 = matrix1.M41*num;
            result.M42 = matrix1.M42*num;
            result.M43 = matrix1.M43*num;
            result.M44 = matrix1.M44*num;
        }

        public MatrixD GetOrientation()
        {
            MatrixD matrixD = MatrixD.Identity;
            matrixD.Forward = this.Forward;
            matrixD.Up = this.Up;
            matrixD.Right = this.Right;
            return matrixD;
        }

        public bool IsValid()
        {
            return
                DoubleExtensions.IsValid(this.M11 + this.M12 + this.M13 + this.M14 + this.M21 + this.M22 + this.M23 +
                                         this.M24 + this.M31 + this.M32 + this.M33 + this.M34 + this.M41 + this.M42 +
                                         this.M43 + this.M44);
        }

        public bool IsNan()
        {
            return
                double.IsNaN(this.M11 + this.M12 + this.M13 + this.M14 + this.M21 + this.M22 + this.M23 + this.M24 +
                             this.M31 + this.M32 + this.M33 + this.M34 + this.M41 + this.M42 + this.M43 + this.M44);
        }

        public bool IsRotation()
        {
            double num = 0.00999999977648258;
            return this.HasNoTranslationOrPerspective() && Math.Abs(this.Right.Dot(this.Up)) <= num &&
                   (Math.Abs(this.Right.Dot(this.Backward)) <= num && Math.Abs(this.Up.Dot(this.Backward)) <= num) &&
                   (Math.Abs(this.Right.LengthSquared() - 1.0) <= num && Math.Abs(this.Up.LengthSquared() - 1.0) <= num &&
                    Math.Abs(this.Backward.LengthSquared() - 1.0) <= num);
        }

        public bool HasNoTranslationOrPerspective()
        {
            double num = 9.99999974737875E-05;
            return this.M41 + this.M42 + this.M43 + this.M34 + this.M24 + this.M14 <= num &&
                   Math.Abs(this.M44 - 1.0) <= num;
        }

        public static MatrixD CreateFromDir(Vector3D dir)
        {
            Vector3D vector2 = new Vector3D(0.0, 0.0, 1.0);
            double num = dir.Z;
            Vector3D vector3D;
            if (num > -0.99999 && num < 0.99999)
            {
                vector2 = Vector3D.Normalize(vector2 - dir*num);
                vector3D = Vector3D.Cross(dir, vector2);
            }
            else
            {
                vector2 = new Vector3D(dir.Z, 0.0, -dir.X);
                vector3D = new Vector3D(0.0, 1.0, 0.0);
            }
            MatrixD matrixD = MatrixD.Identity;
            matrixD.Right = vector2;
            matrixD.Up = vector3D;
            matrixD.Forward = dir;
            return matrixD;
        }

        public static MatrixD CreateFromDir(Vector3D dir, Vector3D suggestedUp)
        {
            Vector3D up = Vector3D.Cross(Vector3D.Cross(dir, suggestedUp), dir);
            return MatrixD.CreateWorld(Vector3D.Zero, dir, up);
        }

        public static MatrixD Normalize(MatrixD matrix)
        {
            MatrixD matrixD = matrix;
            matrixD.Right = Vector3D.Normalize(matrixD.Right);
            matrixD.Up = Vector3D.Normalize(matrixD.Up);
            matrixD.Forward = Vector3D.Normalize(matrixD.Forward);
            return matrixD;
        }

        public static MatrixD Orthogonalize(MatrixD rotationMatrix)
        {
            MatrixD matrixD = rotationMatrix;
            matrixD.Right = Vector3D.Normalize(matrixD.Right);
            matrixD.Up = Vector3D.Normalize(matrixD.Up - matrixD.Right*matrixD.Up.Dot(matrixD.Right));
            matrixD.Backward =
                Vector3D.Normalize(matrixD.Backward - matrixD.Right*matrixD.Backward.Dot(matrixD.Right) -
                                   matrixD.Up*matrixD.Backward.Dot(matrixD.Up));
            return matrixD;
        }

        public static MatrixD AlignRotationToAxes(ref MatrixD toAlign, ref MatrixD axisDefinitionMatrix)
        {
            MatrixD matrixD = MatrixD.Identity;
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            double num1 = toAlign.Right.Dot(axisDefinitionMatrix.Right);
            double num2 = toAlign.Right.Dot(axisDefinitionMatrix.Up);
            double num3 = toAlign.Right.Dot(axisDefinitionMatrix.Backward);
            if (Math.Abs(num1) > Math.Abs(num2))
            {
                if (Math.Abs(num1) > Math.Abs(num3))
                {
                    matrixD.Right = num1 > 0.0 ? axisDefinitionMatrix.Right : axisDefinitionMatrix.Left;
                    flag1 = true;
                }
                else
                {
                    matrixD.Right = num3 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
                    flag3 = true;
                }
            }
            else if (Math.Abs(num2) > Math.Abs(num3))
            {
                matrixD.Right = num2 > 0.0 ? axisDefinitionMatrix.Up : axisDefinitionMatrix.Down;
                flag2 = true;
            }
            else
            {
                matrixD.Right = num3 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
                flag3 = true;
            }
            double num4 = toAlign.Up.Dot(axisDefinitionMatrix.Right);
            double num5 = toAlign.Up.Dot(axisDefinitionMatrix.Up);
            double num6 = toAlign.Up.Dot(axisDefinitionMatrix.Backward);
            bool flag4;
            if (flag2 || Math.Abs(num4) > Math.Abs(num5) && !flag1)
            {
                if (Math.Abs(num4) > Math.Abs(num6) || flag3)
                {
                    matrixD.Up = num4 > 0.0 ? axisDefinitionMatrix.Right : axisDefinitionMatrix.Left;
                    flag1 = true;
                }
                else
                {
                    matrixD.Up = num6 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
                    flag4 = true;
                }
            }
            else if (Math.Abs(num5) > Math.Abs(num6) || flag3)
            {
                matrixD.Up = num5 > 0.0 ? axisDefinitionMatrix.Up : axisDefinitionMatrix.Down;
                flag2 = true;
            }
            else
            {
                matrixD.Up = num6 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
                flag4 = true;
            }
            if (!flag1)
            {
                double num7 = toAlign.Backward.Dot(axisDefinitionMatrix.Right);
                matrixD.Backward = num7 > 0.0 ? axisDefinitionMatrix.Right : axisDefinitionMatrix.Left;
            }
            else if (!flag2)
            {
                double num7 = toAlign.Backward.Dot(axisDefinitionMatrix.Up);
                matrixD.Backward = num7 > 0.0 ? axisDefinitionMatrix.Up : axisDefinitionMatrix.Down;
            }
            else
            {
                double num7 = toAlign.Backward.Dot(axisDefinitionMatrix.Backward);
                matrixD.Backward = num7 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
            }
            return matrixD;
        }

        public static bool GetEulerAnglesXYZ(ref MatrixD mat, out Vector3D xyz)
        {
            double x1 = (double) mat.GetRow(0).X;
            double num1 = (double) mat.GetRow(0).Y;
            double d = (double) mat.GetRow(0).Z;
            double y = (double) mat.GetRow(1).X;
            double x2 = (double) mat.GetRow(1).Y;
            double num2 = (double) mat.GetRow(1).Z;
            double num3 = (double) mat.GetRow(2).X;
            double num4 = (double) mat.GetRow(2).Y;
            double x3 = (double) mat.GetRow(2).Z;
            double num5 = d;
            if (num5 < 1.0)
            {
                if (num5 > -1.0)
                {
                    xyz = new Vector3D(Math.Atan2(-num2, x3), Math.Asin(d), Math.Atan2(-num1, x1));
                    return true;
                }
                else
                {
                    xyz = new Vector3D(-Math.Atan2(y, x2), -1.57079601287842, 0.0);
                    return false;
                }
            }
            else
            {
                xyz = new Vector3D(Math.Atan2(y, x2), -1.57079601287842, 0.0);
                return false;
            }
        }

        public static MatrixD SwapYZCoordinates(MatrixD m)
        {
            MatrixD matrixD = m;
            Vector3D right = m.Right;
            Vector3D up = m.Up;
            Vector3D forward = m.Forward;
            matrixD.Right = new Vector3D(right.X, right.Z, -right.Y);
            matrixD.Up = new Vector3D(forward.X, forward.Z, -forward.Y);
            matrixD.Forward = new Vector3D(-up.X, -up.Z, up.Y);
            Vector3D translation = m.Translation;
            matrixD.Translation = Vector3D.SwapYZCoordinates(translation);
            return matrixD;
        }

        public bool IsMirrored()
        {
            return this.Determinant() < 0.0;
        }

        private struct F16
        {
            public unsafe fixed double data [16];
        }
    }
}