using QGXUN0_HFT_2023241.Models.Models;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows
{
    /// <summary>
    /// Interaction logic for AuthorUpdateWindow.xaml
    /// </summary>
    public partial class AuthorUpdateWindow : Window
    {
        public AuthorUpdateWindow()
        {
            InitializeComponent();
        }


        public void Show(ref Author author)
        {
            DataContext = author;
            base.Show();
        }

        public bool? ShowDialog(ref Author author)
        {
            DataContext = author;
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
