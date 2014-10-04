using System;
using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  public class FanSectionBuilder : SectionBuilderBase<Fan>
  {
    private readonly FanSection fanSection;
    private bool fanSpecified;

    public FanSectionBuilder()
    {
      fanSection = new FanSection();
      fanSpecified = false;
    }

    public FanSectionBuilder WithFadeTime(int fadeTime)
    {
      SetFadeTime(fanSection, fadeTime);
      return this;
    }

    public FanSectionBuilder WithAllFans(Fan fan)
    {
      WithFanInDirection(eDirection.East, fan);
      WithFanInDirection(eDirection.West, fan);
      return this;
    }

    public FanSectionBuilder WithFanInDirection(eDirection direction, Fan fan)
    {
      var fanExists = fanSection.SetComponentValueInDirection(fan, direction);
      if (!fanExists)
      {
        throw new InvalidOperationException("Attempted to update a fan which does not exist");
      }
      fanSpecified = true;
      return this;
    }

    public FanSection Build()
    {
      if (!FanSectionIsValid)
      {
        throw new ArgumentException("Incomplete FanSection built.  At least one fan and the Fade Time must be specified.");
      }

      return fanSection;
    }

    private bool FanSectionIsValid
    {
      get
      {
        return fanSection.FadeTime != default(int) && fanSpecified;
      }
    }
  }
}
