using System.IO;
using System.Xml.Serialization;
using Common.Entities;

namespace Client
{
  public class amBXSceneSerialiser
  {
    public static amBXScene Deserialise(string xiFilePath)
    {
      using (var lReader = new StreamReader(xiFilePath))
      {
        var lSerialiser = new XmlSerializer(typeof(amBXScene));
        return (amBXScene)lSerialiser.Deserialize(lReader);
      }
    }
  }
}