using aPC.Chromesthesia.Lights.Colour;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia.Tests.Lights.Colour
{
  [TestFixture]
  internal class ColourTriangleTests
  {
    private const int lowerFrequency = 0;
    private const int upperFrequency = 1000;

    private int midpointFrequency
    {
      get { return (upperFrequency - lowerFrequency) / 2; }
    }

    private ColourTriangle triangle;

    [SetUp]
    public void Setup()
    {
      triangle = new ColourTriangle(new Tuple<int, int>(lowerFrequency, upperFrequency));
    }

    [Test]
    public void MidpointOfTriangle_GivesMaximalValue()
    {
      var intensity = triangle.GetValue(midpointFrequency);

      Assert.AreEqual(0.5, intensity);
    }

    [Test]
    [TestCase(250)]
    [TestCase(750)]
    public void HalfwayBetweenMidpointAndEdge_GivesHalfOfMaximumValue(int frequency)
    {
      var intensity = triangle.GetValue(frequency);

      Assert.AreEqual(0.25, intensity);
    }

    [Test]
    [TestCase(lowerFrequency)]
    [TestCase(upperFrequency)]
    public void EdgesOfTriangle_ReturnZero(int frequency)
    {
      var intensity = triangle.GetValue(frequency);

      Assert.AreEqual(0, intensity);
    }

    [Test]
    [TestCase(-200)]
    [TestCase(-1)]
    [TestCase(-1001)]
    [TestCase(12500)]
    public void FrequencyOutsideOfTriangle_ReturnZero(int frequency)
    {
      var intensity = triangle.GetValue(frequency);

      Assert.AreEqual(0, intensity);
    }
  }
}