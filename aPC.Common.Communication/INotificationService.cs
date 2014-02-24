using System.ServiceModel;

namespace aPC.Common.Communication
{
  [ServiceContract]
  public interface INotificationService
  {
    [OperationContract]
    void RunCustomScene(string xiSceneXml);

    [OperationContract]
    void RunIntegratedScene(string xiSceneName);
  }
}
