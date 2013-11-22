using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Entities;

namespace Client
{
  class ArgumentReader
  {
    public ArgumentReader(List<string> xiArguments)
    {
      if (xiArguments.Count != 2)
      {
        throw new UsageException("Unexpected number of arguments");
      }

      if (xiArguments[0] == @"/F")
      {
        IsIntegratedScene = false;
        InputScene = RetrieveAndParseFile(xiArguments[0]);
        // File passed in
      }
      else if (xiArguments[0] == @"/I")
      {
        // (Integrated) Scene passed in
        IsIntegratedScene = true;
        InputScene = GetDefaultScene(xiArguments[1]);
      }
      else
      {
        throw new UsageException("Unexpected first argument");
      }
    }

    private amBXScene RetrieveAndParseFile(string xifilePath)
    {
      string lInputFilePath;

      try
      {
        lInputFilePath = Path.GetFullPath(xifilePath);
      }
      catch
      {
        // File not there / error
        throw new UsageException("Input was not a valid path");
      }

      return GetSceneFromXml(lInputFilePath);
    }

    //TODO: Add an amBXSceneSerialiser that handles this (in COMMON)
    private amBXScene GetSceneFromXml(string xiInputXmlPath)
    {
      using (var lReader = new StreamReader(xiInputXmlPath))
      {
        var lSerialiser = new XmlSerializer(typeof(amBXScene));
        return (amBXScene) lSerialiser.Deserialize(lReader);
      }
    }

    private amBXScene GetDefaultScene(string xiDescription)
    {
      var lAccessor = new IntegratedamBXSceneAccessor();
      return lAccessor.GetScene(xiDescription);
    }

    //qqUMI Currently unused - once we move to WCF this will be important
    public bool IsIntegratedScene { get; private set; }
    public amBXScene InputScene { get; private set; }
  }
}
