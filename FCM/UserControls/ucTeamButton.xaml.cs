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
    /// Interaction logic for ucTeamButton.xaml
    /// </summary>
    public partial class ucTeamButton : UserControl
    {
        public Team team { get; set; }
        public MainWindow mainWindow { get; set; }
        public MainViewModel mainViewModel { get; set; }
        public ucTeamButton()
        {
            InitializeComponent();
        }
        public ucTeamButton(Team team, MainWindow mainWindow, MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.team = team;
            this.mainWindow = mainWindow;
            this.mainViewModel = mainViewModel;
            this.tblName.Text = team.nameTeam;
            this.imgLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team.logo));
        }
    }
}
