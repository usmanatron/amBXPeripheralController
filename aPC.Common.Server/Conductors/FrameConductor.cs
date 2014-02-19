﻿using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using System.Linq;
using System;
using System.Collections.Generic;

namespace aPC.Common.Server.Conductors
{
  public class FrameConductor : ConductorBase<FrameSnapshot>
  {
    public FrameConductor(FrameActor xiActor) 
      : this(xiActor, null)
    {
    }

    public FrameConductor(FrameActor xiActor, Action xiEventComplete)
      : base(xiActor, xiEventComplete)
    {
      SetupNewScene(CurrentScene);
    }

    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFrames = xiFrames
        .Where(frame => frame.Lights  != null || 
                        frame.Fans    != null || 
                        frame.Rumbles != null);

      return lFrames.Any(frame => frame != null);
    }

    public override FrameSnapshot GetNextSnapshot()
    {
      var lFrame = GetNextFrame();
      return new FrameSnapshot(lFrame, 0);
    }
  }
}