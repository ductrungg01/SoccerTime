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
    /// Interaction logic for AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : Window
    {
        public bool isEdit = false; // true: đang thực hiện chức năng SỬA thẻ phạt, false: đang thực hiện chức năng THÊM thẻ phạt
        public Card oldCard;

        public ResultRecordingWindow resultWD;
        public bool isTeam1;
        public List<Player> players;
        public Team team;
      

        void Init()
        {
            this.tblName.Text = team.nameTeam;
            this.imgLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team.logo));

            players = PlayerDAO.Instance.GetListPlayer(team.id);

            this.cbPlayer.Items.Clear();

            for (int i = 0; i < players.Count; i++)
            {
                if (this.resultWD.GetTimePlayerReceiveRedCard(players[i]) != int.MaxValue)
                {
                    continue;
                }
                this.cbPlayer.Items.Add(players[i].namePlayer.ToString());
            }
        }
        public AddCardWindow(ResultRecordingWindow resultWD, bool isTeam1, Team team)
        {
            InitializeComponent();

            this.resultWD = resultWD;
            this.isTeam1 = isTeam1;
            this.team = team;

            Init();
        }
        public AddCardWindow(Card card, ResultRecordingWindow resultWD)
        {
            InitializeComponent();

            this.resultWD = resultWD;

            this.tblTitle.Text = "SỬA THẺ PHẠT";
            this.Title = "Sửa thẻ phạt";
            this.tblAddCard.Text = "Lưu";
            this.team = TeamDAO.Instance.GetTeamById(card.idTeams);
            this.isEdit = true;

            this.oldCard = card;

            Init();

            for (int i = 0; i < this.players.Count; i++)
            {
                if (this.players[i].id == card.idPlayer)
                {
                    this.cbPlayer.SelectedIndex = i;
                    break;
                }
            }

            if (card.typeOfCard == "Thẻ vàng")
            {
                this.rbtnYellowCard.IsChecked = true;
            } 
            else
            {
                this.rbtnRedCard.IsChecked = true;
            }

            this.tbTime.Text = card.time;
                
        }
        public Player GetPlayerBySelectedIndex(int selectedIndex)
        {
            int j = 0;

            Player player = new Player();

            for (int i = 0; i < players.Count; i++)
            {
                if (this.resultWD.GetTimePlayerReceiveRedCard(players[i]) != int.MaxValue)
                {
                    continue;
                }

                if (j == selectedIndex)
                {
                    player = players[i];
                    break;
                }

                j++;
            }

            return player;
        }
    }
}
