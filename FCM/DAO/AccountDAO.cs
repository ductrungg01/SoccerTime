using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FCM.DTO;
using System.Data;

namespace FCM.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            set => instance = value;
        }
        public void DeleteAtId(int id)
        {
            string query = "Delete users  " +
                          " Where id= "+id;
            DataProvider.Instance.ExecuteQuery(query);
        }
        public List<Account> GetListAccount()
        {
            List<Account> users= new List<Account>();

            string query = "Select* " +
                           "From Users  ";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            
            foreach (DataRow row in tb.Rows)
            {
                Account user = new Account(row);
                users.Add(user);
            }    
            
            return users;
        }
        public void CreateAccount(Account account)
        {
            string query = "Insert into Users (username, password, displayname, roleLevel, idlastleague) " +
                           "Values  (" +
                           "N'"+ account.userName.ToString() + "'" + "," +
                           "N'" + account.password.ToString() + "'" + "," +
                           "N'" + account.displayName.ToString()+" " + "'" +
                           "," + account.roleLevel.ToString() + "" +
                           "," + account.idLastLeague.ToString() + "" +
                           ")";
           DataProvider.Instance.ExecuteQuery(query);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string GetPasswordAdmin()
        {
            string query = "Select* " +
                           "From Users " +
                           "Where roleLevel = 1  ";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            
            if (tb!=null)
                return (string)tb.Rows[0]["password"];
            return "";
        }
        public int GetId(string userName)
        {
            string query = "Select* " +
                           "From Users " +
                           "Where username = N'" +userName + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            if (tb.Rows[0] != null)
                return (int)tb.Rows[0]["id"];
            return 1;
        }
        public string GetPassword(string userName)
        {
            string query = "Select* " +
                           "From Users " +
                           "Where username = N'" + userName + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);

            if (tb.Rows[0] != null)
                return (string)tb.Rows[0]["password"];
            return " ";
        }
        public void UpdatePassword(string userName, string password)
        {
            string query = "Update Users " +
                            "Set password = N'" + password + "' " +
                            "Where username = N'" + userName + "' ";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public void UpdateIdLastLeague(string userName, int idLastLeague)
        {
            string query = "Update Users " +
                            "Set idLastLeague = " + idLastLeague + " " +
                            "Where username = N'" + userName + "' ";
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
