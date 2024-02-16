using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;

namespace FCM.DAO
{
    class MatchDAO
    {
        private static MatchDAO instance;

        public static MatchDAO Instance
        {
            get { if (instance == null) instance = new MatchDAO(); return instance; }
            set => instance = value;
        }

        //public void AddMatch(Match match)
        //{
        //    string query = "Insert into Matchs (IdTournaments,IdTeam01,IdTeam02,Date,Time,Round,Stadium,allowDraw ) " +
        //                 "Values (  " +
        //                 "N'" + match.idTournaments + "' ," +
        //                 "N'" + match.idTeam01 + "' ," +
        //                 "N'" + match.idTeam02 + "' ," +
        //                 "N'" + match.date.ToString("yyyy-MM-dd HH:mm:ss") + "' ," +
        //                 "N'" + match.time.ToString("yyyy-MM-dd HH:mm:ss") + "' ," +
        //                 "N'" + match.round + "' ," +
        //                 "N'" + match.statium + "' ," +
        //                 "N'" + match.allowDraw + "'" +
        //                 ")";
        //    DataProvider.Instance.ExecuteQuery(query);
        //}
        public void AddMatch(Match match)
        {
            string query = "Insert into Matchs (IdTournaments,IdTeam01,IdTeam02,Date,Time,Round,Stadium,allowDraw,nameBoard ,score1,score2) " +
                         "Values (  " +
                         "N'" + match.idTournaments + "' ," +
                         "N'" + match.idTeam01 + "' ," +
                         "N'" + match.idTeam02 + "' ," +
                         "N'" + "2000-11-11" + "' ," +
                         "N'" + "00:00:00" + "' ," +
                         "N'" + match.round + "' ," +
                         "N'" + match.statium + "' ," +
                         "N'" + match.allowDraw + "'," +
                         "N'" + match.nameBoard + "'," +
                         " -1 ," +
                         " -1 " +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateMatch(Match match)
        {
            string query = "Update Matchs " +
                            "Set Date = '" + match.date.ToString("yyyy-MM-dd HH:mm:ss") + "' , " +
                            " Time = N'" + match.time.ToString("yyyy-MM-dd HH:mm:ss") + "' , " +
                            " Stadium = N'" + match.statium + "' ," +
                            " PenaltyTeam1 = " + match.PenaltyTeam1 + " , " +
                            " PenaltyTeam2 = " + match.PenaltyTeam2 + ", " +
                            " isStarted = '" + match.isStarted + "' , " +
                            " Score1 = " + match.Score1 + " , " +
                            " Score2 = " + match.Score2 + " " +
                            " Where ID = " + match.id;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public List<Match> GetListMatch(int idTournament)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public List<Match> GetListMatchByRound(int idTournament, int round)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament + " AND " +
                            "Round = " + round;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public List<Match> GetListMatchByBoard(int idTournament, string board)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament+
                             " AND " + "nameBoard = N'" + board + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public List<Match> GetListMatchByBoardAndRound(int idTournament, int round, string board)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament +
                            " AND " + "Round = N'" + round + "'" +
                            " AND " + "nameBoard = N'" + board + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public Match getMatchByID(int idMatch)
        {
            string query = "Select* " +
                         "From Matchs " +
                         "Where id= " + idMatch;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Match match = new Match(tb.Rows[0]);
            return match;
        }
        public List<Match> GetListMatchStartedByIDTeamWithOrder(int idTournament, int idTeam01)
        {
            List<Match> matches = new List<Match>();

            string query = "Select * From (" +
                            "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament +
                            " and isStarted = 1) lm" +
                            " Where lm.IdTeam01 = " + idTeam01 +
                            " or lm.IdTeam02 = " + idTeam01 +
                            " Order by lm.Date, lm.Time";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public List<Match> GetListMatchStartedByID2Team(int idTournament, int idTeam01, int idTeam02)
        {
            List<Match> matches = new List<Match>();

            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament +
                            " and IdTeam01 = " + idTeam01 +
                            " and IdTeam02 = " + idTeam02 +
                            " Order by Date, Time";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
        public bool IsExist(Match match)
        {
            string query = "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + match.idTournaments +
                            " and IdTeam01 = " + match.idTeam01 +
                            " and IdTeam02 = " + match.idTeam02 +
                            " and allowDraw = '" + match.allowDraw + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            if (tb.Rows.Count > 0)
                return true;
            return false;
        }
        public int GetNewestMatchid()
        {
            string query = "SELECT MAX(Id) as id FROM Matchs";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            return (int)db.Rows[0]["id"];
        }
        public void DeleteMatch(int id)
        {
            LineupsDAO.Instance.DeleteLineupsByMatchID(id);
            GoalDAO.Instance.DeleteGoalByMatchID(id);
            CardDAO.Instance.DeleteCardByMatchID(id);
            SwitchedPlayerDAO.Instance.DeleteSwitchedPlayersByMatchID(id);
            string query = "Delete Matchs " +
                "  where id = " + id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public DateTime MaxTimeBoard(Match match)
        {
            string query = " Select max(date) as date " +
                            " From Matchs " +
                            " Where idTournaments = " + match.idTournaments + " " +
                            " And allowDraw ='true' ";

            DateTime dateTime = DateTime.Now;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            dateTime = (DateTime)tb.Rows[0]["date"];
            return dateTime;
        }
        public DateTime MaxTimeNockOut(Match match)
        {
            string query = " Select top 1 date, time" +
                            " From Matchs " +
                            " Where idTournaments = " + match.idTournaments + " " +
                            " And round < " + match.round + "  " +
                            " Order by date desc , time desc";

            DateTime dateTime = DateTime.Now;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            if (tb.Rows.Count ==0)
            {
                return LeagueDAO.Instance.GetLeagueById(match.idTournaments).dateTime.AddHours(-2);
            }
            dateTime = (DateTime)tb.Rows[0]["date"];
            dateTime =  dateTime.AddHours(((DateTime)tb.Rows[0]["time"]).Hour);
            dateTime = dateTime.AddMinutes(((DateTime)tb.Rows[0]["time"]).Minute);
            return dateTime;
        }

        public bool IsExistTimeMatch(Match match)
        {
            string query = " Select *  " +
                            " From Matchs " +
                            " Where idTournaments = " + match.idTournaments + "" +
                            " and id <> + " + match.id +
                            " and (idteam01 = " + match.idTeam01 +
                             " or idteam01 = " + match.idTeam02 +
                              " or idteam02 = " + match.idTeam01 +
                            " or idteam02 = " + match.idTeam02 + ")";

            int hour = 2;
            int minute = 0;
            if (match.allowDraw == true)
            {
                hour = 1;
                minute = 30;
            }
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dataRow in tb.Rows)
            {
                DateTime date = (DateTime)dataRow["date"];
                if (date.Date.ToString("d") == match.date.Date.ToString("d"))
                {
                    DateTime time = (DateTime)dataRow["time"];

                    time.AddMinutes(minute);

                    DateTime datetime2 = time.AddHours(hour);
                    datetime2 = datetime2.AddMinutes(minute);

                    if (match.time.TimeOfDay >= time.TimeOfDay && match.time.TimeOfDay <= datetime2.TimeOfDay)
                    {
                        return true;
                    }



                    DateTime datetime3 = time.AddHours(-hour);
                    datetime3 = datetime3.AddMinutes(-minute);

                    if (match.time.TimeOfDay <= time.TimeOfDay && match.time.TimeOfDay >= datetime3.TimeOfDay)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public List<Match> GetListMatchDetail(int idLeague)
        {
            List<Match> matches = new List<Match>();
            string query = " Select top(6) * " +
                           " From Matchs " +
                           " where Date<> '2000-11-11' " + " " +
                           " And score1 = -1 " +
                           " And score2 = -1 " +
                           " And idTournaments = " + idLeague +
                           " Order by date,time ";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dataRow in tb.Rows)
            {
                Match match = new Match(dataRow);
                matches.Add(match);
            }
            return matches;
        }
        public int GetCountMatchWait(int idleague)
        {
            string query = " Select count(id) as count " + "" +
                           " From matchs  " +
                           " Where idTournaments =   " + idleague + " " +
                           " and score1 =-1 " +
                           " and allowdraw = 1";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return (int)tb.Rows[0]["count"];
        }
        public bool HaveMatch(int idLeague)
        {
            List<Match>  match  = GetListMatch(idLeague);
            if (match.Count > 0)
                return true;
            return false;
        }
        public void DeleteMatchInLeague(int idLeague)
        {
            List<Match> matches = MatchDAO.Instance.GetListMatch(idLeague);
            foreach (Match match in matches)
            {
                DeleteMatch(match.id);
            }    
        }

        public List<string> GetListRoundInLeague(int idTournament)
        {
            List<string> rounds = new List<string>();

            string query = "Select round " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament +
                            " Group by round";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                rounds.Add(row["round"].ToString());
            }
            return rounds;
        }
        public List<Match> GetListMatchStartedByIDTeamAndRoundWithOrder(int idTournament, int idTeam01, int round)
        {
            List<Match> matches = new List<Match>();

            string query = "Select * From (" +
                            "Select* " +
                            "From Matchs " +
                            "Where idTournaments = " + idTournament +
                            " and isStarted = 1 ";
            if (round != 0)
                query += " and round =" + round;
            query += ") lm" +
                     " Where lm.IdTeam01 = " + idTeam01 +
                     " or lm.IdTeam02 = " + idTeam01 +
                     " Order by lm.Date, lm.Time";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Match match = new Match(row);
                matches.Add(match);
            }
            return matches;
        }
    }
}
