using aPC.Common.Defaults;
using aPC.ServerV3.Communication;
using aPC.ServerV3.Engine;
using aPC.ServerV3.Entities;

namespace aPC.ServerV3
{
  internal class Server
  {
    /// <remarks>
    ///   TODO: Add DI.  When doing so, note that the following need to be Singletons:
    ///   * AmbxEngineWrapper (has amBX objects)
    ///   * RunningDirectionalComponentList (Holds data across classes)
    /// </remarks>
    private static void Main(string[] args)
    {
      var wrapper = new AmbxEngineWrapper();
      var runningDirectionalComponentList = new RunningDirectionalComponentList();

      new ServerTask(new NewSceneProcessor(new SceneSplitter(runningDirectionalComponentList), new TaskManager(new EngineActor(wrapper), new DirectionalComponentActionList(), runningDirectionalComponentList)), new NotificationService(), wrapper).Run();
    }
  }
}