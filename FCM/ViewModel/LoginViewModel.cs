using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.DTO;

namespace FCM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand ExitLoginCommand { get; set; }
        public ICommand OpenRegisterCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand OpenLoginCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        MessageBoxWindow wd;
        public LoginViewModel()
        {
            
            ExitLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => parameter.Close());
            OpenRegisterCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => OpenRegister(parameter));
            RegisterCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => Register(parameter));
            OpenLoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => OpenLogin(parameter));
            LoginCommand = new RelayCommand<LoginWindow>((parameter) => true, (parameter) => Login(parameter));
        }

        public void OpenRegister(LoginWindow parameter)
        {
            parameter.grdLogin.Visibility = Visibility.Hidden;
            parameter.grdRegister.Visibility = Visibility.Visible;
        }
        public void OpenLogin(LoginWindow parameter)
        {
            parameter.grdLogin.Visibility = Visibility.Visible;
            parameter.grdRegister.Visibility = Visibility.Hidden;
        }
        public void Login(LoginWindow parameter)
        {
            //   MessageBox.Show(UserName);
            if (parameter.tbUsername.Text == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập tên tài khoản");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbPassword.Password == "") 
            { 
                wd = new MessageBoxWindow(false, "Vui lòng nhập mật khẩu");
                wd.ShowDialog();
                return;
            }

            string userName = parameter.tbUsername.Text;
            string password = AccountDAO.MD5Hash(AccountDAO.Base64Encode(parameter.pbPassword.Password));
            //string password = parameter.pbPassword.Password;
            List<Account> accounts = AccountDAO.Instance.GetListAccount();
            //MessageBox.Show(accounts.Count.ToString());
            foreach (Account account in accounts)
            {
                if (account.userName == userName && account.password == password)
                {
                    MainWindow mainWindow = new MainWindow(account);
                    parameter.Hide();
                    mainWindow.Show();
                    parameter.Close();
                    return;
                }
                if (account.userName == userName && account.password != password)
                {
                    wd = new MessageBoxWindow(false, "Sai mật khẩu");
                    wd.ShowDialog();
                    return;
                }    
            }
            wd = new MessageBoxWindow(false, "Tên tài khoản không tồn tại");
            wd.ShowDialog();
        }
        public void Register(LoginWindow parameter)
        {
            if (parameter.tbRegUsername.Text == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập tên tài khoản");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbRegPassword.Password == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập mật khẫu");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbRePassword.Password == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập mật khẩu xác nhận");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbRePassword.Password != parameter.pbRegPassword.Password)
            {
                wd = new MessageBoxWindow(false, "Mật khẩu không khớp với Mật khẩu xác nhận");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbAdminPassword.Password == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập mật khẩu admin");
                wd.ShowDialog();
                return;
            }
            if (parameter.cbAcccountType.SelectedItem == null)
            {
                wd = new MessageBoxWindow(false, "Vui lòng chọn loại tài khoản");
                wd.ShowDialog();
                return;
            }

            string userName = parameter.tbRegUsername.Text;
            string password = AccountDAO.MD5Hash(AccountDAO.Base64Encode(parameter.pbRegPassword.Password));
            //string password = parameter.pbRegPassword.Password;
            string rePassword = parameter.pbRePassword.Password;
            string regPasswordAdmin = AccountDAO.MD5Hash(AccountDAO.Base64Encode(parameter.pbAdminPassword.Password));

            List<Account> accounts = AccountDAO.Instance.GetListAccount();
            foreach (Account account in accounts)
            {
                if (account.userName == userName)
                {
                    wd = new MessageBoxWindow(false, "Tài khoản đã tồn tại");
                    wd.ShowDialog();
                    return;
                }
            }
            string passwordAdmin = AccountDAO.Instance.GetPasswordAdmin();
            if (passwordAdmin != regPasswordAdmin)
            {
                wd = new MessageBoxWindow(false, "Mật khẩu admin không chính xác");
                wd.ShowDialog();
                return;
            }
            Account account1 = new Account(userName, password, "",parameter.cbAcccountType.SelectedIndex+2, -1);
            AccountDAO.Instance.CreateAccount(account1);
            account1.id = AccountDAO.Instance.GetId(account1.userName);
            MainWindow mainWindow = new MainWindow(account1);
            parameter.Hide();
            mainWindow.Show();
            parameter.Close();

        }
    }
}