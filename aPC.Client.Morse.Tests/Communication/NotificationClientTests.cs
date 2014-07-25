using System;
using System.ServiceModel;
using NUnit.Framework;
using aPC.Client.Morse.Communication;
using aPC.Common.Client.Tests.Communication;
using aPC.Common.Defaults;

namespace aPC.Client.Morse.Tests.Communication
{
  [TestFixture]
  class NotificationClientTests
  {
    [TestFixtureSetUp]
    public void SetupTests()
    {
      mHost = new TestNotificationService();
      mClient = new NotificationClient(new EndpointAddress(mHost.Url));
    }

    [TestFixtureTearDown]
    public void FixtureTearDown()
    {
      mHost.Dispose();
    }

    [TearDown]
    public void TearDown()
    {
      mHost.Scenes.Clear();
    }

    [Test]
    public void PushingAnIntegratedScene_ThrowsException()
    {
      Assert.Throws<NotSupportedException>(() => mClient.PushIntegratedScene("blah"));
    }

    [Test]
    public void PushingACustomScene_SendsTheExpectedScene()
    {
      mClient.PushCustomScene("scene");

      Assert.AreEqual(1, mHost.Scenes.Count);
      Assert.AreEqual(false, mHost.Scenes[0].Item1);
      Assert.AreEqual("scene", mHost.Scenes[0].Item2);
    }

    [Test]
    public void PushingACustomAmBXScene_SuccesfullySendsScene()
    {
      var lScene = new DefaultScenes().Rainbow;

      mClient.PushCustomScene(lScene);

      Assert.AreEqual(1, mHost.Scenes.Count);
      Assert.AreEqual(false, mHost.Scenes[0].Item1);
      Assert.AreEqual(mSerialisedRainbow, mHost.Scenes[0].Item2);
    }

    private NotificationClient mClient;
    private TestNotificationService mHost;

    private const string mSerialisedRainbow = @"<?xml version=""1.0"" encoding=""utf-16""?>
<amBXScene xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" IsExclusive=""true"" SceneType=""Desync"">
  <Frames>
    <Frame Length=""1000"" IsRepeated=""true"">
      <Lights FadeTime=""500"">
        <North Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
        <NorthEast Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
        <East Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
        <SouthEast Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
        <South Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
        <SouthWest Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
        <West Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
        <NorthWest Intensity=""1"" Red=""1"" Green=""0"" Blue=""0"" />
      </Lights>
    </Frame>
    <Frame Length=""1000"" IsRepeated=""true"">
      <Lights FadeTime=""500"">
        <North Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
        <NorthEast Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
        <East Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
        <SouthEast Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
        <South Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
        <SouthWest Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
        <West Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
        <NorthWest Intensity=""1"" Red=""1"" Green=""0.5"" Blue=""0"" />
      </Lights>
    </Frame>
    <Frame Length=""1000"" IsRepeated=""true"">
      <Lights FadeTime=""500"">
        <North Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
        <NorthEast Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
        <East Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
        <SouthEast Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
        <South Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
        <SouthWest Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
        <West Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
        <NorthWest Intensity=""1"" Red=""1"" Green=""1"" Blue=""0"" />
      </Lights>
    </Frame>
    <Frame Length=""1000"" IsRepeated=""true"">
      <Lights FadeTime=""500"">
        <North Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
        <NorthEast Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
        <East Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
        <SouthEast Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
        <South Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
        <SouthWest Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
        <West Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
        <NorthWest Intensity=""1"" Red=""0"" Green=""1"" Blue=""0"" />
      </Lights>
    </Frame>
    <Frame Length=""1000"" IsRepeated=""true"">
      <Lights FadeTime=""500"">
        <North Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
        <NorthEast Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
        <East Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
        <SouthEast Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
        <South Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
        <SouthWest Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
        <West Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
        <NorthWest Intensity=""1"" Red=""0"" Green=""0"" Blue=""1"" />
      </Lights>
    </Frame>
    <Frame Length=""1000"" IsRepeated=""true"">
      <Lights FadeTime=""500"">
        <North Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
        <NorthEast Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
        <East Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
        <SouthEast Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
        <South Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
        <SouthWest Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
        <West Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
        <NorthWest Intensity=""1"" Red=""0.3"" Green=""0"" Blue=""0.5"" />
      </Lights>
    </Frame>
    <Frame Length=""1000"" IsRepeated=""true"">
      <Lights FadeTime=""500"">
        <North Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
        <NorthEast Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
        <East Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
        <SouthEast Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
        <South Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
        <SouthWest Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
        <West Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
        <NorthWest Intensity=""1"" Red=""0.56"" Green=""0"" Blue=""1"" />
      </Lights>
    </Frame>
  </Frames>
</amBXScene>";
  }
}
