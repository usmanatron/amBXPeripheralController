using aPC.Chromesthesia.Pitch;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Chromesthesia.Server
{
  class SceneBuilder
  {
    public amBXScene BuildSceneFromPitchResults(StereoPitchResult pitchResults)
    {
      var leftMaxPitch = pitchResults.left.PeakPitch.averageFrequency;

      var lightSection = new LightSectionBuilder()
        .WithAllLights(new Light {Blue = leftMaxPitch / 600})
        .WithFadeTime(2)
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
