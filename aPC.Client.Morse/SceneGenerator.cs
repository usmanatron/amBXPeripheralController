using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using aPC.Common;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Client.Morse
{
  public class SceneGenerator
  {
    private MessageTranslator messageTranslator;

    public SceneGenerator(MessageTranslator messageTranslator)
    {
      this.messageTranslator = messageTranslator;
    }

    public amBXScene Generate(Settings settings)
    {
      var frames = GetFrames(settings);
      return BuildScene(settings, frames);
    }

    private List<Frame> GetFrames(Settings settings)
    {
      var frameBuilder = new MorseFrameBuilder(settings);

      var morseBlocks = messageTranslator.Translate(settings.Message);
      if (settings.RepeatMessage)
      {
        morseBlocks.Add(new MessageEndMarker());
      }

      return frameBuilder.AddFrames(morseBlocks).Build();
    }

    private amBXScene BuildScene(Settings settings, List<Frame> frames)
    {
      var scene = new amBXScene()
      {
        SceneType = settings.RepeatMessage ? eSceneType.Sync : eSceneType.Event
      };
      scene.Frames = frames;

      return scene;
    }
  }
}