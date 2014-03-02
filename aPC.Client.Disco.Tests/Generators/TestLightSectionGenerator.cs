using aPC.Client.Disco.Generators;
using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Client.Disco.Tests.Generators
{
  internal class TestLightSectionGenerator : IGenerator<LightSection>
  {
    public TestLightSectionGenerator()
    {
      mLightSection = DefaultLightSections.Orange;
    }

    public LightSection Generate()
    {
      return mLightSection;
    }

    public string GeneratedScene()
    {
      return @"<?xml version=""1.0"" encoding=""utf-16""?>
<amBXScene xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" IsExclusive=""false"" IsEvent=""false"" IsSynchronised=""false"">
  <Frames>
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
  </Frames>
</amBXScene>";
    }

    private readonly LightSection mLightSection;
  }
}
