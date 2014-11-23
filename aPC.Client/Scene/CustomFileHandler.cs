using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;

namespace aPC.Client.Scene
{
  public class CustomFileHandler
  {
    private readonly CustomListing customListing;

    public CustomFileHandler(CustomListing customListing)
    {
      this.customListing = customListing;
    }

    public string AddNewFile()
    {
      var fullFilePath = GetFilenameFromDialog();
      if (fullFilePath == string.Empty)
      {
        return string.Empty;
      }

      return ImportFile(fullFilePath)
        ? ProfilesStore.GetFilenameWithoutExtension(fullFilePath)
        : string.Empty;
    }

    private string GetFilenameFromDialog()
    {
      var dialog = new OpenFileDialog { Multiselect = false, Filter = "Xml Files (*.xml)|*.xml" };
      dialog.ShowDialog();
      return dialog.FileName;
    }

    /// <summary>
    ///   Import the given file, after doing a couple of checks.
    /// </summary>
    /// <returns>True if the file was successfully imported.</returns>
    public bool ImportFile(string filename)
    {
      if (customListing.Scenes.Keys.Any(scene => scene == filename))
      {
        var overwriteFile = MessageBox.Show("A scene with this filename already exists and will be overwritten.  Do you want to continue?",
                                         "Overwrite file?",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

        if (overwriteFile != MessageBoxResult.Yes)
        {
          // Bail out
          return false;
        }
      }

      var targetFullFilePath = Path.Combine(ProfilesStore.Directory, ProfilesStore.GetFilenameWithoutExtension(filename) + ".xml");
      File.Copy(filename, targetFullFilePath);
      return true;
    }

    /// <summary>
    /// Delete the file with the given name
    /// </summary>
    public void Delete(string key)
    {
      var confirmDeletion = MessageBox.Show("Are you sure you want to delete this file?",
                                             "Delete file?",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);
      if (confirmDeletion != MessageBoxResult.Yes)
      {
        return;
      }

      File.Delete(Path.Combine(ProfilesStore.Directory, ProfilesStore.GetFilenameWithoutExtension(key) + ".xml"));
    }
  }
}