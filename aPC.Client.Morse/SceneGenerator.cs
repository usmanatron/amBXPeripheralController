using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using aPC.Common;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Client.Morse
{
  public class SceneGenerator
  {
    private Settings settings;

    public SceneGenerator(Settings settings)
    {
      this.settings = settings;
    }

    public amBXScene Generate()
    {
      var frames = GetFrames();
      return BuildScene(frames);
    }

    private List<Frame> GetFrames()
    {
      var frameBuilder = new MorseFrameBuilder(settings);

      var morseBlocks = new MessageTranslator(settings.Message).Translate();
      if (settings.RepeatMessage)
      {
        morseBlocks.Add(new MessageEndMarker());
      }

      return frameBuilder.AddFrames(morseBlocks).Build();
    }

    private amBXScene BuildScene(List<Frame> xiFrames)
    {
      var scene = new amBXScene()
      {
        SceneType = settings.RepeatMessage ? eSceneType.Sync : eSceneType.Event
      };
      scene.Frames = xiFrames;

      return scene;
    }
  }
}