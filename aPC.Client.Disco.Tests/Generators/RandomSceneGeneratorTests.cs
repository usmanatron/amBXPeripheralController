using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aPC.Client.Disco.Generators;
using aPC.Common.Entities;
using aPC.Common.Defaults;
using aPC.Client.Disco.Tests;
using NUnit.Framework;

namespace aPC.Client.Disco.Tests.Generators
{
  [TestFixture]
  class RandomSceneGeneratorTests
  {
    [TestFixtureSetUp]
    public void SetupGenerator()
    {
      mSettings = new Settings();
      mGenerator = new RandomSceneGenerator(mSettings, new TestRandom(0.25));
    }

    [Test]
    public void NewScene_HasSpecificNumberOfFrames()
    {
      var lScene = mGenerator.Get();
      Assert.AreEqual(mSettings.FramesPerScene, lScene.Frames.Count);
    }

    [Test]
    // Currently fans and rumbles are not supported
    public void NewScene_DisablesFansAndRumble()
    {
      var lScene = mGenerator.Get();

      Assert.AreEqual(DefaultFanSections.Off, lScene.Frames[0].Fans);
      Assert.AreEqual(DefaultRumbleSections.Off, lScene.Frames[0].Rumbles);
    }

    private Settings mSettings;
    private RandomSceneGenerator mGenerator;
  }
}
