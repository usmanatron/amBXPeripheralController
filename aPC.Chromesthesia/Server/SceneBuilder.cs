using aPC.Chromesthesia.Pitch;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Chromesthesia.Server
{
  class SceneBuilder
  {
    private readonly CompositeLightSectionBuilder compositeLightSectionBuilder;
    private readonly LightBuilder lightBuilder;

    // Settings
    private const int diagonalLightPercentage = 70;
    private const int frameLength = 10;

    public SceneBuilder(CompositeLightSectionBuilder compositeLightSectionBuilder, LightBuilder lightBuilder)
    {
      this.compositeLightSectionBuilder = compositeLightSectionBuilder;
      this.lightBuilder = lightBuilder;
    }

    public amBXScene BuildSceneFromPitchResults(StereoPitchResult pitchResults)
    {
      var leftLight = lightBuilder.BuildLightFrom(pitchResults.Left);
      var rightLight = lightBuilder.BuildLightFrom(pitchResults.Right);

      var lightSection = compositeLightSectionBuilder
        .WithLights(leftLight, rightLight)
        .WithSidePercentageOnDiagonal(diagonalLightPercentage)
        .Build();

      var frames = new FrameBuilder()
        .AddFrame()
        .WithLightSection(lightSection)
        .WithRepeated(true)
        .WithFrameLength(frameLength)
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
