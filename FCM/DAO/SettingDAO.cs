using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;
namespace FCM.DAO
{
    class SettingDAO
    {
        private static SettingDAO instance;

        public static SettingDAO Instance
        {
            get { if (instance == null) instance = new SettingDAO(); return instance; }
            set => instance = value;
        }
        public Setting GetSetting(int idTournament)
        {
            string query = "Select* " +
                        "From Settings " +
                        "Where idTournaments= " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Setting setting = new Setting();
            if (tb.Rows.Count == 0)
                return null;
            setting= new Setting(tb.Rows[0]);
            return setting;
        }
        public void CreateSetting(int idTournament, int numberOfTeam)
        {
            string query = "Insert into Settings (IdTournaments,NumberOfTeams,MinPlayerOfTeams,MaxPlayerOfTeams,MinAge,MaxAge,MaxNumberForeignPlayers,Score_win,Score_draw,Score_lose,NumberOfTeamsIn) " +
                         "Values (" + idTournament + "," + numberOfTeam +
                         ",11,20,18,40,3,3,1,0,2)";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateSetting(Setting setting)
        {
            string query = "Update Players " +
                           "Set " +
                           " numberofteams = " + "" + setting.numberOfTeam + " ," +
                           " minPlayerOfTeams = " + "" + setting.minPlayerOfTeam + " ," +
                           " maxPlayerOfTeams = " + "" + setting.maxPlayerOfTeam + " ," +
                           " minAge = " + "" + setting.minAge + " ," +
                           " maxAge = " + "" + setting.maxAge + " ," +
                           " MaxnumberforeignPlayers = " + "" + setting.maxForeignPlayers + " ," +
                           " Score_win = " + "'" + setting.scoreWin + " ," +
                           " Score_Draw = " + "" + setting.scoreDraw + " ," +
                           " Score_Lose = " + "" + setting.scoreLose + " ," +
                           " Where id = " + setting.idTournament;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateSetting_NumberOfTeams(int idTournament, int numberOfTeam)
        {
            string query = "Update Players " +
                          "Set " +
                          " numberofteams = " + "" + numberOfTeam + " ," +
                           " Where id = " + idTournament;
        }
        public void DeleteSetting(int id)
        {
            string query = "Delete " +
                            "From Settings " +
                            "Where idTournaments = " + id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void EditSetting(int idTournament, string nameCol, string value)
        {
            string query = "Update Settings " +
                            "Set " + nameCol +
                            " = '" + value +
                            "' " +
                            "Where idTournaments = '" +
                            idTournament + "'";
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
