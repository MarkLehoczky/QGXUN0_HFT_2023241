using System.Collections.Generic;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows
{
    /// <summary>
    /// Interaction logic for ItemSelectorWindow.xaml
    /// </summary>
    public partial class ItemSelectorWindow : Window
    {
        public ItemSelectorWindow(IEnumerable<object> items)
        {
            InitializeComponent();
            itembox.ItemsSource = items;
        }


        public void Show(out object? selectedItem)
        {
            base.Show();
            selectedItem = itembox.SelectedItem;
        }

        public bool? ShowDialog(out object? selectedItem)
        {
            var ret = base.ShowDialog();
            selectedItem = itembox.SelectedItem;
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
