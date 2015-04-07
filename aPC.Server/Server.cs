using aPC.Common.Server;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Entities;
using aPC.Server.Communication;

namespace aPC.Server
{
  internal class Server
  {
    /// <remarks>
    ///   TODO: Add DI.  When doing so, note that the following need to be Singletons:
    ///   * AmbxEngineWrapper (has amBX objects)
    /// </remarks>
    private static void Main()
    {
      var wrapper = new AmbxEngineWrapper();

      new ServerTask(new NewSceneProcessor(new SceneSplitter(), new TaskManager(new EngineActor(wrapper), new DirectionalComponentActionList()), new RunningDirectionalComponentList()), new NotificationService(), wrapper).Run();
    }
  }
}