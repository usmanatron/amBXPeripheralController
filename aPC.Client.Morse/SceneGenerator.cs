using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using aPC.Common;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Client.Morse
{
  public class SceneGenerator
  {
    public SceneGenerator(Settings xiSettings)
    {
      mSettings = xiSettings;
    }

    public amBXScene Generate()
    {
      var lFrames = GetFrames();
      return BuildScene(lFrames);
    }

    private List<Frame> GetFrames()
    {
      var lFrameBuilder = new MorseFrameBuilder(mSettings);

      var lMorseBlocks = new MessageTranslator(mSettings.Message).Translate();
      if (mSettings.RepeatMessage)
      {
        lMorseBlocks.Add(new MessageEndMarker());
      }

      return lFrameBuilder.AddFrames(lMorseBlocks).Build();
    }

    private amBXScene BuildScene(List<Frame> xiFrames)
    {
      var lScene = new amBXScene()
      {
        SceneType = mSettings.RepeatMessage ? eSceneType.Sync : eSceneType.Event
      };
      lScene.Frames = xiFrames;

      return lScene;
    }

    private Settings mSettings;
  }
}