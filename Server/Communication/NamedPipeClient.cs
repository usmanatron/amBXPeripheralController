using System.IO;
using System.IO.Pipes;
using System.Xml.Serialization;
using Common;
using Common.Entities;

namespace Server.Communication
{

  // confusingly, the Server application needs the NP Client and the Client application needs the NP Server!
  public class NamedPipeClient
  {
    public amBXScene TryGetNewState()
    {
      var lNamedPipeClient = new NamedPipeClientStream(
        System.Net.Dns.GetHostName(),
        Communicationsettings.NamedPipeInstance, 
        PipeDirection.InOut);
      try
      {
        lNamedPipeClient.Connect(100);
      }
      catch (System.TimeoutException)
      {
        // Nobody at the end of the pipe - return nothing
        return null;
      }

      return ReadResponseFromPipe(lNamedPipeClient);
    }

    private amBXScene ReadResponseFromPipe(NamedPipeClientStream xiNamedPipeclient)
    {
      using (var lReader = new StreamReader(xiNamedPipeclient))
      {
        var lDeserialiser = new XmlSerializer(typeof (amBXScene));
        return (amBXScene) lDeserialiser.Deserialize(lReader);
      }
    }
  }
}
