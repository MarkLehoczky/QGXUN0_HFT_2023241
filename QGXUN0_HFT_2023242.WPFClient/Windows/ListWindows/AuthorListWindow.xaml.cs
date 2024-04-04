using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for AuthorListWindow.xaml
    /// </summary>
    public partial class AuthorListWindow : Window
    {
        private Author? selected;

        public AuthorListWindow(IList<Author> items, string windowName = "Author list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid_author.ItemsSource = items;
            selected = null;
        }


        public void Show(out Author? selected)
        {
            Show();
            selected = this.selected;
        }

        public bool? ShowDialog(out Author? selected)
        {
            ShowDialog();
            selected = this.selected;
            return DialogResult;
        }

        private void ConfrimedSelection(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            selected = datagrid_author.SelectedItem as Author;
            Close();
        }
    }
}
