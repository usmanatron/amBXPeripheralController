using System;
using System.Messaging;
using Common;

namespace Server.Communication
{
  static class MSMQAdministrator
  {
    public static void SetupQueueAccess()
    {
      RecreateQueue();
      mMessageQueue = new MessageQueue(Communicationsettings.MessageQueueName);
    }

    private static void RecreateQueue()
    {
      if (MessageQueue.Exists(Communicationsettings.MessageQueueName))
      {
        MessageQueue.Delete(Communicationsettings.MessageQueueName);
      }
      MessageQueue.Create(Communicationsettings.MessageQueueName);
    }

    public static Message Read()
    {
      lock (mMessageQueue)
      {
        return mMessageQueue.Receive(new TimeSpan(100));
      }
    }

    public static void DeleteAll()
    {
      lock (mMessageQueue)
      {
        mMessageQueue.Purge();
      }
    }

    private static MessageQueue mMessageQueue;
  }
}
