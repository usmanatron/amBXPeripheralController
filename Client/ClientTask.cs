using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Common.Communication;

namespace Client
{
  class ClientTask
  {
    public ClientTask(IEnumerable<string> xiArguments)
    {
      try
      {
        mArgs = new ArgumentReader(xiArguments.ToList());
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
        Environment.Exit(1);
      }
    }

    public void Push()
    {
      var lClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(), 
        CommunicationSettings.ServiceUrl);

      var lOutput = mArgs.IsIntegratedScene 
        ? lClient.CreateChannel().RunIntegratedScene(mArgs.SceneName) 
        : lClient.CreateChannel().RunCustomScene(mArgs.SceneXml);

      if (!string.IsNullOrEmpty(lOutput))
      {
        Console.WriteLine("An error occurred when communicating to the server:");
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine(lOutput);
      }
    }

    private readonly ArgumentReader mArgs;
  }
}
