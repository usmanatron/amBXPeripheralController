using System;
using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server;
using aPC.Common.Entities;
using NAudio.Wave;
using System.Threading;

namespace aPC.Chromesthesia
{
  class SceneGeneratorProvider : IWaveProvider
  {
    private readonly PitchGeneratorProvider pitchGenerator;
    private readonly SceneBuilder sceneBuilder;
    private readonly ConductorManager conductorManager;

    public SceneGeneratorProvider(PitchGeneratorProvider pitchGenerator, SceneBuilder sceneBuilder, ConductorManager conductorManager)
    {
      this.pitchGenerator = pitchGenerator;
      this.sceneBuilder = sceneBuilder;
      this.conductorManager = conductorManager;
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      var results = GetResultsFromPitchGenerator(buffer, offset, count);

      // BuildScene      
      var scene = sceneBuilder.BuildSceneFromPitchResults(results);

      if (SceneIsEmpty(scene))
      {
        return results.bytesRead;
      }

      // Push through conductorManager
      conductorManager.Update(scene);
      
      return results.bytesRead;
    }

    private StereoPitchResult GetResultsFromPitchGenerator(byte[] buffer, int offset, int count)
    {
      pitchGenerator.Read(buffer, offset, count);
      return pitchGenerator.PitchResults;
    }

    private bool SceneIsEmpty(amBXScene scene)
    {
      return LightIsEmpty(scene.Frames[0].Lights.East) &&
             LightIsEmpty(scene.Frames[0].Lights.West);
    }

    private bool LightIsEmpty(Light light)
    {
      return Math.Abs(light.Red) < TOLERANCE && Math.Abs(light.Green) < TOLERANCE && Math.Abs(light.Blue) < TOLERANCE;
    }

    private const float TOLERANCE = 0.01f;

    public WaveFormat WaveFormat { get; private set; }
  }
}
