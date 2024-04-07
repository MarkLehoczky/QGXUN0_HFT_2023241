using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows
{
    /// <summary>
    /// Interaction logic for NumberInputWindow.xaml
    /// </summary>
    public partial class NumberInputWindow : Window
    {
        public NumberInputWindow(string description = "Enter an integer")
        {
            InitializeComponent();
            desc.Text = description;
        }

        public void Show(out int? number)
        {
            base.Show();
            if (int.TryParse(value.Text, out int temp)) number = temp;
            else number = null;
        }

        public bool? ShowDialog(out int? number)
        {
            var ret = base.ShowDialog();
            if (int.TryParse(value.Text, out int temp)) number = temp;
            else number = null;
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
