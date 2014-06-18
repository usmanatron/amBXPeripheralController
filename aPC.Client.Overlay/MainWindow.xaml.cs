using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }

    private void CustomSceneSelected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = true;
    }

    private void CustomSceneDeselected(object sender, RoutedEventArgs e)
    {
      CustomSceneList.IsEnabled = false;
    }

    private void IntegratedSceneSelected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = true;
    }

    private void IntegratedSceneDeselected(object sender, RoutedEventArgs e)
    {
      IntegratedSceneList.IsEnabled = false;
    }

  }
}
