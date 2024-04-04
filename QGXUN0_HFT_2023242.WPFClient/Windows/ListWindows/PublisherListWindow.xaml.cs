using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for PublisherListWindow.xaml
    /// </summary>
    public partial class PublisherListWindow : Window
    {
        private Publisher? selected;

        public PublisherListWindow(IList<Publisher> items, string windowName = "Publisher list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid_publisher.ItemsSource = items;
            selected = null;
        }


        public void Show(out Publisher? selected)
        {
            Show();
            selected = this.selected;
        }

        public bool? ShowDialog(out Publisher? selected)
        {
            ShowDialog();
            selected = this.selected;
            return DialogResult;
        }

        private void ConfrimedSelection(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            selected = datagrid_publisher.SelectedItem as Publisher;
            Close();
        }
    }
}
