using System.Threading;
using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server;
using aPC.Common;
using NAudio.Wave;

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

      // Push through conductorManager
      conductorManager.Update(scene);
      return results.bytesRead;
    }

    //public void TestServer()
    //{
    //  var sceneName = isRed ? "ccnet_red" : "ccnet_green";
    //  isRed = !isRed;
    //  var scene = new SceneAccessor().GetScene(sceneName);
    //  conductorManager.Update(scene);
    //  Thread.Sleep(900);
    //}
    //private bool isRed;


    private StereoPitchResult GetResultsFromPitchGenerator(byte[] buffer, int offset, int count)
    {
      pitchGenerator.Read(buffer, offset, count);
      return pitchGenerator.PitchResults;
    }

    public WaveFormat WaveFormat { get; private set; }
  }
}
