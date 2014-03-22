using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Morse
{
  public class ArgumentReader
  {
    public ArgumentReader(string xiArguments)
    {
      mArguments = xiArguments;
    }

    public void Read()
    {
      if (string.IsNullOrEmpty(mArguments))
      {
        throw new UsageException("Invalid Arguments");
      }
    }

    private string mArguments;
  }
}
