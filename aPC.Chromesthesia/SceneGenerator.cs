using aPC.Chromesthesia.Server;
using aPC.Chromesthesia.Sound;
using aPC.Chromesthesia.Sound.Entities;
using aPC.Common;
using aPC.Common.Client.Communication;
using aPC.Common.Entities;
using System;

namespace aPC.Chromesthesia
{
  internal class SceneGenerator
  {
    private readonly PitchGeneratorProvider pitchGenerator;
    private readonly FrameBuilder frameBuilder;
    private readonly NotificationClientBase sceneRunner;
    private const float lightTolerance = 0.01f;

    public SceneGenerator(PitchGeneratorProvider pitchGenerator, FrameBuilder frameBuilder, NotificationClientBase newSceneProcessor)
    {
      this.pitchGenerator = pitchGenerator;
      this.frameBuilder = frameBuilder;
      sceneRunner = newSceneProcessor;
    }

    public void Execute(int readLength)
    {
      var results = GetResultsFromPitchGenerator(new byte[readLength], 0, readLength);

      var frame = frameBuilder.BuildFrameFromPitchResults(results);
      sceneRunner.PushExclusive(frame);

      if (!FrameIsEmpty(frame))
      {
        sceneRunner.PushExclusive(frame);
      }
    }

    private StereoPitchResult GetResultsFromPitchGenerator(byte[] buffer, int offset, int count)
    {
      pitchGenerator.Read(buffer, offset, count);
      return pitchGenerator.PitchResults;
    }

    private bool FrameIsEmpty(Frame frame)
    {
      var section = frame.LightSection;
      return LightIsEmpty(section.GetComponentSectionInDirection(eDirection.East).GetLight()) &&
             LightIsEmpty(section.GetComponentSectionInDirection(eDirection.West).GetLight());
    }

    private bool LightIsEmpty(Light light)
    {
      return Math.Abs(light.Red) < lightTolerance && 
        Math.Abs(light.Green) < lightTolerance && 
        Math.Abs(light.Blue) < lightTolerance;
    }
  }
}