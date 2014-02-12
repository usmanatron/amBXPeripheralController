using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Common.Tests.Defaults
{
  [TestFixture]
  class DefaultSceneTests
  {
    [Test]
    public void DefaultScenesAreValid()
    {
      Assert.DoesNotThrow(() => BuildAllDefaultScenes());
    }

    private void BuildAllDefaultScenes()
    {
      amBXScene lScene;

      foreach (var lDefaultScene in GetScenes())
      {
        lScene = lDefaultScene;
      }
    }

    private IEnumerable<amBXScene> GetScenes()
    {
      var lDefaultScenes = new DefaultScenes();

      return lDefaultScenes
        .GetType()
        .GetProperties()
        .Where(property => property.GetCustomAttribute<SceneNameAttribute>() != null)
        .Select(property => property.GetValue(lDefaultScenes) as amBXScene);
    }
  }
}
