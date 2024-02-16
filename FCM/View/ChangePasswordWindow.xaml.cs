using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.DTO;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public Account account { get; set; }
        public ChangePasswordWindow(string username)
        {
            InitializeComponent();
        }
        public ChangePasswordWindow(Account account)
        {
            this.account = account;
            InitializeComponent();
        }
    }
}
