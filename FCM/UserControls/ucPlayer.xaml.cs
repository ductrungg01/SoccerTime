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
    /// Interaction logic for ucPlayer.xaml
    /// </summary>
    public partial class ucPlayer : UserControl
    {
        public Player player { get; set; }
        public int roleLevel { get; set; }
        public MainViewModel mainViewModel { get; set; }
        public MainWindow mainWindow { get; set; }
        public ucPlayer()
        {
            InitializeComponent();
        }
        public ucPlayer(Player player, int roleLevel,int index, MainWindow mainWindow, MainViewModel mainViewModel, int status)
        {
            InitializeComponent();
            this.player = player;
            this.roleLevel = roleLevel;
            tblName.Text = player.namePlayer;
            tblNationality.Text = player.nationality;
            tblIndex.Text = index.ToString();
            tblNumber.Text = player.uniformNumber.ToString();
            tblDoB.Text = player.birthDay.ToString("dd/MM/yyyy");
            tblPosition.Text = player.position;
            this.mainWindow = mainWindow;
            this.mainViewModel = mainViewModel;
            if ((roleLevel !=1 && roleLevel != 2) || MatchDAO.Instance.HaveMatch(mainWindow.league.id))
                btnDeletePlayer.IsEnabled = false;
        }
    }
}
