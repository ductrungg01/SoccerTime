using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class Goal
    {
        public int idMatchs, idPlayerGoals, idPlayerAssist, idTeams, idTypeOfGoals;
        public string time;

        public Goal(int idMatchs,int idPlayerGoals, int idPlayerAssist, int idTeams, int idTypeOfGoals, string time)
        {
            this.idMatchs = idMatchs;
            this.idPlayerGoals = idPlayerGoals;
            this.idPlayerAssist = idPlayerAssist;
            this.idTeams = idTeams;
            this.idTypeOfGoals = idTypeOfGoals;
            this.time = time;
        }
        public Goal(DataRow row)
        {
            this.idMatchs = (int)row["IdMatchs"];
            this.idPlayerGoals = (int)row["IdPlayerGoals"];
            this.idPlayerAssist = (int)row["IdPlayerAssist"];
            this.idTeams = (int)row["IdTeams"];
            this.idTypeOfGoals = (int)row["IdTypeOfGoals"];

            DateTime time = (DateTime)row["Time"];

            this.time = (time.Hour * 60 + time.Minute).ToString();
        }
    }
}
