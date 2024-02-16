using FCM.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DTO
{
    public class Lineups
    {
        public int idMatch, idPlayer, idTeam, isOfficial;
        public string card;

        public Lineups(int idMatch, int idPlayer, int idTeam, int isOfficial, string card)
        {
            this.idMatch = idMatch;
            this.idPlayer = idPlayer;
            this.idTeam = idTeam;
            this.isOfficial = isOfficial;
            this.card = card;
        }

        public void setCardInfor()
        {
            Player p = new Player(PlayerDAO.Instance.GetPlayerById(idPlayer).idTeam,
                            PlayerDAO.Instance.GetPlayerById(idPlayer).namePlayer,
                            PlayerDAO.Instance.GetPlayerById(idPlayer).uniformNumber,
                            PlayerDAO.Instance.GetPlayerById(idPlayer).birthDay,
                            PlayerDAO.Instance.GetPlayerById(idPlayer).position,
                            PlayerDAO.Instance.GetPlayerById(idPlayer).nationality,
                            PlayerDAO.Instance.GetPlayerById(idPlayer).note,
                            PlayerDAO.Instance.GetPlayerById(idPlayer).image);

            p.id = idPlayer;

            List<string> cardsList = CardDAO.Instance.getListCardOfPlayer(this.idMatch, p);

            foreach (string c in cardsList)
            {
                if (c == "Thẻ vàng")
                {
                    this.card = c;
                }    
                    
                if (c == "Thẻ đỏ")
                {
                    this.card = c;
                    break;
                }    
            }
        }

        public Lineups(DataRow row)
        {
            this.idMatch = (int)row["IdMatchs"];
            this.idPlayer = (int)row["IdPlayers"];
            this.idTeam = (int)row["IdTeams"];
            this.isOfficial = (int)row["isOfficial"];
        }
    }
}
