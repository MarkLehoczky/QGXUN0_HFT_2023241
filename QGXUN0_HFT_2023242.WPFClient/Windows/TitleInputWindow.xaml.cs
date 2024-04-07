using System.Collections.Generic;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows
{
    /// <summary>
    /// Interaction logic for TitleInputWindow.xaml
    /// </summary>
    public partial class TitleInputWindow : Window
    {
        public TitleInputWindow()
        {
            InitializeComponent();
        }


        public void Show(out IEnumerable<string> titles)
        {
            base.Show();
            titles = text.Text.Replace("\r", "").Split("\n");
        }

        public bool? ShowDialog(out IEnumerable<string> titles)
        {
            var ret = base.ShowDialog();
            titles = text.Text.Replace("\r", "").Split("\n");
            return ret;
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
