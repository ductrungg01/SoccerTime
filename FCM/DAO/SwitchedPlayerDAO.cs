using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class SwitchedPlayerDAO
    {
        private static SwitchedPlayerDAO instance;

        public static SwitchedPlayerDAO Instance
        {
            get { if (instance == null) instance = new SwitchedPlayerDAO(); return instance; }
            set => instance = value;
        }
        public List<SwitchedPlayer> GetListSwitchedPlayer(int idMatch, int idTeam)
        {
            List<SwitchedPlayer> switchedPlayers = new List<SwitchedPlayer>();

            string query = "SELECT * " +
                " FROM SwitchedPlayers " +
                " Where IdMatchs = " + idMatch + " AND IdTeams = " + idTeam;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                SwitchedPlayer switchedPlayer = new SwitchedPlayer(row);
                switchedPlayers.Add(switchedPlayer);
            }

            return switchedPlayers;
        }
        public void UpdateSwitchedPlayer(SwitchedPlayer oldS, SwitchedPlayer newS)
        {
            DateTime time = new DateTime(1900, 1, 1, 0, Int32.Parse(newS.time), 0);

            string query = "Update SwitchedPlayers " +
                "Set IdPlayerIn = " + newS.idPlayerIn + " , " +
                " IdPlayerOut = " + newS.idPlayerOut + " , " +
                " Time = '" + time.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " Where IdMatchs = " + oldS.idMatch + " AND IdPlayerIn = " + oldS.idPlayerIn + " AND IdPlayerOut = " + oldS.idPlayerOut + " AND IdTeams = " + oldS.idTeam;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void AddSwitchPlayer(SwitchedPlayer s)
        {
            DateTime time = new DateTime(1900, 1, 1, 0, Int32.Parse(s.time), 0);

            string query = "insert into SwitchedPlayers(IdMatchs, IdPlayerIn, IdPlayerOut, IdTeams, Time) values (" +
                s.idMatch + " , " 
                + s.idPlayerIn + " , "
                + s.idPlayerOut + " , "
                + s.idTeam + " , "
                + "'" + time.ToString("yyyy-MM-dd HH:mm:ss") + "'" 
                + ") ";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void DeleteSwitchedPlayersByMatchID(int idMatch)
        {
            string query = "DELETE FROM SwitchedPlayers " +
                " WHERE IdMatchs = " + idMatch;
            DataProvider.Instance.ExecuteQuery(query);
        }

    }
}
