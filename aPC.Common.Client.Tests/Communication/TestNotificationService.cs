using aPC.Common.Communication;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace aPC.Common.Client.Tests.Communication
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  public sealed class TestNotificationService : INotificationService, IDisposable
  {
    // Item1 is a boolean specifying if the item pushed was an integrated scene
    // Item2 is the information sent
    public List<Tuple<bool, object>> Scenes { get; private set; }

    private string url;
    public string Hostname;
    private readonly ServiceHost host;

    public TestNotificationService()
    {
      Hostname = "localhost";
      url = CommunicationSettings.GetServiceUrl(Hostname, eApplicationType.aPCTest);
      ClearScenes();

      host = new ServiceHost(this);
      host.AddServiceEndpoint(typeof(INotificationService),
                               new BasicHttpBinding(),
                               url);
      IncludeExceptionsInTestFaults();
      host.Open();
    }

    private void IncludeExceptionsInTestFaults()
    {
      ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();

      // if not found - add behavior with setting turned on
      if (debug == null)
      {
        host.Description.Behaviors.Add(
             new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
      }
      else
      {
        // make sure setting is turned ON
        if (!debug.IncludeExceptionDetailInFaults)
        {
          debug.IncludeExceptionDetailInFaults = true;
        }
      }
    }

    public void ClearScenes()
    {
      Scenes = new List<Tuple<bool, object>>();
    }

    public void RunScene(amBXScene scene)
    {
      ThrowExceptionIfSpecified(scene);
      Scenes.Add(new Tuple<bool, object>(false, scene));
    }

    public void RunSceneName(string sceneName)
    {
      ThrowExceptionIfSpecified(sceneName);
      Scenes.Add(new Tuple<bool, object>(true, sceneName));
    }

    public string[] GetAvailableScenes()
    {
      throw new NotImplementedException();
    }

    private void ThrowExceptionIfSpecified(amBXScene scene)
    {
      if (scene.PropertyOneString == "ThrowException")
      {
        throw new InvalidOperationException("Test exception");
      }
    }

    private void ThrowExceptionIfSpecified(string content)
    {
      if (content == "ThrowException")
      {
        throw new InvalidOperationException("Test exception");
      }
    }

    public void Dispose()
    {
      host.Close();
      GC.SuppressFinalize(this);
    }
  }
}