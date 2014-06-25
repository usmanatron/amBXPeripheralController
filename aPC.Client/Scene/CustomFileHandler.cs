using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Scene
{
  class CustomFileHandler
  {
    public string GetFilenameFromDialog()
    {
      var lDialog = new OpenFileDialog();
      lDialog.Multiselect = false;
      lDialog.ShowDialog();
      return lDialog.FileName;
    }

    public string LoadFile(string xiFilename)
    {
      throw new NotImplementedException();
    }

    public string ImportAndReturnNewPath(string xiFilename)
    {
      throw new NotImplementedException();
    }

    public void Delete(string xiFilename)
    {
      throw new NotImplementedException();
    }
  }
}
