using System;

namespace Client
{
  class UsageException : Exception
  {
    public UsageException(Exception xiException, string xiDescription)
    {
      mException = xiException;
      mUserDescription = xiDescription;
    }

    public UsageException(string xiDescription)
    {
      mUserDescription = xiDescription;
    }

    public void DisplayUsage()
    {
      Console.WriteLine("Error: " + mUserDescription);
      Console.WriteLine();

      if (mException != null)
      {
        Console.WriteLine("Underlying Exception: " + mException.Message);
      }

      var lUsage = "qqUMI Usage";
      Console.WriteLine(lUsage);
    }

    private readonly Exception mException;
    private readonly string mUserDescription;
  }
}
