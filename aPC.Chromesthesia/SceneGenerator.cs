using aPC.Chromesthesia.Communication;
using aPC.Chromesthesia.Server;
using aPC.Chromesthesia.Sound;
using aPC.Chromesthesia.Sound.Entities;
using aPC.Common;
using aPC.Common.Client.Communication;
using aPC.Common.Communication;
using aPC.Common.Entities;
using NAudio.Wave;
using System;
using System.Threading;

namespace aPC.Chromesthesia
{
  internal class SceneGenerator
  {
    private readonly PitchGeneratorProvider pitchGenerator;
    private readonly SceneBuilder sceneBuilder;
    private readonly NotificationClientBase sceneRunner;
    private const float lightTolerance = 0.01f;

    public SceneGenerator(PitchGeneratorProvider pitchGenerator, SceneBuilder sceneBuilder, NotificationClientBase newSceneProcessor)
    {
      this.pitchGenerator = pitchGenerator;
      this.sceneBuilder = sceneBuilder;
      sceneRunner = newSceneProcessor;
    }

    public void Execute(int readLength)
    {
      var results = GetResultsFromPitchGenerator(new byte[readLength], 0, readLength);

      var scene = sceneBuilder.BuildSceneFromPitchResults(results);

      if (!SceneIsEmpty(scene))
      {
        sceneRunner.PushScene(scene);
      }
    }

    private StereoPitchResult GetResultsFromPitchGenerator(byte[] buffer, int offset, int count)
    {
      pitchGenerator.Read(buffer, offset, count);
      return pitchGenerator.PitchResults;
    }

    private bool SceneIsEmpty(amBXScene scene)
    {
      return LightIsEmpty((Light)scene.Frames[0].LightSection.GetComponentSectionInDirection(eDirection.East)) &&
             LightIsEmpty((Light)scene.Frames[0].LightSection.GetComponentSectionInDirection(eDirection.West));
    }

    private bool LightIsEmpty(Light light)
    {
      return Math.Abs(light.Red) < lightTolerance && Math.Abs(light.Green) < lightTolerance && Math.Abs(light.Blue) < lightTolerance;
    }
  }
}