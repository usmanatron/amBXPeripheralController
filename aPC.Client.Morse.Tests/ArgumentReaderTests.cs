using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Client.Morse;
using NUnit.Framework;

namespace aPC.Client.Morse
{
  [TestFixture]
  class ArgumentReaderTests
  {
    [Test]
    public void EmptyArguments_ThrowsUsageException()
    {
      var lArgumentReader = new ArgumentReader("");
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }
  }
}
