using Common.Entities;
using System.ServiceModel;

namespace Common.Communication
{
  [ServiceContract]
  public interface INotificationService
  {
    [OperationContract]
    string RunCustomScene(amBXScene xiScene);

    [OperationContract]
    string RunIntegratedScene(string xiSceneName);
  }
}
