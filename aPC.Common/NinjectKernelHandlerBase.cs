using Ninject;

namespace aPC.Common
{
  public abstract class NinjectKernelHandlerBase
  {
    public NinjectKernelHandlerBase()
    {
      mKernel = new StandardKernel();
    }

    //qqUMI consider if object is alright?
    protected abstract void SetupBindings(params object[] xiParams);

    public T Get<T>()
    {
      return mKernel.Get<T>();
    }

    protected StandardKernel mKernel;
  }
}
