using aPC.Common;
using aPC.Common.Entities;
using System;

namespace aPC.Server.Engine
{
  public interface IEngine : IDisposable
  {
    void UpdateLight(eDirection xiDirection, Light xiLight, int xiFadeTime);
    void UpdateFan(eDirection xiDirection, Fan xiFan);
    void UpdateRumble(eDirection xiDirection, Rumble xiRumble);
  }
}
