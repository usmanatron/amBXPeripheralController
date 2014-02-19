using System.Linq;
using System.Reflection;
using aPC.Common.Entities;
using System.Collections.Generic;

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
