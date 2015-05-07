using System.Runtime.InteropServices;

namespace aPC.Client.Console
{
  internal class NativeMethods
  {
    [DllImport("Kernel32.dll")]
    public static extern bool AllocConsole();
  }
}