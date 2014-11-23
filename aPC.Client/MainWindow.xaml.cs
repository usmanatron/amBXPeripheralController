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
    private readonly Settings settings;
    private readonly ISceneListing integratedSceneListing;
    private readonly ISceneListing customSceneListing;
    private ObservableCollection<string> integratedScenes;
    private ObservableCollection<string> customScenes;
    private readonly SceneRunner sceneRunner;
    private readonly UpdatableHostnameAccessor hostnameAccessor;
    private readonly CustomFileHandler customFileHandler;

    public MainWindow(Settings settings, IntegratedListing integratedListing, CustomListing customListing,
                      UpdatableHostnameAccessor hostnameAccessor, CustomFileHandler customFileHandler, SceneRunner sceneRunner)
    {
      this.settings = settings;
      this.integratedSceneListing = integratedListing;
      this.customSceneListing = customListing;
      this.hostnameAccessor = hostnameAccessor; //qqUMI This will break if you update twice+
      this.customFileHandler = customFileHandler;
      this.sceneRunner = sceneRunner;

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
      integratedScenes = new ObservableCollection<string>(integratedSceneListing.DropdownListing);
      IntegratedSceneList.ItemsSource = integratedScenes;
    }

    private void PopulateCustomList()
    {
      customScenes = new ObservableCollection<string>(customSceneListing.DropdownListing);
      CustomSceneList.ItemsSource = customScenes;
    }

    private void PopulateHostname()
    {
      UpdateHostnameContent();
    }

    #region Hostname selection \ update

    private void UpdateHostnameContent()
    {
      Hostname.Content = hostnameAccessor.Get();
    }

    public void ChangeHostnameClick(object sender, RoutedEventArgs e)
    {
      hostnameAccessor.Update();
      UpdateHostnameContent();
      ReloadDropdown(integratedSceneListing, integratedScenes);
    }

    #endregion Hostname selection \ update

    #region Integrated Scenes

    private void IntegratedSceneSelected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = true;
      SceneSelectionChanged(IntegratedSceneList, integratedSceneListing, true);
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      SceneSelectionChanged(IntegratedSceneList, integratedSceneListing, true);
    }

    #endregion Integrated Scenes

    #region Custom Scenes

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
      SceneSelectionChanged(CustomSceneList, customSceneListing, false);
    }

    private void CustomSceneDeselected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = false;
    }

    private void CustomSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      if ((string)CustomSceneList.SelectedValue == customSceneListing.BrowseItemName)
      {
        var newFile = customFileHandler.AddNewFile();

        if (string.IsNullOrEmpty(newFile))
        {
          return;
        }

        ReloadDropdown(customSceneListing, customScenes);
        CustomSceneList.Text = newFile;
      }
      SceneSelectionChanged(CustomSceneList, customSceneListing, false);
    }

    #endregion Custom Scenes

    private void ReloadDropdown(ISceneListing sceneListing, ObservableCollection<string> scenes)
    {
      scenes.Clear();
      sceneListing.Reload();
      foreach (var scene in sceneListing.DropdownListing)
      {
        scenes.Add(scene);
      }
    }

    private void SceneSelectionChanged(ComboBox sceneList, ISceneListing sceneListing, bool isIntegratedScene)
    {
      if (sceneList.SelectedIndex > -1)
      {
        settings.Update(isIntegratedScene, sceneListing.GetValue((string)sceneList.SelectedValue));
      }

      StartButton.IsEnabled = settings.IsValid;
    }

    private void RunClick(object sender, RoutedEventArgs e)
    {
      if (!settings.IsValid)
      {
        throw new ArgumentException("The information given is invalid");
      }
      sceneRunner.RunScene();
    }
  }
}