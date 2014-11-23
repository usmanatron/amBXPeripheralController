using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;

namespace aPC.Client.Scene
{
  public class CustomFileHandler
  {
    public CustomFileHandler(CustomListing xiCustomListing)
    {
      mCustomListing = xiCustomListing;
    }

    public string AddNewFile()
    {
      var lFullFilePath = GetFilenameFromDialog();
      if (lFullFilePath == string.Empty)
      {
        return string.Empty;
      }

      return ImportFile(lFullFilePath)
        ? ProfilesStore.GetFilenameWithoutExtension(lFullFilePath)
        : string.Empty;
    }

    private string GetFilenameFromDialog()
    {
      var lDialog = new OpenFileDialog { Multiselect = false, Filter = "Xml Files (*.xml)|*.xml" };
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

      var lTargetFullFilePath = Path.Combine(ProfilesStore.Directory, ProfilesStore.GetFilenameWithoutExtension(xiFilename) + ".xml");
      File.Copy(xiFilename, lTargetFullFilePath);
      return true;
    }

    /// <summary>
    /// Delete the file with the given name
    /// </summary>
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

      File.Delete(Path.Combine(ProfilesStore.Directory, ProfilesStore.GetFilenameWithoutExtension(xiKey) + ".xml"));
    }

    private readonly CustomListing mCustomListing;
  }
}