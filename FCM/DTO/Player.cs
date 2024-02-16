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
    public class Player
    {
        public int id { get; set; }
        public int idTeam { get; set; }
        public string namePlayer { get; set; }
        public int uniformNumber { get; set; }
        public DateTime birthDay { get; set; }
        public string position { get; set; }
        public string nationality { get; set; }
        public string note { get; set; }
        public byte[] image { get; set; }

        public Player(DataRow row)
        {
            this.id = (int)row["id"];
            this.idTeam = (int)row["idTeams"];
            this.namePlayer = (string)row["displayname"];
            this.uniformNumber = (int)row["uniformnumber"];
            this.birthDay = (DateTime)row["birthDay"];
            this.position = (string)row["position"];
            this.nationality = (string)row["nationality"];
            this.note = (string)row["note"];
            this.image = (byte[])row["imagee"];
        }
        public Player(int idTeam, string namePlayer, int uniformnumber, DateTime birthDay, string position, string nationality, string note, byte[] image)
        {
            this.idTeam = idTeam;
            this.namePlayer = namePlayer;
            this.uniformNumber = uniformnumber;
            this.birthDay = birthDay;
            this.position = position;
            this.nationality = nationality;
            this.note = note;
            this.image = image;
        }
        public Player() { }
    }
}
