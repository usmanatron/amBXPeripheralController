using System.Linq;
using aPC.Chromesthesia.Lights;
using aPC.Chromesthesia.Sound.Entities;
using aPC.Common.Entities;

namespace aPC.Chromesthesia.Server
{
  internal class FrameBuilder
  {
    private readonly CompositeLightSectionBuilder compositeLightSectionBuilder;
    private readonly SoundToLightConverter converter;
    private readonly int diagonalLightPercentage;
    private readonly int frameLength;

    public FrameBuilder(CompositeLightSectionBuilder compositeLightSectionBuilder, SoundToLightConverter converter)
    {
      this.compositeLightSectionBuilder = compositeLightSectionBuilder;
      this.converter = converter;
      diagonalLightPercentage = ChromesthesiaConfig.DiagonalLightPercentageOfSide;
      frameLength = ChromesthesiaConfig.SceneFrameLength;
    }

    public Frame BuildFrameFromPitchResults(StereoPitchResult pitchResults)
    {
      var leftLight = converter.BuildLightFrom(pitchResults.Left);
      var rightLight = converter.BuildLightFrom(pitchResults.Right);

      var lightSection = compositeLightSectionBuilder
        .WithLights(leftLight, rightLight)
        .WithSidePercentageOnDiagonal(diagonalLightPercentage)
        .Build();

      return new Common.Builders.FrameBuilder()
        .AddFrame()
        .WithLightSection(lightSection)
        .WithRepeated(false)
        .WithFrameLength(frameLength)
        .Build()
        .Single();
    }
  }
}