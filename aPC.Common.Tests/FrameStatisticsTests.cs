using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using aPC.Common.Entities;

namespace aPC.Common.Tests
{
  [TestFixture]
  class FrameStatisticsTests
  {

    [Test]
    public void Test1()
    {
      var lFrame= new Frame()
      {
        Lights = new LightSection()
        {
          North = new Light()
        }
      };
      var lStats = new FrameStatistics(new List<Frame> { lFrame });

      lStats.AreEnabledForComponentAndDirection(eComponentType.Light, eDirection.North);
      Assert.AreEqual(1, lStats.mEnabledDirectionalComponents.Count());
    }
  }
}
