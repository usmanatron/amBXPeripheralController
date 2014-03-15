using aPC.Common;

namespace aPC.Server
{
  public class SceneStatus
  {
    public SceneStatus(eSceneType xiSceneType)
    {
      mCurrentSceneType = xiSceneType;
      mPreviousSceneType = xiSceneType;
    }

    public eSceneType CurrentSceneType
    {
      get
      {
        return mCurrentSceneType;
      }
      set
      {
        mPreviousSceneType = mCurrentSceneType;
        mCurrentSceneType = value;
      }
    }

    public eSceneType PreviousSceneType
    {
      get
      {
        return mPreviousSceneType;
      }
    }

    private eSceneType mCurrentSceneType;
    private eSceneType mPreviousSceneType;
  }
}
