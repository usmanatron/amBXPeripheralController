using aPC.Common;
using aPC.Common.Entities;

namespace aPC.Server.Engine
{
  public interface IEngine
  {
    void UpdateLight(eDirection xiDirection, Light xiLight, int xiFadeTime);
    void UpdateFan(eDirection xiDirection, Fan xiFan);
    void UpdateRumble(eDirection xiDirection, Rumble xiRumble);
  }
}
