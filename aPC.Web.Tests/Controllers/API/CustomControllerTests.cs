using aPC.Common.Client.Tests.Communication;
using aPC.Common.Communication;
using aPC.Common.Entities;
using aPC.Web.Controllers.API;
using NUnit.Framework;

namespace aPC.Web.Tests.Controllers.API
{
  [TestFixture]
  class CustomControllerTests
  {
    [SetUp]
    public void Setup()
    {
      mClient = new TestNotificationClient();
      mController = new CustomController(mClient);
    }

    [Test]
    [TestCaseSource("ValidInput")]
    public void Parse_WithValidInput_ReturnsOK(string xiScene)
    {
      amBXScene lScene;
      
      Assert.DoesNotThrow(() => lScene = mController.Parse(xiScene););
      
    }

    [Test]
    [TestCaseSource("InvalidInput")]
    public void Parse_WithInvalidInput_ReturnsOK(string xiScene)
    {
      amBXScene lScene;
      
      Assert.DoesNotThrow(() => lScene = mController.Parse(xiScene););
      
    }
    
    private string[] ValidInput()
    {
      return new[] {mValidXmlInput, mValidJsonInput};
    }

    private string[] InvalidInput()
    {
      return new[] { mInvalidXmlInput, mInvalidJsonInput };
    }

    private INotificationClient mClient;
    private CustomController mController;

    private const string mValidXmlInput = @"<amBXScene>
  <IsExclusive>true</IsExclusive>
  <SceneType>Desync</SceneType>
  <Frames>
    <Frame>
      <Length>500</Length>
      <IsRepeated>true</IsRepeated>
      <Lights>
        <FadeTime>200</FadeTime>
        <North>
          <Intensity>1</Intensity>
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
          <Intensity>1</Intensity>
          <Red>0</Red>
          <Green>1</Green>
          <Blue>0</Blue>
        </North>
      </Lights>
    </Frame>
  </Frames>
</amBXScene>";

    private const string mValidJsonInput = "";

    // These are invalidated by having Scene Types which don't exist
    private const string mInvalidXmlInput = "";
    private const string mInvalidJsonInput = "";
  }
}
