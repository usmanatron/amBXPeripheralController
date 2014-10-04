using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  public class SectionBuilderBase<T> where T : IComponent
  {
    protected void SetFadeTime(SectionBase<T> section, int fadeTime)
    {
      section.FadeTime = fadeTime;
    }
  }
}
