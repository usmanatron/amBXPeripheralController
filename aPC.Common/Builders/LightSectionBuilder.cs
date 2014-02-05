using System.Linq;
using System.Reflection;
using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  public class LightSectionBuilder
  {
    public LightSectionBuilder()
    {
      mLightSection = new LightSection();
    }

    public LightSectionBuilder WithFadeTime(int xiFadeTime)
    {
      mLightSection.FadeTime = xiFadeTime;
      return this;
    }

    public LightSectionBuilder WithAllLights(Light xiLight)
    {
      WithLightInDirection(eDirection.North, xiLight);
      WithLightInDirection(eDirection.NorthEast, xiLight);
      WithLightInDirection(eDirection.East, xiLight);
      WithLightInDirection(eDirection.SouthEast, xiLight);
      WithLightInDirection(eDirection.South, xiLight);
      WithLightInDirection(eDirection.SouthWest, xiLight);
      WithLightInDirection(eDirection.West, xiLight);
      WithLightInDirection(eDirection.NorthWest, xiLight);

      return this;
    }

    public LightSectionBuilder WithLightInDirection(eDirection xiDirection, Light xiLight)
    {
      var lSectionInfo = mLightSection
        .GetType()
        .GetFields()
        .SingleOrDefault(field => MatchesDirection(field, xiDirection));

      lSectionInfo.SetValue(mLightSection, xiLight);

      return this;
    }

    private bool MatchesDirection(FieldInfo xiPropertyInfo, eDirection xiDescription)
    {
      var lAttribute = xiPropertyInfo.GetCustomAttribute<DirectionAttribute>();

      if (lAttribute == null) return false;

      return lAttribute.Direction == xiDescription;
    }

    public LightSection Build()
    {
      return mLightSection;
    }

    private LightSection mLightSection;
  }
}
