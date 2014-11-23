using aPC.Common.Server.Engine;
using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests
{
  // TODO: These tests are dependent on amBXLib => they require ambxrt.
  // It's not possible to include this in source control and they will fail
  // until this file is copied to the appropriate places.
  // Need to work out what I can do about this
  [TestFixture]
  internal class ConversionHelperTests
  {
    [Test]
    [TestCaseSource("Directions")]
    public void eDirectionEnum_AgreesExactlyWithCompassDirectionEnum(eDirection direction)
    {
      var compassDirection = ConversionHelpers.GetDirection(direction);
      Assert.AreEqual((int)direction, (int)compassDirection);
      Assert.AreEqual(direction.ToString(), compassDirection.ToString());
    }

    [Test]
    [TestCaseSource("RumbleTypes")]
    public void eRumbletypeEnum_AgreesExactlyWithRumbleTypeEnum(eRumbleType rumbleType)
    {
      var convertedRumbleType = ConversionHelpers.GetRumbleType(rumbleType);
      Assert.AreEqual((int)convertedRumbleType, (int)convertedRumbleType);
      Assert.AreEqual(convertedRumbleType.ToString(), convertedRumbleType.ToString());
    }

    private readonly eDirection[] directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private readonly eRumbleType[] rumbleTypes = (eRumbleType[])Enum.GetValues(typeof(eRumbleType));
  }
}