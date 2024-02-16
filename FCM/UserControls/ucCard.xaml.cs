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
    /// Interaction logic for ucCard.xaml
    /// </summary>
    public partial class ucCard : UserControl
    {
        public ucCard()
        {
            InitializeComponent();
        }

        public ResultRecordingWindow resultWD;
        public Card card;
        public ucCard(ResultRecordingWindow resultWD, Card card)
        {
            InitializeComponent();

            this.resultWD = resultWD;
            this.card = card;

            this.tblNumber.Text = PlayerDAO.Instance.GetPlayerById(card.idPlayer).uniformNumber.ToString();
            this.tblFootballer.Text = PlayerDAO.Instance.GetPlayerById(card.idPlayer).namePlayer;
            this.tblTime.Text = card.time;
            //this.tblTypeOfCard.Text = card.typeOfCard;
            if (card.typeOfCard == "Thẻ vàng")
            {
                this.icTypeOfCard.Foreground = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                this.icTypeOfCard.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
