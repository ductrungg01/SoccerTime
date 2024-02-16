using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class LineupsDAO
    {
        private static LineupsDAO instance;

        public static LineupsDAO Instance
        {
            get { if (instance == null) instance = new LineupsDAO(); return instance; }
            set => instance = value;
        }
        public List<Lineups> GetListLineups(int idMatchs, int idTeams, int isOfficial = 1)
        {
            List<Lineups> lineups = new List<Lineups>();

            string query = "SELECT * " +
                "FROM Lineups " +
                " Where IdMatchs = " + idMatchs + " AND IdTeams = " + idTeams + " AND isOfficial = " + isOfficial;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                Lineups lineup = new Lineups(row);
                lineups.Add(lineup);
            }
            return lineups;
        }
        public void UpdateStatusInLineups(int idMatchs, int idTeams, int idPlayers, int isOfficial)
        {
            string query = "Update Lineups" +
                " IdPlayers = " + idPlayers + " , " +
                " isOfficial = N'" + isOfficial + "' ," +
                " Where IdMatchs = " + idMatchs + " AND " + " IdTeams = " + idTeams;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void AddLinups(Lineups p)
        {
            string query = "insert into Lineups(IdMatchs, IdPlayers, IdTeams, isOfficial) values (" +
                p.idMatch + " , " 
                + p.idPlayer + " , "
                + p.idTeam + " , "
                + p.isOfficial
                + ")";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteLineupsByMatchID(int idMatch)
        {
            string query = "DELETE FROM Lineups " +
                " WHERE IdMatchs = " + idMatch;
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
