using aPC.Common.Entities;

namespace aPC.Server.Processors
{
  public interface IProcessor
  {
    void Process(amBXScene newScene);
  }
}
