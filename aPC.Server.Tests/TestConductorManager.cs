using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Engine;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.Tests
{
  internal class TestConductorManager : ConductorManager
  {
    public TestConductorManager(IEngine engine, amBXScene scene)
      : base(engine, scene, null)
    {
    }

    public FrameConductor FrameConductor
    {
      get { return frameConductor; }
    }

    public IEnumerable<ComponentConductor> LightConductors
    {
      get { return desyncConductors.Where(conductor => conductor.ComponentType == eComponentType.Light); }
    }

    public IEnumerable<ComponentConductor> FanConductors
    {
      get { return desyncConductors.Where(conductor => conductor.ComponentType == eComponentType.Fan); }
    }

    public IEnumerable<ComponentConductor> RumbleConductors
    {
      get { return desyncConductors.Where(conductor => conductor.ComponentType == eComponentType.Rumble); }
    }
  }
}