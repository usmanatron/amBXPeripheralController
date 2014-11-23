using aPC.Client.Scene;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(Settings xiSettings, IntegratedListing xiIntegratedListing, CustomListing xiCustomListing,
                      UpdatableHostnameAccessor xiHostnameAccessor, CustomFileHandler xiCustomFileHandler, SceneRunner xiSceneRunner)
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
      ReloadDropdown(mIntegratedSceneListing, mIntegratedScenes);
    }

    #endregion Hostname selection \ update

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

    #endregion Integrated Scenes

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
        var lNewFile = mCustomFileHandler.AddNewFile();

        if (string.IsNullOrEmpty(lNewFile))
        {
          return;
        }

        ReloadDropdown(mCustomSceneListing, mCustomScenes);
        CustomSceneList.Text = lNewFile;
      }
      SceneSelectionChanged(CustomSceneList, mCustomSceneListing, false);
    }

    #endregion Custom Scenes

    private void ReloadDropdown(ISceneListing xiSceneListing, ObservableCollection<string> xiScenes)
    {
      xiScenes.Clear();
      xiSceneListing.Reload();
      foreach (var lScene in xiSceneListing.DropdownListing)
      {
        xiScenes.Add(lScene);
      }
    }

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
    private readonly UpdatableHostnameAccessor mHostnameAccessor;
    private readonly CustomFileHandler mCustomFileHandler;
  }
}