using System.Linq;
using System.Reflection;
using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  public class SectionBuilderBase
  {
    protected FieldInfo GetComponentFieldInfoInDirection(SectionBase xiSection, eDirection xiDirection)
    {
      return xiSection
        .GetType()
        .GetFields()
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, xiDirection));
    }

    protected void SetFadeTime(SectionBase xiSection, int xiFadeTime)
    {
      xiSection.FadeTime = xiFadeTime;
    }
  }
}
