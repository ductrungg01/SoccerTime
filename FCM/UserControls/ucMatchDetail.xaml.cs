using FCM.DAO;
using FCM.DTO;
using FCM.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FCM.UserControls
{
    /// <summary>
    /// Interaction logic for ucMatchDetail.xaml
    /// </summary>
    public partial class ucMatchDetail : UserControl
    {
        public Match match { get; set; }
        public MainWindow mainWindow { get; set; }
        public MainViewModel main { get; set; }

        public ucMatchDetail()
        {
            InitializeComponent();
        }

        public ucMatchDetail(int stt, Match match, MainWindow parameter, MainViewModel main, bool canEdit)
        {
            InitializeComponent();
            this.mainWindow = parameter;
            this.main = main;
            this.match = match;

            this.tblSTT.Text = stt.ToString();
            this.tblDate.Text = match.date.ToString("dd/MM/yyyy");
            this.tblTime.Text = match.time.ToString("HH:mm");
            if (tblDate.Text == "11/11/2000")
            //if (this.match.isStarted == false)
            {
                tblDate.Text = "--/--/----";
                tblTime.Text = "--:--";
                btnRecordResult.IsEnabled = false;
            }
            this.tblTeam1.Text = TeamDAO.Instance.GetTeamById(match.idTeam01).nameTeam;

            this.tblTeam2.Text = TeamDAO.Instance.GetTeamById(match.idTeam02).nameTeam;
            this.tblStadium.Text = MatchDAO.Instance.getMatchByID(match.id).statium.ToString();
            this.tblRound.Text = MatchDAO.Instance.getMatchByID(match.id).round.ToString();
            switch(tblRound.Text)
            {
                case "-1":
                    tblRound.Text = "Chung kết";
                    break;
                case "-2":
                    tblRound.Text = "Bán kết";
                    break;
                case "-3":
                    tblRound.Text = "Tứ kết";
                    break;
                case "-4":
                    tblRound.Text = "1/8";
                    break;

            }


            if (tblStadium.Text == "")
                tblStadium.Text = "-";

            if (match.Score1 == -1 && match.Score2 == -1)
            //if (this.match.isStarted == false)
            {
                this.tblScore.Text = "-- - --";
                btnCancelResult.IsEnabled = false;
            }
            else
                this.tblScore.Text = match.Score1 + " - " + match.Score2;
            if (match.allowDraw == false && match.Score1 == match.Score2 && match.Score1!=-1)
                this.tblScore.Text =" "+ match.Score1 + " - " + match.Score2 + "\n(" +match.PenaltyTeam1 +" - "+ match.PenaltyTeam2 + ")";


            if (!canEdit)
            {
                btnEditInfor.IsEnabled = false;
                btnCancelResult.IsEnabled = false;
                btnRecordResult.IsEnabled = false;
            }
        }
    }
}
