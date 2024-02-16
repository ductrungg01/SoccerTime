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
    /// Interaction logic for AddGoalWindow.xaml
    /// </summary>
    public partial class AddGoalWindow : Window
    {
        public bool isEdit = false;
        public Goal goal;

        public ResultRecordingWindow resultWD;
        public bool isTeam1;
        public Team team;
        public List<Player> players = new List<Player>();
        public List<TypeOfGoal> typeOfGoals = new List<TypeOfGoal>();

        void Init()
        {
            this.cbScored.Items.Clear();
            this.cbAsssist.Items.Clear();
            this.cbTypeOfGoal.Items.Clear();

            this.imgLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(this.team.logo));

            this.tblName.Text = this.team.nameTeam;

            this.players = PlayerDAO.Instance.GetListPlayer(this.team.id);

            foreach (Player p in this.players)
            {
                this.cbScored.Items.Add(getNumberAndNameOfPlayer(p));
                this.cbAsssist.Items.Add(getNumberAndNameOfPlayer(p));
            }

            this.typeOfGoals = TypeOfGoalDAO.Instance.GetListTypeOfGoal(team.idTournamnt);

            foreach (TypeOfGoal t in this.typeOfGoals)
            {
                this.cbTypeOfGoal.Items.Add(t.displayName);
            }
        }
        public AddGoalWindow(bool isTeam1, ResultRecordingWindow resultWD)
        {
            InitializeComponent();

            this.isTeam1 = isTeam1;
            this.resultWD = resultWD;

            if (this.isTeam1)
            {
                this.team = TeamDAO.Instance.GetTeamById(this.resultWD.match.idTeam01);
            }
            else
            {
                this.team = TeamDAO.Instance.GetTeamById(this.resultWD.match.idTeam02);
            }

            Init();
        }

        public AddGoalWindow(Goal goal, ResultRecordingWindow resultWD)
        {
            InitializeComponent();

            this.tblTitle.Text = "SỬA BÀN THẮNG";
            this.Title = "Sửa bàn thắng";
            this.tblAddGoal.Text = "Lưu";

            this.team = TeamDAO.Instance.GetTeamById(goal.idTeams);

            Init();

            this.isEdit = true;
            this.goal = goal;
            this.resultWD = resultWD;

            Team team1 = TeamDAO.Instance.GetTeamById(resultWD.match.idTeam01);
            Team team2 = TeamDAO.Instance.GetTeamById(resultWD.match.idTeam02);

            this.isTeam1 = team1.id == goal.idTeams ? true : false;

            for (int i = 0; i < this.players.Count; i++)
            {
                if (goal.idPlayerGoals == this.players[i].id)
                {
                    this.cbScored.SelectedIndex = i;
                    if (goal.idPlayerAssist == goal.idPlayerGoals)
                    {
                        break;
                    }
                }
                if (goal.idPlayerAssist == this.players[i].id)
                {
                    this.cbAsssist.SelectedIndex = i;
                }
            }
            for (int i = 0; i < this.typeOfGoals.Count; i++)
            {
                if (goal.idTypeOfGoals == this.typeOfGoals[i].id)
                {
                    this.cbTypeOfGoal.SelectedIndex = i;
                }
            }
            this.tbTime.Text = goal.time;
            
        }
        string getNumberAndNameOfPlayer(Player player)
        {
            string numberUniform = player.uniformNumber.ToString();
            string name = player.namePlayer.ToString();
            return numberUniform + ". " + name;
        }

    }
}
