using System.Collections.Generic;

namespace aPC.Common.Entities
{
  public interface IComponentSection
  {
    IEnumerable<IDirectionalComponent> GetComponents();
  }
}