using System.Linq;
using System.Reflection;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Common.Builders
{
  public class SectionBuilderBase
  {
    protected void SetFadeTime(SectionBase xiSection, int xiFadeTime)
    {
      xiSection.FadeTime = xiFadeTime;
    }
  }
}
