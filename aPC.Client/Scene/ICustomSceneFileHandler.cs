namespace aPC.Client.Scene
{
  interface ICustomSceneFileHandler
  {
    string GetFilenameFromDialog();

    void Import();

    void Delete(string xiFilename);
  }
}
