using System.ServiceModel;

namespace Common.Communication
{
  [ServiceContract]
  public interface INotificationService
  {
    [OperationContract]
    string RunCustomScene(string xiSceneXml);

    [OperationContract]
    string RunIntegratedScene(string xiSceneName);
  }
}
