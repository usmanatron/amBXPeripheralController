using aPC.Common.Entities;
using System;
using System.Collections.Generic;

namespace aPC.Common.Builders
{
  /// <summary>
  /// Aids in building Rumble Sections.
  /// </summary>
  public class RumbleSectionBuilder
  {
    private RumbleSection rumbleSection;
    private bool rumbleSpecified;

    public RumbleSectionBuilder()
    {
      Reset();
      rumbleSpecified = false;
    }

    private void Reset()
    {
      rumbleSection = new RumbleSection() { Rumbles = new List<Rumble>() };
    }

    public RumbleSectionBuilder WithRumbleInDirection(eDirection direction, Rumble rumble)
    {
      if (rumbleSection.GetComponentValueInDirection(direction) != null)
      {
        throw new ArgumentException("Attempted to add multiple Rumbles in the same direction");
      }

      rumble.Direction = direction;
      rumbleSection.Rumbles.Add(rumble);
      rumbleSpecified = true;
      return this;
    }

    public RumbleSection Build()
    {
      if (!RumbleSectionIsValid)
      {
        throw new ArgumentException("Incomplete FanSection built.  At least one fan and the Fade Time must be specified.");
      }

      var builtRumbleSection = rumbleSection;
      Reset();
      return builtRumbleSection;
    }

    private bool RumbleSectionIsValid
    {
      get
      {
        return rumbleSpecified;
      }
    }
  }
}