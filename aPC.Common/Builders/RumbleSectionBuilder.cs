using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  /// <summary>
  /// Aids in building Rumble Sections.
  /// </summary>
  public class RumbleSectionBuilder
  {
    private readonly RumbleSection rumbleSection;

    public RumbleSectionBuilder()
    {
      rumbleSection = new RumbleSection();
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