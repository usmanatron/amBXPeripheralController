using System.Windows;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for HostnameInput.xaml
  /// </summary>
  public partial class HostnameInput : Window
  {
    public HostnameInput()
    {
      InitializeComponent();
    }

    public void OkButtonClick(object sender, RoutedEventArgs e)
    {
      var lNewHostnameContent = NewHostnameTextBox.Text;

      if (string.IsNullOrEmpty(lNewHostnameContent))
      {
        MessageBox.Show("No hostname given!  Please check and try again.", 
                        "Error", 
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
        return;
      }

      NewHostname = NewHostnameTextBox.Text;
      Close();
    }

    public string NewHostname;
  }
}
