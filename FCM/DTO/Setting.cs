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
    public class Setting
    {
        public int idTournament { get; set; }
        public int numberOfTeam { get; set; }
        public int minPlayerOfTeam { get; set; }
        public int maxPlayerOfTeam { get; set; }
        public int minAge { get; set; }
        public int maxAge { get; set; }
        public int maxForeignPlayers { get; set; }
        public int scoreWin { get; set; }
        public int scoreDraw { get; set; }
        public int scoreLose { get; set; }
        public int NumberOfTeamIn { get; set; }

        public Setting(DataRow row)
        {
            this.idTournament = (int)row["idTournaments"];
            this.numberOfTeam = (int)row["numberOfTeams"];
            this.minPlayerOfTeam = (int)row["MinPlayerOfTeams"];
            this.maxPlayerOfTeam = (int)row["MaxPlayerOfTeams"];
            this.minAge = (int)row["minAge"];
            this.maxAge = (int)row["maxAge"];
            this.maxForeignPlayers = (int)row["MaxNumberForeignPlayers"];
            this.scoreWin = (int)row["score_Win"];
            this.scoreDraw = (int)row["score_Draw"];
            this.scoreLose = (int)row["score_Lose"];
            this.NumberOfTeamIn = (int)row["NumberOfTeamsIn"];
        }
        public Setting(int idTournament, int numberOfTeam, int minPlayerOfTeam, int maxPlayerOfTeam, int minAge, int maxForeignPlayers, int scoreWin, int scoreDraw, int scoreLose, int numberOfTeamIn)
        {
            this.idTournament = idTournament;
            this.numberOfTeam = numberOfTeam;
            this.minPlayerOfTeam = minPlayerOfTeam;
            this.maxPlayerOfTeam = maxPlayerOfTeam;
            this.minAge = minAge;
            this.maxForeignPlayers = maxForeignPlayers;
            this.scoreWin = scoreWin;
            this.scoreDraw = scoreDraw;
            this.scoreLose = scoreLose;
            this.NumberOfTeamIn = numberOfTeamIn;

        }

        public Setting()
        {
        }
    }
}
