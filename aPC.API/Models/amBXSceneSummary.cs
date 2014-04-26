using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using aPC.Common.Entities;
using aPC.Common;

namespace aPC.API.Models
{
  // represents summary information from a given amBXScene
  [DataContractAttribute]
  public class amBXSceneSummary
  {
    public amBXSceneSummary(KeyValuePair<string, amBXScene> xiScene)
    {
      this.mSceneName = xiScene.Key;
      this.mSceneType = xiScene.Value.SceneType;
      this.mFrameStatistics = xiScene.Value.FrameStatistics;
    }

    [DataMemberAttribute]
    public string Name
    {
      get
      {
        return mSceneName;
      }
      private set
      {
      }
    }

    [DataMemberAttribute]
    public bool IsEvent
    {
      get
      {
        return mSceneType == eSceneType.Event;
      }
      private set
      {
      }
    }

    [DataMemberAttribute]
    public bool IsSynchronised
    {
      get
      {
        return mSceneType == eSceneType.Sync ||
               mSceneType == eSceneType.Event;
      }
      private set
      {
      }
    }

    [DataMemberAttribute]
    public int SceneLength
    {
      get
      {
        return mFrameStatistics.SceneLength;
      }
      private set
      {
      }
    }

    private readonly string mSceneName;
    private readonly eSceneType mSceneType;
    private readonly FrameStatistics mFrameStatistics;
  }
}