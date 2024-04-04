using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for BookListWindow.xaml
    /// </summary>
    public partial class BookListWindow : Window
    {
        private Book? selected;

        public BookListWindow(IList<Book> items, string windowName = "Book list")
        {
            InitializeComponent();
            Title = windowName;
            datagrid_book.ItemsSource = items;
            selected = null;
        }


        public void Show(out Book? selected)
        {
            Show();
            selected = this.selected;
        }

        public bool? ShowDialog(out Book? selected)
        {
            ShowDialog();
            selected = this.selected;
            return DialogResult;
        }

        private void ConfrimedSelection(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            selected = datagrid_book.SelectedItem as Book;
            Close();
        }
    }
}
