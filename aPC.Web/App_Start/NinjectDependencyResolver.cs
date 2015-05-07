using Ninject;
using System.Web.Http.Dependencies;

namespace aPC.Web.App_Start
{
  /// <summary>
  /// Taken from https://gist.github.com/bradwilson/2417226
  /// </summary>
  public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
  {
    private readonly IKernel kernel;

    public NinjectDependencyResolver(IKernel kernel)
      : base(kernel)
    {
      this.kernel = kernel;
    }

    public IDependencyScope BeginScope()
    {
      return new NinjectDependencyScope(kernel.BeginBlock());
    }
  }
}