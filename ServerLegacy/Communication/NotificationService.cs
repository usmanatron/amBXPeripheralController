﻿using Common.Entities;
using Common.Server.Communication;

namespace ServerLegacy.Communication
{
  class NotificationService : NotificationServiceBase
  {
    protected override void UpdateScene(amBXScene xiScene)
    {
      ServerTask.Applicator.UpdateManager(xiScene);
    }
  }
}