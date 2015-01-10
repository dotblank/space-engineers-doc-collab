// Decompiled with JetBrains decompiler
// Type: Sandbox.Definitions.MyAudioDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders.Audio;
using Sandbox.Common.ObjectBuilders.Definitions;
using System;
using System.Collections.Generic;

namespace Sandbox.Definitions
{
  [MyDefinitionType(typeof (MyObjectBuilder_AudioDefinition))]
  public class MyAudioDefinition : MyDefinitionBase
  {
    public string Category;
    public MyAudioHelpers.CurveType VolumeCurve;
    public float MaxDistance;
    public float Volume;
    public float VolumeVariation;
    public float PitchVariation;
    public bool Loopable;
    public string Alternative2D;
    public bool UseOcclusion;
    public List<MyAudioHelpers.Wave> Waves;

    public bool IsHudCue
    {
      get
      {
        return StringComparer.InvariantCultureIgnoreCase.Equals(this.Category, "hud");
      }
    }

    protected override void Init(MyObjectBuilder_DefinitionBase builder)
    {
      base.Init(builder);
      MyObjectBuilder_AudioDefinition builderAudioDefinition = builder as MyObjectBuilder_AudioDefinition;
      this.Category = builderAudioDefinition.Category;
      this.VolumeCurve = builderAudioDefinition.VolumeCurve;
      this.MaxDistance = builderAudioDefinition.MaxDistance;
      this.Volume = builderAudioDefinition.Volume;
      this.VolumeVariation = builderAudioDefinition.VolumeVariation;
      this.PitchVariation = builderAudioDefinition.PitchVariation;
      this.Loopable = builderAudioDefinition.Loopable;
      this.Alternative2D = builderAudioDefinition.Alternative2D;
      this.UseOcclusion = builderAudioDefinition.UseOcclusion;
      this.Waves = builderAudioDefinition.Waves;
    }

    public override MyObjectBuilder_DefinitionBase GetObjectBuilder()
    {
      MyObjectBuilder_AudioDefinition builderAudioDefinition = (MyObjectBuilder_AudioDefinition) base.GetObjectBuilder();
      builderAudioDefinition.Category = this.Category;
      builderAudioDefinition.VolumeCurve = this.VolumeCurve;
      builderAudioDefinition.MaxDistance = this.MaxDistance;
      builderAudioDefinition.Volume = this.Volume;
      builderAudioDefinition.VolumeVariation = this.VolumeVariation;
      builderAudioDefinition.PitchVariation = this.PitchVariation;
      builderAudioDefinition.Loopable = this.Loopable;
      builderAudioDefinition.Alternative2D = this.Alternative2D;
      builderAudioDefinition.UseOcclusion = this.UseOcclusion;
      builderAudioDefinition.Waves = this.Waves;
      return (MyObjectBuilder_DefinitionBase) builderAudioDefinition;
    }
  }
}
