using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Client.Disco
{
  class RandomLightSectionGenerator
  {
    public RandomLightSectionGenerator(Settings xiSettings, Random xiRandom)
    {
      mSettings = xiSettings;
      mRandom = xiRandom;
    }

    public LightSection Generate()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(mSettings.FadeTime);

      foreach (eDirection lDirection in (eDirection[]) Enum.GetValues(typeof(eDirection)))
      {
        lSection.WithLightInDirection(lDirection, GetRandomLight());
      }

      return lSection.Build();
    }

    private Light GetRandomLight()
    {
      return mRandom.NextDouble() < mSettings.ChangeThreshold
        ? null
        : new Light
      {
        Red = mSettings.RedColourWidth.GetScaledValue(mRandom.NextDouble()),
        Blue = mSettings.BlueColourWidth.GetScaledValue(mRandom.NextDouble()),
        Green = mSettings.GreenColourWidth.GetScaledValue(mRandom.NextDouble()),
        Intensity = mSettings.LightIntensityWidth.GetScaledValue(mRandom.NextDouble())
      };
    }

    private Random mRandom;
    private Settings mSettings;
  }
}
