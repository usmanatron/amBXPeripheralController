using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using System.Linq;
using System.Threading.Tasks;

namespace aPC.Chromesthesia.Server
{
  internal class SceneRunner
  {
    private EngineActor engineActor;

    public SceneRunner(EngineActor engineActor)
    {
      this.engineActor = engineActor;
    }

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///   This is greatly cutdown compared to the Standard server, therefore many things are assumed:
    ///   * The scene is desync
    ///   * Only one frame
    ///   * Only lights
    /// </remarks>
    /// <param name="scene"></param>
    public void RunScene(amBXScene scene)
    {
      Parallel.ForEach<Light>(scene.Frames.Single().LightSection.Lights, light => engineActor.UpdateComponent(light));
    }
  }
}