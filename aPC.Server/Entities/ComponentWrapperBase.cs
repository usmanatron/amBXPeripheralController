using System.Threading;
using aPC.Common;
using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  /// <summary>
  /// Wraps a Component and its relevant metadata
  /// for manipulation by the Server
  /// </summary>
  public abstract class ComponentWrapperBase
  {
    public amBXScene Scene { get; private set; }

    public AtypicalFirstRunInfiniteTicker Ticker { get; private set; }

    public CancellationTokenSource CancellationToken { get; private set; }

    protected ComponentWrapperBase(amBXScene scene, AtypicalFirstRunInfiniteTicker ticker)
    {
      Scene = scene;
      Ticker = ticker;
    }

    public abstract eSceneType ComponentType { get; }

    public void Run(CancellationTokenSource token)
    {
      CancellationToken = token;
    }
  }
}
