using aPC.Common.Client;
using System;

namespace aPC.Client
{
  [Serializable]
  public class UsageException : UsageExceptionBase
  {
    public UsageException(string xiDescription)
      : base(xiDescription)
    {
    }

    protected override string Usage()
    {
      return 
@"Usage: Client [/I | /F] argument

One of either /I or /F MUST be given.

/I : Use an integrated amBXScene built into the application.  The exact
     name of the scene will need to be confirmed before running this 
     command and can currenly be found in the Source code.
     Here, the argument is the name of the integrated ambxScene you 
     would like to run.

/F : Run a custom xml ambxScene.  In this case, the argument is the full 
     absolute filepath of the xml file (not that relative paths are 
     currently unsupported).

Examples:

Client.exe /I CCNet_Green
Client.exe /F C:\path\to\scene\Scene.xml";
    }
  }
}