using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  /// <summary>
  /// Aids in building Rumble Sections.
  /// </summary>
  public class RumbleSectionBuilder
  {
    private RumbleSection rumbleSection;

    public RumbleSectionBuilder()
    {
      Reset();
    }

    private void Reset()
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
      var builtRumbleSection = rumbleSection;
      Reset();
      return builtRumbleSection;
    }
  }
}