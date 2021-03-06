﻿using aPC.Common.Entities;
using System;

namespace aPC.Chromesthesia.Lights
{
  internal class CompositeLightBuilder
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
        FadeTime = firstLight.FadeTime
      };
    }

    private bool IsPercentage(int value)
    {
      return 0 <= value && value <= 100;
    }

    private float BuildCompositeValue(float firstValue, float secondValue, int firstValuePercentage)
    {
      var secondValuePercentage = 100 - firstValuePercentage;

      return (firstValue * GetPercentage(firstValuePercentage)) + (secondValue * GetPercentage(secondValuePercentage));
    }

    private float GetPercentage(int value)
    {
      return value / 100f;
    }
  }
}