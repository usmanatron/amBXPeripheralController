using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  public class LightSectionBuilder : SectionBuilderBase
  {

    public LightSectionBuilder() : this(new LightSection())
    {
    }

    public LightSectionBuilder(LightSection xiLightSection)
    {
      mLightSection = xiLightSection;
    }


    public LightSectionBuilder WithFadeTime(int xiFadeTime)
    {
      SetFadeTime(mLightSection, xiFadeTime);
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
      var lFieldInfo = GetComponentFieldInfoInDirection(mLightSection, xiDirection);
      lFieldInfo.SetValue(mLightSection, xiLight);

      return this;
    }

    public LightSection Build()
    {
      return mLightSection;
    }

    private LightSection mLightSection;
  }
}
