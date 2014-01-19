using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Common.Entities
{
  /// <summary>
  /// The base class for each individual light / fan / rumble source
  /// </summary>
  public abstract class Component
  {
    public abstract eSectionType ComponentType(); 

  }
}
