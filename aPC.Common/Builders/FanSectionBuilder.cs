using aPC.Common.Entities;
using System;

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
      fanSection = new FanSection();
      fanSpecified = false;
    }

    public FanSectionBuilder WithAllFans(Fan fan)
    {
      WithFanInDirection(eDirection.East, fan);
      WithFanInDirection(eDirection.West, fan);
      return this;
    }

    public FanSectionBuilder WithFanInDirection(eDirection direction, Fan fan)
    {
      if (fanSection.GetComponentValueInDirection(direction) != null)
      {
        throw new ArgumentException("Attempted to add multiple fans in the same direction");
      }

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