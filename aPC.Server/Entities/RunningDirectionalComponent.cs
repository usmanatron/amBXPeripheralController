using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Server.Entities
{
  /// <summary>
  ///   Encapsulates a running DirectionalComponent
  /// </summary>
  /// <remarks>
  ///   This also encapsulates a Frame when running in Sync mode.  This is
  ///   arguably an abuse of this class
  /// </remarks>
  public class RunningDirectionalComponent
  {
    public amBXScene Scene { get; private set; }

    public DirectionalComponent DirectionalComponent { get; private set; }

    public eComponentType ComponentType { get; private set; }

    public AtypicalFirstRunInfiniteTicker Ticker { get; private set; }

    public RunningDirectionalComponent(amBXScene scene, DirectionalComponent directionalComponent, AtypicalFirstRunInfiniteTicker ticker)
    {
      this.Scene = scene;
      this.DirectionalComponent = directionalComponent;
      this.Ticker = ticker;
    }
  }
}