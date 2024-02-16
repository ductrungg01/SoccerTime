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
    /// Interaction logic for SwitchPlayersWindow.xaml
    /// </summary>
    public partial class SwitchPlayersWindow : Window
    {
        public ResultRecordingWindow resultWD;
        bool isEdit = false;    // true: màn hình chức năng SỬA thông tin thay người, false: màn hình chức năng THÊM thông tin thay người
        public SwitchPlayersWindow(ResultRecordingWindow resultWD, bool isEdit = false)
        {
            InitializeComponent();

            this.resultWD = resultWD;
            this.isEdit = isEdit;
            if (this.isEdit == true)
            {
                this.Title = "Sửa bàn thắng";
                this.btnAccept.Content = "Lưu";
            }


            this.cbPlayer1.Items.Clear();
            this.cbPlayer2.Items.Clear();

            if (this.resultWD.isTeam1)
            {
                for (int i = 0; i < this.resultWD.listLineups_Offical_Team1.Count; i++)
                {
                    this.cbPlayer1.Items.Add(getNumberAndNameOfPlayer(this.resultWD.listLineups_Offical_Team1[i]));
                }    
                for (int i = 0; i < this.resultWD.listLineups_Prep_Team1.Count; i++)
                {
                    Player player = PlayerDAO.Instance.GetPlayerById(this.resultWD.listLineups_Prep_Team1[i].idPlayer);

                    if (this.resultWD.GetTimePlayerReceiveRedCard(player) != int.MaxValue)
                    {
                        continue;
                    }    

                    this.cbPlayer2.Items.Add(getNumberAndNameOfPlayer(this.resultWD.listLineups_Prep_Team1[i]));
                }
            }    
            else
            {
                for (int i = 0; i < this.resultWD.listLineups_Offical_Team2.Count; i++)
                {
                    this.cbPlayer1.Items.Add(getNumberAndNameOfPlayer(this.resultWD.listLineups_Offical_Team2[i]));
                }
                for (int i = 0; i < this.resultWD.listLineups_Prep_Team2.Count; i++)
                {
                    Player player = PlayerDAO.Instance.GetPlayerById(this.resultWD.listLineups_Prep_Team2[i].idPlayer);

                    if (this.resultWD.GetTimePlayerReceiveRedCard(player) != int.MaxValue)
                    {
                        continue;
                    }

                    this.cbPlayer2.Items.Add(getNumberAndNameOfPlayer(this.resultWD.listLineups_Prep_Team2[i]));
                }
            }
        }
        string getNumberAndNameOfPlayer(Lineups lineup)
        {
            string numberUniform = PlayerDAO.Instance.GetPlayerById(lineup.idPlayer).uniformNumber.ToString();
            string name = PlayerDAO.Instance.GetPlayerById(lineup.idPlayer).namePlayer.ToString();
            return numberUniform + ". " + name;
        }
    }
}
