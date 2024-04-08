using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for BookListWindow.xaml
    /// </summary>
    public partial class BookListWindow : Window
    {
        public BookListWindow(IEnumerable<Book> items, string windowName = "Book list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
        }


        public void Show(out Book? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            SelectedItem = datagrid.SelectedItem as Book;
        }
        public void Show(out IEnumerable<Book> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            SelectedItems = Enumerable.Cast<Book>(datagrid.SelectedItems);
        }

        public bool? ShowDialog(out Book? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            SelectedItem = datagrid.SelectedItem as Book;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Book> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            SelectedItems = Enumerable.Cast<Book>(datagrid.SelectedItems);
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
