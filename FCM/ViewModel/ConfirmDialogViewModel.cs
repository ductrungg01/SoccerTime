using System;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.View;

namespace FCM.ViewModel
{
    class ConfirmDialogViewModel : BaseViewModel
    {
        public ICommand CancelDialogCommand { get; set; }
        public ICommand ConfirmDialogCommand { get; set; }
        public ConfirmDialogViewModel()
        {
            CancelDialogCommand = new RelayCommand<ConfirmDialogWindow>((parameter) => true, (parameter) => CancelDialog(parameter));
            ConfirmDialogCommand = new RelayCommand<ConfirmDialogWindow>((parameter) => true, (parameter) => ConfirmDialog(parameter));
        }

        void CancelDialog(ConfirmDialogWindow parameter)
        {
            parameter.confirm = false;
            parameter.Close();
        }
        void ConfirmDialog(ConfirmDialogWindow parameter)
        {
            parameter.confirm = true;
            parameter.Close();
        }
    }

}
