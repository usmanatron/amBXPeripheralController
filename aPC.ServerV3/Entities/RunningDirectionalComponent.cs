using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.ServerV3.Entities
{
  /// <summary>
  ///   Encapsulates a running DirectionalComponent
  /// </summary>
  /// <remarks>
  ///   This also encapsulates a Frame when running in Sync mode.  This is
  ///   arguably an abuse of this class
  /// </remarks>
  internal class RunningDirectionalComponent
  {
    public amBXScene Scene { get; private set; }

    public eDirection Direction { get; private set; }

    public eComponentType? ComponentType { get; private set; }

    public AtypicalFirstRunInfiniteTicker Ticker { get; private set; }

    public RunningDirectionalComponent(amBXScene scene, eDirection direction, eComponentType? componentType, AtypicalFirstRunInfiniteTicker ticker)
    {
      this.Scene = scene;
      this.Direction = direction;
      this.ComponentType = componentType;
      this.Ticker = ticker;
    }
  }
}