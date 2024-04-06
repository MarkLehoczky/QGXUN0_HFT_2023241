using QGXUN0_HFT_2023241.Models.Models;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows
{
    /// <summary>
    /// Interaction logic for PublisherUpdateWindow.xaml
    /// </summary>
    public partial class PublisherUpdateWindow : Window
    {
        public PublisherUpdateWindow(ref Publisher publisher)
        {
            InitializeComponent();
            DataContext = publisher;
        }


        public void Show(ref Publisher publisher)
        {
            DataContext = publisher;
            base.Show();
        }

        public bool? ShowDialog(ref Publisher publisher)
        {
            DataContext = publisher;
            return base.ShowDialog();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
