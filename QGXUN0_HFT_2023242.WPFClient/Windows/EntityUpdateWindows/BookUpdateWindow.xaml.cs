using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;
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
        private IEnumerable<Publisher> publishers;

        public BookUpdateWindow()
        {
            InitializeComponent();
            reference = new Book();
        }


        public void Show(ref Book book, IEnumerable<Publisher> publishers)
        {
            reference = book;
            publisher_box.ItemsSource = publishers;
            publisher_box.SelectedItem = book.Publisher;
            DataContext = book;
            base.Show();
        }

        public bool? ShowDialog(ref Book book, IEnumerable<Publisher> publishers)
        {
            reference = book;
            this.publishers = publishers;
            publisher_box.ItemsSource = publishers.Select(t => t.PublisherName);
            publisher_box.SelectedItem = book.Publisher.PublisherName;
            DataContext = book;
            return base.ShowDialog();
        }


        private void PublisherSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reference.PublisherID = publishers.FirstOrDefault(t => (publisher_box.SelectedItem as string) == t.PublisherName)?.PublisherID;
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
