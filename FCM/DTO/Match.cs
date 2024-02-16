using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class Match
    {
        public int id { get; set; }
        public int idTournaments { get; set; }
        public int idTeam01 { get; set; }
        public int idTeam02 { get; set; }
        public int round { get; set; }

        public int Score1, Score2;

        public int PenaltyTeam1, PenaltyTeam2;

        public bool isStarted;

        public bool allowDraw;

        public string nameBoard;

        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public string statium { get; set; }
        public Match(DataRow row)
        {
            this.id = (int)row["id"];
            this.idTournaments = (int)row["IdTournaments"];
            this.idTeam01 = (int)row["IdTeam01"];
            this.idTeam02 = (int)row["IdTeam02"];
            this.round = (int)row["Round"];
            this.date = (DateTime)row["Date"];
            this.time = (DateTime)row["Time"];
            this.statium = (string)row["Stadium"];
            this.PenaltyTeam1 = (int)row["PenaltyTeam1"];
            this.PenaltyTeam2 = (int)row["PenaltyTeam2"];
            this.isStarted = (bool)row["isStarted"];
            this.Score1 = (int)row["Score1"];
            this.Score2 = (int)row["Score2"];
            this.allowDraw = (bool)row["allowDraw"];
            this.nameBoard = (string)row["nameBoard"];
        }
        //public Match(int id, int idTournaments, int idTeam01, int idTeam02, int round,DateTime date, DateTime time, string stadium)
        //{
        //    this.id = id;
        //    this.idTournaments = idTournaments;
        //    this.idTeam01 = idTeam01;
        //    this.idTeam02 = idTeam02;
        //    this.round = round;
        //    this.date = date;
        //    this.time = time;
        //    this.statium = stadium;
        //}
        public Match(int id, int idTournaments, int idTeam01, int idTeam02, int round, DateTime date, DateTime time, string stadium, int penaltyTeam1, int penaltyTeam2, bool isStarted, int Score1, int Score2, bool allowDraw,string nameBoard)
        {
            this.id = id;
            this.idTournaments = idTournaments;
            this.idTeam01 = idTeam01;
            this.idTeam02 = idTeam02;
            this.round = round;
            this.date = date;
            this.time = time;
            this.statium = stadium;
            this.PenaltyTeam1 = penaltyTeam1;
            this.PenaltyTeam2 = penaltyTeam2;
            this.isStarted = isStarted;
            this.Score1 = Score1;
            this.Score2 = Score2;
            this.allowDraw = allowDraw;
            this.nameBoard = nameBoard;
        }
        public Match(int idTournaments, int idTeam01, int idTeam02, int round, string stadium,string nameBoard)
        {
            this.idTournaments = idTournaments;
            this.idTeam01 = idTeam01;
            this.idTeam02 = idTeam02;
            this.round = round;
            this.statium = stadium;
            this.nameBoard = nameBoard;
        }
        public Match(int idTournaments, int idTeam01, int idTeam02, int round, string stadium, bool allowDraw, string nameBoard)
        {
            this.idTournaments = idTournaments;
            this.idTeam01 = idTeam01;
            this.idTeam02 = idTeam02;
            this.round = round;
            this.statium = stadium;
            this.allowDraw = allowDraw;
            this.isStarted = false;
            this.date = DateTime.Today;
            this.time = DateTime.Today;
            this.nameBoard = nameBoard;
        }
        //public Match(int idTournaments, int idTeam01, int idTeam02, int round, string stadium, int penaltyTeam1, int penaltyTeam2)
        //{
        //    this.idTournaments = idTournaments;
        //    this.idTeam01 = idTeam01;
        //    this.idTeam02 = idTeam02;
        //    this.round = round;
        //    this.statium = stadium;
        //    this.PenaltyTeam1 = penaltyTeam1;
        //    this.PenaltyTeam2 = penaltyTeam2;
        //}

    }
}
