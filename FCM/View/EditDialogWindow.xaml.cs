using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for EditDialogWindow.xaml
    /// </summary>
    public partial class EditDialogWindow : Window
    {
        public Setting curSetting;
        public int idSetting;
        public int idTournament;
        public EditDialogWindow()
        {
            InitializeComponent();
        }
        public EditDialogWindow(int id, int index, Setting parameter)
        {
            InitializeComponent();
            this.curSetting = parameter;
            this.idSetting = index;
            this.idTournament = id;
            this.tbValue.Focus();
        }
    }
}
