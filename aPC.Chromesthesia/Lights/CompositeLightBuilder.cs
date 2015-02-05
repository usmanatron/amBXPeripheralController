using aPC.Common.Entities;
using System;

namespace aPC.Chromesthesia.Lights
{
  public class CompositeLightBuilder : ICompositeLightBuilder
  {
    private const int minPercentageValue = 0;
    private const int maxPercentageValue = 100;

    public Light BuildCompositeLight(Light firstLight, Light secondLight, int firstLightPercentage)
    {
      if (!IsPercentage(firstLightPercentage))
      {
        var message = string.Format("Unexpected percentage (must be between {0} and {1})", minPercentageValue, maxPercentageValue);
        throw new ArgumentException(message);
      }

      return new Light
      {
        Red = BuildCompositeValue(firstLight.Red, secondLight.Red, firstLightPercentage),
        Blue = BuildCompositeValue(firstLight.Blue, secondLight.Blue, firstLightPercentage),
        Green = BuildCompositeValue(firstLight.Green, secondLight.Green, firstLightPercentage),
        FadeTime = firstLight.FadeTime
      };
    }

    private bool IsPercentage(int value)
    {
      return minPercentageValue <= value && value <= maxPercentageValue;
    }

    private float BuildCompositeValue(float firstValue, float secondValue, int firstValuePercentage)
    {
      var secondValuePercentage = maxPercentageValue - firstValuePercentage;

      return GetPercentage((firstValue * firstValuePercentage) + (secondValue * secondValuePercentage));
    }

    private float GetPercentage(float value)
    {
      return value / (float)maxPercentageValue;
    }
  }
}