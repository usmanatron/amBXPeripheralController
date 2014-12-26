using aPC.Client.Disco.Generators;
using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Client.Disco.Tests.Generators
{
  internal class TestLightSectionGenerator : IGenerator<LightSection>
  {
    private readonly LightSection lightSection;

    public TestLightSectionGenerator()
    {
      lightSection = DefaultLightSections.Orange;
    }

    public LightSection Generate()
    {
      return lightSection;
    }

    public string GeneratedScene()
    {
      return @"<?xml version=""1.0"" encoding=""utf-16""?>
<amBXScene xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <IsExclusive>false</IsExclusive>
  <SceneType>Desync</SceneType>
  <Frames>
    <Frame>
      <Length>1000</Length>
      <IsRepeated>true</IsRepeated>
      <Lights>
        <North>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </North>
        <NorthEast>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </NorthEast>
        <East>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </East>
        <SouthEast>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </SouthEast>
        <South>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </South>
        <SouthWest>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </SouthWest>
        <West>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </West>
        <NorthWest>
          <FadeTime>100</FadeTime>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </NorthWest>
      </Lights>
    </Frame>
  </Frames>
</amBXScene>";
    }
  }
}