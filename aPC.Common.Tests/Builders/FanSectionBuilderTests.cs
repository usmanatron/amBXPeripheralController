using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Common.Tests.Builders
{
  class FanSectionBuilderTests
  {
    /* Missing FadeTime on Build throws exception
     * FadeTime but no fans throws exception
     * Nothing specified throws exception
     * (i.e. we need fade time and at least one fan!)
     * Trying to specify a fan in a dodgy dirn like South throws an exception
     * Updating East then NEast will overwrite East with NEast => Both directions affect each other
     * Can update both fans at same time successfully
     * A FanSection with two different fans specified have them correctly specified
     */

  }
}
