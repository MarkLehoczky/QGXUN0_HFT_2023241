using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for CollectionListWindow.xaml
    /// </summary>
    public partial class CollectionListWindow : Window
    {
        private Collection? selectedItem;
        private IEnumerable<Collection> selectedItems;

        public CollectionListWindow(IEnumerable<Collection> items, string windowName = "Collection list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
            selectedItem = null;
            selectedItems = new List<Collection>();
        }


        public void Show(out Collection? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            selectedItem = this.selectedItem;
        }
        public void Show(out IEnumerable<Collection> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            selectedItems = this.selectedItems;
        }

        public bool? ShowDialog(out Collection? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            selectedItem = this.selectedItem;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Collection> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            selectedItems = this.selectedItems;
            return DialogResult;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = datagrid.SelectedItem as Collection;
            selectedItems = datagrid.SelectedItems as IList<Collection> ?? new List<Collection>();
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
