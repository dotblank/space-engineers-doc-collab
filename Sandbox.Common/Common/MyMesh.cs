// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyMesh
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRage.Common.Import;

namespace Sandbox.Common
{
    public class MyMesh
    {
        private const string C_CONTENT_ID = "Content\\";
        private const string C_POSTFIX_DIFFUSE = "_d";
        internal const string C_POSTFIX_DIFFUSE_EMISSIVE = "_de";
        internal const string C_POSTFIX_MASK_EMISSIVE = "_me";
        private const string C_POSTFIX_DONT_HAVE_NORMAL = "_dn";
        internal const string C_POSTFIX_NORMAL_SPECULAR = "_ns";
        private const string DEFAULT_DIRECTORY = "\\v01\\";
        public readonly string AssetName;
        public readonly MyMeshMaterial Material;
        public int IndexStart;
        public int TriStart;
        public int TriCount;

        public MyMesh(MyMeshPartInfo meshInfo, string assetName)
        {
            MyMaterialDescriptor materialDescriptor = meshInfo.m_MaterialDesc;
            if (materialDescriptor != null)
            {
                string str;
                materialDescriptor.Textures.TryGetValue("DiffuseTexture", out str);
                this.Material = new MyMeshMaterial()
                {
                    Name = meshInfo.m_MaterialDesc.MaterialName,
                    DiffuseTexture = str,
                    Textures = materialDescriptor.Textures,
                    SpecularIntensity = meshInfo.m_MaterialDesc.SpecularIntensity,
                    SpecularPower = meshInfo.m_MaterialDesc.SpecularPower,
                    DrawTechnique = meshInfo.Technique,
                    GlassCW = meshInfo.m_MaterialDesc.GlassCW,
                    GlassCCW = meshInfo.m_MaterialDesc.GlassCCW,
                    GlassSmooth = meshInfo.m_MaterialDesc.GlassSmoothNormals
                };
            }
            else
                this.Material = new MyMeshMaterial();
            this.AssetName = assetName;
        }
    }
}