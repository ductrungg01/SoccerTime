using FCM.View;
using Microsoft.Expression.Interactivity.Core;
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
    class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand ExitCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ChangePasswordViewModel()
        {
            ExitCommand = new RelayCommand<ChangePasswordWindow>((parameter) => true, (parameter) => parameter.Close());
            ChangePasswordCommand = new RelayCommand<ChangePasswordWindow>((parameter) => true, (parameter) => RePassword(parameter));
        }
        MessageBoxWindow wd;
        public void RePassword(ChangePasswordWindow parameter)
        {
            if (parameter.pbOldPassword.Password == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập mật khẩu cũ");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbNewPassword.Password == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập mật khẩu mới");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbRefillPassword.Password == "")
            {
                wd = new MessageBoxWindow(false, "Vui lòng nhập lại mật khẩu xác nhận");
                wd.ShowDialog();
                return;
            }
            if (parameter.pbNewPassword.Password != parameter.pbRefillPassword.Password)
            {
                wd = new MessageBoxWindow(false, "Mất khẩu mới không khớp với mật khẩu xác nhận");
                wd.ShowDialog();
                return;
            }

            string oldPass = parameter.pbOldPassword.Password;
            string newPass = parameter.pbNewPassword.Password;

            if (AccountDAO.MD5Hash(AccountDAO.Base64Encode(oldPass)) != AccountDAO.Instance.GetPassword(parameter.account.userName))
            {
                wd = new MessageBoxWindow(false, "Mật khẩu cũ không chính xác ");
                wd.ShowDialog();
                return;
            }
            AccountDAO.Instance.UpdatePassword(parameter.account.userName,AccountDAO.MD5Hash(AccountDAO.Base64Encode(newPass)));
            parameter.account.password = AccountDAO.MD5Hash(AccountDAO.Base64Encode(newPass));
            wd = new MessageBoxWindow(true, "Đổi mật khẩu thành công");
            wd.ShowDialog();
            parameter.Close();
        }
    }
}
