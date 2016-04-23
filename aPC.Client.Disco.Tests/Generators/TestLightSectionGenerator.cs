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

    public LightSection GeneratedScene()
    {
      return lightSection;
    }
  }
}