using System.Web.Http.Dependencies;
using Ninject;

namespace aPC.Web.App_Start
{
  /// <summary>
  /// Taken from https://gist.github.com/bradwilson/2417226
  /// </summary>
  public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
  {
    private IKernel mKernel;

    public NinjectDependencyResolver(IKernel kernel)
      : base(kernel)
    {
      this.mKernel = kernel;
    }

    public IDependencyScope BeginScope()
    {
      return new NinjectDependencyScope(mKernel.BeginBlock());
    }
  }
}