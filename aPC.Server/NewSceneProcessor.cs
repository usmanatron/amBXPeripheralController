using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aPC.Server
{
  // Handles new scenes - ultimately passes onto the SceneDisseminator for processing
  public class NewSceneProcessor
  {
    private amBXScene previousScene;
    private amBXScene currentScene;
    private SceneSplitter sceneSplitter;
    private TaskManager taskManager;
    private RunningDirectionalComponentList runningDirectionalComponents;

    public NewSceneProcessor(SceneSplitter sceneSplitter, TaskManager taskManager, RunningDirectionalComponentList runningDirectionalComponents)
    {
      this.sceneSplitter = sceneSplitter;
      this.taskManager = taskManager;
      this.runningDirectionalComponents = runningDirectionalComponents;
      currentScene = new amBXScene() { SceneType = eSceneType.Desync };
    }

    public void Process(amBXScene scene)
    {
      AssignPreviousSceneIfApplicable(scene);
      currentScene = scene;

      PushChanges();

      SetupRollbackIfEvent(scene);
    }

    private void AssignPreviousSceneIfApplicable(amBXScene scene)
    {
      if (currentScene.SceneType == eSceneType.Event)
      {
        if (scene.SceneType == eSceneType.Event)
        {
          // Skip updating the previous scene, to ensure that we don't get
          // stuck in an infinite loop of events.
        }
        else
        {
          // Don't interrupt the currently playing event - instead quietly update
          // the previous scene so that we fall back to this when the event is done.
          previousScene = scene;
          return;
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

        var task = Task.Run(async delegate
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
      sceneSplitter.SplitScene(runningDirectionalComponents, currentScene);
      taskManager.RefreshTasks(runningDirectionalComponents);
    }
  }
}