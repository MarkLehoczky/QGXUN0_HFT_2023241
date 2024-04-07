using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for BookListWindow.xaml
    /// </summary>
    public partial class BookListWindow : Window
    {
        private Book? selectedItem;
        private IEnumerable<Book> selectedItems;

        public BookListWindow(IEnumerable<Book> items, string windowName = "Book list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
            selectedItem = null;
            selectedItems = new List<Book>();
        }


        public void Show(out Book? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            selectedItem = this.selectedItem;
        }
        public void Show(out IEnumerable<Book> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            selectedItems = this.selectedItems;
        }

        public bool? ShowDialog(out Book? selectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            selectedItem = this.selectedItem;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Book> selectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            selectedItems = this.selectedItems;
            return DialogResult;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = datagrid.SelectedItem as Book;
            selectedItems = datagrid.SelectedItems as IList<Book> ?? new List<Book>();
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
