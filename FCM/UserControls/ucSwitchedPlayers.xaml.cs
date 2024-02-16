using FCM.DAO;
using FCM.DTO;
using FCM.View;
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
    /// Interaction logic for ucSwitchedPlayers.xaml
    /// </summary>
    public partial class ucSwitchedPlayers : UserControl
    {
        public ResultRecordingWindow resultWD;
        public SwitchedPlayer switchedPlayer;
        public ucSwitchedPlayers()
        {
            InitializeComponent();
        }
        public ucSwitchedPlayers(ResultRecordingWindow resultWD)
        {
            InitializeComponent();

            this.resultWD = resultWD;
        }
        public ucSwitchedPlayers(SwitchedPlayer switchedPlayer, ResultRecordingWindow resultWD)
        {
            InitializeComponent();

            this.resultWD = resultWD;
            this.switchedPlayer = switchedPlayer;

            this.tblNameIn.Text = getNumberAndNameOfPlayer(switchedPlayer.idPlayerIn);
            this.tblNameOut.Text = getNumberAndNameOfPlayer(switchedPlayer.idPlayerOut);
            this.tblMinute.Text = switchedPlayer.time;
        }
        string getNumberAndNameOfPlayer(int idPlayer)
        {
            string numberUniform = PlayerDAO.Instance.GetPlayerById(idPlayer).uniformNumber.ToString();
            string name = PlayerDAO.Instance.GetPlayerById(idPlayer).namePlayer.ToString();
            return numberUniform + ". " + name;
        }
    }
}
