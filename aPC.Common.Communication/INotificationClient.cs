namespace aPC.Common.Communication
{
  // Used to communicate with INotificationService
  public interface INotificationClient
  {
    void PushCustomScene(string xiScene);

    void PushIntegratedScene(string xiScene);

    string[] GetSupportedIntegratedScenes();
  }
}