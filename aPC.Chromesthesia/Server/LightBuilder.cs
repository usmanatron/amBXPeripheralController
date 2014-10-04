using aPC.Chromesthesia.Pitch;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Server
{
  class LightBuilder
  {
    public Light BuildLightFrom(PitchResult pitchResult)
    {
      var light = new Light();
      light.Blue = pitchResult.PeakPitch.averageFrequency / 600;
      return light;
    }
  }
}
