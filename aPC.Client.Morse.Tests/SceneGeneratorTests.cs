using aPC.Client.Morse.Codes;
using aPC.Common;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Tests
{
  [TestFixture]
  internal class SceneGeneratorTests
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
      var lGeneratedScene = new SceneGenerator(new Settings("T")).Generate();

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

    #endregion Default Settings

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
      var lSettings = new Settings("T");
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

    [Test]
    public void GeneratedScene_WithMessageRepeated_EndsWithMessageEndMarker()
    {
      var lSettings = new Settings("Test");
      lSettings.RepeatMessage = true;
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();

      var lScene = lGeneratedScene.Frames.Last();
      var lExpectedBlock = new MessageEndMarker();

      Assert.AreEqual(lExpectedBlock.Enabled, lScene.Lights.North == lSettings.Colour);
      Assert.AreEqual(lExpectedBlock.Length * lSettings.UnitLength, lScene.Length);
    }

    [Test]
    public void GeneratedScene_WithMessageNotRepeated_DoesNotEndWithMessageEndMarker()
    {
      var lSettings = new Settings("Test");
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();

      var lScene = lGeneratedScene.Frames.Last();
      var lExpectedBlock = new MessageEndMarker();

      Assert.AreNotEqual(lExpectedBlock.Enabled, lScene.Lights.North == lSettings.Colour);
      Assert.AreNotEqual(lExpectedBlock.Length * lSettings.UnitLength, lScene.Length);
    }

    #endregion Switch changes

    #region Message Tests

    [Test]
    public void GeneratedScene_WithSingleCharacterMessage_AndMessageIsNotRepeated_ReturnsCharacter()
    {
      var lSettings = new Settings("T");
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();

      Assert.AreEqual(1, lGeneratedScene.Frames.Count);
      var lFrame = lGeneratedScene.Frames.Single();

      Assert.IsNotNull(lFrame.Lights);
      Assert.AreEqual(lSettings.Colour, lFrame.Lights.North); // Sufficient to just test North
      Assert.AreEqual(new Dash().Length * lSettings.UnitLength, lFrame.Length);
    }

    [Test]
    public void GeneratedScene_WithSingleCharacterMessage_AndMessageIsRepeated_ReturnsCharacterAndMessageEndMarker()
    {
      var lSettings = new Settings("T");
      lSettings.RepeatMessage = true;
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();

      Assert.AreEqual(2, lGeneratedScene.Frames.Count);

      // First frame should be a dash (which is T)
      // Second frame should be the "end of message" marker
      var lFirstExpectedFrame = new Dash();
      var lSecondExpectedFrame = new MessageEndMarker();

      Assert.IsNotNull(lGeneratedScene.Frames[0].Lights);
      Assert.AreEqual(lSettings.Colour, lGeneratedScene.Frames[0].Lights.North); // Sufficient to just test North
      Assert.AreEqual(lFirstExpectedFrame.Length * lSettings.UnitLength, lGeneratedScene.Frames[0].Length);
      Assert.IsNotNull(lGeneratedScene.Frames[1].Lights);
      Assert.AreEqual(DefaultLights.Off, lGeneratedScene.Frames[1].Lights.North);
      Assert.AreEqual(lSecondExpectedFrame.Length * lSettings.UnitLength, lGeneratedScene.Frames[1].Length);
    }

    #endregion Message Tests

    //qqUMI Consider moving this idea into SectionBaseExtensions and call it RealDirections or similar (non-compound)
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