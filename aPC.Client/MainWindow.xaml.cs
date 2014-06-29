using System;
using System.Linq;
using System.Windows;
using aPC.Common;
using Ninject;
using aPC.Client.Scene;
using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;

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
      IntegratedSceneList.ItemsSource = mIntegratedScenes.DropdownListing;
    }

    private void PopulateCustomList()
    {
      mCustomScenes = mKernel.Kernel.Get<CustomListing>();
      CustomSceneList.ItemsSource = mCustomScenes.DropdownListing;
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
        var lFileHandler = mKernel.Kernel.Get<CustomFileHandler>();
        var lNewFile = lFileHandler.AddNewFileAndUpdateListing();
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
        mSettings.Apply(xiIsIntegratedScene, xiSceneListing.GetValue((string)xiSceneList.SelectedValue));
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
    private ISceneListing mIntegratedScenes;
    private ISceneListing mCustomScenes;
  }
}
