using System.Linq;
using System.Threading.Tasks;
using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Engine;

namespace aPC.Server.Processors
{
  class ExclusiveProcessor : IProcessor
  {
    private readonly EngineActor engineActor;

    public ExclusiveProcessor(EngineActor engineActor)
    {
      this.engineActor = engineActor;
    }

    public void Process(amBXScene scene)
    {
      var frame = scene.Frames.Single();
      ProcessSection(frame.LightSection);
      ProcessSection(frame.FanSection);
      ProcessSection(frame.RumbleSection);
    }

    private void ProcessSection(IComponentSection section)
    {
      if (section == null)
      {
        return;
      }
      Parallel.ForEach(
        section.GetComponents(), 
        component => engineActor.UpdateComponent(component, RunMode.Synchronous));
    }
  }
}
