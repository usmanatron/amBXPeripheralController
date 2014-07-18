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
      mIntegratedScenes = mKernel.Kernel.Get<IntegratedListing>();
      IntegratedSceneList.ItemsSource = mIntegratedScenes.DropdownListing;
    }

    private void PopulateCustomList()
    {
      mCustomScenes = mKernel.Kernel.Get<CustomListing>();
      CustomSceneList.ItemsSource = mCustomScenes.DropdownListing;
    }

    private void PopulateHostname()
    {
      mHostnameAccessor = mKernel.Kernel.Get<HostnameAccessor>();
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
      SceneSelectionChanged(IntegratedSceneList, mIntegratedScenes, true);
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      SceneSelectionChanged(IntegratedSceneList, mIntegratedScenes, true);
    }

    #endregion

    #region Custom Scenes

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
      SceneSelectionChanged(CustomSceneList, mCustomScenes, false);
    }

    private void CustomSceneDeselected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = false;
    }

    private void CustomSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      if ((string)CustomSceneList.SelectedValue == mCustomScenes.BrowseItemName)
      {
        var lNewFile = mCustomFileHandler.AddNewFileAndUpdateListing();
        ReloadCustomDropdown(lNewFile);
          
      }
      SceneSelectionChanged(CustomSceneList, mCustomScenes, false);
    }

    private void ReloadCustomDropdown(string xiNewFile)
    {
      CustomSceneList.ItemsSource = mCustomScenes.DropdownListing;
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
    private ISceneListing mIntegratedScenes;
    private ISceneListing mCustomScenes;
    private readonly SceneRunner mSceneRunner;
    private readonly HostnameAccessor mHostnameAccessor;
    private readonly CustomFileHandler mCustomFileHandler;
  }
}
