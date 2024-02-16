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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FCM.UserControls
{
    /// <summary>
    /// Interaction logic for ucMatchDetailNoEdit.xaml
    /// </summary>
    public partial class ucMatchDetailNoEdit : UserControl
    {
        public ucMatchDetailNoEdit()
        {
            InitializeComponent();
        }
        public ucMatchDetailNoEdit(int index,Match match)
        {
            InitializeComponent();
            this.id.Text = index.ToString();
            this.Team1.Text = TeamDAO.Instance.GetTeamById(match.idTeam01).nameTeam;
            this.Team2.Text = TeamDAO.Instance.GetTeamById(match.idTeam02).nameTeam;
            this.San.Text = match.statium;
            this.time.Text = match.time.ToString("HH:mm");
            this.Date.Text = match.date.ToString("dd/MM/yyyy");
            if (match.round>0)
            {
                this.Round.Text = match.round.ToString();
            } else
            {
                switch (match.round)
                {
                    case -1:
                        Round.Text = "Chung kết";
                        break;
                    case -2:
                        Round.Text = "Bán kết";
                        break;
                    case -3:
                        Round.Text = "Tứ kết";
                        break;
                    case -4:
                        Round.Text = "1/8";
                        break;
                }
            }

        }
    }
}
