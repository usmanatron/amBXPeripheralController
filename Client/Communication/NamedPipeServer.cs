using System.IO;
using System.IO.Pipes;
using Common;

namespace Client.Communication
{
  class NamedPipeServer
  {
    public void WriteToPipe(string xiInput)
    {
      var lNamedPipeServer = new NamedPipeServerStream(
        Communicationsettings.NamedPipeInstance, 
        PipeDirection.InOut);
      lNamedPipeServer.WaitForConnection();

      using (var lWriter = new StreamWriter(lNamedPipeServer))
      {
        lWriter.Write(xiInput);
      }
    }
  }
}
