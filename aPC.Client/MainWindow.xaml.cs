using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using aPC.Common;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      mSettings = new Settings();
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
      var lScenes = new List<string> { mBrowse };
      CustomSceneList.ItemsSource = lScenes;
    }

    private void IntegratedSceneSelected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = true;
      mSettings.IsIntegratedScene = true;
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      mSettings.SceneData = (string)IntegratedSceneList.SelectedValue;
    }

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
      mSettings.IsIntegratedScene = false;
    }

    private void CustomSceneDeselected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = false;
    }

    private void CustomSceneSelectionChanged(object sender, RoutedEventArgs e)
    {
      if ((string)CustomSceneList.SelectedValue == mBrowse)
      {
        MessageBox.Show("Not yet implemented!");
      }
    }

    private const string mBrowse = "<Browse...>";

    private void CloseClick(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    private void RunClick(object sender, RoutedEventArgs e)
    {
      if (!mSettings.IsIntegratedScene)
      { 
        throw new NotImplementedException();
      }

      if (!mSettings.IsValid)
      {
        throw new ArgumentException("The information given is invalid");
      }

      Client.ConsoleMain(mSettings);
    }

    private Settings mSettings;
  }
}
