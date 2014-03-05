using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using aPC.Common.Entities;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  class SceneHandlerBaseTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      mInitialScene = new SceneAccessor().GetScene("default_redvsblue");
    }

    [Test]
    public void NewSceneHandler_IsNotDormant()
    {
      var lHandler = new TestSceneHandler(mInitialScene);
      Assert.IsFalse(lHandler.IsDormant);
    }

    //[Test] //qqUMI - doesn't work yet.  Need to override equals
    public void GetNextFrame_GetsExpectedData()
    {
      var lHandler = new TestSceneHandler(mInitialScene);
      
      var lFrame = lHandler.NextFrame;
      var lExpectedFrame = mInitialScene.Frames.First();

      Assert.AreEqual(lExpectedFrame.Length, lFrame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lFrame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lFrame.Lights);
    }

    /* Check GetNextFrame works
     * Check Advance works (do advance then check second frame against expected
     * 
     * Check UpdateScene works - confirm Scene is newand check first frame
     * Check events work
    */


    [Test]
    public void UpdateScene_UpdatesScene()
    {
    }

    private amBXScene mInitialScene;
  }
}
