using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  /// <summary>
  ///   Encapsulates a running DirectionalComponent
  /// </summary>
  /// <remarks>
  ///   This also encapsulates a Frame when running in Sync mode.  This is
  ///   arguably an abuse of this class
  /// </remarks>
  public class PreRunComponenet
  {
    public amBXScene Scene { get; private set; }

    public DirectionalComponent DirectionalComponent { get; private set; }

    public AtypicalFirstRunInfiniteTicker Ticker { get; private set; }

    public PreRunComponenet(amBXScene scene, DirectionalComponent directionalComponent, AtypicalFirstRunInfiniteTicker ticker)
    {
      Scene = scene;
      DirectionalComponent = directionalComponent;
      Ticker = ticker;
    }
  }
}