using aPC.Common.Entities;
using aPC.Server.Engine;

namespace aPC.Server
{
  class ExclusiveProcessor
  {
    private readonly EngineActorSync engineActor;

    public ExclusiveProcessor(EngineActorSync engineActor)
    {
      this.engineActor = engineActor;
    }

    public void Process(Frame frame)
    {
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

      foreach (var component in section.GetComponents())
      {
        engineActor.UpdateComponent(component);
      }
    }
  }
}
