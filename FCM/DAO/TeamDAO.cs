using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;

namespace FCM.DAO
{
    class TeamDAO
    {
        private static TeamDAO instance;

        public static TeamDAO Instance
        {
            get { if (instance == null) instance = new TeamDAO(); return instance; }
            set => instance = value;
        }
        public Team GetTeamById(int id)
        {
            string query = "Select* " +
                         "From Teams " +
                         "Where id= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Team team = new Team(tb.Rows[0]);
            return team;
        }
        public int GetCountTeam(int idTournament, string nameBoard)
        {
            string query = "Select count(id) as count " +
                         "From Teams " +
                         "Where nameBoard= N'" + nameBoard + "' " +
                         " and idtournaments = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return (int)tb.Rows[0]["count"];

        }

        public int GetPlayerCountOfTeam(int id)
        {
            string query = "Select* " +
                         "From Players " +
                         "Where IdTeams= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return tb.Rows.Count;
        }
        public List<Team> GetListTeam(string nameBoard, int idTournament)
        {
            List<Team> teams = new List<Team>();

            string query = "Select* " +
                            "From Teams " +
                            "Where nameBoard = N'" + nameBoard + "' " +
                            " and idTournaments = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Team team = new Team(row);
                teams.Add(team);
            }
            return teams;
        }
        public List<Team> GetListTeamInLeague(int idTournament)
        {
            List<Team> teams = new List<Team>();

            string query = "Select* " +
                            "From Teams " +
                            "Where idTournaments =" + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Team team = new Team(row);
                teams.Add(team);
            }
            return teams;
        }
        public void DeleteTeam(int idTournament)
        {
            List<Team> teams = GetListTeamInLeague(idTournament);
            foreach (Team team in teams)
            {
                PlayerDAO.Instance.DeletePlayerInTeam(team.id);
            }
            string query = "Delete " +
                            "From Teams " +
                            "Where idTournaments = " + idTournament;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteTeam(Team team)
        {
             PlayerDAO.Instance.DeletePlayerInTeam(team.id);
            string query = "Delete " +
                            "From Teams " +
                            "Where idTournaments = " + team.idTournamnt + "" +
                            " and id = " + team.id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void CreateTeams(Team team)
        {
            string query = "Insert into Teams (idTournaments, nameBoard,DisplayName,Coach,Stadium,nation) " +
                         "Values (  " +
                         "" + team.idTournamnt + " ," +
                         "N'" + team.nameBoard + "' ," +
                         "N'" + team.nameTeam + "' ," +
                         "N'" + team.coach + "' ," +
                         "N'" + team.stadium + "' ," +
                         "N'" + team.nation + "' " +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Teams SET logo = @img WHERE ID = (SELECT MAX(Id) FROM Teams)";
            DataProvider.Instance.ExecuteQuery(query, new object[] { team.logo });
        }
        public void UpdateTeam(Team team)
        {
            string query = "Update Teams " +
                            "Set " +
                            " Displayname = " + "N'" + team.nameTeam + "' ," +
                            " NameBoard = N'" + team.nameBoard + "' ,"+
                            " coach = " + "N'" + team.coach + "' ," +
                            " stadium = " + "N'" + team.stadium + "' ," +
                            " nation = " + "N'" + team.nation + "' " +
                            " Where id = " + team.id;
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Teams SET logo = @img WHERE ID = " + team.id;
            DataProvider.Instance.ExecuteQuery(query, new object[] { team.logo });
        }
        public int GetNewestTeamid(int idTournament)
        {
            string query = "SELECT MAX(Id) as id FROM Teams";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            return (int)db.Rows[0]["id"];
        }
        public int GetTeamIDByName(int idTournament, string teamName)
        {
            string query = "Select id From Teams where idTournaments = " + idTournament +
                           " and DisplayName = N'" + teamName + "'";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            if (db.Rows.Count < 1)
                return -1;
            return (int)db.Rows[0]["id"];
        }
        public bool IsExistTeamName(string teamName, int idTournament)
        {
            string query = "Select count(id) as count From Teams where idTournaments = " + idTournament +
                         " and DisplayName = N'" + teamName + "'";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            if ((int)db.Rows[0]["count"] > 0)
                return true;
            return false;
        }
    }
}
