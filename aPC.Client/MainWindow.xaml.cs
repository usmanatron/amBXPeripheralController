using System;
using System.Linq;
using System.Windows;
using aPC.Common;
using Microsoft.Win32;
using Ninject;
using aPC.Client.Scene;

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

    private void IntegratedSceneSelected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = true;
      if (IntegratedSceneList.SelectedIndex > -1)
      {
        mSettings.Apply(true, (string)IntegratedSceneList.SelectedValue);
      }
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      mSettings.Apply(true, (string)IntegratedSceneList.SelectedValue);
    }

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
      if (CustomSceneList.SelectedIndex > -1)
      {
        mSettings.Apply(false, mCustomScenes.Scenes[(string)CustomSceneList.SelectedValue]);
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
      mSettings.Apply(false, mCustomScenes.Scenes[(string)CustomSceneList.SelectedValue]);
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
    private IntegratedListing mIntegratedScenes;
    private CustomListing mCustomScenes;
    private readonly NinjectKernelHandler mKernel;
  }
}
