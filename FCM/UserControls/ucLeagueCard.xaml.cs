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
using FCM.DAO;
using FCM.DTO;
using FCM.ViewModel;

namespace FCM.UserControls
{
    /// <summary>
    /// Interaction logic for ucLeagueCard.xaml
    /// </summary>
    public partial class ucLeagueCard : UserControl
    {
        public League league { get; set; }
        public MainWindow mainWindow { get; set; }
        public MainViewModel main { get; set; }
        public ucLeagueCard()
        {
            InitializeComponent();
        }
        public ucLeagueCard(League league, MainWindow parameter, MainViewModel main, bool canDelete)
        {
            InitializeComponent();
            this.mainWindow = parameter;
            this.main = main;
            this.league = league;
            imgLeagueLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(league.logo));
            tblLeagueName.Text = league.nameLeague;
            switch (league.typeLeague)
            {
                case 0:
                    tblLeagueFormula.Text = "Đấu vòng tròn";
                    break;
                case 1:
                    tblLeagueFormula.Text = "Đấu loại trực tiếp";
                    break;
                case 2:
                    tblLeagueFormula.Text = "Chia bảng đấu";
                    break;
            }
            tblLeagueTeamsCount.Text = league.countTeam.ToString()+" Đội";
            tblLeagueTime.Text ="Thời gian: " + league.dateTime.ToString("dd/MM/yyyy");
            if (!canDelete)
                btnRemoveLeague.IsEnabled = false;
        }

    }
}
