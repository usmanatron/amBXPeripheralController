using System;
using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  public class FanSectionBuilder : SectionBuilderBase<Fan>
  {
    public FanSectionBuilder()
    {
      mFanSection = new FanSection();
      mFanSpecified = false;
    }

    public FanSectionBuilder WithFadeTime(int xiFadeTime)
    {
      SetFadeTime(mFanSection, xiFadeTime);
      return this;
    }

    public FanSectionBuilder WithAllFans(Fan xiFan)
    {
      WithFanInDirection(eDirection.East, xiFan);
      WithFanInDirection(eDirection.West, xiFan);
      return this;
    }

    public FanSectionBuilder WithFanInDirection(eDirection xiDirection, Fan xiFan)
    {
      mFanSection.SetComponentValueInDirection(xiFan, xiDirection);
      mFanSpecified = true;
      return this;
    }

    public FanSection Build()
    {
      if (!FanSectionIsValid)
      {
        throw new ArgumentException("Incomplete FanSection built.  At least one fan and the Fade Time must be specified.");
      }

      return mFanSection;
    }

    private bool FanSectionIsValid
    {
      get
      {
        return mFanSection.FadeTime != default(int) && mFanSpecified;
      }
    }

    private readonly FanSection mFanSection;
    private bool mFanSpecified;
  }
}
