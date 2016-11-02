using aPC.Client.Gui.Scene;
using aPC.Client.Shared;
using aPC.Common.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace aPC.Client.Gui
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    private readonly Settings settings;
    private readonly ISceneListing integratedSceneListing;
    private readonly ISceneListing customSceneListing;
    private ObservableCollection<string> integratedScenes;
    private ObservableCollection<string> customScenes;
    private readonly SceneRunner sceneRunner;
    private readonly SingleHostnameAccessor hostnameAccessor;
    private readonly CustomFileHandler customFileHandler;

    public MainWindow(Settings settings, IntegratedListing integratedListing, CustomListing customListing,
                      SingleHostnameAccessor hostnameAccessor, CustomFileHandler customFileHandler, SceneRunner sceneRunner)
    {
      this.settings = settings;
      integratedSceneListing = integratedListing;
      customSceneListing = customListing;
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
      UpdateHostnameContent(hostnameAccessor.Get());
    }

    #region Hostname selection \ update

    private void UpdateHostnameContent(string newHostname)
    {
      Hostname.Content = hostnameAccessor.Get();
      hostnameAccessor.PersistConfig(newHostname);
      
    }

    public void ChangeHostnameClick(object sender, RoutedEventArgs e)
    {
      var newHostname = GetNewHostname();
      hostnameAccessor.Update(newHostname);
      UpdateHostnameContent(newHostname);
      ReloadDropdown(integratedSceneListing, integratedScenes);
    }

  private string GetNewHostname()
  {
    var hostnameInput = new HostnameInput();
    hostnameInput.ShowDialog();
    var newHostname = hostnameInput.NewHostname;

    return string.IsNullOrEmpty(newHostname)
      ? hostnameAccessor.Get()
      : newHostname;
  }


  public void ReloadClick(object sender, RoutedEventArgs e)
    {
      ReloadDropdown(integratedSceneListing, integratedScenes);
      ReloadDropdown(customSceneListing, customScenes);
    }

    #endregion Hostname selection \ update

    #region Integrated Scenes

    private void IntegratedSceneSelected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = true;
      IntegratedSceneChanged();
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      IntegratedSceneChanged();
    }

    private void IntegratedSceneChanged()
    {
      if (IntegratedSceneList.SelectedIndex > -1)
      {
        settings.SetSceneName(integratedSceneListing.GetValue((string)IntegratedSceneList.SelectedValue));
      }

      StartButton.IsEnabled = settings.IsValid;
    }

    #endregion Integrated Scenes

    #region Custom Scenes

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
      CustomSceneChanged();
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
      CustomSceneChanged();
    }

    private void CustomSceneChanged()
    {
      if (CustomSceneList.SelectedIndex > -1)
      {
        //TODO: Fix this!
        //settings.SetScene((amBXScene)customSceneListing.GetValue((string)CustomSceneList.SelectedValue));
      }

      StartButton.IsEnabled = settings.IsValid;
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