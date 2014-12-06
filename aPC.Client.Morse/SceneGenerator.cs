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
    private MessageTranslator messageTranslator;

    public SceneGenerator(Settings settings, MessageTranslator messageTranslator)
    {
      this.settings = settings;
      this.messageTranslator = messageTranslator;
    }

    public amBXScene Generate()
    {
      var frames = GetFrames();
      return BuildScene(frames);
    }

    private List<Frame> GetFrames()
    {
      var frameBuilder = new MorseFrameBuilder(settings);

      var morseBlocks = messageTranslator.Translate(settings.Message);
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