﻿using System;
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

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for CollectionManagerWindow.xaml
    /// </summary>
    public partial class CollectionManagerWindow : Window
    {
        public CollectionManagerWindow()
        {
            InitializeComponent();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
