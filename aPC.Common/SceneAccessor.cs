using System;
using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Common
{
  public class SceneAccessor
  {
    public amBXScene GetScene(string xiDescription)
    {
      switch (xiDescription.ToLower())
      {
        case "ccnet_red":
          return mDefaultScenes.BuildBroken;
        case "ccnet_green":
          return mDefaultScenes.BuildSuccess;
        case "ccnet_flashingyellow":
          return mDefaultScenes.Building;
        case "ccnet_flashingorange":
          return mDefaultScenes.BuildBrokenAndBuilding;
        case "ccnet_grey":
          return mDefaultScenes.BuildNotConnected;

        case "default_redvsblue":
          return mDefaultScenes.DefaultRedVsBlue;
        case "poolq2_event":
          return mDefaultScenes.PoolQ2_Event;
        case "shiprec_praise":
          return mDefaultScenes.Shiprec_Praise;
        case "support_jiraday":
          return mDefaultScenes.Support_JIRADay;

        case "error_flash":
          return mDefaultScenes.Error_Flash;
        case "empty":
          return mDefaultScenes.Empty;

        default:
          Console.WriteLine("Integrated scene with description {0} not found.", xiDescription);
          return null;
      }
    }

    private static readonly DefaultScenes mDefaultScenes = new DefaultScenes();
  }
}
