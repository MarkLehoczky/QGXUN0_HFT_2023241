using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for CollectionListWindow.xaml
    /// </summary>
    public partial class CollectionListWindow : Window
    {
        public CollectionListWindow(IEnumerable<Collection> items, string windowName = "Collection list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
        }


        public void Show(out Collection? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            SelectedItem = datagrid.SelectedItem as Collection;
        }
        public void Show(out IEnumerable<Collection> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            SelectedItems = Enumerable.Cast<Collection>(datagrid.SelectedItems);
        }

        public bool? ShowDialog(out Collection? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            SelectedItem = datagrid.SelectedItem as Collection;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Collection> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            SelectedItems = Enumerable.Cast<Collection>(datagrid.SelectedItems);
            return DialogResult;
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
