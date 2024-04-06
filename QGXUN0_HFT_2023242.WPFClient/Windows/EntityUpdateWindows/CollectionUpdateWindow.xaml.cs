using QGXUN0_HFT_2023241.Models.Models;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows
{
    /// <summary>
    /// Interaction logic for CollectionUpdateWindow.xaml
    /// </summary>
    public partial class CollectionUpdateWindow : Window
    {
        public CollectionUpdateWindow()
        {
            InitializeComponent();
        }


        public void Show(ref Collection collection)
        {
            DataContext = collection;
            base.Show();
        }

        public bool? ShowDialog(ref Collection collection)
        {
            DataContext = collection;
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
