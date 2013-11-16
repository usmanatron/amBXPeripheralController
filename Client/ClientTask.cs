using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Client.Communication;
using Common;
using Common.Entities;

namespace Client
{
  class ClientTask
  {
    public ClientTask(IEnumerable<string> xiArguments)
    {
      try
      {
        ParseArguments(xiArguments.ToList());
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
        Environment.Exit(1);
      }
    }

    private void ParseArguments(List<string> xiArgs)
    {
      if (xiArgs.Count == 1)
      {
        // Expect to be a file path
        mInputScene = RetrieveAndParseFile(xiArgs[0]);
        return;
      }

      if (xiArgs.Count == 2 && xiArgs[0] == @"/I")
      {
        // Default hard-coded type
        mInputScene = GetDefaultScene(xiArgs[1]);
        return;
      }

      throw new UsageException("Invalid Arguments!");
    }

    #region Input - File path

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

    private amBXScene GetSceneFromXml(string xiInputXmlPath)
    {
      using (var lReader = new StreamReader(xiInputXmlPath))
      {
        var lSerialiser = new XmlSerializer(typeof(amBXScene));
        return (amBXScene) lSerialiser.Deserialize(lReader);
      }
    }

    #endregion

    #region Input - Default Scene

    private amBXScene GetDefaultScene(string xiDescription)
    {
      var lAccessor = new IntegratedamBXSceneAccessor();
      return lAccessor.GetScene(xiDescription);
    }

    #endregion

    public void Push()
    {
      var lQueueWriter = new MSMQWriter();
      lQueueWriter.SendMessage(mInputScene);
    }

    private static amBXScene mInputScene;    
  }
}
