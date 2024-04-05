using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows
{
    /// <summary>
    /// Interaction logic for BookUpdateWindow.xaml
    /// </summary>
    public partial class BookUpdateWindow : Window
    {
        public BookUpdateWindow(ref Book book, IList<Publisher> publishers)
        {
            InitializeComponent();
            publisher_box.ItemsSource = publishers;
            DataContext = book;
        }
    }
}
