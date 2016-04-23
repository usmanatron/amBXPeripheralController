using aPC.Common.Client.Communication;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;

namespace aPC.Common.Client.Tests.Communication
{
  public class TestNotificationClient : NotificationClientBase
  {
    public readonly List<string> IntegratedScenesPushed;
    public readonly List<amBXScene> CustomScenesPushed;

    public TestNotificationClient()
      : base(new HostnameAccessor())
    {
      IntegratedScenesPushed = new List<string>();
      CustomScenesPushed = new List<amBXScene>();
    }

    protected override bool SupportsScenes
    {
      get { return true; }
    }

    protected override bool SupportsSceneNames
    {
      get { return true; }
    }

    public override void PushScene(amBXScene scene)
    {
      CustomScenesPushed.Add(scene);
    }

    public override void PushSceneName(string sceneName)
    {
      IntegratedScenesPushed.Add(sceneName);
    }

    public override string[] GetAvailableScenes()
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