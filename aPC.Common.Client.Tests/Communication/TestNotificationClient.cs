using aPC.Common.Client.Communication;
using System;
using System.Collections.Generic;

namespace aPC.Common.Client.Tests.Communication
{
  public class TestNotificationClient : NotificationClientBase
  {
    public TestNotificationClient()
      : base(new HostnameAccessor())
    {
      IntegratedScenesPushed = new List<string>();
      CustomScenesPushed = new List<string>();
    }

    protected override bool SupportsCustomScenes
    {
      get { return true; }
    }

    protected override bool SupportsIntegratedScenes
    {
      get { return true; }
    }

    public override void PushCustomScene(string xiScene)
    {
      CustomScenesPushed.Add(xiScene);
    }

    public override void PushIntegratedScene(string xiScene)
    {
      IntegratedScenesPushed.Add(xiScene);
    }

    public override string[] GetSupportedIntegratedScenes()
    {
      throw new NotImplementedException();
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