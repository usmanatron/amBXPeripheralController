using System.Collections.Generic;
using aPC.Server;
using aPC.Server.Conductors;
using aPC.Server.Engine;
using aPC.Common.Entities;

namespace aPC.Server.Tests
{
  class TestConductorManager : ConductorManager
  {
    public TestConductorManager(IEngine xiEngine, amBXScene xiScene) : base (xiEngine, xiScene, null)
    {
    }

    public FrameConductor FrameConductor
    {
      get { return mFrameConductor; }
    }

    public List<LightConductor> LightConductors
    {
      get { return mLightConductors; }
    }

    public List<FanConductor> FanConductors
    {
      get { return mFanConductors; }
    }

    public List<RumbleConductor> RumbleConductors
    {
      get { return mRumbleConductors; }
    }
  }
}
