using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for AuthorListWindow.xaml
    /// </summary>
    public partial class AuthorListWindow : Window
    {
        public AuthorListWindow(IEnumerable<Author> items, string windowName = "Author list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
        }


        public void Show(out Author? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            SelectedItem = datagrid.SelectedItem as Author;
        }
        public void Show(out IEnumerable<Author> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            SelectedItems = Enumerable.Cast<Author>(datagrid.SelectedItems);
        }

        public bool? ShowDialog(out Author? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            SelectedItem = datagrid.SelectedItem as Author;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Author> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            SelectedItems = Enumerable.Cast<Author>(datagrid.SelectedItems);
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
