using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;


namespace FCM.DAO
{
    class BoardDAO
    {
        private static BoardDAO instance;

        public static BoardDAO Instance
        {
            get { if (instance == null) instance = new BoardDAO(); return instance; }
            set => instance = value;
        }
        public Board GetBoardById(int id)
        {
            string query = "Select* " +
                         "From Board " +
                         "Where id= " + id;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            Board board = new Board(tb.Rows[0]);
            return board;
        }
        public List<Board> GetListBoard(int idTournament)
        {
            List<Board> boards = new List<Board>();

            string query = "Select* " +
                            "From Board " +
                            "Where idTournament = " + idTournament;
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in tb.Rows)
            {
                Board board = new Board(row);
                boards.Add(board);
            }
            // MessageBox.Show(" count =" + boards.Count);
            return boards;
        }
        public void CreateBoard(Board board)
        {
            string query = "Insert into Board (idTournament,nameBoard, countTeam) " +
                         "Values (  " +
                         "" + board.idTournament + " ," +
                         "N'" + board.nameBoard + "' ," +
                         "" + board.countTeam + "" +
                         ")";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void DeleteBoardInLeague(int idTournament)
        {
            string query = "Delete " +
                            "From Board" +
                            " Where idTournament =" + idTournament;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public int CountBoardFull(int idTournament)
        {
            List<Board> boards = GetListBoard(idTournament);
            int count = 0;
            for (int i = 0; i < boards.Count; i++)
            {
                int c = TeamDAO.Instance.GetCountTeam(idTournament, boards[i].nameBoard);
                string query = "Select id " +
                               "From Board " +
                               "Where idTournament =" + idTournament + " and " +
                               "countTeam = " + c;
                DataTable table = DataProvider.Instance.ExecuteQuery(query);
                if (table.Rows.Count > 0)
                    count++;
            }
            return count;
        }

        //public void AddToKOBoard(int idTournament, int idTeam)
        //{
        //    string query = "Insert into TeamIntoNextQualifyingRound (idTournaments, idTeams) " +
        //                 "Values ( " + idTournament + " , " + idTeam + ")";
        //    DataProvider.Instance.ExecuteQuery(query);
        //}
        //public void DeleteKOBoard(int idTournament)
        //{
        //    string query = "Delete From TeamIntoNextQualifyingRound " +
        //                 "Where idTournaments = " + idTournament;
        //    DataProvider.Instance.ExecuteQuery(query);
        //}
        public void Update(Board board)
        {
            string query = " Update Board " +
                            " Set CountTeam =  " + board.countTeam + " " +
                            " Where id=" + board.id;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public bool HaveNockOutBoard(int id)
        {
            List<Board> boards = BoardDAO.Instance.GetListBoard(id);
            foreach (Board board in boards)
                if (board.nameBoard == "Bảng đấu loại trực tiếp")
                    return true;
            return false;
        }
    }
}
