using FCM.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FCM.DAO
{
    class TypeOfGoalDAO
    {
        private static TypeOfGoalDAO instance;

        public static TypeOfGoalDAO Instance
        {
            get { if (instance == null) instance = new TypeOfGoalDAO(); return instance; }
            set => instance = value;
        }
        public List<TypeOfGoal> GetListTypeOfGoal(int idTournament)
        {
            List<TypeOfGoal> listTypeOfGoals = new List<TypeOfGoal>();

            string query = "Select* " +
                          "From TypeOfGoals " +
                          "Where idTournaments = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in tb.Rows)
            {
                TypeOfGoal typeOfGoal = new TypeOfGoal(row);
                listTypeOfGoals.Add(typeOfGoal);
            }
            return listTypeOfGoals;
        }
        public string GetTypeOfGoalNameByID(int id)
        {
            string query = "Select* " +
                          "From TypeOfGoals " +
                          "Where id = '" + id + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            if (tb.Rows[0] != null)
                return tb.Rows[0]["DisplayName"].ToString();
            return "";
        }

        public void AddTypeGoal(int idTournament, string nameGoalType)
        {
            string query = "insert into TypeOfGoals(IdTournaments, DisplayName) values(" +
                            idTournament +
                            ", N'" + nameGoalType + "')";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public bool IsExistNameTypeGoal(int idTournament, string name)
        {
            string query = "Select DisplayName " +
                          "From TypeOfGoals " +
                          "Where idTournaments = " + idTournament +
                          " and DisplayName = N'" + name + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            if (tb.Rows.Count != 0)
                return true;
            return false;
        }

        public void EditNameTypeGoal(int idTournament, string name, string oldName)
        {
            string query = "update TypeOfGoals " +
                            "set DisplayName = N'" + name + "' " +
                            "where idTournaments =" + idTournament +
                            " and DisplayName = N'" + oldName + "'";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteTypeGoal(int idTournament, string name)
        {
            string query = "delete from TypeOfGoals " +
                            "where idTournaments =" + idTournament +
                            " and DisplayName = N'" + name + "'";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteTypeGoal(int idTournament)
        {
            string query = "delete from TypeOfGoals " +
                            "where idTournaments =" + idTournament;
            DataProvider.Instance.ExecuteQuery(query);
        }


    }
}
