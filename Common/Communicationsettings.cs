namespace Common
{
    public static class Communicationsettings
    {
      public const string NamedPipeInstance = "amBX_NotificationsPipe";
      public const string MessageQueueName = @".\Private$\amBXNotifications";
      public const bool UseMSMQ = true;
    }
}
