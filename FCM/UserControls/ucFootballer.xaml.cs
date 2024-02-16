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
    /// Interaction logic for ucFootballer.xaml
    /// </summary>
    public partial class ucFootballer : UserControl
    {
        public ucFootballer()
        {
            InitializeComponent();
        }
        public ucFootballer(Lineups lineups)
        {
            InitializeComponent();
            this.tblNumber.Text = PlayerDAO.Instance.GetPlayerById(lineups.idPlayer).uniformNumber.ToString();
            this.tblName.Text = PlayerDAO.Instance.GetPlayerById(lineups.idPlayer).namePlayer.ToString();
            this.tblPosition.Text = PlayerDAO.Instance.GetPlayerById(lineups.idPlayer).position.ToString();

            string card = lineups.card;

            SolidColorBrush brush = new SolidColorBrush(Colors.Transparent);

            if (card == "Thẻ vàng") 
                brush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            if (card == "Thẻ đỏ")
                brush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            this.icCard.Foreground = brush;

        }
    }
}
