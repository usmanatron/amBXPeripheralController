using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  /// <summary>
  ///   Encapsulates a running DirectionalComponent
  /// </summary>
  public interface IPreRunComponent
  {
    amBXScene Scene { get; }

    AtypicalFirstRunInfiniteTicker Ticker { get;  }
  }
}