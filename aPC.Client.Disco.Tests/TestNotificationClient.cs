using System.Collections.Generic;
using aPC.Common.Communication;

namespace aPC.Client.Disco.Tests
{
  class TestNotificationClient : INotificationClient
  {
    public TestNotificationClient()
    {
      mIntegratedScenesPushed = new List<string>();
      mCustomScenesPushed = new List<string>();
    }

    public void PushCustomScene(string xiScene)
    {
      mCustomScenesPushed.Add(xiScene);
    }

    public void PushIntegratedScene(string xiScene)
    {
      mIntegratedScenesPushed.Add(xiScene);
    }

    public int NumberOfCustomScenesPushed
    {
      get { return mCustomScenesPushed.Count; }
    }

    public int NumberOfIntegratedScenesPushed
    {
      get { return mIntegratedScenesPushed.Count; }
    }

    private readonly List<string> mIntegratedScenesPushed;
    private readonly List<string> mCustomScenesPushed;
  }
}
