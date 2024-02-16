using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class SwitchedPlayer
    {
        //       IdMatchs int,
        //IdPlayerIn int,
        //IdPlayerOut int,
        //IdTeams int,
        //Time nvarchar(100),

        public int idMatch, idPlayerIn, idPlayerOut, idTeam;
        public string time;

        public SwitchedPlayer(int idMatch, int idPlayerIn, int idPlayerOut, int idTeam, string time)
        {
            this.idMatch = idMatch;
            this.idPlayerIn = idPlayerIn;
            this.idPlayerOut = idPlayerOut;
            this.idTeam = idTeam;
            this.time = time;
        }
        public SwitchedPlayer(DataRow row)
        {
            this.idMatch = (int)row["IdMatchs"];
            this.idPlayerIn = (int)row["IdPlayerIn"];
            this.idPlayerOut = (int)row["IdPlayerOut"];
            this.idTeam = (int)row["IdTeams"];

            DateTime time = (DateTime)row["Time"];

            this.time = time.Minute.ToString();
        }


    }
}
