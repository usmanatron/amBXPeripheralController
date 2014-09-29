using Ninject;

namespace aPC.Common.Client
{
  public abstract class NinjectKernelHandlerBase
  {
    protected NinjectKernelHandlerBase()
    {
      kernel = new StandardKernel();
      SetupBindings();
    }

    protected abstract void SetupBindings();

    public T Get<T>()
    {
      return kernel.Get<T>();
    }

    protected StandardKernel kernel;
  }
}
