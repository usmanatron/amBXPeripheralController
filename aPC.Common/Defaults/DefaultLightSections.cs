using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  class DefaultLightSections
  {
    public static LightSection Red = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Red).Build();
    public static LightSection Green = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Green).Build();
    public static LightSection Yellow = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Yellow).Build();
    public static LightSection SoftYellow = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.SoftYellow).Build();
    public static LightSection Orange = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Orange).Build();
    public static LightSection Off = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Off).Build();
    
  }
}
