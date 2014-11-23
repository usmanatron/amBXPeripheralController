using System;
using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server;
using aPC.Common.Entities;
using NAudio.Wave;

namespace aPC.Chromesthesia
{
  class SceneGenerator
  {
    private readonly PitchGeneratorProvider pitchGenerator;
    private readonly SceneBuilder sceneBuilder;
    private readonly ConductorManager conductorManager;

    public SceneGenerator(PitchGeneratorProvider pitchGenerator, SceneBuilder sceneBuilder, ConductorManager conductorManager)
    {
      this.pitchGenerator = pitchGenerator;
      this.sceneBuilder = sceneBuilder;
      this.conductorManager = conductorManager;
    }

    public void Execute(int readLength)
    {
      var results = GetResultsFromPitchGenerator(new byte[readLength], 0, readLength);

      // BuildScene      
      var scene = sceneBuilder.BuildSceneFromPitchResults(results);

      if (!SceneIsEmpty(scene))
      {
        // Push through conductorManager
        conductorManager.Update(scene);
      }

      return;
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