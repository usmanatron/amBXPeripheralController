namespace Common.Communication
{
    public static class CommunicationSettings
    {
      public const string NamedPipeInstance = "amBX_NotificationsPipe";
      public const string MessageQueueName = @".\Private$\amBXNotifications";
      public const string ServiceUrl = @"http://jackalope.zoo.lan/amBXNotification";
    }
}
