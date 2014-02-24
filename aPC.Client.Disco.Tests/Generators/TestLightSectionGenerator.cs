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

    private readonly LightSection mLightSection;
  }
}
