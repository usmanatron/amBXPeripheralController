using System.Collections.Generic;
using aPC.Common.Communication;

namespace aPC.Client.Disco.Tests
{
  class TestNotificationClient : INotificationClient
  {
    public TestNotificationClient()
    {
      mIntegratedScenesPushed = new List<string>();
      CustomScenesPushed = new List<string>();
    }

    public void PushCustomScene(string xiScene)
    {
      CustomScenesPushed.Add(xiScene);
    }

    public void PushIntegratedScene(string xiScene)
    {
      mIntegratedScenesPushed.Add(xiScene);
    }

    public int NumberOfCustomScenesPushed
    {
      get { return CustomScenesPushed.Count; }
    }

    public int NumberOfIntegratedScenesPushed
    {
      get { return mIntegratedScenesPushed.Count; }
    }

    private readonly List<string> mIntegratedScenesPushed;
    public readonly List<string> CustomScenesPushed;
  }
}
