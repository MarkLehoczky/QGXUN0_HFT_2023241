using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for PublisherListWindow.xaml
    /// </summary>
    public partial class PublisherListWindow : Window
    {
        public PublisherListWindow(IEnumerable<Publisher> items, string windowName = "Publisher list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid.ItemsSource = items;
        }


        public void Show(out Publisher? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            Show();
            SelectedItem = datagrid.SelectedItem as Publisher;
        }
        public void Show(out IEnumerable<Publisher> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            Show();
            SelectedItems = Enumerable.Cast<Publisher>(datagrid.SelectedItems);
        }

        public bool? ShowDialog(out Publisher? SelectedItem)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            ShowDialog();
            SelectedItem = datagrid.SelectedItem as Publisher;
            return DialogResult;
        }
        public bool? ShowDialog(out IEnumerable<Publisher> SelectedItems)
        {
            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            ShowDialog();
            SelectedItems = Enumerable.Cast<Publisher>(datagrid.SelectedItems);
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
