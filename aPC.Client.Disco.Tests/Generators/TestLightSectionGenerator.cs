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
        <FadeTime>500</FadeTime>
        <North>
          <Intensity>1</Intensity>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </North>
        <NorthEast>
          <Intensity>1</Intensity>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </NorthEast>
        <East>
          <Intensity>1</Intensity>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </East>
        <SouthEast>
          <Intensity>1</Intensity>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </SouthEast>
        <South>
          <Intensity>1</Intensity>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </South>
        <SouthWest>
          <Intensity>1</Intensity>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </SouthWest>
        <West>
          <Intensity>1</Intensity>
          <Red>1</Red>
          <Green>0.5</Green>
          <Blue>0</Blue>
        </West>
        <NorthWest>
          <Intensity>1</Intensity>
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