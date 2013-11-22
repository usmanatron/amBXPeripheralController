using System;
using System.Collections.Generic;
using System.Linq;
using Client.Communication;

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
      var lQueueWriter = new MSMQWriter();
      lQueueWriter.SendMessage(mArgs.InputScene);
    }

    private readonly ArgumentReader mArgs;
  }
}
