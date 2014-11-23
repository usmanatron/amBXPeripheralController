namespace aPC.Common.Communication
{
  // Used to communicate with INotificationService
  public interface INotificationClient
  {
    void PushCustomScene(string scene);

    void PushIntegratedScene(string scene);

    string[] GetSupportedIntegratedScenes();
  }
}