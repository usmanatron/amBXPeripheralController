using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Client.Disco.Generators
{
  public class RandomLightSectionGenerator : IGenerator<LightSection>
  {
    public RandomLightSectionGenerator(Settings xiSettings, Random xiRandom)
    {
      mSettings = xiSettings;
      mRandom = xiRandom;
    }

    public LightSection Generate()
    {
      var lSectionBuilder = new LightSectionBuilder()
        .WithFadeTime(mSettings.FadeTime);

      foreach (eDirection lDirection in Enum.GetValues(typeof(eDirection)).Cast<eDirection>())
      {
        lSectionBuilder.WithLightInDirectionIfPhysical(lDirection, GetRandomLight());
      }

      return lSectionBuilder.Build();
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
