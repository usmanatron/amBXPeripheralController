using aPC.Common.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Builders
{
  public class LightSectionBuilder : SectionBuilderBase<Light>
  {
    public LightSectionBuilder()
    {
      mLightSection = new LightSection();
      mLightSpecified = false;
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
      mLightSection.SetComponentValueInDirection(xiLight, xiDirection);
      mLightSpecified = true;
      return this;
    }

    public LightSectionBuilder WithLightInDirectionIfPhysical(eDirection xiDirection, Light xiLight)
    {
      var lFieldInfo = mLightSection.GetPhysicalComponentInfoInDirection(xiDirection);
      if (lFieldInfo != null)
      {
        SetLight(lFieldInfo, xiLight);
      }

      return this;
    }

    private void SetLight(FieldInfo xiFieldInfo, Light xiLight)
    {
      xiFieldInfo.SetValue(mLightSection, xiLight);
      mLightSpecified = true;
    }

    public LightSection Build()
    {
      if (!LightSectionIsValid)
      {
        throw new ArgumentException("Incomplete LightSection built.  At least one light and the Fade Time must be specified.");
      }

      return mLightSection;
    }

    private bool LightSectionIsValid
    {
      get
      {
        return mLightSection.FadeTime != default(int) && mLightSpecified;
      }
    }

    private LightSection mLightSection;
    private bool mLightSpecified;
  }
}
