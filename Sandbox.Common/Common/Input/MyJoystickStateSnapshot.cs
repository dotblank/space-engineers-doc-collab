// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Input.MyJoystickStateSnapshot
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Collections.Generic;
using System.Reflection;

namespace Sandbox.Common.Input
{
    [Obfuscation(Exclude = true, Feature = "cw symbol renaming")]
    public class MyJoystickStateSnapshot
    {
        public int[] AccelerationSliders { get; set; }

        public int AccelerationX { get; set; }

        public int AccelerationY { get; set; }

        public int AccelerationZ { get; set; }

        public int AngularAccelerationX { get; set; }

        public int AngularAccelerationY { get; set; }

        public int AngularAccelerationZ { get; set; }

        public int AngularVelocityX { get; set; }

        public int AngularVelocityY { get; set; }

        public int AngularVelocityZ { get; set; }

        public List<int> Buttons { get; set; }

        public int[] ForceSliders { get; set; }

        public int ForceX { get; set; }

        public int ForceY { get; set; }

        public int ForceZ { get; set; }

        public int[] PointOfViewControllers { get; set; }

        public int RotationX { get; set; }

        public int RotationY { get; set; }

        public int RotationZ { get; set; }

        public int[] Sliders { get; set; }

        public int TorqueX { get; set; }

        public int TorqueY { get; set; }

        public int TorqueZ { get; set; }

        public int[] VelocitySliders { get; set; }

        public int VelocityX { get; set; }

        public int VelocityY { get; set; }

        public int VelocityZ { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }
    }
}