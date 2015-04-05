using aPC.Common.Entities;
using System;
using System.Collections.Generic;

namespace aPC.Common.Builders
{
  public class FanSectionBuilder
  {
    private FanSection fanSection;
    private bool fanSpecified;

    public FanSectionBuilder()
    {
      Reset();
    }

    private void Reset()
    {
      fanSection = new FanSection() { Fans = new List<Fan>() };
      fanSpecified = false;
    }

    public FanSectionBuilder WithAllFans(Fan fan)
    {
      WithFanInDirection(eDirection.East, (Fan)fan.Clone());
      WithFanInDirection(eDirection.West, (Fan)fan.Clone());
      return this;
    }

    public FanSectionBuilder WithFanInDirection(eDirection direction, Fan fan)
    {
      if (fanSection.GetComponentSectionInDirection(direction) != null)
      {
        throw new ArgumentException("Attempted to add multiple fans in the same direction");
      }

      fan.Direction = direction;
      fanSection.Fans.Add(fan);
      fanSpecified = true;
      return this;
    }

    public FanSection Build()
    {
      if (!FanSectionIsValid)
      {
        throw new ArgumentException("Incomplete FanSection built.  At least one fan and the Fade Time must be specified.");
      }

      var builtFanSection = fanSection;
      Reset();
      return builtFanSection;
    }

    private bool FanSectionIsValid
    {
      get
      {
        return fanSpecified;
      }
    }
  }
}