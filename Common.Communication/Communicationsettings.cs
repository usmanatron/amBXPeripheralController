namespace Common.Communication
{
    public static class CommunicationSettings
    {
      // Template to use for the Url - [HOSTNAME] needs to be replaced with
      // the actual hostname to use.
      public const string HostnameHolder = @"[HOSTNAME]";
      public const string ServiceUrlTemplate = @"http://" + HostnameHolder + "/amBXNotification";
    }
}
