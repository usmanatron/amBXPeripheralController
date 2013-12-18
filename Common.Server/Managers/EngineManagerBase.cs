using System;
using amBXLib;

namespace Common.Server.Managers
{
  // Manages the amBXEngine interface - deals with adding and setting stuff etc.
  public abstract class EngineManagerBase : IDisposable
  {
    protected EngineManagerBase()
    {
      mEngine = new amBX(1, 0, "amBXNotification", "1.0");
      InitialiseEngine(mEngine);
    }

    private void InitialiseEngine(amBX xiEngine)
    {
      NorthLight     = xiEngine.CreateLight(CompassDirection.North,     RelativeHeight.AnyHeight);
      NorthEastLight = xiEngine.CreateLight(CompassDirection.NorthEast, RelativeHeight.AnyHeight);
      EastLight      = xiEngine.CreateLight(CompassDirection.East,      RelativeHeight.AnyHeight);
      SouthEastLight = xiEngine.CreateLight(CompassDirection.SouthEast, RelativeHeight.AnyHeight);
      SouthLight     = xiEngine.CreateLight(CompassDirection.South,     RelativeHeight.AnyHeight);
      SouthWestLight = xiEngine.CreateLight(CompassDirection.SouthWest, RelativeHeight.AnyHeight);
      WestLight      = xiEngine.CreateLight(CompassDirection.West,      RelativeHeight.AnyHeight);
      NorthWestLight = xiEngine.CreateLight(CompassDirection.NorthWest, RelativeHeight.AnyHeight);

      EastFan = xiEngine.CreateFan(CompassDirection.East, RelativeHeight.AnyHeight);
      WestFan = xiEngine.CreateFan(CompassDirection.West, RelativeHeight.AnyHeight);

      Rumble = xiEngine.CreateRumble(CompassDirection.Everywhere, RelativeHeight.AnyHeight);
    }

    public void Dispose()
    {
      mEngine.Dispose();
    }

    private readonly amBX mEngine;

    #region amBXLib Members

    #region Lights

    protected amBXLight NorthLight;
    protected amBXLight NorthEastLight;
    protected amBXLight EastLight;
    protected amBXLight SouthEastLight;
    protected amBXLight SouthLight;
    protected amBXLight SouthWestLight;
    protected amBXLight WestLight;
    protected amBXLight NorthWestLight;

    #endregion

    #region Fans

    protected amBXFan EastFan;
    protected amBXFan WestFan;

    #endregion

    protected amBXRumble Rumble;

    #endregion
  }
}
