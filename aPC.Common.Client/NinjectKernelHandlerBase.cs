using Ninject;

namespace aPC.Common.Client
{
  public abstract class NinjectKernelHandlerBase
  {
    protected NinjectKernelHandlerBase(ISettings xiSettings)
    {
      mKernel = new StandardKernel();
      AttachSettings(xiSettings);
      SetupBindings();
    }

    private void AttachSettings(ISettings xiSettings)
    {
      mKernel.Bind<ISettings>().ToConstant(xiSettings);
    }

    protected abstract void SetupBindings();

    public T Get<T>()
    {
      return mKernel.Get<T>();
    }

    protected StandardKernel mKernel;
  }
}
