using System;
using System.Runtime.Serialization;

namespace aPC.Common.Client
{
  [Serializable]
  public abstract class UsageExceptionBase : Exception
  {
    private readonly string userDescription;

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
  }
}