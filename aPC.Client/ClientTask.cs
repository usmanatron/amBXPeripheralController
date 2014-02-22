using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using aPC.Common.Communication;
using aPC.Common;

namespace aPC.Client
{
  class ClientTask
  {
    public ClientTask(IEnumerable<string> xiArguments)
    {
      mNotificationClient = new Communication.NotificationClient();

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
      if (mArgs.IsIntegratedScene)
      {
        mNotificationClient.PushIntegratedScene(mArgs.SceneName);
      }
      else
      {
        mNotificationClient.PushCustomScene(mArgs.SceneXml);
      }
    }

    private readonly ArgumentReader mArgs;
    private INotificationClient mNotificationClient;
  }
}
