using aPC.Chromesthesia.Lights;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia.Tests.Lights
{
  [TestFixture]
  internal class CompositeLightSectionBuilderTests
  {
    private CompositeLightSectionBuilder compositeLightSectionBuilder;

    private ICompositeLightBuilder compositeLightBuilder;
    private ILightSectionBuilder lightSectionBuilder;

    [SetUp]
    public void Setup()
    {
      compositeLightBuilder = A.Fake<ICompositeLightBuilder>();
      lightSectionBuilder = A.Fake<ILightSectionBuilder>();
      A.CallTo(lightSectionBuilder)
        .WithReturnType<ILightSectionBuilder>()
        .Returns(lightSectionBuilder);

      compositeLightSectionBuilder = new CompositeLightSectionBuilder(lightSectionBuilder, compositeLightBuilder);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(49)]
    [TestCase(101)]
    [TestCase(200)]
    public void InvalidSidePercentageValue_ThrowsOnBuild(int percentage)
    {
      compositeLightSectionBuilder
        .WithLights(A.Fake<Light>(), A.Fake<Light>())
        .WithSidePercentageOnDiagonal(percentage);

      Assert.Throws<ArgumentException>(() => compositeLightSectionBuilder.Build());
    }

    [Test]
    public void MissingSidePercentage_ThrowsOnBuild()
    {
      compositeLightSectionBuilder.WithLights(A.Fake<Light>(), A.Fake<Light>());

      Assert.Throws<ArgumentException>(() => compositeLightSectionBuilder.Build());
    }

    [Test]
    public void MissingLights_ThrowsOnBuild()
    {
      compositeLightSectionBuilder.WithSidePercentageOnDiagonal(75);

      Assert.Throws<ArgumentException>(() => compositeLightSectionBuilder.Build());
    }

    [Test]
    public void IncompleteFirstLight_ThrowsOnBuild()
    {
      compositeLightSectionBuilder.WithLights(null, A.Fake<Light>());

      Assert.Throws<ArgumentException>(() => compositeLightSectionBuilder.Build());
    }

    [Test]
    public void IncompleteSecondLight_ThrowsOnBuild()
    {
      compositeLightSectionBuilder.WithLights(A.Fake<Light>(), null);

      Assert.Throws<ArgumentException>(() => compositeLightSectionBuilder.Build());
    }

    [Test]
    public void BuildingLightSection_CallsExpectedMethodsAndAssignsCompositeLightsToOtherDirections()
    {
      var sidePercentage = 75;

      var westLight = new Light { Red = 0.2f, Green = 0.2f, Blue = 0.2f, FadeTime = 100 };
      var eastLight = new Light { Red = 0.8f, Green = 0.8f, Blue = 0.8f, FadeTime = 400 };
      var westDiagonalLight = new Light { Red = 0.35f, Green = 0.35f, Blue = 0.35f, FadeTime = 100 };
      var eastDiagonalLight = new Light { Red = 0.65f, Green = 0.65f, Blue = 0.65f, FadeTime = 100 };
      var centreLight = new Light { Red = 0.5f, Green = 0.5f, Blue = 0.5f, FadeTime = 100 };

      A.CallTo(() => compositeLightBuilder.BuildCompositeLight(westLight, eastLight, sidePercentage)).Returns(westDiagonalLight);
      A.CallTo(() => compositeLightBuilder.BuildCompositeLight(westLight, eastLight, 100 - sidePercentage)).Returns(eastDiagonalLight);
      A.CallTo(() => compositeLightBuilder.BuildCompositeLight(westLight, eastLight, 50)).Returns(centreLight);

      var section = compositeLightSectionBuilder
        .WithSidePercentageOnDiagonal(sidePercentage)
        .WithLights(westLight, eastLight)
        .Build();

      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.North, centreLight)).MustHaveHappened(Repeated.Exactly.Once);
      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.NorthEast, eastDiagonalLight)).MustHaveHappened(Repeated.Exactly.Once);
      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.East, eastLight)).MustHaveHappened(Repeated.Exactly.Once);
      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.SouthEast, eastDiagonalLight)).MustHaveHappened(Repeated.Exactly.Once);
      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.South, centreLight)).MustHaveHappened(Repeated.Exactly.Once);
      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.SouthWest, westDiagonalLight)).MustHaveHappened(Repeated.Exactly.Once);
      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.West, westLight)).MustHaveHappened(Repeated.Exactly.Once);
      A.CallTo(() => lightSectionBuilder.WithLightInDirection(eDirection.NorthWest, westDiagonalLight)).MustHaveHappened(Repeated.Exactly.Once);
    }
  }
}