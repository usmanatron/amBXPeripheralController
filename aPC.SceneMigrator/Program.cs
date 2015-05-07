using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using amBXSceneV1 = aPC.SceneMigrator.EntitiesV1.amBXScene;

namespace aPC.SceneMigrator
{
  internal class Program
  {
    /// <summary>
    /// An application to migrate old Xml files to the new system
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
      var directory = new DirectoryInfo(args[0]);
      var migrator = new SceneMigrator(new LightSectionMigrator(), new FanSectionMigrator(), new RumbleSectionMigrator());

      foreach (var file in directory.EnumerateFiles(@"*.xml", SearchOption.AllDirectories))
      {
        var filepath = file.FullName;
        var oldScene = LoadScene(filepath);
        var newScene = migrator.Migrate(oldScene);
        SaveNewScene(newScene, file.DirectoryName, file.Name);
      }
    }

    private static amBXSceneV1 LoadScene(string filepath)
    {
      using (var stream = new FileStream(filepath, FileMode.Open))
      {
        var deserialiser = new XmlSerializer(typeof(amBXSceneV1));
        var scene = (amBXSceneV1)deserialiser.Deserialize(stream);
        return scene;
      }
    }

    private static void SaveNewScene(amBXScene newScene, string path, string filename)
    {
      var fullpath = Path.Combine(path, "new_" + filename);
      using (var stream = new FileStream(fullpath, FileMode.Create))
      {
        var deserialiser = new XmlSerializer(typeof(amBXScene));
        deserialiser.Serialize(stream, newScene);
      }
    }
  }
}