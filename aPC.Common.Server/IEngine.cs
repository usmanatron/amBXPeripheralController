using aPC.Common.Entities;

namespace aPC.Common.Server
{
  public interface IEngine
  {
    void UpdateLight(eDirection xiDirection, Light xiLight, int xiFadeTime);
    void UpdateFan(eDirection xiDirection, Fan xiFan);
    void UpdateRumble(eDirection xiDirection, Rumble xiRumble);
  }
}
