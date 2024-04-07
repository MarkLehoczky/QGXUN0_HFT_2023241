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

namespace QGXUN0_HFT_2023242.WPFClient.Windows
{
    /// <summary>
    /// Interaction logic for SimpleOutput.xaml
    /// </summary>
    public partial class SimpleOutput : Window
    {
        public SimpleOutput(object description, object value)
        {
            InitializeComponent();
            desc.Text = description.ToString();
            val.Text = value.ToString();
        }
    }
}
