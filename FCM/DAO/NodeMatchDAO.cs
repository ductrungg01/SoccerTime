using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FCM.DTO;
using System.Windows;

namespace FCM.DAO
{
    class NodeMatchDAO
    {
        private static NodeMatchDAO instance;

        public static NodeMatchDAO Instance
        {
            get { if (instance == null) instance = new NodeMatchDAO(); return instance; }
            set => instance = value;
        }
        public int GetNewestId()
        {
            string query = "Select max(id) as id " +
                            " From NodeMatch  ";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
            return (int)db.Rows[0]["id"];
        }
        public void CreateNodeMatch(NodeMatch nodeMatch)
        {
            string query = "Insert into NodeMatch(idTree,idTeam,idNodeLeft,idNodeRight,high,idMatch) " +
                           " Values (" +
                           " " + nodeMatch.idTree + ", " +
                           " " + nodeMatch.idTeam + ", " +
                           " " + nodeMatch.idNodeLeft + ", " +
                           " " + nodeMatch.idNodeRight + ", " +
                           " " + nodeMatch.high + ", " +
                           " " + nodeMatch.idMatch + " " +
                           ")  ";
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
        public NodeMatch GetNodeMatch(int idTree)
        {

            string query = "Select * " +
                            " From NodeMatch" +
                            " Where idTree = " + idTree;

            DataTable db = DataProvider.Instance.ExecuteQuery(query);

            return new NodeMatch(db.Rows[0]);
        }
        public NodeMatch GetNodeById(int id)
        {

            string query = "Select * " +
                            " From NodeMatch" +
                            " Where id = " + id;

            DataTable db = DataProvider.Instance.ExecuteQuery(query);

            return new NodeMatch(db.Rows[0]);
        }
        public void DeleteNodeMatch(NodeMatch node)
        {
            string query = "Delete NodeMatch" +
                          "  Where id = " + node.id;
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteNodeMatchByTree(int idTree)
        {
            string query = "Delete NodeMatch" +
                          "  Where idTree = " + idTree;
            DataTable db = DataProvider.Instance.ExecuteQuery(query);
        }
        public void SetIdTree(int idTree)
        {
            string query = "Update NodeMatch " +
                        " Set idTree  =" + idTree + "" +
                        " Where idTree = -1";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateNode(NodeMatch node)
        {
            string query = "Update NodeMatch " +
                        " Set idTeam  =" + node.idTeam + "," +
                        " idMatch  =" + node.idMatch + "" +
                        " Where id = " + node.id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateNode(int idLeague, int idTeamLeft, int idTeamRight, int idTeamWin)
        {
            TreeMatch treeMatch = TreeMatchDAO.Instance.GetTree(idLeague);
            string query = "SELECT ID " +
                       " FROM NODEMATCH " +
                       " Where idTEAM = " + idTeamLeft + "" +
                       " and idTree = " + treeMatch.id;
            DataTable dbTeamLeft = DataProvider.Instance.ExecuteQuery(query);

            query = "SELECT ID " +
                       " FROM NODEMATCH " +
                       " Where idTEAM = " + idTeamRight + "" +
                        " and idTree = " + treeMatch.id;
            DataTable dbTeamRight = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow dataRowTeamLeft in dbTeamLeft.Rows)
            {
                foreach (DataRow dataRowTeamRight in dbTeamRight.Rows)
                {
                    query = "SELECT ID " +
                      " FROM NODEMATCH " +
                      " Where idNodeLeft = " + (int)dataRowTeamLeft["id"] + "" +
                      " And idNodeRight = " + (int)dataRowTeamRight["id"] + "" +
                       " and idTree = " + treeMatch.id;
                    DataTable dbNode = DataProvider.Instance.ExecuteQuery(query);
                    if (dbNode.Rows.Count > 0)
                    {
                        NodeMatch node = GetNodeById((int)dbNode.Rows[0]["id"]);
                        if (node.idTeam != idTeamWin)
                        {
                            treeMatch.DeleteNode(NodeMatchDAO.Instance.GetNodeById(treeMatch.idFirstNode),GetNodeById((int)dataRowTeamLeft["id"]).idMatch);
                        }
                        if (idTeamWin == node.idTeam)
                            return;
                        node.idTeam = idTeamWin;
                        UpdateNode(node);
                        TreeMatch tree = TreeMatchDAO.Instance.GetTree(idLeague);
                        NodeMatch firstNode = NodeMatchDAO.Instance.GetNodeById(tree.idFirstNode);
                        tree.CheckPriority(firstNode);
                        tree.CreateMatch(firstNode);
                        return;
                    }
                }
            }


        }
    }
}
