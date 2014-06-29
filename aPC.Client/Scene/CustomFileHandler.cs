﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace aPC.Client.Scene
{
  class CustomFileHandler
  {
    public CustomFileHandler(CustomListing xiCustomListing)
    {
      mCustomListing = xiCustomListing;
    }

    public string AddNewFileAndUpdateListing()
    {
      var lFullFilePath = GetFilenameFromDialog();
      
      var lKeepScene = MessageBox.Show("Do you want to store this scene for future use?", "Store for later?", MessageBoxButton.YesNo, MessageBoxImage.Question);

      if (lKeepScene == MessageBoxResult.Yes)
      {
        ImportFile(lFullFilePath);
      }

      var lFilename = Profiles.GetFilenameWithoutExtension(lFullFilePath);
      mCustomListing.AddScene(lFilename, File.ReadAllText(lFullFilePath));
      return lFilename;
    }


    private string GetFilenameFromDialog()
    {
      var lDialog = new OpenFileDialog();
      lDialog.Multiselect = false;
      lDialog.ShowDialog();
      return lDialog.FileName;
    }

    /// <summary>
    ///   Import the given file, after doing a couple of checks.
    /// </summary>
    /// <returns>True if the file was successfully imported.</returns>
    public bool ImportFile(string xiFilename)
    {
      if (mCustomListing.Scenes.Keys.Any(scene => scene == xiFilename))
      {
        var lOverwrite = MessageBox.Show("A scene with this filename already exists and will be overwritten.  Do you want to continue?",
                                         "Overwrite file?", 
                                         MessageBoxButton.YesNo, 
                                         MessageBoxImage.Question);

        if (lOverwrite != MessageBoxResult.Yes)
        {
          // Bail out
          return false;
        }
      }

      System.IO.File.Copy(xiFilename, Profiles.Directory);

      throw new NotImplementedException();
    }

    /// <summary>
    /// Delete the file with the given name
    /// </summary>
    /// <param name="xiFilename"></param>
    public void Delete(string xiKey)
    {
      var lConfirmDeletion = MessageBox.Show("Are you sure you want to delete this file?",
                                             "Delete file?",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);
      if (lConfirmDeletion != MessageBoxResult.Yes)
      {
        return;
      }

      File.Delete(Path.Combine(Profiles.Directory, Profiles.GetFilenameWithoutExtension(xiKey) + ".xml"));
    }

    private CustomListing mCustomListing;
  }
}
