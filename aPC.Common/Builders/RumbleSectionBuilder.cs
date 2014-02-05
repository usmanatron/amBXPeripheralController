using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  class RumbleSectionBuilder : SectionBuilderBase
  {
    public RumbleSectionBuilder()
    {
      mRumbleSection = new RumbleSection();
    }

    public RumbleSectionBuilder WithFadeTime(int xiFadeTime)
    {
      SetFadeTime(mRumbleSection, xiFadeTime);
      return this;
    }

    public RumbleSectionBuilder WithRumble(Rumble xiRumble)
    {
      mRumbleSection.Rumble = xiRumble;
      return this;
    }

    public RumbleSection Build()
    {
      return mRumbleSection;
    }

    private readonly RumbleSection mRumbleSection;
  }
}
