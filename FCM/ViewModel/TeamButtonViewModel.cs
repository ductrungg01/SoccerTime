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
    public class TeamButtonViewModel : BaseViewModel
    {
        public ICommand GetDetailCommand { get; set; }
        public TeamButtonViewModel()
        {
            GetDetailCommand = new RelayCommand<ucTeamButton>((parameter) => true, (parameter) => GetDetail(parameter));
        }
        void GetDetail(ucTeamButton parameter)
        {
            parameter.mainViewModel.LoadDetailTeam(parameter.mainWindow, parameter.team);
        }
    }
}
