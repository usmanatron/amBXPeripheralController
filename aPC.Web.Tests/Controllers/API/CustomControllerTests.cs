using aPC.Common.Client.Tests.Communication;
using aPC.Common.Communication;
using aPC.Common.Entities;
using aPC.Web.Controllers.API;
using NUnit.Framework;

namespace aPC.Web.Tests.Controllers.API
{
  [TestFixture]
  internal class CustomControllerTests
  {
    private INotificationClient client;
    private CustomController controller;

    private const string validJsonInput = "";

    // These are invalidated by having Scene Types which don't exist
    private const string invalidXmlInput = "";

    private const string invalidJsonInput = "";

    [SetUp]
    public void Setup()
    {
      client = new TestNotificationClient();
      controller = new CustomController(client);
    }

    [Test]
    [TestCaseSource("ValidInput")]
    public void Parse_WithValidInput_ReturnsOK(string scene)
    {
      amBXScene sceneOut;

      Assert.DoesNotThrow(() => sceneOut = controller.Parse(scene));
    }

    [Test]
    [TestCaseSource("InvalidInput")]
    public void Parse_WithInvalidInput_ReturnsOK(string scene)
    {
      amBXScene sceneOut;

      Assert.DoesNotThrow(() => sceneOut = controller.Parse(scene));
    }

    private string[] ValidInput()
    {
      return new[] { validXmlInput, validJsonInput };
    }

    private string[] InvalidInput()
    {
      return new[] { invalidXmlInput, invalidJsonInput };
    }

    private const string validXmlInput = @"<amBXScene>
  <IsExclusive>true</IsExclusive>
  <SceneType>Desync</SceneType>
  <Frames>
    <Frame>
      <Length>500</Length>
      <IsRepeated>true</IsRepeated>
      <Lights>
        <FadeTime>200</FadeTime>
        <North>
          <Red>1</Red>
          <Green>0</Green>
          <Blue>1</Blue>
        </North>
      </Lights>
    </Frame>
    <Frame>
      <Length>500</Length>
      <IsRepeated>true</IsRepeated>
      <Lights>
        <FadeTime>200</FadeTime>
        <North>
          <Red>0</Red>
          <Green>1</Green>
          <Blue>0</Blue>
        </North>
      </Lights>
    </Frame>
  </Frames>
</amBXScene>";
  }
}