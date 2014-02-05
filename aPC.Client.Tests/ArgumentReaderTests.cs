using System;
using System.Collections.Generic;
using NUnit.Framework;
using aPC.Client;

namespace aPC.Client.Tests
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    [Test]
    [TestCase()]
    public void NotHavingTwoArguments_ThrowsException(List<string> xiArguments)
    {
      //Assert.Throws<UsageException>(new ArgumentReader());
    }


    /*
     * Should throw if !=2 arguments
     * First arg must be /I or /f, otherwise throw
     */
  }
}
