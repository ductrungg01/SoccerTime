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
using FCM.DAO;
using FCM.DTO;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for AddLeagueWindow.xaml
    /// </summary>
    public partial class AddLeagueWindow : Window
    {
        public League league { get; set; }
        public AddLeagueWindow()
        {
            InitializeComponent();
        }
        public AddLeagueWindow(League league)
        {
            InitializeComponent();
            
            this.league = league;
        }
    }
}
