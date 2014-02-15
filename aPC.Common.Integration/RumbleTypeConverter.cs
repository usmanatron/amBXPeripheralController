using amBXLib;

namespace aPC.Common.Integration
{
  public static class RumbleTypeConverter
  {
    public static RumbleType GetRumbleType(eRumbleType xiRumbleType)
    {
      switch (xiRumbleType)
      {
        case eRumbleType.Boing:
          return RumbleType.Boing;
        case eRumbleType.Crash:
          return RumbleType.Crash;
        case eRumbleType.Engine:
          return RumbleType.Engine;
        case eRumbleType.Explosion:
          return RumbleType.Explosion;
        case eRumbleType.Hit:
          return RumbleType.Hit;
        case eRumbleType.Quake:
          return RumbleType.Quake;
        case eRumbleType.Rattle:
          return RumbleType.Rattle;
        case eRumbleType.Road:
          return RumbleType.Road;
        case eRumbleType.Shot:
          return RumbleType.Shot;
        case eRumbleType.Thud:
          return RumbleType.Thud;
        case eRumbleType.Thunder:
          return RumbleType.Thunder;
      }

      throw new InvalidRumbleException();
    }
  }
}
