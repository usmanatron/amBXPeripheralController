using System;
using System.Linq;
using System.Windows;
using aPC.Common;
using Microsoft.Win32;
using Ninject;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      mSettings = Settings.Instance;
      mKernel = NinjectKernelHandler.Instance;
      InitializeComponent();
      PopulateSceneLists();
    }

    private void PopulateSceneLists()
    {
      PopulateIntegratedList();
      PopulateCustomList();
    }

    private void PopulateIntegratedList()
    {
      var lScenes = new SceneAccessor()
        .GetAllScenes()
        .Select(scene => scene.Key)
        .OrderBy(scene => scene);

      IntegratedSceneList.ItemsSource = lScenes;
    }

    private void PopulateCustomList()
    {
      mCustomScenes = new CustomSceneListing();
      CustomSceneList.ItemsSource = mCustomScenes.Scenes.Keys;
    }

    private void IntegratedSceneSelected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = true;
      if (IntegratedSceneList.SelectedIndex > -1)
      {
        UpdateSettings(true, (string) IntegratedSceneList.SelectedValue);
      }
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      UpdateSettings(true, (string) IntegratedSceneList.SelectedValue);
    }

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
      if (CustomSceneList.SelectedIndex > -1)
      {
        UpdateSettings(false, mCustomScenes.Scenes[(string) CustomSceneList.SelectedValue]);
      }
    }

    private void CustomSceneDeselected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = false;
    }

    private void CustomSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      if ((string)CustomSceneList.SelectedValue == mCustomScenes.BrowseItemName)
      {
        //var lFilename = GetFileFromDialog();
        MessageBox.Show("Not yet implemented!");
      }
      UpdateSettings(false, mCustomScenes.Scenes[(string)CustomSceneList.SelectedValue]);
    }

    

    private void UpdateSettings(bool xiIsintegratedScene, string xiSceneData)
    {
      mSettings.IsIntegratedScene = xiIsintegratedScene;
      mSettings.SceneData = xiSceneData;
    }
    
    private void CloseClick(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    private void RunClick(object sender, RoutedEventArgs e)
    {
      if (!mSettings.IsValid)
      {
        throw new ArgumentException("The information given is invalid");
      }

      var lTask = mKernel.Kernel.Get<SceneRunner>();
      lTask.RunScene();
    }

    private readonly Settings mSettings;
    private CustomSceneListing mCustomScenes;
    private readonly NinjectKernelHandler mKernel;
  }
}
