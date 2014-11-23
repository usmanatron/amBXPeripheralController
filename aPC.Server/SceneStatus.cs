using aPC.Common;

namespace aPC.Server
{
  public interface ISceneStatus
  {
    eSceneType CurrentSceneType { get; set; }

    eSceneType PreviousSceneType { get; }
  }

  public class SceneStatus : ISceneStatus
  {
    private eSceneType currentSceneType;
    private eSceneType previousSceneType;

    public SceneStatus(eSceneType sceneType)
    {
      currentSceneType = sceneType;
      previousSceneType = sceneType;
    }

    public eSceneType CurrentSceneType
    {
      get
      {
        return currentSceneType;
      }
      set
      {
        previousSceneType = currentSceneType;
        currentSceneType = value;
      }
    }

    public eSceneType PreviousSceneType
    {
      get
      {
        return previousSceneType;
      }
    }
  }
}