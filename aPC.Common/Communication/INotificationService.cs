using aPC.Common.Entities;
using System.ServiceModel;

namespace aPC.Common.Communication
{
  [ServiceContract]
  public interface INotificationService
  {
    [OperationContract]
    void RunScene(amBXScene scene);

    [OperationContract]
    void RunSceneName(string sceneName);

    [OperationContract]
    string[] GetAvailableScenes();
  }
}