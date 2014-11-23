using aPC.Common.Entities;
using System;

namespace aPC.Common.Server.Engine
{
  public interface IEngine : IDisposable
  {
    void UpdateLight(eDirection direction, Light light, int fadeTime);

    void UpdateFan(eDirection direction, Fan fan);

    void UpdateRumble(eDirection direction, Rumble rumble);
  }
}