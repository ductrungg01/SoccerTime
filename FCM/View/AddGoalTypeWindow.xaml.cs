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
    /// Interaction logic for AddGoalTypeWindow.xaml
    /// </summary>
    public partial class AddGoalTypeWindow : Window
    {
        public bool isAdd = false;
        public int idTournament;
        public string oldName;
        public AddGoalTypeWindow()
        {
            InitializeComponent();
        }
        public AddGoalTypeWindow(MainWindow parameter, string nameGoalType)
        {
            InitializeComponent();
            this.idTournament = parameter.league.id;
            this.tbName.Text = nameGoalType;
            this.oldName = nameGoalType;
            if (nameGoalType == "")
                isAdd = true;
            this.tbName.SelectionStart = this.tbName.Text.Length;
            this.tbName.Focus();
        }


    }
}
