using FCM.DAO;
using FCM.DTO;
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
    /// Interaction logic for PenaltyWindow.xaml
    /// </summary>
    public partial class PenaltyWindow : Window
    {
        public ResultRecordingWindow resultWD;

        public PenaltyWindow()
        {
            InitializeComponent();
        }

        public PenaltyWindow(ResultRecordingWindow resultWD)
        {
            InitializeComponent();

            this.resultWD = resultWD;

            Team team1 = TeamDAO.Instance.GetTeamById(resultWD.match.idTeam01);
            Team team2 = TeamDAO.Instance.GetTeamById(resultWD.match.idTeam02);

            this.imgLogoTeam1.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team1.logo));
            this.imgLogoTeam2.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team2.logo));

            this.tblNameTeam1.Text = team1.nameTeam;
            this.tblNameTeam2.Text = team2.nameTeam;
        }
    }
}
