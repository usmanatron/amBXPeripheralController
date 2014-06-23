using Ninject;

namespace aPC.Common.Client
{
  public abstract class NinjectKernelHandlerBase
  {
    protected NinjectKernelHandlerBase()
    {
      mKernel = new StandardKernel();
      SetupBindings();
    }

    protected abstract void SetupBindings();

    public T Get<T>()
    {
      return mKernel.Get<T>();
    }

    protected StandardKernel mKernel;
  }
}
