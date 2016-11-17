using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Entities;
using System;
using System.Threading.Tasks;

namespace aPC.Server
{
  // Handles new scenes - Passes things to be calculated and subsequently sends them to be run.
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
      currentScene = new amBXScene { SceneType = eSceneType.Composite };
    }

    public void Process(amBXScene newScene)
    {
      previousScene = GetPreviousScene(newScene);
      currentScene = newScene;

      PushChanges();

      if (!newScene.HasRepeatableFrames)
      {
        SetupRollback();
      }
    }

    private amBXScene GetPreviousScene(amBXScene newScene)
    {
      if (currentScene.SceneType == eSceneType.Composite || 
         (currentScene.SceneType == eSceneType.Singular && currentScene.HasRepeatableFrames))
      {
        return currentScene;
      }

      // SceneType is Singular and No Repeatable Frames
      if (newScene.HasRepeatableFrames)
      {
        // Don't interrupt the currently playing scene - it'll finish and rollback.
        // Instead quietly update the previous scene so that we fall back to this
        // new one
        return newScene;
      }

      // In this case, both don't have repeated frames.
      // Dont update the previous scene, to ensure that we don't get
      // stuck in an infinite loop.
      return previousScene;
    }

    private void SetupRollback()
    {
      var eventLength = currentScene.FrameStatistics.SceneLength;

      Task.Run(async delegate
                     {
                       await Task.Delay(TimeSpan.FromMilliseconds(eventLength));
                       RollbackScene();
                     });
    }

    private void RollbackScene()
    {
      currentScene = previousScene;
      PushChanges();
    }

    private void PushChanges()
    {
      if (!previousScene.HasRepeatableFrames && !currentScene.HasRepeatableFrames)
      {
        // TODO: Log error
        return;
      }

      preRunComponents = preRunComponentListBuilder.Build(currentScene);
      taskManager.RefreshTasks(preRunComponents);
    }
  }
}