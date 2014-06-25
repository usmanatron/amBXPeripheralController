using System;
using System.Linq;
using System.Windows;
using aPC.Common;
using Microsoft.Win32;
using Ninject;
using aPC.Client.Scene;
using System.IO;
using System.Windows.Controls;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      mKernel = NinjectKernelHandler.Instance;
      mSettings = mKernel.Kernel.Get<Settings>();
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
      mIntegratedScenes = mKernel.Kernel.Get<IntegratedListing>();
      IntegratedSceneList.ItemsSource = mIntegratedScenes.Scenes.Keys;
    }

    private void PopulateCustomList()
    {
      mCustomScenes = mKernel.Kernel.Get<CustomListing>();
      CustomSceneList.ItemsSource = mCustomScenes.Scenes.Keys;
    }

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
        GetCustomSceneNotKnown();
      }
      SceneSelectionChanged(CustomSceneList, mCustomScenes, false);
    }

    //qqUMI rename
    private void GetCustomSceneNotKnown()
    {
      var lHandler = new CustomFileHandler();
      var lFilename = lHandler.GetFilenameFromDialog();
      var lKeepScene = MessageBox.Show("Do you want to store this scene for future use?", "Store for later?", MessageBoxButton.YesNo, MessageBoxImage.Question);

      if (lKeepScene == MessageBoxResult.Yes)
      {
        lHandler.ImportAndReturnNewPath(lFilename);
        //qqUMI need to reload the dropdown on the UI, and then select it!
        mCustomScenes.Reload();
        
      }
      
      var lFileContents = File.ReadAllText(lFilename);
      CustomSceneList.SelectedValue = lFileContents;
    }

    #endregion

    private void SceneSelectionChanged(ComboBox xiSceneList, ISceneListing xiSceneListing, bool xiIsIntegratedScene)
    {
      if (xiSceneList.SelectedIndex > -1)
      {
        mSettings.Apply(xiIsIntegratedScene, xiSceneListing.Scenes[(string)xiSceneList.SelectedValue]);
      }
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

    private readonly NinjectKernelHandler mKernel;
    private readonly Settings mSettings;
    private IntegratedListing mIntegratedScenes;
    private CustomListing mCustomScenes;
  }
}
