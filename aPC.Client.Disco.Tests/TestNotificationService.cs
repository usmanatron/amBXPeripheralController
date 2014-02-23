using System;
using System.Collections.Generic;
using System.Linq;
using aPC.Common.Communication;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace aPC.Client.Disco.Tests
{
  //qqUMI Move this to aPC.Common.Communication.Tests?
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  class TestNotificationService : INotificationService, IDisposable
  {
    public TestNotificationService(string xiUrl)
    {
      Scenes = new List<Tuple<bool, string>>();

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
             new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
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

    public string RunCustomScene(string xiSceneXml)
    {
      Scenes.Add(new Tuple<bool, string>(false, xiSceneXml));
      return "";
    }

    public string RunIntegratedScene(string xiSceneName)
    {
      Scenes.Add(new Tuple<bool, string>(true, xiSceneName));
      return "";
    }

    public void Dispose()
    {
      mHost.Close();
    }

    // Item1 is a boolean specifying if the item pushed was an integrated scene
    public List<Tuple<bool, string>> Scenes { get; private set; }
    private ServiceHost mHost;
  }
}
