using aPC.Common;

namespace aPC.Server
{
  class SceneStatus
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
        return mCurrentSceneType;
      }
    }

    private eSceneType mCurrentSceneType;
    private eSceneType mPreviousSceneType;
  }
}
