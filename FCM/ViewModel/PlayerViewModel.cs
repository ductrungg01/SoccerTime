using FCM.View;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;

namespace FCM.ViewModel
{
    class PlayerViewModel : BaseViewModel
    {
        public ICommand DeletePlayerCommand{ get; set; }
        public ICommand EditPlayerCommand{ get; set; }
        public PlayerViewModel()
        {
            DeletePlayerCommand = new RelayCommand<ucPlayer>((parameter) => true, (parameter) => DeletePlayer(parameter));
            EditPlayerCommand = new RelayCommand<ucPlayer>((parameter) => true, (parameter) => EditPlayer(parameter));
        }
        void DeletePlayer(ucPlayer parameter)
        {
            ConfirmDialogWindow wdd = new ConfirmDialogWindow("Xác nhận xóa cầu thủ : "+parameter.tblName.Text +" ?");
            wdd.ShowDialog();
            if (wdd.confirm == false)
            {
                return;
            }
            PlayerDAO.Instance.DeletePlayer(parameter.player.id);
            if (parameter.roleLevel==1)
            {
                parameter.mainViewModel.LoadListPlayer(parameter.mainWindow, parameter.player.idTeam);
            }    
        }
        void EditPlayer(ucPlayer parameter)
        {
            parameter.mainViewModel.OpenEditPlayerWindow(parameter.mainWindow, parameter.player);
        }
    }
}
