using Common.Entities;
using Common.Accessors;

namespace ServerMT.Communication
{
  class SceneDistributor
  {
    public SceneDistributor(amBXScene xiScene)
    {
      mScene = xiScene;
      mEmptyScene = new SceneAccessor().GetScene("Empty");
    }

    /*qqUMI
     * 
     * There are a number of issues here:
     * * If we're in de-sync mode and run a synchronised event, we'll fall back to whatever the 
     *   synchronised previous scene was - not ideal
     * * If we go from sync to de--sync (with, say, a de-sync scene with only one light defined), then the
     *   other lights will stop animating.  Ideally we want to copy this all to the others
     *   
     * Fixing this may just be a case of changing how UpdateManager works a little bit to allow
     * us to force a value for IsDormant?
     */
    public void Distribute()
    {
      if (mScene.IsSynchronised)
      {
        UpdateSynchronisedApplicator(mScene);
        UpdateUnsynchronisedElements(mEmptyScene);
      }
      else
      {
        UpdateUnsynchronisedElements(mScene);
        UpdateSynchronisedApplicator(mEmptyScene);
      }
    }

    private void UpdateSynchronisedApplicator(amBXScene xiScene)
    {
      ServerTask.Frame.UpdateManager(xiScene);
    }

    private void UpdateUnsynchronisedElements(amBXScene xiScene)
    {
      foreach (var lLight in ServerTask.Lights)
      {
        lLight.Value.UpdateManager(xiScene);
      }

      foreach (var lFan in ServerTask.Fans)
      {
        lFan.Value.UpdateManager(xiScene);
      }

      //qqUMI Rumble not supported yet
    }


    readonly amBXScene mScene;
    readonly amBXScene mEmptyScene;
  }
}
