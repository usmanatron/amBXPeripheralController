using System;
using NUnit.Framework;
using aPC.Client;
using aPC.Client.Shared;

namespace aPC.Client.Tests
{
  [TestFixture]
  class SettingsTests
  {
    private Settings settings;

    [SetUp]
    public void Setup()
    {
      settings = new Settings();
    }

    [Test]
    public void AssigningNothing_IsInvalid()
    {
      Assert.False(settings.IsValid);
    }

    [Test]
    public void MissingScene_GivesInvalidSettings()
    {
      settings.SetScene(null);

      Assert.IsFalse(settings.IsValid);
    }

    [Test]
    public void MissingSceneName_GivesInvalidSettings()
    {
      settings.SetSceneName(string.Empty);

      Assert.IsFalse(settings.IsValid);
    }
  }
}
