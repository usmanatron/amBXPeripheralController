﻿using Common.Entities;
using Common.Server.Communication;

namespace Server.Communication
{
  class NotificationService : NotificationServiceBase
  {
    protected override void UpdateScene(amBXScene xiScene)
    {
      ServerTask.Applicator.UpdateManager(xiScene);
    }
  }
}
