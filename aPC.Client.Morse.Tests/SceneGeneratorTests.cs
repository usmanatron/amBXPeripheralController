using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using aPC.Client.Morse;
using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Defaults;

namespace aPC.Client.Morse.Tests
{
  [TestFixture]
  class SceneGeneratorTests
  {
    [Test]
    public void Generator_ReturnsScene()
    {
      var lGeneratedScene = new SceneGenerator(new Settings("Test")).Generate();
      Assert.AreEqual(typeof(amBXScene), lGeneratedScene.GetType());
    }

    #region Default Settings

    [Test]
    // "Is not repeated" implies that this is an event
    public void GeneratedScene_WithDefaultSettings_IsNotRepeated()
    {
      var lGeneratedScene = new SceneGenerator(new Settings("Test")).Generate();
      Assert.AreEqual(eSceneType.Event, lGeneratedScene.SceneType);
    }

    [Test]
    public void GeneratedScene_WithDefaultSettings_HasLightsEnabledOnAllFrames()
    {
      var lGeneratedScene = new SceneGenerator(new Settings("Test")).Generate();
      
      foreach (var lLight in lGeneratedScene.Frames.Select(frame => frame.Lights))
      {
        foreach (eDirection lDirection in ApplicableLightDirections)
        {
          Assert.IsNotNull(lLight.GetComponentValueInDirection(lDirection));
        }
      }
    }

    [Test]
    public void GeneratedScene_WithDefaultSettings_HasRumblesDisabledOnAllFrames()
    {
      var lGeneratedScene = new SceneGenerator(new Settings("Test")).Generate();

      foreach (var lRumble in lGeneratedScene.Frames.Select(frame => frame.Rumbles))
      {
        Assert.IsNull(lRumble.GetComponentValueInDirection(eDirection.Center));
      }
    }

    [Test]
    public void GeneratedScene_WithDefaultSettings_HasWhiteLights()
    {
      var lWhiteLight = DefaultLights.White;
      var lGeneratedScene = new SceneGenerator(new Settings("Test")).Generate();
      
      foreach (var lLight in lGeneratedScene.Frames.Select(frame => frame.Lights))
      {
        foreach (eDirection lDirection in ApplicableLightDirections)
        {
          Assert.AreEqual(lWhiteLight, lLight.GetComponentValueInDirection(lDirection));
        }
      }
    }

    [Test]
    public void GeneratedScene_WithDefaultSettings_HasLengthsInMultiplesOf100()
    {
      var lGeneratedScene = new SceneGenerator(new Settings("Test")).Generate();

      foreach (var lLight in lGeneratedScene.Frames)
      {
        Assert.AreEqual(0, lLight.Length % 100);
      }
    }

    #endregion

    #region Switch changes

    [Test]
    // "Is repeated" implies that this is a synched event with all frames repeated
    public void GeneratedScene_WithRepetitionEnabled_IsRepeated()
    {
      var lSettings = new Settings("Test");
      lSettings.RepeatMessage = true;
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();
      
      Assert.AreEqual(eSceneType.Sync, lGeneratedScene.SceneType);

      foreach (var lFrame in lGeneratedScene.Frames)
      {
        Assert.AreEqual(true, lFrame.IsRepeated);
      }
    }

    [Test]
    // In general, so long as the Settings class is built using ArgumentReader,
    // this Exception should not be thrown (as we enforce either lights or rumble enabled.
    public void GeneratedScene_WithLightsAndRumblesDisabled_Throws()
    {
      var lSettings = new Settings("Test");
      lSettings.LightsEnabled = false;
      
      Assert.Throws<ArgumentException>(() => new SceneGenerator(lSettings).Generate());
    }

    [Test]
    public void GeneratedScene_WithRumbleEnabled_HasRumblesEnabledOnAllFrames()
    {
      var lSettings = new Settings("Test");
      lSettings.RumblesEnabled = true;
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();

      foreach (var lRumble in lGeneratedScene.Frames.Select(frame => frame.Rumbles))
      {
        Assert.IsNotNull(lRumble.GetComponentValueInDirection(eDirection.Center));
      }
    }

    [Test]
    public void GeneratedScene_WithDifferentColouredLights_IsPropogatedToScene()
    {
      var lSettings = new Settings("Test");
      lSettings.Colour = DefaultLights.Red;
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();

      foreach (var lLight in lGeneratedScene.Frames.Select(frame => frame.Lights))
      {
        foreach (eDirection lDirection in ApplicableLightDirections)
        {
          Assert.AreEqual(DefaultLights.Red, lLight.GetComponentValueInDirection(lDirection));
        }
      }
    }

    [Test]
    public void GeneratedScene_WithOverriddenUnitLength_HasLengthsInMultiplesOfNewLength()
    {
      var lSettings = new Settings("Test");
      lSettings.UnitLength = 17;
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();

      foreach (var lLight in lGeneratedScene.Frames)
      {
        Assert.AreEqual(0, lLight.Length % 17);
      }
    }

    #endregion

    //qqUMI Test the actual message now!



    private List<eDirection> ApplicableLightDirections
    {
      get
      {
        return ((eDirection[])Enum.GetValues(typeof(eDirection)))
          .Where(dirn => dirn != eDirection.Center &&
                         dirn != eDirection.Everywhere)
          .ToList();
      }
    }
  }
}
