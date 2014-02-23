using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using aPC.Client;

namespace aPC.Client.Tests
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    [Test]
    [TestCase("One")]
    [TestCase("One|Two|Three")]
    public void NotHavingTwoArguments_ThrowsException(string xiArguments)
    {
      var lArguments = xiArguments.Split('|').ToList();
      Assert.Throws<UsageException>(() => new ArgumentReader(lArguments).ParseArguments());
    }

    /*
     * Should throw if !=2 arguments
     * First arg must be /I or /f, otherwise throw
     */
  }
}
