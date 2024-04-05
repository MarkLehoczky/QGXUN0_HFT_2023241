using QGXUN0_HFT_2023241.Models.Models;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows
{
    /// <summary>
    /// Interaction logic for PublisherUpdateWindow.xaml
    /// </summary>
    public partial class PublisherUpdateWindow : Window
    {
        public PublisherUpdateWindow(ref Publisher publisher)
        {
            InitializeComponent();
            DataContext = publisher;
        }
    }
}
