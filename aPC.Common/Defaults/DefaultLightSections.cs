using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultLightSections
  {
    public static LightSection Red = new LightSectionBuilder().WithAllLights(DefaultLights.Red).Build();
    public static LightSection Green = new LightSectionBuilder().WithAllLights(DefaultLights.Green).Build();
    public static LightSection Yellow = new LightSectionBuilder().WithAllLights(DefaultLights.Yellow).Build();
    public static LightSection SoftYellow = new LightSectionBuilder().WithAllLights(DefaultLights.SoftYellow).Build();
    public static LightSection Orange = new LightSectionBuilder().WithAllLights(DefaultLights.Orange).Build();
    public static LightSection Off = new LightSectionBuilder().WithAllLights(DefaultLights.Off).Build();
    public static LightSection Blue = new LightSectionBuilder().WithAllLights(DefaultLights.Blue).Build();
    public static LightSection Indigo = new LightSectionBuilder().WithAllLights(DefaultLights.Indigo).Build();
    public static LightSection Violet = new LightSectionBuilder().WithAllLights(DefaultLights.Violet).Build();

    public static LightSection JiraBlue = new LightSectionBuilder().WithAllLights(DefaultLights.JiraBlue).Build();
    public static LightSection SoftPink = new LightSectionBuilder().WithAllLights(DefaultLights.SoftPink).Build();
    public static LightSection StrongPurple = new LightSectionBuilder().WithAllLights(DefaultLights.StrongPurple).Build();
  }
}