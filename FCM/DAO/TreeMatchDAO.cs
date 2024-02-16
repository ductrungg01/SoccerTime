using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FCM.DTO;

namespace FCM.DAO
{
    class TreeMatchDAO
    {
        private static TreeMatchDAO instance;

        public static TreeMatchDAO Instance
        {
            get { if (instance == null) instance = new TreeMatchDAO(); return instance; }
            set => instance = value;
        }
        public void CreateTreeMatch(TreeMatch tree)
        {
            string query = "Insert into TreeMatch(idleague, size, high,idFirstNode) " +
                        " Values (   " + " " +
                        "  " + tree.idLeague + ", " +
                        "  " + tree.size + ", " +
                        "  " + tree.high + ", " +
                        "  " + tree.idFirstNode + " " +
                        ")  ";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteTree(TreeMatch tree)
        {
            if (tree != null)
            {
                NodeMatchDAO.Instance.DeleteNodeMatchByTree(tree.id);
                string query = "Delete treeMatch" +
                                "  Where  id = " + tree.id;
                DataTable db = DataProvider.Instance.ExecuteQuery(query);
            }
        }
        public int GetNewestIdTreeNode()
        {
            string query = "Select max(id) as id " +
                            " From TreeMatch  ";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            return (int)db.Rows[0]["id"];
        }
        public TreeMatch GetTree(int idLeague)
        {
            string query = "Select * " +
                            " From TreeMatch " +
                            " Where idleague = " + idLeague;
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            if (db.Rows.Count > 0)
                return new TreeMatch(db.Rows[0]);
            else
                return null;
        }
        public TreeMatch GetTreeById(int idTree)
        {
            string query = "Select * " +
                            " From TreeMatch " +
                            " Where id = " + idTree;
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            return new TreeMatch(db.Rows[0]);
        }
    }
}
