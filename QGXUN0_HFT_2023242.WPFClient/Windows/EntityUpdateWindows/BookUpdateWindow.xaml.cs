using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows
{
    /// <summary>
    /// Interaction logic for BookUpdateWindow.xaml
    /// </summary>
    public partial class BookUpdateWindow : Window
    {
        private Book reference;

        public BookUpdateWindow()
        {
            InitializeComponent();
            reference = new Book();
        }


        public void Show(ref Book book, IList<Publisher> publishers)
        {
            reference = book;
            publisher_box.ItemsSource = publishers;
            publisher_box.SelectedItem = book.Publisher;
            DataContext = book;
            base.Show();
        }

        public bool? ShowDialog(ref Book book, IList<Publisher> publishers)
        {
            reference = book;
            publisher_box.ItemsSource = publishers;
            publisher_box.SelectedItem = book.Publisher;
            DataContext = book;
            return base.ShowDialog();
        }


        private void PublisherSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reference.PublisherID = (sender as Publisher)?.PublisherID;
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
