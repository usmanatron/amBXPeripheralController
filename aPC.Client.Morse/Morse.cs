﻿using aPC.Common.Entities;
using aPC.Client.Morse.Communication;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse
{
  class Morse
  {
    static void Main(string[] xiArgs)
    {
      var lSettings = GetSettings(xiArgs);
      var lGeneratedScene = new SceneGenerator(lSettings).Generate();
      new NotificationClient().PushCustomScene(lGeneratedScene);
    }

    private static Settings GetSettings(string[] xiArgs)
    {
      try
      {
        var lArguments = string.Join(" ", xiArgs);
        return new ArgumentReader(lArguments).Read();
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
        throw;
      }
    }
  }
}