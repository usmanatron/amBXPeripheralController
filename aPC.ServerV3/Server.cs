using aPC.Common.Defaults;
using aPC.ServerV3.Communication;
using aPC.ServerV3.Engine;

namespace aPC.ServerV3
{
  internal class Server
  {
    /// <remarks>
    ///   TODO: Add DI.  When doing so, note that AmbxEngineWrapper must be a Singleton!
    /// </remarks>
    private static void Main(string[] args)
    {
      var wrapper = new AmbxEngineWrapper();
      var splitter = new SceneSplitter();
      new ServerTask(new NewSceneProcessor(splitter, new TaskManager(splitter, new EngineActor(wrapper), new DirectionalComponentActionList())), new NotificationService(), wrapper).Run();
    }
  }
}