using System;

namespace ServerMT
{
  class SynchronisationManager
  {
    public SynchronisationManager()
    {
      IsSynchronised = true;
    }

    public void RunWhileSynchronised(Action xiAction)
    {
      while (IsSynchronised)
      {
        xiAction();
      }
    }

    public void RunWhileUnSynchronised(Action xiAction)
    {
      while (!IsSynchronised)
      {
        xiAction();
      }
    }

    public bool IsSynchronised
    {
      get
      {
        lock (mLocker)
        {
          return mIsSynchronised;
        }
      }
      set
      {
        lock (mLocker)
        {
          mIsSynchronised = value;
        }
      }
    }

    private object mLocker = new object();
    private bool mIsSynchronised;
  }
}
