namespace aPC.Client
{
  /// <remarks>
  ///   Designed as a singleton, as there should only ever be one set of settings.
  /// </remarks>
  public sealed class Settings
  {
    private Settings()
    {
    }

    public static Settings Instance
    {
      get
      {
        return mInstance;
      }
    }

    /// <summary>
    /// Used specifically when a new (separate) instance is required. For example,
    /// when mimicking the structure of the Settings (e.g. when reading arguments).
    /// </summary>
    public static Settings NewInstance
    {
      get
      {
        return new Settings();
      }
    }

    public bool IsValid
    {
      get
      {
        return !string.IsNullOrEmpty(SceneData);
      }
    }

    public bool IsIntegratedScene;
    public string SceneData;

    private static readonly Settings mInstance = new Settings();
  }
}
