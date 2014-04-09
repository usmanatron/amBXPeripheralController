using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultLightSections
  {
    public static LightSection Red = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Red).Build();
    public static LightSection Green = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Green).Build();
    public static LightSection Yellow = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Yellow).Build();
    public static LightSection SoftYellow = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.SoftYellow).Build();
    public static LightSection Orange = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Orange).Build();
    public static LightSection Off = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Off).Build();
    public static LightSection Blue = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Blue).Build();
    public static LightSection Indigo = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Indigo).Build();
    public static LightSection Violet = new LightSectionBuilder().WithFadeTime(500).WithAllLights(DefaultLights.Violet).Build();

    public static LightSection JiraBlue = new LightSectionBuilder().WithFadeTime(100).WithAllLights(DefaultLights.JiraBlue).Build();
    public static LightSection SoftPink = new LightSectionBuilder().WithFadeTime(100).WithAllLights(DefaultLights.SoftPink).Build();
    public static LightSection StrongPurple = new LightSectionBuilder().WithFadeTime(200).WithAllLights(DefaultLights.StrongPurple).Build();
  }
}
