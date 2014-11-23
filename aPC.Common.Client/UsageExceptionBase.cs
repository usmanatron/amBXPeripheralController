using System;

namespace aPC.Common.Client
{
  [Serializable]
  public abstract class UsageExceptionBase : Exception
  {
    protected UsageExceptionBase(string description)
    {
      userDescription = description;
    }

    public void DisplayUsage()
    {
      Console.WriteLine("Error: " + userDescription);
      Console.WriteLine(Environment.NewLine + Environment.NewLine);
      Console.WriteLine(Usage());
    }

    protected abstract string Usage();

    private readonly string userDescription;
  }
}