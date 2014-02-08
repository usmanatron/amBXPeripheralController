using System.Linq;
using System.Reflection;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Common.Builders
{
  public class SectionBuilderBase
  {
    public FieldInfo GetComponentInfoInDirection(SectionBase xiSection, eDirection xiDirection)
    {
      return GetSectionFields(xiSection)
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, xiDirection));
    }

    public FieldInfo GetPhysicalComponentInfoInDirection(SectionBase xiSection, eDirection xiDirection)
    {
      var lFieldInrightDirection = GetComponentInfoInDirection(xiSection, xiDirection);

      return PhysicalComponentAttribute.IsPhysicalDirection(lFieldInrightDirection)
        ? lFieldInrightDirection
        :
        null;
    }

    private IEnumerable<FieldInfo> GetSectionFields(SectionBase xiSection)
    {
      return xiSection
        .GetType()
        .GetFields();
    }

    protected void SetFadeTime(SectionBase xiSection, int xiFadeTime)
    {
      xiSection.FadeTime = xiFadeTime;
    }
  }
}
