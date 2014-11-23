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
    public TestConductorManager(IEngine xiEngine, amBXScene xiScene)
      : base(xiEngine, xiScene, null)
    {
    }

    public FrameConductor FrameConductor
    {
      get { return mFrameConductor; }
    }

    public IEnumerable<LightConductor> LightConductors
    {
      get { return mDesyncConductors.Where(conductor => conductor.ComponentType == eComponentType.Light).Cast<LightConductor>(); }
    }

    public IEnumerable<FanConductor> FanConductors
    {
      get { return mDesyncConductors.Where(conductor => conductor.ComponentType == eComponentType.Fan).Cast<FanConductor>(); }
    }

    public IEnumerable<RumbleConductor> RumbleConductors
    {
      get { return mDesyncConductors.Where(conductor => conductor.ComponentType == eComponentType.Rumble).Cast<RumbleConductor>(); }
    }
  }
}