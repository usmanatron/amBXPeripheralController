using aPC.Common.Client;
using System;

namespace aPC.Client.Cli
{
  [Serializable]
  public class UsageException : UsageExceptionBase
  {
    public UsageException(string description)
      : base(description)
    {
    }

    protected override string Usage()
    {
      return
@"Usage: aPC.Client [/I | /F] argument

One of either /I or /F MUST be given first.

/I : Use an integrated amBXScene built into the application.  The exact
     name of the scene will need to be confirmed before running this
     command and can currenly be found by running this app without
     arguments and looking under the ""Integrated"" dropdown.
     Here, the argument is the name of the integrated amBXScene you
     would like to run.

/F : Run a custom xml amBXScene.  In this case, the argument is the
     path to the xml file.

Examples:

aPC.Client.exe /I CCNet_Green
aPC.Client.exe /F C:\path\to\scene\Scene.xml";
    }
  }
}