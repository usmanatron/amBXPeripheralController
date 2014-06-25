using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace aPC.Client.Scene
{
  class CustomSceneFileHandler : ICustomSceneFileHandler
  {
    public string GetFilenameFromDialog()
    {
      var lDialog = new OpenFileDialog();
      lDialog.Multiselect = false;
      lDialog.ShowDialog();
      return lDialog.FileName;
    }

    public void Import()
    {
      throw new NotImplementedException();
    }

    public void Delete(string xiFilename)
    {
      throw new NotImplementedException();
    }
  }
}
