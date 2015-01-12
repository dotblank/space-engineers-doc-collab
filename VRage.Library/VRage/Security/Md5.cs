// Decompiled with JetBrains decompiler
// Type: VRage.Security.Md5
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage.Security
{
    public static class Md5
    {
        public static readonly uint[] T = new uint[64]
        {
            3614090360U,
            3905402710U,
            606105819U,
            3250441966U,
            4118548399U,
            1200080426U,
            2821735955U,
            4249261313U,
            1770035416U,
            2336552879U,
            4294925233U,
            2304563134U,
            1804603682U,
            4254626195U,
            2792965006U,
            1236535329U,
            4129170786U,
            3225465664U,
            643717713U,
            3921069994U,
            3593408605U,
            38016083U,
            3634488961U,
            3889429448U,
            568446438U,
            3275163606U,
            4107603335U,
            1163531501U,
            2850285829U,
            4243563512U,
            1735328473U,
            2368359562U,
            4294588738U,
            2272392833U,
            1839030562U,
            4259657740U,
            2763975236U,
            1272893353U,
            4139469664U,
            3200236656U,
            681279174U,
            3936430074U,
            3572445317U,
            76029189U,
            3654602809U,
            3873151461U,
            530742520U,
            3299628645U,
            4096336452U,
            1126891415U,
            2878612391U,
            4237533241U,
            1700485571U,
            2399980690U,
            4293915773U,
            2240044497U,
            1873313359U,
            4264355552U,
            2734768916U,
            1309151649U,
            4149444226U,
            3174756917U,
            718787259U,
            3951481745U
        };

        private static uint RotateLeft(uint uiNumber, ushort shift)
        {
            return uiNumber >> 32 - (int) shift | uiNumber << (int) shift;
        }

        public static unsafe Md5.Hash ComputeHash(byte[] input)
        {
            uint* X = stackalloc uint[16];
            Md5.Hash hash = new Md5.Hash();
            hash.A = 1732584193U;
            hash.B = 4023233417U;
            hash.C = 2562383102U;
            hash.D = 271733878U;
            uint num = (uint) (input.Length*8)/32U;
            for (uint block = 0U; block < num/16U; ++block)
            {
                Md5.CopyBlock(input, block, X);
                Md5.PerformTransformation(ref hash.A, ref hash.B, ref hash.C, ref hash.D, X);
            }
            if (input.Length%64 >= 56)
            {
                Md5.CopyLastBlock(input, X);
                Md5.PerformTransformation(ref hash.A, ref hash.B, ref hash.C, ref hash.D, X);
                for (int index = 0; index < 16; ++index)
                    X[index] = 0U;
                *(long*) (X + 14) = (long) input.Length*8L;
                Md5.PerformTransformation(ref hash.A, ref hash.B, ref hash.C, ref hash.D, X);
            }
            else
            {
                Md5.CopyLastBlock(input, X);
                *(long*) (X + 14) = (long) input.Length*8L;
                Md5.PerformTransformation(ref hash.A, ref hash.B, ref hash.C, ref hash.D, X);
            }
            return hash;
        }

        private static unsafe void TransF(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i, uint* X)
        {
            a = b +
                Md5.RotateLeft(a + (uint) ((int) b & (int) c | ~(int) b & (int) d) + X[k] + Md5.T[(i - 1U)], s);
        }

        private static unsafe void TransG(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i, uint* X)
        {
            a = b +
                Md5.RotateLeft(a + (uint) ((int) b & (int) d | (int) c & ~(int) d) + X[k] + Md5.T[(i - 1U)], s);
        }

        private static unsafe void TransH(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i, uint* X)
        {
            a = b + Md5.RotateLeft(a + (b ^ c ^ d) + X[k] + Md5.T[(i - 1U)], s);
        }

        private static unsafe void TransI(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i, uint* X)
        {
            a = b + Md5.RotateLeft(a + (c ^ (b | ~d)) + X[k] + Md5.T[(i - 1U)], s);
        }

        private static unsafe void PerformTransformation(ref uint A, ref uint B, ref uint C, ref uint D, uint* X)
        {
            uint num1 = A;
            uint num2 = B;
            uint num3 = C;
            uint num4 = D;
            Md5.TransF(ref A, B, C, D, 0U, (ushort) 7, 1U, X);
            Md5.TransF(ref D, A, B, C, 1U, (ushort) 12, 2U, X);
            Md5.TransF(ref C, D, A, B, 2U, (ushort) 17, 3U, X);
            Md5.TransF(ref B, C, D, A, 3U, (ushort) 22, 4U, X);
            Md5.TransF(ref A, B, C, D, 4U, (ushort) 7, 5U, X);
            Md5.TransF(ref D, A, B, C, 5U, (ushort) 12, 6U, X);
            Md5.TransF(ref C, D, A, B, 6U, (ushort) 17, 7U, X);
            Md5.TransF(ref B, C, D, A, 7U, (ushort) 22, 8U, X);
            Md5.TransF(ref A, B, C, D, 8U, (ushort) 7, 9U, X);
            Md5.TransF(ref D, A, B, C, 9U, (ushort) 12, 10U, X);
            Md5.TransF(ref C, D, A, B, 10U, (ushort) 17, 11U, X);
            Md5.TransF(ref B, C, D, A, 11U, (ushort) 22, 12U, X);
            Md5.TransF(ref A, B, C, D, 12U, (ushort) 7, 13U, X);
            Md5.TransF(ref D, A, B, C, 13U, (ushort) 12, 14U, X);
            Md5.TransF(ref C, D, A, B, 14U, (ushort) 17, 15U, X);
            Md5.TransF(ref B, C, D, A, 15U, (ushort) 22, 16U, X);
            Md5.TransG(ref A, B, C, D, 1U, (ushort) 5, 17U, X);
            Md5.TransG(ref D, A, B, C, 6U, (ushort) 9, 18U, X);
            Md5.TransG(ref C, D, A, B, 11U, (ushort) 14, 19U, X);
            Md5.TransG(ref B, C, D, A, 0U, (ushort) 20, 20U, X);
            Md5.TransG(ref A, B, C, D, 5U, (ushort) 5, 21U, X);
            Md5.TransG(ref D, A, B, C, 10U, (ushort) 9, 22U, X);
            Md5.TransG(ref C, D, A, B, 15U, (ushort) 14, 23U, X);
            Md5.TransG(ref B, C, D, A, 4U, (ushort) 20, 24U, X);
            Md5.TransG(ref A, B, C, D, 9U, (ushort) 5, 25U, X);
            Md5.TransG(ref D, A, B, C, 14U, (ushort) 9, 26U, X);
            Md5.TransG(ref C, D, A, B, 3U, (ushort) 14, 27U, X);
            Md5.TransG(ref B, C, D, A, 8U, (ushort) 20, 28U, X);
            Md5.TransG(ref A, B, C, D, 13U, (ushort) 5, 29U, X);
            Md5.TransG(ref D, A, B, C, 2U, (ushort) 9, 30U, X);
            Md5.TransG(ref C, D, A, B, 7U, (ushort) 14, 31U, X);
            Md5.TransG(ref B, C, D, A, 12U, (ushort) 20, 32U, X);
            Md5.TransH(ref A, B, C, D, 5U, (ushort) 4, 33U, X);
            Md5.TransH(ref D, A, B, C, 8U, (ushort) 11, 34U, X);
            Md5.TransH(ref C, D, A, B, 11U, (ushort) 16, 35U, X);
            Md5.TransH(ref B, C, D, A, 14U, (ushort) 23, 36U, X);
            Md5.TransH(ref A, B, C, D, 1U, (ushort) 4, 37U, X);
            Md5.TransH(ref D, A, B, C, 4U, (ushort) 11, 38U, X);
            Md5.TransH(ref C, D, A, B, 7U, (ushort) 16, 39U, X);
            Md5.TransH(ref B, C, D, A, 10U, (ushort) 23, 40U, X);
            Md5.TransH(ref A, B, C, D, 13U, (ushort) 4, 41U, X);
            Md5.TransH(ref D, A, B, C, 0U, (ushort) 11, 42U, X);
            Md5.TransH(ref C, D, A, B, 3U, (ushort) 16, 43U, X);
            Md5.TransH(ref B, C, D, A, 6U, (ushort) 23, 44U, X);
            Md5.TransH(ref A, B, C, D, 9U, (ushort) 4, 45U, X);
            Md5.TransH(ref D, A, B, C, 12U, (ushort) 11, 46U, X);
            Md5.TransH(ref C, D, A, B, 15U, (ushort) 16, 47U, X);
            Md5.TransH(ref B, C, D, A, 2U, (ushort) 23, 48U, X);
            Md5.TransI(ref A, B, C, D, 0U, (ushort) 6, 49U, X);
            Md5.TransI(ref D, A, B, C, 7U, (ushort) 10, 50U, X);
            Md5.TransI(ref C, D, A, B, 14U, (ushort) 15, 51U, X);
            Md5.TransI(ref B, C, D, A, 5U, (ushort) 21, 52U, X);
            Md5.TransI(ref A, B, C, D, 12U, (ushort) 6, 53U, X);
            Md5.TransI(ref D, A, B, C, 3U, (ushort) 10, 54U, X);
            Md5.TransI(ref C, D, A, B, 10U, (ushort) 15, 55U, X);
            Md5.TransI(ref B, C, D, A, 1U, (ushort) 21, 56U, X);
            Md5.TransI(ref A, B, C, D, 8U, (ushort) 6, 57U, X);
            Md5.TransI(ref D, A, B, C, 15U, (ushort) 10, 58U, X);
            Md5.TransI(ref C, D, A, B, 6U, (ushort) 15, 59U, X);
            Md5.TransI(ref B, C, D, A, 13U, (ushort) 21, 60U, X);
            Md5.TransI(ref A, B, C, D, 4U, (ushort) 6, 61U, X);
            Md5.TransI(ref D, A, B, C, 11U, (ushort) 10, 62U, X);
            Md5.TransI(ref C, D, A, B, 2U, (ushort) 15, 63U, X);
            Md5.TransI(ref B, C, D, A, 9U, (ushort) 21, 64U, X);
            A = A + num1;
            B = B + num2;
            C = C + num3;
            D = D + num4;
        }

        private static unsafe void CopyBlock(byte[] bMsg, uint block, uint* X)
        {
            block <<= 6;
            uint num = 0U;
            while (num < 61U)
            {
                X[num >> 2] =
                    (uint)
                        ((int) bMsg[ (block + (num + 3U))] << 24 |
                         (int) bMsg[ (block + (num + 2U))] << 16 |
                         (int) bMsg[ (block + (num + 1U))] << 8) | (uint) bMsg[ (block + num)];
                num += 4U;
            }
        }

        private static unsafe void CopyLastBlock(byte[] bMsg, uint* X)
        {
            
        }

        public class Hash
        {
            public uint A;
            public uint B;
            public uint C;
            public uint D;

            public override string ToString()
            {
                return Md5.Hash.ReverseByte(this.A).ToString("X8") + Md5.Hash.ReverseByte(this.B).ToString("X8") +
                       Md5.Hash.ReverseByte(this.C).ToString("X8") + Md5.Hash.ReverseByte(this.D).ToString("X8");
            }

            public string ToLowerString()
            {
                return Md5.Hash.ReverseByte(this.A).ToString("x8") + Md5.Hash.ReverseByte(this.B).ToString("x8") +
                       Md5.Hash.ReverseByte(this.C).ToString("x8") + Md5.Hash.ReverseByte(this.D).ToString("x8");
            }

            private static uint ReverseByte(uint uiNumber)
            {
                return
                    (uint)
                        (((int) uiNumber & (int) byte.MaxValue) << 24 | (int) (uiNumber >> 24) |
                         (int) ((uiNumber & 16711680U) >> 8) | ((int) uiNumber & 65280) << 8);
            }
        }
    }
}