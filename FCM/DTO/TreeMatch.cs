using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using FCM.DAO;
using System.Windows;

namespace FCM.DTO
{
    class TreeMatch
    {
        public int id { get; set; }
        public int idLeague { get; set; }
        public int size { get; set; }
        public int high { get; set; }
        public int idFirstNode { get; set; }
        public List<int> idTeams { get; set; }
        public List<NodeMatch> nodeMatches { get; set; }

        public TreeMatch(DataRow row)
        {
            this.id = (int)row["id"];
            this.idLeague = (int)row["idLeague"];
            this.size = (int)row["size"];
            this.high = (int)row["high"];
            this.idFirstNode = (int)row["idFirstNode"];

        }
        public TreeMatch(int idLeague, int size, List<int> idTeams)
        {
            this.idLeague = idLeague;
            if (size <= 4)
                size = 4;
            else
                if (size <= 8)
                size = 8;
            else
                if (size <= 16)
                size = 16;
            this.size = size;
            this.idTeams = idTeams;
            switch (size)
            {
                case 4:
                    this.high = 3;
                    break;
                case 8:
                    this.high = 4;
                    break;
                case 16:
                    this.high = 5;
                    break;
            }
            this.idFirstNode = CreateTree(1);
            NodeMatch firstNode = NodeMatchDAO.Instance.GetNodeById(this.idFirstNode);
            CheckPriority(firstNode);
            CreateMatch(firstNode);
        }
        public int CreateTree(int high)
        {
            if (high > this.high)
                return -1;
            NodeMatch nodeMatch;
            if (high < this.high)
            {
                nodeMatch = new NodeMatch(-1, -1, CreateTree(high + 1), CreateTree(high + 1), high, -1);
                NodeMatchDAO.Instance.CreateNodeMatch(nodeMatch);
            }
            else
            {
                if (idTeams.Count > 0)
                {
                    nodeMatch = new NodeMatch(-1, idTeams[0], -1, -1, high, -1);
                    NodeMatchDAO.Instance.CreateNodeMatch(nodeMatch);
                    idTeams.RemoveAt(0);
                }
            }

            return NodeMatchDAO.Instance.GetNewestId();
        }
        public void CreateMatch(NodeMatch node)
        {
            if (node.idNodeLeft == -1 && node.idNodeRight == -1)
                return;
            NodeMatch nodeLeft = NodeMatchDAO.Instance.GetNodeById(node.idNodeLeft);
            NodeMatch nodeRight = NodeMatchDAO.Instance.GetNodeById(node.idNodeRight);
            CreateMatch(nodeLeft);
            CreateMatch(nodeRight);
            if (node.idTeam == -1 && nodeLeft.idTeam > 0 && nodeRight.idTeam > 0)
            {
                Match match = new Match(this.idLeague, nodeLeft.idTeam, nodeRight.idTeam, -1 * node.high, "", false , "Bảng đấu loại trực tiếp");
                if (!MatchDAO.Instance.IsExist(match))
                {
                    MatchDAO.Instance.AddMatch(match);
                    int matchId = MatchDAO.Instance.GetNewestMatchid();
                    nodeLeft.idMatch = matchId;
                    nodeRight.idMatch = matchId;
                    NodeMatchDAO.Instance.UpdateNode(nodeLeft);
                    NodeMatchDAO.Instance.UpdateNode(nodeRight);
                }
            }
            if (node.idTeam > 0 && (nodeLeft.idTeam == -1 || nodeRight.idTeam == -1))
            {
                node.idTeam = -1;
                MatchDAO.Instance.DeleteMatch(node.idMatch);
                NodeMatchDAO.Instance.UpdateNode(node);
            }
        }
        public void CheckPriority(NodeMatch node)
        {
            if (node.idNodeLeft == -1 && node.idNodeRight == -1)
                return;
            NodeMatch nodeLeft = NodeMatchDAO.Instance.GetNodeById(node.idNodeLeft);
            NodeMatch nodeRight = NodeMatchDAO.Instance.GetNodeById(node.idNodeRight);
            CheckPriority(nodeLeft);
            CheckPriority(nodeRight);
            if (nodeLeft.idTeam == 0 && nodeRight.idTeam == 0)
            {
                node.idTeam = 0;
                NodeMatchDAO.Instance.UpdateNode(node);
            }
            if (nodeRight.idTeam == 0 && nodeLeft.idTeam != -1)
            {
                node.idTeam = nodeLeft.idTeam;
                NodeMatchDAO.Instance.UpdateNode(node);
            }
            if (nodeLeft.idTeam == 0 && nodeRight.idTeam != -1)
            {
                node.idTeam = nodeRight.idTeam;
                NodeMatchDAO.Instance.UpdateNode(node);
            }
        }
        public void DeleteNode(NodeMatch node, int idMatch)
        {
            if (node.idNodeLeft == -1 || node.idNodeRight == -1)
                return;
            NodeMatch nodeLeft = NodeMatchDAO.Instance.GetNodeById(node.idNodeLeft);
            NodeMatch nodeRight = NodeMatchDAO.Instance.GetNodeById(node.idNodeRight);
            DeleteNode(nodeLeft, idMatch);
            DeleteNode(nodeRight, idMatch);
            if (nodeLeft.idMatch == idMatch || nodeRight.idMatch == idMatch)
            {
                node.idTeam = -1;
                if (node.idMatch != -1)
                {
                    MatchDAO.Instance.DeleteMatch(node.idMatch);
                }

                node.idMatch = -1;
                NodeMatchDAO.Instance.UpdateNode(node);
                CreateMatch(NodeMatchDAO.Instance.GetNodeById(idFirstNode));
            }
            if (node.idTeam == -1 && node.idMatch != -1)
            {
                node.idMatch = -1;
                NodeMatchDAO.Instance.UpdateNode(node);
                MatchDAO.Instance.DeleteMatch(node.idMatch);
            }

        }
    }
}
