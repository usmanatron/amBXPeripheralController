using System.Windows;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for HostnameInput.xaml
  /// </summary>
  public partial class HostnameInput
  {
    public HostnameInput()
    {
      InitializeComponent();
    }

    public void OkButtonClick(object sender, RoutedEventArgs e)
    {
      NewHostname = NewHostnameTextBox.Text;
      Close();
    }

    public string NewHostname;
  }
}