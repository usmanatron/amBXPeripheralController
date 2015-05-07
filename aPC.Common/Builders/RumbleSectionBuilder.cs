using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
      rumbleSection = new RumbleSection { Rumbles = new List<Rumble>() };
    }

    public RumbleSectionBuilder WithRumbleInDirection(eDirection direction, Rumble rumble)
    {
      if (rumbleSection.GetComponentSectionInDirection(direction) != null)
      {
        throw new ArgumentException("Attempted to add multiple Rumbles in the same direction");
      }

      rumble.Direction = direction;
      rumbleSection.Rumbles.Add(rumble);
      return this;
    }

    public RumbleSection Build()
    {
      if (!RumbleSectionIsValid)
      {
        throw new ArgumentException("Incomplete RumbleSection built - at least one rumble must be specified.");
      }

      var builtRumbleSection = rumbleSection;
      Reset();
      return builtRumbleSection;
    }

    private bool RumbleSectionIsValid
    {
      get
      {
        return rumbleSection.Rumbles.Any();
      }
    }
  }
}