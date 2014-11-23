using aPC.Common.Entities;
using System;

namespace aPC.Common.Server.Engine
{
  public interface IEngine : IDisposable
  {
    void UpdateComponent(eDirection direction, IComponent component, int fadeTime);
  }
}