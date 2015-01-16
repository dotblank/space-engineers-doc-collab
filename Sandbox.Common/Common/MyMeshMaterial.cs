// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyMeshMaterial
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Collections.Generic;
using VRage.Common.Import;
using VRageMath;

namespace Sandbox.Common
{
    public class MyMeshMaterial
    {
        public MyMeshDrawTechnique DrawTechnique;
        public string Name;
        public Color DiffuseColor;
        public float SpecularIntensity;
        public float SpecularPower;
        public string GlassCW;
        public string GlassCCW;
        public bool GlassSmooth;
        public string DiffuseTexture;
        public Dictionary<string, string> Textures;

        public override int GetHashCode()
        {
            int num1 = 1;
            int num2 = 0;
            if ((double) this.SpecularIntensity != 0.0)
            {
                num1 = num1*397 ^ this.SpecularIntensity.GetHashCode();
                num2 += 2;
            }
            if ((double) this.SpecularPower != 0.0)
            {
                num1 = num1*397 ^ this.SpecularPower.GetHashCode();
                num2 += 4;
            }
            if (this.DiffuseColor.GetHashCode() != 0)
            {
                num1 = num1*397 ^ this.DiffuseColor.GetHashCode();
                num2 += 8;
            }
            int num3 = 3;
            foreach (KeyValuePair<string, string> keyValuePair in this.Textures)
            {
                num1 = num1*397 ^ keyValuePair.Key.GetHashCode();
                int num4;
                num2 += 1 << (num4 = num3 + 1);
                num1 = num1*397 ^ keyValuePair.Value.GetHashCode();
                num2 += 1 << (num3 = num4 + 1);
            }
            return num1*397 ^ num2;
        }
    }
}