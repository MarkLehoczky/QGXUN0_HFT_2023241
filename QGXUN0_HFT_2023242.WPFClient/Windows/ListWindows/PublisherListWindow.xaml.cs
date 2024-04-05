using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for PublisherListWindow.xaml
    /// </summary>
    public partial class PublisherListWindow : Window
    {
        private Publisher? selectedItem;
        private IList<Publisher> selectedItems;

        public PublisherListWindow(IList<Publisher> items, string windowName = "Publisher list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
            selectedItem = null;
            selectedItems = new List<Publisher>();
        }


        public void Show(out Publisher? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            selectedItem = this.selectedItem;
        }
        public void Show(out IEnumerable<Publisher> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            selectedItems = this.selectedItems;
        }

        public bool? ShowDialog(out Publisher? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            selectedItem = this.selectedItem;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Publisher> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            selectedItems = this.selectedItems;
            return DialogResult;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = datagrid.SelectedItem as Publisher;
            selectedItems = datagrid.SelectedItems as IList<Publisher> ?? new List<Publisher>();
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
