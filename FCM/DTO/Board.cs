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
    public class Board
    {
        public int id { get; set; }
        public int idTournament { get; set; }
        public string nameBoard { get; set; }
        public int countTeam { get; set; }
        public Board(DataRow row)
        {
            this.id = (int)row["id"];
            this.idTournament = (int)row["idTournament"];
            this.nameBoard = (string)row["nameBoard"];
            this.countTeam = (int)row["countTeam"];
        }
        public Board(int idTournament, string nameBoard, int countTeam)
        {
            this.idTournament = idTournament;
            this.nameBoard = nameBoard;
            this.countTeam = countTeam;
        }
    }
}
