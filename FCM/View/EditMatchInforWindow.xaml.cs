using FCM.DAO;
using FCM.DTO;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for EditMatchInforWindow.xaml
    /// </summary>
    public partial class EditMatchInforWindow : Window
    {
        public Match match { get; set; }
        public EditMatchInforWindow()
        {
            InitializeComponent();
            
        }
        public EditMatchInforWindow(Match match)
        {
            InitializeComponent();
            this.match = match;

            // Sân vận động
            //this.cbStadium.Items.Clear();
            Team team01 = TeamDAO.Instance.GetTeamById(match.idTeam01);
            Team team02 = TeamDAO.Instance.GetTeamById(match.idTeam02);
            this.cbStadium.Items.Add(team01.stadium);
            this.cbStadium.Items.Add(team02.stadium);
            this.cbStadium.SelectedIndex = 0;

            // Ngày thi đấu
            this.dpDate.SelectedDate = match.date.Date;

            // Thời gian
            //this.tpTime.SelectedTime = match.time.ToString("hh:mm");ư
           ;
            this.tpTime.SelectedTime = match.time;

            InitializeComponent();

        }
    }
}
