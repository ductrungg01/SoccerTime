using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace FCM.DTO
{
    class NodeMatch
    {
        public int id { get; set; }
        public int idTree { get; set; }
        public int idTeam { get; set; }
        public int idMatch { get; set; }
        public int idNodeLeft { get; set; }
        public int idNodeRight { get; set; }
        public int high { get; set; }

        public NodeMatch(DataRow row)
        {
            this.id = (int)row["id"];
            this.idTree = (int)row["idTree"];
            this.idTeam = (int)row["idTeam"];
            this.idMatch = (int)row["idMatch"];
            this.idNodeLeft = (int)row["idNodeLeft"];
            this.idNodeRight = (int)row["idNodeRight"];
            this.high = (int)row["high"];
        }
        public NodeMatch(int idTree,int idTeam, int idNodeLeft, int idNodeRight, int high,int idMatch)
        {
            this.idTree = idTree;
            this.idTeam = idTeam;
            this.idNodeLeft = idNodeLeft;
            this.idNodeRight = idNodeRight;
            this.high = high;
            this.idMatch = idMatch;
        }
    }
}
