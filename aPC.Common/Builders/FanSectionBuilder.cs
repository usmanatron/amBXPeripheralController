using System;
using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  class FanSectionBuilder : SectionBuilderBase
  {
    public FanSectionBuilder()
    {
      mFanSection = new FanSection();
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
      var lFieldInfo = GetComponentFieldInfoInDirection(mFanSection, xiDirection);
      if (lFieldInfo == null)
      {
        throw new InvalidOperationException("Attempted to specify an unsupported Fan");
      }

      lFieldInfo.SetValue(mFanSection, xiFan);
      return this;
    }

    public FanSection Build()
    {
      return mFanSection;
    }

    private readonly FanSection mFanSection;
  }
}
