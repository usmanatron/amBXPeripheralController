namespace aPC.Common.Communication
{
  public static class CommunicationSettings
  {
    public static string GetServiceUrl(string xiHostname, eApplicationType xiApplicationType)
    {
      return @"http://" + xiHostname + @"/" + xiApplicationType.ToString();
    }
  }

  public enum eApplicationType
  {
    amBXPeripheralController,
    aPCTest
  }
}
