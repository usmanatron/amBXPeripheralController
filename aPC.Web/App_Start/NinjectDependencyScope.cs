using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;

namespace aPC.Web.App_Start
{
  /// <summary>
  /// Taken from https://gist.github.com/bradwilson/2417226
  /// </summary>
  public class NinjectDependencyScope : IDependencyScope
  {
    private IResolutionRoot mResolver;

    internal NinjectDependencyScope(IResolutionRoot resolver)
    {
      Contract.Assert(resolver != null);

      this.mResolver = resolver;
    }

    public void Dispose()
    {
      IDisposable disposable = mResolver as IDisposable;
      if (disposable != null)
        disposable.Dispose();

      mResolver = null;
    }

    public object GetService(Type serviceType)
    {
      if (mResolver == null)
        throw new ObjectDisposedException("this", "This scope has already been disposed");

      return mResolver.TryGet(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
      if (mResolver == null)
        throw new ObjectDisposedException("this", "This scope has already been disposed");

      return mResolver.GetAll(serviceType);
    }
  }
}