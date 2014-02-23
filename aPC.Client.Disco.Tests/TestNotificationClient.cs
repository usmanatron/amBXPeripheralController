using aPC.Common.Communication;

namespace aPC.Client.Disco.Tests
{
  class TestNotificationClient : INotificationClient
  {
    public TestNotificationClient()
    {
      mIntegratedScenesPushed = 0;
      mCustomScenesPushed = 0;
    }

    public void PushCustomScene(string xiScene)
    {
      mCustomScenesPushed++;
    }

    public void PushIntegratedScene(string xiScene)
    {
      mIntegratedScenesPushed++;
    }

    public int NumberOfCustomScenesPushed
    {
      get { return mCustomScenesPushed; }
    }

    public int NumberOfIntegratedScenesPushed
    {
      get { return mIntegratedScenesPushed; }
    }

    private int mIntegratedScenesPushed;
    private int mCustomScenesPushed;
  }
}
