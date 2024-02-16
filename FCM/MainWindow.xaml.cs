using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;
using FCM.ViewModel;

namespace FCM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Account currentAccount { get; set; }
        public League league { get; set; }

        public List<Board> boards { get; set; }
        public Team team { get; set; }
        public Setting setting { get; set; }
        public MainViewModel mainViewModel { get; set; }
        public MainWindow()
        {
            MessageBox.Show(((char)0).ToString());
            InitializeComponent();
        }
        public MainWindow(Account account)
        {
            currentAccount = account;
            InitializeComponent();
            tblUsername.Text = "Xin chào " + account.userName;
            MainViewModel mainViewModel = new MainViewModel();
            if (currentAccount.idLastLeague != -1)
            {
                try
                {
                    league = LeagueDAO.Instance.GetLeagueById(currentAccount.idLastLeague);
                    this.mainViewModel = mainViewModel;
                    mainViewModel.LoadDetailLeague(league, this);
                }
                catch
                {
                    league = null;
                    currentAccount.idLastLeague = -1;
                    AccountDAO.Instance.UpdateIdLastLeague(currentAccount.userName,-1);
                }
            }
            mainViewModel.LoadScreenHomeWithLeague(this);
            mainViewModel = new MainViewModel();
            this.mainViewModel = mainViewModel;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel!=null)
            this.mainViewModel.DeleteAccount(this);
        }
    }
}
