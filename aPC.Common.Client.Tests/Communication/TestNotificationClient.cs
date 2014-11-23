using aPC.Common.Client.Communication;
using System;
using System.Collections.Generic;

namespace aPC.Common.Client.Tests.Communication
{
  public class TestNotificationClient : NotificationClientBase
  {
    public readonly List<string> IntegratedScenesPushed;
    public readonly List<string> CustomScenesPushed;

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

    public override void PushCustomScene(string scene)
    {
      CustomScenesPushed.Add(scene);
    }

    public override void PushIntegratedScene(string scene)
    {
      IntegratedScenesPushed.Add(scene);
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
  }
}