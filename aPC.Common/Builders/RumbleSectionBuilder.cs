using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  /// <summary>
  /// Aids in building Rumble Sections.
  /// </summary>
  public class RumbleSectionBuilder : SectionBuilderBase<Rumble>
  {
    private readonly RumbleSection rumbleSection;

    public RumbleSectionBuilder()
    {
      rumbleSection = new RumbleSection();
    }

    public RumbleSectionBuilder WithFadeTime(int fadeTime)
    {
      SetFadeTime(rumbleSection, fadeTime);
      return this;
    }

    public RumbleSectionBuilder WithRumble(Rumble rumble)
    {
      rumbleSection.Rumble = rumble;
      return this;
    }

    public RumbleSection Build()
    {
      return rumbleSection;
    }
  }
}
