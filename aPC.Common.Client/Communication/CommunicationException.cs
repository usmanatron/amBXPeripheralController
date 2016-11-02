using System;

namespace aPC.Common.Client.Communication
{
  public class CommunicationException : Exception
  {
    private string applicationId;
    private string message;

    public CommunicationException(string applicationId, string message)
    {
      this.applicationId = applicationId;
      this.message = message;
    }

    public override string ToString()
    {
      return base.ToString();
    }
  }
}
