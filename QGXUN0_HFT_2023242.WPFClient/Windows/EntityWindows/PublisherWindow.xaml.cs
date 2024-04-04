using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for PublisherWindow.xaml
    /// </summary>
    public partial class PublisherWindow : Window
    {
        public PublisherWindow(Publisher publisher)
        {
            InitializeComponent();
            DataContext = publisher;
        }
    }
}
