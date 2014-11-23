using System.ServiceModel;

namespace aPC.Common.Communication
{
  [ServiceContract]
  public interface INotificationService
  {
    [OperationContract]
    void RunCustomScene(string sceneXml);

    [OperationContract]
    void RunIntegratedScene(string sceneName);

    [OperationContract]
    string[] GetSupportedIntegratedScenes();
  }
}