using System;
using System.Collections.Generic;
using aPC.Common.Communication;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace aPC.Common.Client.Test.Communication
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  public class TestNotificationService : INotificationService, IDisposable
  {
    public TestNotificationService(string xiUrl)
    {
      ClearScenes();
      mHost = new ServiceHost(this);
      mHost.AddServiceEndpoint(typeof(INotificationService),
                                 new BasicHttpBinding(),
                                 xiUrl);
      IncludeExceptionsInTestFaults(mHost);
      mHost.Open();
    }

    private void IncludeExceptionsInTestFaults(ServiceHost xiHost)
    {
      ServiceDebugBehavior debug = xiHost.Description.Behaviors.Find<ServiceDebugBehavior>();

      // if not found - add behavior with setting turned on 
      if (debug == null)
      {
        xiHost.Description.Behaviors.Add(
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
      Scenes = new List<Tuple<bool, string>>();
    }

    public void RunCustomScene(string xiSceneXml)
    {
      ThrowExceptionIfSpecified(xiSceneXml);
      Scenes.Add(new Tuple<bool, string>(false, xiSceneXml));
    }

    public void RunIntegratedScene(string xiSceneName)
    {
      ThrowExceptionIfSpecified(xiSceneName);
      Scenes.Add(new Tuple<bool, string>(true, xiSceneName));
    }

    private void ThrowExceptionIfSpecified(string xiContent)
    {
      if (xiContent == "ThrowException")
      {
        throw new InvalidOperationException("Test exception");
      }
    }

    public void Dispose()
    {
      mHost.Close();
    }

    // Item1 is a boolean specifying if the item pushed was an integrated scene
    // Item2 is the information sent
    public List<Tuple<bool, string>> Scenes { get; private set; }
    private readonly ServiceHost mHost;
  }
}
