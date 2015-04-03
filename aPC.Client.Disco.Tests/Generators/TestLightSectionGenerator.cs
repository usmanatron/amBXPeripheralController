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
      return @"<?xml version=""1.0\"" encoding=""utf-16""?>
<amBXScene xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <IsExclusive>false</IsExclusive>
  <SceneType>Desync</SceneType>
  <Frames>
    <Frame>
      <Length>1000</Length>
      <IsRepeated>true</IsRepeated>
      <LightSection>
        <Lights>
          <Light>
            <Direction>North</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
          <Light>
            <Direction>NorthEast</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
          <Light>
            <Direction>East</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
          <Light>
            <Direction>SouthEast</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
          <Light>
            <Direction>South</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
          <Light>
            <Direction>SouthWest</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
          <Light>
            <Direction>West</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
          <Light>
            <Direction>NorthWest</Direction>
            <FadeTime>100</FadeTime>
            <Red>1</Red>
            <Green>0.5</Green>
            <Blue>0</Blue>
          </Light>
        </Lights>
      </LightSection>
    </Frame>
  </Frames>
</amBXScene>";
    }
  }
}