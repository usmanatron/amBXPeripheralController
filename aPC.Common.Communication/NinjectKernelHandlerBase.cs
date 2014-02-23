using Ninject;

namespace aPC.Common.Communication
{
  public abstract class NinjectKernelHandlerBase
  {
    public NinjectKernelHandlerBase()
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
