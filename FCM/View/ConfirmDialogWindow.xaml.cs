using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for ConfirmDialogWindow.xaml
    /// </summary>
    public partial class ConfirmDialogWindow : Window
    {
        public bool confirm = false;
        public ConfirmDialogWindow()
        {
            InitializeComponent();
        }
        public ConfirmDialogWindow(string content)
        {
            InitializeComponent();
            tblMessage.Text = content;
        }
    }
}
