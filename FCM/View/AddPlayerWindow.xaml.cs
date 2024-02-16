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
using FCM.DAO;
using FCM.DTO;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary> 
    public partial class AddPlayerWindow : Window
    {
        public Team team { get; set; }
        public Player player { get; set; }
        public Setting setting { get; set; }
        public bool CanGetOutNation { get; set; }
        public AddPlayerWindow()
        {
            InitializeComponent();
        }
        public AddPlayerWindow(Team team,Setting setting,bool outNation)
        {
            InitializeComponent();
            this.team = team;
            this.setting = setting;
            this.CanGetOutNation = outNation;
        }
        public AddPlayerWindow(Team team,Player player, Setting setting, bool outNation)
        {
            InitializeComponent();
            this.team = team;
            this.player = player;
            this.setting = setting;
            this.CanGetOutNation = outNation;
            tbName.Text = player.namePlayer;
            tbNationality.Text = player.nationality;
            tbNumber.Text = player.uniformNumber.ToString();
            tbNote.Text = player.note;
            tbPosition.Text = player.position;
            imgPlayerImage.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(player.image));
            dpDoB.SelectedDate = player.birthDay;
        }
    }
}
