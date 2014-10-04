using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia.Server
{
  class CompositeLightBuilder
  {
    public Light BuildCompositeLight(Light firstLight, Light secondLight, int firstLightPercentage)
    {
      if (!IsPercentage(firstLightPercentage))
      {
        throw new ArgumentException("Unexpected percentage (must be between 0 and 100)");
      }

      return new Light
      {
        Red = BuildCompositeValue(firstLight.Red, secondLight.Red, firstLightPercentage),
        Blue = BuildCompositeValue(firstLight.Blue, secondLight.Blue, firstLightPercentage),
        Green = BuildCompositeValue(firstLight.Green, secondLight.Green, firstLightPercentage),
        Intensity = BuildCompositeValue(firstLight.Intensity, secondLight.Intensity, firstLightPercentage)
      };
    }

    private bool IsPercentage(int value)
    {
      return 0 <= value && value <= 100;
    }

    private float BuildCompositeValue(float firstValue, float secondValue, int firstValuePercentage)
    {
      var secondValuePercentage = 100 - firstValuePercentage;

      return (firstValue * firstValuePercentage) + (secondValue * secondValuePercentage);
    }
  }
}