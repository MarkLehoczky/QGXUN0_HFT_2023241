using QGXUN0_HFT_2023241.Models.Models;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows
{
    /// <summary>
    /// Interaction logic for CollectionUpdateWindow.xaml
    /// </summary>
    public partial class CollectionUpdateWindow : Window
    {
        public CollectionUpdateWindow(ref Collection collection)
        {
            InitializeComponent();
            DataContext = collection;
        }
    }
}
