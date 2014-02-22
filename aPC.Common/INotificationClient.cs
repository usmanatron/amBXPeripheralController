using aPC.Common.Entities;

namespace aPC.Common
{
  // Used to communicate with INotificationService
  public interface INotificationClient
  {
    void PushCustomScene(amBXScene xiScene);

    void PushIntegratedScene(string xiScene);
  }
}
