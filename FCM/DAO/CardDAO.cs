using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class CardDAO
    {
        private static CardDAO instance;

        public static CardDAO Instance
        {
            get { if (instance == null) instance = new CardDAO(); return instance; }
            set => instance = value;
        }
        public List<Card> GetListCards(int idMatchs, int idTeams)
        {
            List<Card> cards = new List<Card>();

            string query = "Select* " +
                          "From Cards  " +
                          " Where IdMatchs = " + idMatchs + " AND " + " IdTeams = " + idTeams;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                Card card = new Card(row);
                cards.Add(card);
            }
            return cards;
        }
        public List<string> getListCardOfPlayer(int idMatch, Player p)
        {
            string query = "Select TypeOfCard " +
                " FROM CARDS " +
                " WHERE IdMatchs = " + idMatch + " AND " +
                " IdPlayers = " + p.id + " AND " +
                " IdTeams = " + p.idTeam;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            List<string> cardsList = new List<string>();

            foreach (DataRow row in tb.Rows)
            {
                cardsList.Add(row["TypeOfCard"].ToString());
            }

            return cardsList;
        }
        public void UpdateCard(Card card)
        {
            int hour = Int32.Parse(card.time) / 60;
            int min = Int32.Parse(card.time) % 60;

            DateTime time = new DateTime(1900, 1, 1, hour, min, 0);

            string query = "Update Cards" +
                "Set IdPlayers = " + card.idPlayer + " , " +
                " Time = " + "N'" + time.ToString("yyyy-MM-dd HH:mm:ss") + "' , " +
                " TypeOfCard = " + " N'" + card.typeOfCard + "' , " +
                " Where IdMatchs = " + card.idMatchs + " AND " + " IdTeams = " + card.idTeams;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void AddCard(Card c)
        {
            int hour = Int32.Parse(c.time) / 60;
            int min = Int32.Parse(c.time) % 60;

            DateTime time = new DateTime(1900, 1, 1, hour, min, 0);

            string query = "insert into Cards(IdMatchs, IdPlayers, IdTeams, Time, TypeOfCard) values (" +
                c.idMatchs + " , "
                + c.idPlayer + " , "
                + c.idTeams + " , "
                + "N'" + time.ToString("yyyy-MM-dd HH:mm:ss") + "' , " 
                + "N'" + c.typeOfCard + "'"
                + ") ";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void DeleteCardByMatchID(int idMatch)
        {
            string query = "DELETE FROM Cards " +
                " WHERE IdMatchs = " + idMatch;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public int GetCountCardOfPlayerByTypeAndIdMatch(Player p,string TypeOfCard, int idMatch)
        {
            string query = "Select *" +
                " FROM CARDS " +
                " WHERE TypeOfCard = N'" + TypeOfCard + "' AND " +
                " IdPlayers = " + p.id + " AND " +
                " IdTeams = " + p.idTeam + "AND " +
                " IdMatchs = " + idMatch;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            return tb.Rows.Count;
        }

        public int GetCountCardTypeByIDMatch(int idMatch, string TypeOfCard)
        {
            string query = "Select *" +
                " FROM CARDS " +
                " WHERE TypeOfCard = N'" + TypeOfCard + "' AND " +
                " IdMatchs = " + idMatch;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            return tb.Rows.Count;
        }
        public int GetCountCardTypeByIDMatchAndIDTeam(int idMatch, int idTeam, string TypeOfCard)
        {
            string query = "Select *" +
                " FROM CARDS " +
                " WHERE TypeOfCard = N'" + TypeOfCard + "' AND " +
                " IdMatchs = " + idMatch + " AND " +
                " IdTeams = " + idTeam;

            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            return tb.Rows.Count;
        }
    }
}
