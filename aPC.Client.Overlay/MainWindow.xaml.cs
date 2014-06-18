using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using aPC.Common;

namespace aPC.Client.Overlay
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
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
        .Select(scene => scene.Key);

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
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
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
      if (!IntegratedSceneList.IsEnabled)
      {
        throw new NotImplementedException();
      }

      var lArguments = new string[] { @"/I", (string)IntegratedSceneList.SelectedValue };

      aPC.Client.Client.Main(lArguments);
    }

  }
}
