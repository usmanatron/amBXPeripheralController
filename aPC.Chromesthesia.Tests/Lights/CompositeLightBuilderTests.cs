using aPC.Chromesthesia.Lights;
using aPC.Common.Entities;
using NUnit.Framework;
using System;

namespace aPC.Chromesthesia.Tests.Lights
{
  [TestFixture]
  internal class CompositeLightBuilderTests
  {
    private CompositeLightBuilder compositeLightBuilder;
    private Light firstLight;
    private Light secondLight;

    [SetUp]
    public void Setup()
    {
      compositeLightBuilder = new CompositeLightBuilder();
      firstLight = new Light { Red = 0.1f, Green = 0.2f, Blue = 0.3f, FadeTime = 400 };
      secondLight = new Light { Red = 0.4f, Green = 0.5f, Blue = 0.6f, FadeTime = 800 };
    }

    [Test]
    [TestCase(-1)]
    [TestCase(101)]
    public void PercentageValueOutOfRange_Throws(int percentage)
    {
      Assert.Throws<ArgumentException>(() => compositeLightBuilder.BuildCompositeLight(firstLight, secondLight, percentage));
    }

    [Test]
    public void OneHundredPercentOfFirstLight_IgnoresSecondLightValues()
    {
      var compositeLight = compositeLightBuilder.BuildCompositeLight(firstLight, secondLight, 100);

      AssertLightComponentsAreEqual(compositeLight, firstLight);
    }

    [Test]
    [TestCase(0)]
    [TestCase(10)]
    [TestCase(70)]
    [TestCase(100)]
    public void AnyPercentageValue_FadeTimeOfFirstLightAlwaysUsed(int percentage)
    {
      var compositeLight = compositeLightBuilder.BuildCompositeLight(firstLight, secondLight, percentage);

      Assert.AreEqual(compositeLight.FadeTime, firstLight.FadeTime);
      Assert.AreNotEqual(compositeLight.FadeTime, secondLight.FadeTime);
    }

    [Test]
    public void FiftyPercent_BuildsCompositeLightFromBothLights()
    {
      var compositeLight = compositeLightBuilder.BuildCompositeLight(firstLight, secondLight, 50);
      var expectedLight = new Light { Red = 0.25f, Green = 0.35f, Blue = 0.45f, FadeTime = 0 };

      AssertLightComponentsAreEqual(expectedLight, compositeLight);
    }

    private void AssertLightComponentsAreEqual(Light expected, Light actual)
    {
      Assert.AreEqual(expected.Red, actual.Red);
      Assert.AreEqual(expected.Green, actual.Green);
      Assert.AreEqual(expected.Blue, actual.Blue);
    }
  }
}