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
    /// Interaction logic for ucGoal.xaml
    /// </summary>
    public partial class ucGoal : UserControl
    {
        public ucGoal()
        {
            InitializeComponent();
        }

        public ResultRecordingWindow resultWD;
        public Goal goal;
        public ucGoal(Goal goal, ResultRecordingWindow resultWD)
        {
            InitializeComponent();

            this.tblNumber.Text = PlayerDAO.Instance.GetPlayerById(goal.idPlayerGoals).uniformNumber.ToString();
            this.tblFootballer.Text = PlayerDAO.Instance.GetPlayerById(goal.idPlayerGoals).namePlayer.ToString();
            this.tblTime.Text = goal.time;
            this.tblTypeOfGoal.Text = TypeOfGoalDAO.Instance.GetTypeOfGoalNameByID(goal.idTypeOfGoals);

            this.goal = goal;
            this.resultWD = resultWD;
        }
    }
}
