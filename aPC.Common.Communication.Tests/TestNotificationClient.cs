using System.Collections.Generic;
using aPC.Common.Communication;

namespace aPC.Common.Communication.Tests
{
  public class TestNotificationClient : INotificationClient
  {
    public TestNotificationClient()
    {
      IntegratedScenesPushed = new List<string>();
      CustomScenesPushed = new List<string>();
    }

    public void PushCustomScene(string xiScene)
    {
      CustomScenesPushed.Add(xiScene);
    }

    public void PushIntegratedScene(string xiScene)
    {
      IntegratedScenesPushed.Add(xiScene);
    }

    public int NumberOfCustomScenesPushed
    {
      get { return CustomScenesPushed.Count; }
    }

    public int NumberOfIntegratedScenesPushed
    {
      get { return IntegratedScenesPushed.Count; }
    }

    public readonly List<string> IntegratedScenesPushed;
    public readonly List<string> CustomScenesPushed;
  }
}
