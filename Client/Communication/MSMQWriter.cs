using System;
using System.Messaging;
using Common;
using Common.Entities;

namespace Client.Communication
{
  class MSMQWriter
  {
    public void SendMessage(amBXScene xiScene)
    {
      try
      {
        var lMessageQueue = new MessageQueue(Communicationsettings.MessageQueueName);
        lMessageQueue.Send(xiScene);
      }
      catch (Exception e)
      {
        Console.WriteLine("Send failed - Could not communicate with Message Queue:" + Environment.NewLine + e.Message);
        
      }
    }
  }
}
