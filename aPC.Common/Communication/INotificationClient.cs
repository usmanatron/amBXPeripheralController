using aPC.Common.Entities;
namespace aPC.Common.Communication
{
  // Used to communicate with INotificationService
  public interface INotificationClient
  {
    void PushScene(amBXScene scene);

    void PushSceneName(string sceneName);

    string[] GetAvailableScenes();

    void Register(string id);

    void PushExclusive(Frame frame);
  }
}