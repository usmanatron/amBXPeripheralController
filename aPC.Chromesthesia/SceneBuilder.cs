using aPC.Chromesthesia.Lights;
using aPC.Chromesthesia.Sound.Entities;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Chromesthesia.Server
{
  internal class SceneBuilder
  {
    private readonly CompositeLightSectionBuilder compositeLightSectionBuilder;
    private readonly SoundToLightConverter converter;
    private readonly int diagonalLightPercentage;
    private readonly int frameLength;

    public SceneBuilder(CompositeLightSectionBuilder compositeLightSectionBuilder, SoundToLightConverter converter)
    {
      this.compositeLightSectionBuilder = compositeLightSectionBuilder;
      this.converter = converter;
      diagonalLightPercentage = ChromesthesiaConfig.DiagonalLightPercentageOfSide;
      frameLength = ChromesthesiaConfig.SceneFrameLength;
    }

    public amBXScene BuildSceneFromPitchResults(StereoPitchResult pitchResults)
    {
      var leftLight = converter.BuildLightFrom(pitchResults.Left);
      var rightLight = converter.BuildLightFrom(pitchResults.Right);

      var lightSection = compositeLightSectionBuilder
        .WithLights(leftLight, rightLight)
        .WithSidePercentageOnDiagonal(diagonalLightPercentage)
        .Build();

      var frames = new FrameBuilder()
        .AddFrame()
        .WithLightSection(lightSection)
        .WithRepeated(false)
        .WithFrameLength(frameLength)
        .Build();

      return new amBXScene
             {
               Frames = frames,
               IsExclusive = false,
               SceneType = eSceneType.Sync
             };
    }
  }
}