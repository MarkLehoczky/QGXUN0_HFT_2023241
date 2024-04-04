using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for CollectionListWindow.xaml
    /// </summary>
    public partial class CollectionListWindow : Window
    {
        private Collection? selected;

        public CollectionListWindow(IList<Collection> items, string windowName = "Collection list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid_collection.ItemsSource = items;
            selected = null;
        }


        public void Show(out Collection? selected)
        {
            Show();
            selected = this.selected;
        }

        public bool? ShowDialog(out Collection? selected)
        {
            ShowDialog();
            selected = this.selected;
            return DialogResult;
        }

        private void ConfrimedSelection(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            selected = datagrid_collection.SelectedItem as Collection;
            Close();
        }
    }
}
