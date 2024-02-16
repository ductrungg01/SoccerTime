using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class Card
    {
        public int idMatchs, idPlayer, idTeams;
        public string typeOfCard, time;

        public Card(int idMatchs, int idPlayer, int idTeams,string typeOfCard, string time)
        {
            this.idMatchs = idMatchs;
            this.idPlayer = idPlayer;
            this.idTeams = idTeams;
            this.time = time;
            this.typeOfCard = typeOfCard;
        }
        public Card(DataRow row)
        {
            this.idMatchs = (int)row["IdMatchs"];
            this.idPlayer = (int)row["IdPlayers"];
            this.idTeams = (int)row["IdTeams"];
            this.typeOfCard = (string)row["TypeOfCard"];

            DateTime time = (DateTime)row["Time"];

            this.time = (time.Hour * 60 + time.Minute).ToString();
        }
    }
}
