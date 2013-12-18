using amBXLib;

namespace Common.Integration
{
  public static class RumbleTypeConverter
  {
    public static RumbleType GetRumbleType(string xiRumbleType)
    {
      switch (xiRumbleType.ToLower())
      {
        case "boing":
          return RumbleType.Boing;
        case "crash":
          return RumbleType.Crash;
        case "engine":
          return RumbleType.Engine;
        case "explosion":
          return RumbleType.Explosion;
        case "hit":
          return RumbleType.Hit;
        case "quake":
          return RumbleType.Quake;
        case "rattle":
          return RumbleType.Rattle;
        case "road":
          return RumbleType.Road;
        case "shot":
          return RumbleType.Shot;
        case "thud":
          return RumbleType.Thud;
        case "thunder":
          return RumbleType.Thunder;
      }

      throw new InvalidRumbleException();
    }
  }
}
