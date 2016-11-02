using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Entities;
using System;
using System.Threading.Tasks;

namespace aPC.Server
{
  // Handles new scenes - ultimately passes onto the SceneDisseminator for processing
  public class NewSceneProcessor
  {
    private amBXScene previousScene;
    private amBXScene currentScene;
    private PreRunComponentList preRunComponents;
    private readonly PreRunComponentListBuilder preRunComponentListBuilder;
    private readonly TaskManager taskManager;

    public NewSceneProcessor(PreRunComponentListBuilder preRunComponentListBuilder, TaskManager taskManager, PreRunComponentList preRunComponents)
    {
      this.preRunComponentListBuilder = preRunComponentListBuilder;
      this.taskManager = taskManager;
      this.preRunComponents = preRunComponents;
      currentScene = new amBXScene { SceneType = eSceneType.Desync };
    }

    public void Process(amBXScene scene)
    {
      AssignPreviousSceneIfApplicable(scene);
      currentScene = scene;

      PushChanges();

      SetupRollbackIfEvent(scene);
    }

    private void AssignPreviousSceneIfApplicable(amBXScene newScene)
    {
      if (currentScene.SceneType == eSceneType.Event)
      {
        if (newScene.SceneType == eSceneType.Event)
        {
          // Skip updating the previous scene, to ensure that we don't get
          // stuck in an infinite loop of events.
        }
        else
        {
          // Don't interrupt the currently playing event - instead quietly update
          // the previous scene so that we fall back to this when the event is done.
          previousScene = newScene;
        }
      }
      else
      {
        previousScene = currentScene;
      }
    }

    /// <summary>
    /// If the new scene is an event, we need to re-push the old scene when complete
    /// </summary>
    private void SetupRollbackIfEvent(amBXScene scene)
    {
      if (scene.SceneType == eSceneType.Event)
      {
        var eventLength = currentScene.FrameStatistics.SceneLength;

        Task.Run(async delegate
                       {
                         await Task.Delay(TimeSpan.FromMilliseconds(eventLength));
                         RollbackScene();
                       });
      }
    }

    private void RollbackScene()
    {
      currentScene = previousScene;
      PushChanges();
    }

    private void PushChanges()
    {
      preRunComponents = preRunComponentListBuilder.Build(currentScene, previousScene.SceneType);
      taskManager.RefreshTasks(preRunComponents);
    }
  }
}