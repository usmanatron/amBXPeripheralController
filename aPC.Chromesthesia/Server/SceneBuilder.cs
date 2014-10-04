using aPC.Chromesthesia.Pitch;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using System;

namespace aPC.Chromesthesia.Server
{
  class SceneBuilder
  {
    private CompositeLightSectionBuilder compositeLightSectionBuilder;
    private LightBuilder lightBuilder;

    public SceneBuilder(CompositeLightSectionBuilder compositeLightBuilder, LightBuilder lightBuilder)
    {
      this.compositeLightSectionBuilder = compositeLightBuilder;
      this.lightBuilder = lightBuilder;
    }

    public amBXScene BuildSceneFromPitchResults(StereoPitchResult pitchResults)
    {
      var leftLight = lightBuilder.BuildLightFrom(pitchResults.Left);
      var rightLight = lightBuilder.BuildLightFrom(pitchResults.Right);

      var lightSection = compositeLightSectionBuilder
        .WithLights(leftLight, rightLight)
        .WithSidePercentageOnDiagonal(70)
        .Build();

      var frames = new FrameBuilder()
        .AddFrame()
        .WithLightSection(lightSection)
        .WithRepeated(true)
        .WithFrameLength(10)
        .Build();

      return new amBXScene
             {
               Frames = frames,
               IsExclusive = false,
               SceneType = eSceneType.Desync
             };
    }
  }
}
