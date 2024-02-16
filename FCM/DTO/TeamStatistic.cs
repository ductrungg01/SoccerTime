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
    class TeamStatistic
    {
        public int index { get; set; }
        public string nameTeam { get; set; }
        public int m { get; set; }
        public int gf { get; set; }
        public int ga { get; set; }
        public int yc { get; set; }
        public int rc { get; set; }
        public int sumc { get; set; }
        public BitmapImage logo { get; set; }

        public TeamStatistic(string teamName, BitmapImage lg)
        {
            this.index = 0;
            this.nameTeam = teamName;
            this.m = 0;
            this.gf = 0;
            this.ga = 0;
            this.yc = 0;
            this.rc = 0;
            this.sumc = 0;
            this.logo = lg;
        }
    }
}
