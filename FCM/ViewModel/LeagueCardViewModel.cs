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
    public class LeagueCardViewModel : BaseViewModel
    {
        public ICommand GetDetailCommand { get; set; }
        public ICommand DeleteLeagueCommand { get; set; }
        public LeagueCardViewModel()
        {
            GetDetailCommand = new RelayCommand<ucLeagueCard>((parameter) => true, (parameter) => GetDetail(parameter));
            DeleteLeagueCommand = new RelayCommand<ucLeagueCard>((parameter) => true, (parameter) => DeleteLeague(parameter));
        }
        void GetDetail(ucLeagueCard parameter)
        {
            parameter.main.LoadDetailLeague(parameter.league, parameter.mainWindow);
        }
        MessageBoxWindow wd;
        void DeleteLeague(ucLeagueCard parameter)
        {
            ConfirmDialogWindow wdd = new ConfirmDialogWindow("Xác nhận xóa mùa giải : " + parameter.league.nameLeague + " ?");
            wdd.ShowDialog();
            if (wdd.confirm == false)
            {
                return;
            }
            LeagueDAO.Instance.DeleteLeague(parameter.league);
            if (parameter.league.nameLeague==parameter.mainWindow.league.nameLeague)
                parameter.main.DeleteLeague(parameter.mainWindow);
            else
                parameter.main.LoadListLeague(parameter.mainWindow);
            wd = new MessageBoxWindow(true, "Xóa mùa giải " + parameter.league.nameLeague + " thành công");
            wd.ShowDialog();
        }
    }
}
