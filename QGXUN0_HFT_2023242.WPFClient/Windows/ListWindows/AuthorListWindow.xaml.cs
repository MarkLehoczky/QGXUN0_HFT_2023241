using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for AuthorListWindow.xaml
    /// </summary>
    public partial class AuthorListWindow : Window
    {
        private Author? selectedItem;
        private IEnumerable<Author> selectedItems;

        public AuthorListWindow(IEnumerable<Author> items, string windowName = "Author list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
            selectedItem = null;
            selectedItems = new List<Author>();
        }


        public void Show(out Author? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            selectedItem = this.selectedItem;
        }
        public void Show(out IEnumerable<Author> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            selectedItems = this.selectedItems;
        }

        public bool? ShowDialog(out Author? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            selectedItem = this.selectedItem;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Author> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            selectedItems = this.selectedItems;
            return DialogResult;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = datagrid.SelectedItem as Author;
            selectedItems = datagrid.SelectedItems as IList<Author> ?? new List<Author>();
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
