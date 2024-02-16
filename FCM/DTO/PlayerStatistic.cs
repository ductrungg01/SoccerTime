using System.Data;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace FCM.DTO
{
    class PlayerStatistic
    {
        public int index { get; set; }
        public string namePlayer { get; set; }
        public string nameTeam { get; set; }
        public int number { get; set; }
        public int goal { get; set; }
        public int assist { get; set; }
        public int rc { get; set; }
        public int yc { get; set; }
        public BitmapImage logo { get; set; }

        public PlayerStatistic(string playerName, string teamName, BitmapImage lg)
        {
            this.index = 0;
            this.nameTeam = teamName;
            this.number = 0;
            this.goal = 0;
            this.assist = 0;
            this.yc = 0;
            this.rc = 0;
            this.namePlayer = playerName;
            this.logo = lg;
        }
    }
}
