using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FCM.View;

namespace FCM.ViewModel
{
    class MessageBoxViewModel : BaseViewModel
    {
        public ICommand OkCommand { get; set; }

        public MessageBoxViewModel()
        {
            OkCommand = new RelayCommand<MessageBoxWindow>((parameter) => true, (parameter) => parameter.Close());
        }

    }
}
