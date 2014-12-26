using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Client.Morse
{
  public class SceneGenerator
  {
    private MessageTranslator messageTranslator;
    private MorseFrameBuilder frameBuilder;

    public SceneGenerator(MessageTranslator messageTranslator, MorseFrameBuilder frameBuilder)
    {
      this.messageTranslator = messageTranslator;
      this.frameBuilder = frameBuilder;
    }

    public amBXScene Generate(Settings settings)
    {
      var frames = GetFrames(settings);
      return BuildScene(settings, frames);
    }

    private List<Frame> GetFrames(Settings settings)
    {
      var morseBlocks = messageTranslator.Translate(settings.Message);
      if (settings.RepeatMessage)
      {
        morseBlocks.Add(new MessageEndMarker());
      }

      return frameBuilder.AddFrames(settings, morseBlocks).Build();
    }

    private amBXScene BuildScene(Settings settings, List<Frame> frames)
    {
      var sceneType = settings.RepeatMessage
        ? eSceneType.Sync
        : eSceneType.Event;

      return new amBXScene()
      {
        SceneType = sceneType,
        Frames = frames
      };
    }
  }
}