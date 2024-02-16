using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;

namespace FCM.DAO
{
    class LeagueDAO
    {
        private static LeagueDAO instance;

        public static LeagueDAO Instance
        {
            get { if (instance == null) instance = new LeagueDAO(); return instance; }
            set => instance = value;
        }
        public List<League> GetListLeagues()
        {
            List<League> leagues = new List<League>();

            string query = "Select* " +
                          "From Tournaments  ";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                League league = new League(row);
                leagues.Add(league);
            }
            return leagues;
        }
        public League GetLeagueById(int id)
        {
            string query = "Select* " +
                         "From Tournaments " +
                         "Where id= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            League league = new League(tb.Rows[0]);
            return league;
        }
        public int GetNewestLeagurId()
        {
            string query = "SELECT MAX(Id) as id FROM Tournaments";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return (int)tb.Rows[0]["id"];
        }
        public void CreateLeague(League league)
        {
            string query = "Insert into Tournaments (Honors,DisplayName,Time,Status,countTeam,typeLeague, countBoard) " +
                         "Values (  " +
                         "N'" + league.nameSpender + "' ," +
                         "N'" + league.nameLeague + "' ," +
                         "N'" + league.dateTime.ToString("M/d/yyyy") + "' ," +
                         "N'" + league.status + "' ," +
                         " " + league.countTeam + "," +
                         " " + league.typeLeague + "," +
                         " " + league.countBoard + "" +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Tournaments SET logo = @img WHERE ID = (SELECT MAX(Id) FROM Tournaments)";
            DataProvider.Instance.ExecuteQuery(query, new object[] { league.logo });
            SettingDAO.Instance.CreateSetting(GetNewestLeagurId(), league.countTeam);
            TypeOfGoalDAO.Instance.AddTypeGoal(GetNewestLeagurId(),"Phạt góc");
            TypeOfGoalDAO.Instance.AddTypeGoal(GetNewestLeagurId(),"Đánh Đầu");
            TypeOfGoalDAO.Instance.AddTypeGoal(GetNewestLeagurId(),"Penalty");
        }
        public void UpdateLeague(League league)
        {
            string query = "Update Tournaments " +
                            "Set " +
                            " Honors = " + "N'" + league.nameSpender + "' ," +
                            " Displayname = " + "N'" + league.nameLeague + "' ," +
                            " Time = " + "'" + league.dateTime.ToString("M/d/yyyy") + "' ," +
                            " Status = " + "N'" + league.status + "' ," +
                            " countTeam =" + league.countTeam + "," +
                            " typeLeague =" + league.typeLeague + "," +
                            " countBoard =" + league.countBoard + "" +
                            " Where id = " + league.id;
            DataProvider.Instance.ExecuteQuery(query);
            query = "UPDATE Tournaments SET logo = @img WHERE ID = " + league.id;
            DataProvider.Instance.ExecuteQuery(query, new object[] { league.logo });
            SettingDAO.Instance.UpdateSetting_NumberOfTeams(league.id, league.countTeam);
        }

        public void UpdateStatusOfLeague(int id, int status)
        {
            string query = "Update Tournaments " +
                " Set " +
                " Status = " + status +
                " Where id = " + id;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public void DeleteLeague(League league)
        {
            SettingDAO.Instance.DeleteSetting(league.id);
            MatchDAO.Instance.DeleteMatchInLeague(league.id);
            TeamDAO.Instance.DeleteTeam(league.id);
            BoardDAO.Instance.DeleteBoardInLeague(league.id);
            TreeMatchDAO.Instance.DeleteTree(TreeMatchDAO.Instance.GetTree(league.id));
            TypeOfGoalDAO.Instance.DeleteTypeGoal(league.id);
            string query = "Delete " +
                            "From Tournaments " +
                            "Where id = " + league.id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public bool IsExistLeagueName(string nameLeague)
        {
            string query = "Select count(id) as count " +
                           " From Tournaments Where Displayname =N'" + nameLeague + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            if ((int)tb.Rows[0]["count"]>0)
                return true;
            return false;
            
        }

        public void UpdateNumberOfTeams(int idTournament, int num)
        {
            string query = "Update Tournaments " +
                            "Set " +
                            " countTeam =" + num +
                            " Where id = " + idTournament;
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
