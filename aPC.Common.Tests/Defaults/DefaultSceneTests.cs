using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Tests.Defaults
{
  [TestFixture]
  internal class DefaultSceneTests
  {
    [Test]
    public void DefaultScenesAreValid()
    {
      var lScenes = new DefaultScenes()
        .GetType()
        .GetProperties()
        .Where(property => property.GetCustomAttribute<SceneNameAttribute>() != null)
        .Select(property => property.GetValue(new DefaultScenes()) as amBXScene)
        .ToList();

      Assert.IsNotEmpty(lScenes);
    }
  }
}