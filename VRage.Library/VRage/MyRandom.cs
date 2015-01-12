// Decompiled with JetBrains decompiler
// Type: VRage.MyRandom
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace VRage
{
    [Serializable]
    public class MyRandom
    {
        private static byte[] m_tmpLongArray = new byte[8];
        private const int MBIG = 2147483647;
        private const int MSEED = 161803398;
        private const int MZ = 0;
        [ThreadStatic] private static MyRandom m_instance;
        private int inext;
        private int inextp;
        private int[] SeedArray;

        public static MyRandom Instance
        {
            get
            {
                if (MyRandom.m_instance == null)
                    MyRandom.m_instance = new MyRandom();
                return MyRandom.m_instance;
            }
        }

        public MyRandom()
            : this(Environment.TickCount)
        {
        }

        public MyRandom(int Seed)
        {
            this.SeedArray = new int[56];
            this.SetSeed(Seed);
        }

        public MyRandom.StateToken PushSeed(int newSeed)
        {
            return new MyRandom.StateToken(this, newSeed);
        }

        public unsafe void GetState(out MyRandom.State state)
        {
            state = new State();
            // removed because trying to fix the errors is pointless
        }

        public unsafe void SetState(ref MyRandom.State state)
        {
            // removed because trying to fix the errors is pointless
        }

        public int CreateRandomSeed()
        {
            return Environment.TickCount ^ this.Next();
        }

        public void SetSeed(int Seed)
        {
            int num1 = 161803398 - (Seed == int.MinValue ? int.MaxValue : Math.Abs(Seed));
            this.SeedArray[55] = num1;
            int num2 = 1;
            for (int index1 = 1; index1 < 55; ++index1)
            {
                int index2 = 21*index1%55;
                this.SeedArray[index2] = num2;
                num2 = num1 - num2;
                if (num2 < 0)
                    num2 += int.MaxValue;
                num1 = this.SeedArray[index2];
            }
            for (int index1 = 1; index1 < 5; ++index1)
            {
                for (int index2 = 1; index2 < 56; ++index2)
                {
                    this.SeedArray[index2] -= this.SeedArray[1 + (index2 + 30)%55];
                    if (this.SeedArray[index2] < 0)
                        this.SeedArray[index2] += int.MaxValue;
                }
            }
            this.inext = 0;
            this.inextp = 21;
            Seed = 1;
        }

        private double GetSampleForLargeRange()
        {
            int num = this.InternalSample();
            if (this.InternalSample()%2 == 0)
                num = -num;
            return ((double) num + 2147483646.0)/4294967293.0;
        }

        private int InternalSample()
        {
            int num1 = this.inext;
            int num2 = this.inextp;
            int index1;
            if ((index1 = num1 + 1) >= 56)
                index1 = 1;
            int index2;
            if ((index2 = num2 + 1) >= 56)
                index2 = 1;
            int num3 = this.SeedArray[index1] - this.SeedArray[index2];
            if (num3 == int.MaxValue)
                --num3;
            if (num3 < 0)
                num3 += int.MaxValue;
            this.SeedArray[index1] = num3;
            this.inext = index1;
            this.inextp = index2;
            return num3;
        }

        public int Next()
        {
            return this.InternalSample();
        }

        public int Next(int maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            else
                return (int) (this.Sample()*(double) maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            long num = (long) (maxValue - minValue);
            if (num <= (long) int.MaxValue)
                return (int) (this.Sample()*(double) num) + minValue;
            else
                return (int) (long) (this.GetSampleForLargeRange()*(double) num) + minValue;
        }

        public long NextLong()
        {
            this.NextBytes(MyRandom.m_tmpLongArray);
            return BitConverter.ToInt64(MyRandom.m_tmpLongArray, 0);
        }

        public void NextBytes(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            for (int index = 0; index < buffer.Length; ++index)
                buffer[index] = (byte) (this.InternalSample()%256);
        }

        public float NextFloat()
        {
            return (float) this.NextDouble();
        }

        public double NextDouble()
        {
            return this.Sample();
        }

        protected double Sample()
        {
            return (double) this.InternalSample()*4.6566128752458E-10;
        }

        public struct State
        {
            public int Inext;
            public int Inextp;
            public unsafe fixed int Seed [56];
        }

        public struct StateToken : IDisposable
        {
            private MyRandom m_random;
            private MyRandom.State m_state;

            public StateToken(MyRandom random)
            {
                this.m_random = random;
                random.GetState(out this.m_state);
            }

            public StateToken(MyRandom random, int newSeed)
            {
                this.m_random = random;
                random.GetState(out this.m_state);
                random.SetSeed(newSeed);
            }

            public void Dispose()
            {
                if (this.m_random == null)
                    return;
                this.m_random.SetState(ref this.m_state);
            }
        }
    }
}