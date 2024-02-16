using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FCM.DTO
{
    public class League
    {
        public int id { get; set; }
        public string nameSpender { get; set; }
        public string nameLeague { get; set; }
        public int status { get; set; }
        public DateTime dateTime { get; set; }
        public Byte[] logo { get; set; }
        public int countTeam { get; set; } = 0;
        public int typeLeague { get; set; }
        public int countBoard { get; set; }

        public League(DataRow row)
        {
            this.id = (int)row["id"];
            this.nameSpender = (string)row["honors"];
            this.nameLeague = (string)row["displayname"];
            this.status = (int)row["status"];
            this.dateTime = (DateTime)row["time"];
            this.logo = (byte[])row["logo"];
            this.countTeam = (int)row["countTeam"];
            this.typeLeague = (int)row["TypeLeague"];
            this.countBoard = (int)row["CountBoard"];
        }
        public League(string nameSpender, string nameLeague, int status, DateTime dateTime, Byte[] logo, int countTeam, int typeLeague, int countBoard )
        {
            this.nameLeague = nameLeague;
            this.nameSpender = nameSpender;
            this.status = status;
            this.dateTime = dateTime;
            this.logo = logo;
            this.countTeam = countTeam;
            this.typeLeague = typeLeague;
            this.countBoard = countBoard;
        }
    }
}
