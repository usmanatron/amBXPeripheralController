using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.Entities
{
  public class ComponentWrapperList
  {
    private readonly List<ComponentWrapperBase> wrappedComponents;

    //public eSceneType SceneType { get; private set; }

    public ComponentWrapperList()
    {
      wrappedComponents = new List<ComponentWrapperBase>();
    }

    public IEnumerable<ComponentWrapperBase> Get()
    {
      return wrappedComponents;
    }

    public void ReplaceAllWith(params ComponentWrapperBase[] newComponents)
    {
      wrappedComponents.ForEach(task => task.CancellationToken.Cancel());
      wrappedComponents.Clear();
      wrappedComponents.AddRange(newComponents);
    }

    public void MergeComposite(ComponentWrapperBase newComponentWrapper)
    {
      var newDirectionalComponentWrapper = (CompositeComponentWrapper) newComponentWrapper;

      var existingComponent = wrappedComponents
        .SingleOrDefault(cmp => ((CompositeComponentWrapper) cmp)
          .DirectionalComponent.Equals(newDirectionalComponentWrapper.DirectionalComponent));

      if (existingComponent != null)
      {
        existingComponent.CancellationToken.Cancel();
        wrappedComponents.Remove(existingComponent);
      }

      wrappedComponents.Add(newComponentWrapper);
    }
  }
}