using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  public class SectionBuilderBase<T> where T : IComponent
  {
    protected void SetFadeTime(SectionBase<T> xiSection, int xiFadeTime)
    {
      xiSection.FadeTime = xiFadeTime;
    }
  }
}
