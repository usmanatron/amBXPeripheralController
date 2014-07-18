using System;
using System.Collections.ObjectModel;
using System.Windows;
using aPC.Client.Scene;
using System.Windows.Controls;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(Settings xiSettings, IntegratedListing xiIntegratedListing, CustomListing xiCustomListing, 
                      HostnameAccessor xiHostnameAccessor, CustomFileHandler xiCustomFileHandler, SceneRunner xiSceneRunner)
    {
      mSettings = xiSettings;
      mIntegratedSceneListing = xiIntegratedListing;
      mCustomSceneListing = xiCustomListing;
      mHostnameAccessor = xiHostnameAccessor; //qqUMI This will break if you update twice+
      mCustomFileHandler = xiCustomFileHandler;
      mSceneRunner = xiSceneRunner;

      InitializeComponent();
      PopulateSceneLists();
      PopulateHostname();
    }

    private void PopulateSceneLists()
    {
      PopulateIntegratedList();
      PopulateCustomList();
    }

    private void PopulateIntegratedList()
    {
      mIntegratedScenes = new ObservableCollection<string>(mIntegratedSceneListing.DropdownListing);
      IntegratedSceneList.ItemsSource = mIntegratedScenes;
    }

    private void PopulateCustomList()
    {
      mCustomScenes = new ObservableCollection<string>(mCustomSceneListing.DropdownListing);
      CustomSceneList.ItemsSource = mCustomScenes;
    }

    private void PopulateHostname()
    {
      UpdateHostnameContent();
    }

    #region Hostname selection \ update

    private void UpdateHostnameContent()
    {
      Hostname.Content = mHostnameAccessor.Get();
    }

    public void ChangeHostnameClick(object sender, RoutedEventArgs e)
    {
      mHostnameAccessor.Update();
      UpdateHostnameContent();
    }

    #endregion

    #region Integrated Scenes

    private void IntegratedSceneSelected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = true;
      SceneSelectionChanged(IntegratedSceneList, mIntegratedSceneListing, true);
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      SceneSelectionChanged(IntegratedSceneList, mIntegratedSceneListing, true);
    }

    #endregion

    #region Custom Scenes

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
      SceneSelectionChanged(CustomSceneList, mCustomSceneListing, false);
    }

    private void CustomSceneDeselected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = false;
    }

    private void CustomSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      if ((string)CustomSceneList.SelectedValue == mCustomSceneListing.BrowseItemName)
      {
        var lNewFile = mCustomFileHandler.AddNewFileAndUpdateListing();
        AddNewFileToCustomDropdown(lNewFile);
          
      }
      SceneSelectionChanged(CustomSceneList, mCustomSceneListing, false);
    }

    /// <summary>
    /// Adds the newly added file to the dropdown
    /// </summary>
    /// <remarks>
    /// The new file is added in the correct place (according to alphabetical order)
    /// </remarks>
    private void AddNewFileToCustomDropdown(string xiNewFile)
    {
      var lIndex = 0;
      while (lIndex < mCustomScenes.Count && String.CompareOrdinal(mCustomScenes[lIndex], xiNewFile) < 0)
      {
        lIndex++;
      }

      mCustomScenes.Insert(lIndex, xiNewFile);
      CustomSceneList.Text = xiNewFile;
    }

    #endregion

    private void SceneSelectionChanged(ComboBox xiSceneList, ISceneListing xiSceneListing, bool xiIsIntegratedScene)
    {
      if (xiSceneList.SelectedIndex > -1)
      {
        mSettings.Update(xiIsIntegratedScene, xiSceneListing.GetValue((string)xiSceneList.SelectedValue));
      }

      StartButton.IsEnabled = mSettings.IsValid;
    }

    private void RunClick(object sender, RoutedEventArgs e)
    {
      if (!mSettings.IsValid)
      {
        throw new ArgumentException("The information given is invalid");
      }
      mSceneRunner.RunScene();
    }

    private readonly Settings mSettings;
    private readonly ISceneListing mIntegratedSceneListing;
    private readonly ISceneListing mCustomSceneListing;
    private ObservableCollection<string> mIntegratedScenes;
    private ObservableCollection<string> mCustomScenes;
    private readonly SceneRunner mSceneRunner;
    private readonly HostnameAccessor mHostnameAccessor;
    private readonly CustomFileHandler mCustomFileHandler;
  }
}
