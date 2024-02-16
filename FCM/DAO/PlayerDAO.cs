using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;

namespace FCM.DAO
{
    class PlayerDAO
    {
        private static PlayerDAO instance;

        public static PlayerDAO Instance
        {
            get { if (instance == null) instance = new PlayerDAO(); return instance; }
            set => instance = value;
        }
        public Player GetPlayerById(int id)
        {
            string query = "Select* " +
                         "From Players " +
                         "Where id= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Player player = new Player(tb.Rows[0]);
            return player;
        }
        public bool IsHaveNumber(int number, int idTeam)
        {
            string query = "Select count(id) as countId  " +
                           "From players " +
                           "Where uniformNumber = " + number +" and idteams= " + idTeam;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            if ((int)tb.Rows[0]["countId"] >0)
                return true;
            return false;
        }
        public List<Player> GetListPlayer(int idTeams)
        {
            List<Player> players = new List<Player>();

            string query = "Select* " +
                            "From Players " +
                            "Where idTeams = " + idTeams;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Player player = new Player(row);
                players.Add(player);
            }
            return players;
        }
        public void DeletePlayer(int id)
        {
            string query = "Delete " +
                            "From Players " +
                            "Where id = " + id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeletePlayerInTeam(int idTeam)
        {
            string query = "Delete " +
                            "From Players " +
                            "Where idteams = " + idTeam;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void CreatePlayers(Player player)
        {
            string query = "Insert into Players (IdTeams,DisplayName,UniformNumber,Birthday,Position,Nationality,Note) " +
                         "Values (  " +
                         "" + player.idTeam + " ," +
                         "N'" + player.namePlayer + "' ," +
                         "" + player.uniformNumber + " ," +
                         "N'" + player.birthDay.ToString("M/d/yyyy") + "' ," +
                         "N'" + player.position + "' ," +
                         "N'" + player.nationality + "' ," +
                         "N'" + player.note + "' " +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE players SET imagee = @img WHERE ID = (SELECT MAX(Id) FROM Players)";
            DataProvider.Instance.ExecuteQuery(query, new object[] { player.image });
        }
        public void UpdatePlayer(Player player)
        {
            string query = "Update Players " +
                            "Set " +
                            " idteams = " + "" + player.idTeam + " ," +
                            " Displayname = " + "N'" + player.namePlayer + "' ," +
                            " uniformnumber = " + "" + player.uniformNumber + " ," +
                            " birthDay = " + "N'" + player.birthDay.ToString("M/d/yyyy") + "' ," +
                            " position = " + "N'" + player.position + "' ," +
                            " nationality = " + "N'" + player.nationality + "', " +
                            " note = " + "N'" + player.note + "' " +
                            " Where id = " + player.id;
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE players SET imagee = @img WHERE ID = " + player.id;
            DataProvider.Instance.ExecuteQuery(query, new object[] { player.image });
        }

        public Player GetPlayerByUniformNumber(int uniformNumber, int idTeam)
        {
            string query = "Select* " +
                         "From Players " +
                         "Where idTeams = " + idTeam + " AND UniformNumber = " + uniformNumber;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Player player = new Player(tb.Rows[0]);
            return player;
        }
        public int Count(int idTeam)
        {
            string query = "Select count(id) as count " +
                        "From Players " +
                        "Where idTeams = " + idTeam ;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return (int)tb.Rows[0]["count"];
        }
    }
}
