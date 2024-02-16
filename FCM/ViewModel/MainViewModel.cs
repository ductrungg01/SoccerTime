using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Diagnostics;

namespace FCM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Command
        public ICommand SwitchTabCommand { get; set; }
        public ICommand SwitchTabStatisticsCommand { get; set; }
        public ICommand GetUidCommand { get; set; }
        public ICommand GetIdSettingCommand { get; set; }
        
        public ICommand OpenAddLeagueWindowCommand { get; set; }

        public ICommand OpenEditLeagueWindowCommand { get; set; }
        public ICommand DeleteLeagueCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand DeleteGoalTypeCommand { get; set; }
        public ICommand OpenAddTeamWindowCommand { get; set; }
        public ICommand OpenEditTeamWindowCommand { get; set; }
        public ICommand OpenAddPlayerWindowCommand { get; set; }
        public ICommand OpenEditDialogCommand { get; set; }
        public ICommand OpenAddGoalTypeCommand { get; set; }
        public ICommand OpenEditGoalTypeCommand { get; set; }
        public ICommand OpenChangePasswordCommand { get; set; }
        public ICommand OpenLoginCommand { get; set; }

        public ICommand SearchLeagueCommand { get; set; }

        public ICommand CreateScheduleCommand { get; set; }
        public ICommand ChangeRoundCommand { get; set; }
        public ICommand ExportTeamCommand { get; set; }
        public ICommand ChangeBoardCommand { get; set; }
        public ICommand ChangeRankingBoardCommand { get; set; }
        public ICommand ExportRankingBoardCommand { get; set; }

        public ICommand CancelCreateScheduleCommand { get; set; }
        public ICommand CreateScheduleNockOutCommand { get; set; }
        public ICommand ViewScheduleNockOutCommand { get; set; }
        public ICommand CreateNockOutBoard { get; set; }
        public ICommand SelectedNockOutTeamChangeCommamnd { get; set; }
        public ICommand ExportStatisticCommand { get; set; }
        public ICommand FilterStatisticCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }

        public ICommand HowToUseCommand { get; set; }
        public ICommand SendErrorCommand { get; set; }
        public ICommand ContactToDeveloperCommand { get; set; }
        
        //public ICommand OpenEditMatchWindowCommand { get; set; }
        //public ICommand OpenResultRecordWindowCommand { get; set; }

        #endregion

        #region Init
        public int round = 0;

        public string uid;

        public string idSetting = "";

        public SolidColorBrush lightGreen = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);

        public MainViewModel()
        {
            SwitchTabCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => SwitchTab(mainWindow));
            SwitchTabStatisticsCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => SwitchTabStatistics(mainWindow));
            GetUidCommand = new RelayCommand<System.Windows.Controls.Button>((mainWindow) => true, (mainWindow) => uid = mainWindow.Uid);
            GetIdSettingCommand = new RelayCommand<string>((mainWindow) => true, (mainWindow) => idSetting = mainWindow);
            OpenAddLeagueWindowCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenAddLeagueWindow(mainWindow));
            DeleteLeagueCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => DeleteLeague(mainWindow));
            DeleteTeamCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => DeleteTeam(mainWindow));

            OpenEditDialogCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenEditDialogWindow(mainWindow));
            OpenEditLeagueWindowCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenEditLeagueWindow(mainWindow));
            OpenAddTeamWindowCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenAddTeamWindow(mainWindow));
            OpenEditTeamWindowCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenEditTeamWindow(mainWindow));
            OpenAddPlayerWindowCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenAddPlayerWindow(mainWindow));
            OpenAddGoalTypeCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenAddGoalTypeWindow(mainWindow));
            OpenEditGoalTypeCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenEditGoalTypeWindow(mainWindow));
            DeleteGoalTypeCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => DeleteGoalType(mainWindow));
            OpenChangePasswordCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenChangePasswordWindow(mainWindow));
            OpenLoginCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenLoginWindow(mainWindow));
            SearchLeagueCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => SearchLeague(mainWindow));

            CreateScheduleCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => CreateSchedule(mainWindow));
            ChangeRoundCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => ChangeCbxRound(mainWindow));

            ExportTeamCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => ExportTeam(mainWindow));
            ChangeBoardCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => SearchBoard(mainWindow));
            ChangeRankingBoardCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => LoadRanking(mainWindow));
            ExportRankingBoardCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => ExportRanking(mainWindow));

            CancelCreateScheduleCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenScheduleMatch(mainWindow));
            CreateScheduleNockOutCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => CreateScheduleNockOut(mainWindow));
            ViewScheduleNockOutCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => ViewSchedule(mainWindow));
            CreateNockOutBoard = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => CreateBoardKnockOut(mainWindow));

            SelectedNockOutTeamChangeCommamnd = new RelayCommand<ComboBox>((mainWindow) => true, (mainWindow) => SelectedTeamNockOutChange(mainWindow));

            ExportStatisticCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => ExportStatistic(mainWindow));
            FilterStatisticCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => FilterStatisticClick(mainWindow));

            DeleteAccountCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => DeleteAccount(mainWindow));
            HowToUseCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => HowToUse());
            SendErrorCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => SendError());
            ContactToDeveloperCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => ContactToDeveloper());
            //OpenEditMatchWindowCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenEditMatchInfoWindow(mainWindow));
            //OpenResultRecordWindowCommand = new RelayCommand<MainWindow>((mainWindow) => true, (mainWindow) => OpenResultRecordingWindow(mainWindow));
        }
        #endregion

        #region Tab
        public void SwitchTab(MainWindow mainWindow)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Menu
            mainWindow.grdStroke.Margin = new Thickness(0, (150 + 60 * index), 0, 0);

            //Reset Color items
            mainWindow.btnHome.Foreground = white;
            mainWindow.btnLeagues.Foreground = white;
            mainWindow.btnSchedule.Foreground = white;
            mainWindow.btnTeams.Foreground = white;
            mainWindow.btnStanding.Foreground = white;
            mainWindow.btnStatistics.Foreground = white;
            mainWindow.btnSetting.Foreground = white;
            mainWindow.btnHelp.Foreground = white;
            mainWindow.btnAccount.Foreground = white;

            mainWindow.icHome.Foreground = white;
            mainWindow.icLeagues.Foreground = white;
            mainWindow.icSchedule.Foreground = white;
            mainWindow.icTeams.Foreground = white;
            mainWindow.icStanding.Foreground = white;
            mainWindow.icStatistics.Foreground = white;
            mainWindow.icSetting.Foreground = white;
            mainWindow.icHelp.Foreground = white;
            mainWindow.icAccount.Foreground = white;

            // Disable all screen
            mainWindow.grdHomeScreen.Visibility = Visibility.Hidden;
            mainWindow.grdHomeNoLeagueScreen.Visibility = Visibility.Hidden;
            mainWindow.grdLeaguesScreen.Visibility = Visibility.Hidden;
            mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
            mainWindow.grdTeamsScreen.Visibility = Visibility.Hidden;
            mainWindow.grdStandingScreen.Visibility = Visibility.Hidden;
            mainWindow.grdStatisticsScreen.Visibility = Visibility.Hidden;
            mainWindow.grdSettingScreen.Visibility = Visibility.Hidden;
            mainWindow.grdHelpsScreen.Visibility = Visibility.Hidden;
            mainWindow.grdAccountScreen.Visibility = Visibility.Hidden;
            mainWindow.grdScheduleChart.Visibility = Visibility.Hidden;

            // Switch tab - Show selected screen
            switch (index)
            {
                case 0:
                    mainWindow.btnHome.Foreground = lightGreen;
                    mainWindow.icHome.Foreground = lightGreen;
                    if (mainWindow.league != null) // Nếu có ít nhất 1 mùa giải
                    {
                        mainWindow.grdHomeScreen.Visibility = Visibility.Visible;
                        mainWindow.grdHomeNoLeagueScreen.Visibility = Visibility.Hidden;
                        LoadScreenHomeWithLeague(mainWindow);
                    }
                    else
                    {
                        mainWindow.grdHomeNoLeagueScreen.Visibility = Visibility.Visible;
                        mainWindow.grdHomeScreen.Visibility = Visibility.Hidden;
                    }
                    break;
                case 1:
                    LoadListLeague(mainWindow);
                    mainWindow.btnLeagues.Foreground = lightGreen;
                    mainWindow.icLeagues.Foreground = lightGreen;
                    mainWindow.grdLeaguesScreen.Visibility = Visibility.Visible;
                    if (mainWindow.currentAccount.roleLevel != 1)
                    {
                        // mainWindow.btnDeleteLeague.IsEnabled = false;
                        mainWindow.btnEditLeague.IsEnabled = false;
                        mainWindow.btnCreateLeague.IsEnabled = false;
                    }
                    mainWindow.team = null;
                    break;
                case 2:
                    mainWindow.btnSchedule.Foreground = lightGreen;
                    mainWindow.icSchedule.Foreground = lightGreen;
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Visible;


                    AddItemsForCbxRound(mainWindow);
                    mainWindow.cbxRound.SelectedIndex = 0;
                    LoadCBXBoard(mainWindow);
                    LoadListMatch(mainWindow, 0, "Tất cả bảng");
                    //if (MatchDAO.Instance.HaveMatch(mainWindow.league.id))
                    //    mainWindow.btnCreateSchedule.IsEnabled = false;
                    //else
                    //    mainWindow.btnCreateSchedule.IsEnabled = true;
                    if (mainWindow.league.typeLeague == 2)
                    {
                        mainWindow.cbxBoard.Visibility = Visibility.Visible;
                        mainWindow.btnFillMatch.Margin = new Thickness(260,5,0,0);
                        mainWindow.cbxRound.Margin = new Thickness(140, -60, 0, 5);
                    }
                    else
                    {
                        mainWindow.cbxBoard.Visibility = Visibility.Hidden;
                        mainWindow.btnFillMatch.Margin = new Thickness(150, 5, 0, 0);
                        mainWindow.cbxRound.Margin = new Thickness(25, -60, 0, 5);
                    }
                    if (MatchDAO.Instance.HaveMatch(mainWindow.league.id))
                    {
                        if (mainWindow.league.typeLeague == 0 || mainWindow.league.typeLeague == 1)
                        {
                            mainWindow.btnCreateSchedule.IsEnabled = false;
                        }
                        if (mainWindow.league.typeLeague == 2 &&
                            (!BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id) ||
                               (BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id) && TreeMatchDAO.Instance.GetTree(mainWindow.league.id) != null)))
                        {
                            mainWindow.btnCreateSchedule.IsEnabled = false;
                        }

                    }
                    else
                        mainWindow.btnCreateSchedule.IsEnabled = true;
                    TreeMatch tree = TreeMatchDAO.Instance.GetTree(mainWindow.league.id);

                    if (tree == null)
                    {
                        if (mainWindow.league.typeLeague == 0)
                        {
                            mainWindow.btnShowChart.Visibility = Visibility.Hidden;
                        }
                        else
                        if (mainWindow.league.typeLeague == 2)
                        {
                            mainWindow.btnShowChart.Visibility = Visibility.Visible;
                            mainWindow.btnShowChart.Content = "Bắt đầu vòng tiếp theo";
                            mainWindow.btnShowChart.Width = 240;
                        }
                    }
                    else
                    {
                        mainWindow.btnShowChart.Visibility = Visibility.Visible;
                        mainWindow.btnShowChart.Content = "Xem biểu đồ";
                        mainWindow.btnShowChart.Width = 140;
                    }
                    if (BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id))
                    {
                        mainWindow.btnShowChart.Width = 140;
                        mainWindow.btnShowChart.Content = "Xem biểu đồ";
                    }

                    if (mainWindow.currentAccount.roleLevel != 1 && mainWindow.currentAccount.roleLevel != 3)
                    {
                        mainWindow.btnCreateSchedule.IsEnabled = false;
                        mainWindow.btnScheduleChart.IsEnabled = false;
                        mainWindow.btnShowChart.IsEnabled = false;
                        
                    }
                    break;
                case 3:
                    mainWindow.btnTeams.Foreground = lightGreen;
                    mainWindow.icTeams.Foreground = lightGreen;
                    mainWindow.grdTeamsScreen.Visibility = Visibility.Visible;
                    if (mainWindow.league.typeLeague == 0 || mainWindow.league.typeLeague == 1)
                    {
                        mainWindow.borderTeamList.Height = 480;
                        mainWindow.grdTeamList.Height = 475;
                        mainWindow.cbGroup.Visibility = Visibility.Hidden;
                        mainWindow.LbGroup.Text = "";
                        mainWindow.tblStatus.Text = "";
                    }
                    else
                    {
                        mainWindow.borderTeamList.Height = 420;
                        mainWindow.grdTeamList.Height = 415;
                        mainWindow.cbGroup.Visibility = Visibility.Visible;
                        mainWindow.LbGroup.Text = "Bảng đấu";
                        GetBoards(mainWindow);
                    }
                    LoadListTeams(mainWindow);
                    if (mainWindow.league.typeLeague == 1)
                    {
                        mainWindow.btnStanding.IsEnabled = false;
                    }
                    if (mainWindow.currentAccount.roleLevel != 1 && mainWindow.currentAccount.roleLevel != 2)
                    {
                        mainWindow.btnAddPlayer.IsEnabled = false;
                        mainWindow.btnAddTeam.IsEnabled = false;
                        mainWindow.btnDeleteTeam.IsEnabled = false;
                        mainWindow.btnEditTeam.IsEnabled = false;
                    }

                    break;
                case 4:
                    mainWindow.btnStanding.Foreground = lightGreen;
                    mainWindow.icStanding.Foreground = lightGreen;
                    mainWindow.grdStandingScreen.Visibility = Visibility.Visible;
                    InitCbbRanking(mainWindow);
                    GetDetailSetting(mainWindow);
                    LoadRanking(mainWindow);
                    break;
                case 5:
                    mainWindow.btnStatistics.Foreground = lightGreen;
                    mainWindow.icStatistics.Foreground = lightGreen;
                    mainWindow.grdStatisticsScreen.Visibility = Visibility.Visible;
                    mainWindow.cbSelectedTeam.Visibility = Visibility.Hidden;
                    AddToComboboxStatistic(mainWindow);
                    uid = "0";
                    SwitchTabStatistics(mainWindow);
                    break;
                case 6:
                    if (mainWindow.league != null)
                    {
                        mainWindow.btnSetting.Foreground = lightGreen;
                        mainWindow.icSetting.Foreground = lightGreen;
                        mainWindow.grdSettingScreen.Visibility = Visibility.Visible;

                        bool btState = false;
                        if (TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id).Count > 0 || mainWindow.currentAccount.roleLevel != 1 && mainWindow.currentAccount.roleLevel != 3)
                            btState = false;
                        else
                            btState = true;

                        {
                            mainWindow.btEditS0.IsEnabled = btState;
                            mainWindow.btEditS1.IsEnabled = btState;
                            mainWindow.btEditS2.IsEnabled = btState;
                            mainWindow.btEditS3.IsEnabled = btState;
                            mainWindow.btEditS4.IsEnabled = btState;
                            mainWindow.btEditS5.IsEnabled = btState;
                            if (mainWindow.league.typeLeague == 0)
                            {
                                mainWindow.btEditS6.IsEnabled = false;
                            }
                            else
                                mainWindow.btEditS6.IsEnabled = btState;
                            mainWindow.btEditS7.IsEnabled = btState;
                            mainWindow.btEditS8.IsEnabled = btState;
                            mainWindow.btEditS9.IsEnabled = btState;
                            mainWindow.btnAddGoalType.IsEnabled = btState;
                            mainWindow.btnEditGoalType.IsEnabled = btState;
                            mainWindow.btnDeleteGoalType.IsEnabled = btState;
                        }
                        GetDetailSetting(mainWindow);
                        LoadTypesOfGoal(mainWindow);
                        if (mainWindow.league.typeLeague != 2)
                        {
                            mainWindow.btEditS6.Visibility = Visibility.Hidden;
                            mainWindow.tblSettingTeamIn.Visibility = Visibility.Hidden;
                            mainWindow.tbNumberOfTeamsIn.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            mainWindow.btEditS6.Visibility = Visibility.Visible;
                            mainWindow.tblSettingTeamIn.Visibility = Visibility.Visible;
                            mainWindow.tbNumberOfTeamsIn.Visibility = Visibility.Visible;
                        }
                    }
                    break;
                case 7:
                    mainWindow.btnHelp.Foreground = lightGreen;
                    mainWindow.icHelp.Foreground = lightGreen;
                    mainWindow.grdHelpsScreen.Visibility = Visibility.Visible;
                    break;
                case 8:
                    mainWindow.btnAccount.Foreground = lightGreen;
                    mainWindow.icAccount.Foreground = lightGreen;
                    mainWindow.grdAccountScreen.Visibility = Visibility.Visible;
                    if (mainWindow.currentAccount.roleLevel == 1)
                    {
                        mainWindow.dgvAccountList.Visibility = Visibility.Visible;
                        mainWindow.dstk.Visibility = Visibility.Visible;
                        LoadListAccount(mainWindow); 
                    }
                    else
                    {
                        mainWindow.dgvAccountList.Visibility = Visibility.Hidden;
                        mainWindow.dstk.Visibility = Visibility.Hidden;
                    }    
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region League
        public void LoadScreenHomeWithLeague(MainWindow mainWindow)
        {
            if (mainWindow.league != null)
            {
                mainWindow.grdHomeNoLeagueScreen.Visibility = Visibility.Hidden;
                mainWindow.grdHomeScreen.Visibility = Visibility.Visible;
                mainWindow.HomeLeagueLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(mainWindow.league.logo));
                mainWindow.tblHomeLeagueName.Text = mainWindow.league.nameLeague;
                mainWindow.tblHomeSponer.Text = "Nhà tài trợ chính: " + mainWindow.league.nameSpender;
                mainWindow.spnNextMatches.Children.Clear();
                List<Match> matches = MatchDAO.Instance.GetListMatchDetail(mainWindow.league.id);
                if (matches.Count == 0)
                {
                    mainWindow.grdHomeNoLeagueScreen.Visibility = Visibility.Visible;
                    mainWindow.grdHomeScreen.Visibility = Visibility.Hidden;
                    return;
                }
                for (int i = 0; i < matches.Count; i++)
                {
                    ucMatchDetailNoEdit ucMatchDetailNoEdit = new ucMatchDetailNoEdit(i + 1, matches[i]);
                    mainWindow.spnNextMatches.Children.Add(ucMatchDetailNoEdit);
                }
            }
            else
            {
                mainWindow.grdHomeNoLeagueScreen.Visibility = Visibility.Visible;
                mainWindow.grdHomeScreen.Visibility = Visibility.Hidden;
            }    
        }
        public void OpenAddLeagueWindow(MainWindow mainWindow)
        {
            if (mainWindow.currentAccount.roleLevel == 1)
            {
                AddLeagueWindow wd = new AddLeagueWindow();
                wd.ShowDialog();
                LoadListLeague(mainWindow);
            }
        }
        public List<League> leagues;
        public void LoadListLeague(MainWindow mainWindow)
        {
            leagues = LeagueDAO.Instance.GetListLeagues();
            LoadListLeagueToScreen(leagues, mainWindow);
        }
        public void LoadListLeagueToScreen(List<League> listLeagues, MainWindow mainWindow)
        {
            mainWindow.wpLeagueCards.Children.Clear();
            if (listLeagues != null)
            {
                if (mainWindow.league == null)
                {
                    if (leagues.Count > 0)
                    {
                        LoadDetailLeague(leagues[0], mainWindow);
                    }
                }
                if (listLeagues.Count == 0)
                    ChangeStatus(-1, mainWindow);
                foreach (League league in listLeagues)
                {
                    bool canDelete = false;
                    if (mainWindow.currentAccount.roleLevel == 1)
                        canDelete = true;
                    ucLeagueCard ucLeagueCard = new ucLeagueCard(league, mainWindow, this, canDelete);
                    mainWindow.wpLeagueCards.Children.Add(ucLeagueCard);
                }
            }
        }
        public void LoadDetailLeague(League league, MainWindow window)
        {
            try 
            {
                if (league == null|| SettingDAO.Instance.GetSetting(league.id)==null)
                    return;
                window.league = league;
                window.imgLeagueLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(league.logo));
                window.tblLeagueName.Text = "Tên mùa giải: " + league.nameLeague;
                window.tblSponsor.Text = "Nhà tài trợ: " + league.nameSpender;
                window.setting = SettingDAO.Instance.GetSetting(league.id);
                window.setting.numberOfTeam = league.countTeam;
                window.boards = BoardDAO.Instance.GetListBoard(league.id);
                window.currentAccount.idLastLeague = league.id;
                AccountDAO.Instance.UpdateIdLastLeague(window.currentAccount.userName, league.id);
                switch (league.status)
                {
                    case 0:
                        window.tblLeagueStatus.Text = "Trạng thái: Đang đăng ký";
                        break;
                    case 1:
                        window.tblLeagueStatus.Text = "Trạng thái: Chuẩn bị bắt đầu";
                        break;
                    case 2:
                        window.tblLeagueStatus.Text = "Trạng thái: Đã bắt đầu";
                        break;
                }
                ChangeStatus(league.status, window);
                window.tblLeagueTime.Text = "Thời gian: " + league.dateTime.ToString("dd/MM/yyyy");
                LoadListTeams(window);
                if (MatchDAO.Instance.HaveMatch(window.league.id))
                {
                    window.btnEditLeague.IsEnabled = false;
                }
                else
                {
                    window.btnEditLeague.IsEnabled = true;
                }
                if (TreeMatchDAO.Instance.GetTree(window.league.id) != null)
                {
                    window.btnStanding.IsEnabled = false;
                }
                if (window.currentAccount.roleLevel != 1)
                {
                    // window.btnDeleteLeague.IsEnabled = false;
                    window.btnEditLeague.IsEnabled = false;
                    window.btnCreateLeague.IsEnabled = false;
                }
            }
            catch
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Lỗi dữ liệu mùa giải");
                wd.ShowDialog();
            }
        }
        public void ChangeStatus(int status, MainWindow mainWindow)
        {
            switch (status)
            {
                case -1:
                    mainWindow.btnSchedule.IsEnabled = false;
                    mainWindow.btnReport.IsEnabled = false;
                    mainWindow.btnTeams.IsEnabled = false;
                    mainWindow.btnStanding.IsEnabled = false;
                    mainWindow.btnStatistics.IsEnabled = false;
                    mainWindow.btnSetting.IsEnabled = false;
                    break;
                case 0:
                    mainWindow.btnSchedule.IsEnabled = false;
                    mainWindow.btnReport.IsEnabled = false;
                    mainWindow.btnTeams.IsEnabled = true;
                    mainWindow.btnStanding.IsEnabled = false;
                    mainWindow.btnStatistics.IsEnabled = false;
                    mainWindow.btnSetting.IsEnabled = true;
                    mainWindow.btnAddTeam.IsEnabled = true;
                    mainWindow.btnDeleteTeam.IsEnabled = true;
                    mainWindow.btnAddPlayer.IsEnabled = true;
                    break;
                case 1:
                    mainWindow.btnSchedule.IsEnabled = true;
                    mainWindow.btnReport.IsEnabled = true;
                    mainWindow.btnTeams.IsEnabled = true;
                    if (TreeMatchDAO.Instance.GetTree(mainWindow.league.id) == null)
                        mainWindow.btnStanding.IsEnabled = true;
                    else
                        mainWindow.btnStanding.IsEnabled = true;
                    mainWindow.btnStatistics.IsEnabled = true;
                    mainWindow.btnSetting.IsEnabled = true;
                    mainWindow.btnAddTeam.IsEnabled = false;
                    //mainWindow.btnDeleteTeam.IsEnabled = false;
                    //mainWindow.btnAddPlayer.IsEnabled = false;
                    break;
                case 2:
                    mainWindow.btnSchedule.IsEnabled = true;
                    mainWindow.btnReport.IsEnabled = true;
                    mainWindow.btnTeams.IsEnabled = true;
                    mainWindow.btnStanding.IsEnabled = true;
                    mainWindow.btnStatistics.IsEnabled = true;
                    mainWindow.btnSetting.IsEnabled = true;
                    break;
            }
            if (mainWindow.league==null||
            BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id))
            {
                mainWindow.btnStanding.IsEnabled = false;
            }
        }
        public void DeleteLeague(MainWindow mainWindow)
        {
            if (mainWindow.currentAccount.roleLevel == 1)
            {
                if (mainWindow.league == null)
                {
                }
                else
                {
                    //eagueDAO.Instance.DeleteLeague(mainWindow.league);
                    mainWindow.league = null;

                    mainWindow.imgLeagueLogo.Source = mainWindow.nullImage.Source;
                    mainWindow.tblLeagueName.Text = "";
                    mainWindow.tblSponsor.Text = "";
                    mainWindow.tblLeagueStatus.Text = "";
                    mainWindow.tblLeagueTime.Text = "";
                    mainWindow.currentAccount.idLastLeague = -1;
                    AccountDAO.Instance.UpdateIdLastLeague(mainWindow.currentAccount.userName, -1);
                    LoadListLeague(mainWindow);
                }
            }
        }
        public void OpenEditLeagueWindow(MainWindow mainWindow)
        {
            if (mainWindow.currentAccount.roleLevel == 1)
            {
                if (mainWindow.league!=null)
                {
                    AddLeagueWindow wd = new AddLeagueWindow(mainWindow.league);
                    wd.tblTitle.Text = "SỬA THÔNG TIN GIẢI ĐẤU";
                    wd.btnCreateLeague.Content = "Sửa";
                    wd.tbSponsor.Text = mainWindow.league.nameSpender;
                    wd.tbUsername.Text = mainWindow.league.nameLeague;
                    wd.imgLeagueLogo.Source = mainWindow.imgLeagueLogo.Source;
                    wd.datePicker.SelectedDate = mainWindow.league.dateTime;
                    wd.tbCountOfTeams.Text = mainWindow.league.countTeam.ToString();
                    wd.cbTypeOfLeague.SelectedIndex = wd.league.typeLeague;
                    if (wd.cbTypeOfLeague.SelectedIndex == 2)
                    {
                        wd.cbCountOfGroups.Visibility = Visibility.Visible;
                        wd.cbCountOfGroups.SelectedIndex = wd.league.countBoard - 2;
                    }
                    if (TeamDAO.Instance.GetListTeamInLeague(wd.league.id).Count > 0)
                    {
                        wd.tbCountOfTeams.IsEnabled = false;
                        wd.cbCountOfGroups.IsEnabled = false;
                        wd.cbTypeOfLeague.IsEnabled = false;
                    }
                    if (MatchDAO.Instance.GetListMatch(wd.league.id).Count > 0)
                    {
                        wd.datePicker.IsEnabled = false;
                    }
                    wd.ShowDialog();
                    LoadDetailLeague(LeagueDAO.Instance.GetLeagueById(mainWindow.league.id), mainWindow);
                    LoadListLeague(mainWindow);
                }
            }
        }

        public void SearchLeague(MainWindow mainWindow)
        {
            string name = InputFormat.Instance.FomartSpace(mainWindow.tbSearchLeague.Text);
            List<League> listLeague = new List<League>();
            foreach (League league in leagues)
            {
                if (league.nameLeague.Contains(name))
                    listLeague.Add(league);
            }
            LoadListLeagueToScreen(listLeague, mainWindow);
        }
        #endregion

        #region Team

        List<Team> teams;
        void ExportTeam(MainWindow mainWindow)
        {
            if (mainWindow.team != null)
            {
                ExcelProcessing.Instance.ExportFile(mainWindow.team);
            }
        }
        public void LoadListTeams(MainWindow mainWindow)
        {

            if (mainWindow.league != null)
            {
                teams = TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id);
                foreach (Team team in teams)
                {
                    ucTeamButton teamButton = new ucTeamButton(team, mainWindow, this);
                    //mainWindow.wpTeamsList.Children.Add(teamButton);
                }
                if (teams.Count == mainWindow.setting.numberOfTeam)
                {
                    mainWindow.btnAddTeam.Visibility = Visibility.Hidden;
                    int countTeamValid = 0;
                    foreach (Team team in teams)
                    {
                        int countPlayer = PlayerDAO.Instance.Count(team.id);
                        if (countPlayer >= mainWindow.setting.minPlayerOfTeam && countPlayer <= mainWindow.setting.maxPlayerOfTeam)
                            countTeamValid++;
                    }
                    if (countTeamValid == mainWindow.setting.numberOfTeam)
                    {
                        ChangeStatus(1, mainWindow);
                    }
                    else
                        ChangeStatus(0, mainWindow);
                }
                else
                {
                    ChangeStatus(0, mainWindow);
                    mainWindow.btnAddTeam.Visibility = Visibility.Visible;
                }
                if (teams.Count > 0)
                {
                    if (mainWindow.team == null)
                    {
                        LoadDetailTeam(mainWindow, teams[0]);
                    }
                    else
                        LoadDetailTeam(mainWindow, mainWindow.team);
                }
                else
                    LoadDetailTeam(mainWindow, null);
                if (mainWindow.league.typeLeague == 0 || mainWindow.league.typeLeague == 1)
                    ShowTeam(mainWindow, "Tất cả");
                else
                    ShowTeam(mainWindow, mainWindow.cbGroup.Text);
            }
        }
        public void GetBoards(MainWindow mainWindow)
        {
            mainWindow.cbGroup.Items.Clear();
            mainWindow.cbGroup.Items.Add("Tất cả");
            mainWindow.cbGroup.SelectedItem = 0;
            mainWindow.cbGroup.SelectedIndex = 0;
            foreach (Board board in mainWindow.boards)
            {
                mainWindow.cbGroup.Items.Add(board.nameBoard);
            }
        }
        public void SearchBoard(MainWindow mainWindow)
        {
            if (mainWindow.cbGroup.SelectedItem != null)
                ShowTeam(mainWindow, mainWindow.cbGroup.SelectedItem.ToString());

        }
        public void ShowTeam(MainWindow mainWindow, string name)
        {
            if (teams == null)
                return;
            mainWindow.wpTeamsList.Children.Clear();
            if (name == "Tất cả")
                foreach (Team team in teams)
                {
                    ucTeamButton teamButton = new ucTeamButton(team, mainWindow, this);
                    mainWindow.wpTeamsList.Children.Add(teamButton);
                }
            else
            {
                foreach (Team team in teams)
                    if (team.nameBoard == name)
                    {
                        ucTeamButton teamButton = new ucTeamButton(team, mainWindow, this);
                        mainWindow.wpTeamsList.Children.Add(teamButton);
                    }
            }
        }
        public void LoadDetailTeam(MainWindow mainWindow, Team team)
        {
            if (team == null)
            {
                mainWindow.tblTeamName.Text = "";
                mainWindow.tblCoach.Text = "";
                mainWindow.tblNational.Text = "";
                mainWindow.tblStadium.Text = "";
                mainWindow.tblCountOfMembers.Text = "";
                mainWindow.imgTeamLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/NoLogoSelected.png"));
                LoadListPlayer(mainWindow, -1);
                mainWindow.btnAddPlayer.Visibility = Visibility.Hidden;
                mainWindow.btnExportTeam.Visibility = Visibility.Hidden;
                mainWindow.btnEditTeam.Visibility = Visibility.Hidden;
                mainWindow.btnDeleteTeam.Visibility = Visibility.Hidden;
                return;
            }
            mainWindow.btnAddPlayer.Visibility = Visibility.Visible;
            mainWindow.btnExportTeam.Visibility = Visibility.Visible;
            mainWindow.btnEditTeam.Visibility = Visibility.Visible;
            mainWindow.btnDeleteTeam.Visibility = Visibility.Visible;
            mainWindow.team = team;
            mainWindow.tblTeamName.Text = team.nameTeam;
            mainWindow.tblCoach.Text = team.coach;
            mainWindow.tblNational.Text = team.nation;
            mainWindow.tblStadium.Text = team.stadium;
            mainWindow.imgTeamLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(team.logo));
            LoadListPlayer(mainWindow, team.id);
            mainWindow.tblCountOfMembers.Text = mainWindow.wpPlayersList.Children.Count.ToString();
            mainWindow.tblStatus.Text = mainWindow.team.nameBoard;

            if (MatchDAO.Instance.HaveMatch(mainWindow.league.id) || (mainWindow.currentAccount.roleLevel!=1 && mainWindow.currentAccount.roleLevel != 2))
            {
                mainWindow.btnDeleteTeam.IsEnabled = false;
                mainWindow.btnAddPlayer.IsEnabled = false;
                mainWindow.btnEditTeam.IsEnabled = false;
                mainWindow.btnAddTeam.IsEnabled = false;
            }
            else
            {
                mainWindow.btnDeleteTeam.IsEnabled = true;
                mainWindow.btnAddPlayer.IsEnabled = true;
                mainWindow.btnEditTeam.IsEnabled = true;
                mainWindow.btnAddTeam.IsEnabled = true;
            }
            if (mainWindow.league.typeLeague != 2)
                mainWindow.tblStatus.Visibility = Visibility.Hidden;
            else
                mainWindow.tblStatus.Visibility = Visibility.Visible;
            mainWindow.btnAddPlayer.Visibility = Visibility.Visible;
            mainWindow.btnExportTeam.Visibility = Visibility.Visible;
        }
        public int CountNationatily(MainWindow mainWindow)
        {
            int s = 0;
            foreach (ucPlayer ucPlayer in mainWindow.wpPlayersList.Children)
            {
                if (ucPlayer.player.nationality != mainWindow.team.nation)
                    s++;

            }
            return s;
        }
        public void OpenAddTeamWindow(MainWindow mainWindow)
        {
            if (mainWindow.currentAccount.roleLevel == 1 || mainWindow.currentAccount.roleLevel==2)
            {
                if (TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id).Count == 0)
                {
                    ConfirmDialogWindow wdd = new ConfirmDialogWindow("Sau khi tạo đội bóng đầu tiên sẽ không thể thay đổi quy định của giải nữa \nXác nhận tạo đội?");
                    wdd.ShowDialog();
                    if (wdd.confirm == false)
                    {
                        return;
                    }
                }
                if (mainWindow.wpTeamsList.Children.Count == mainWindow.setting.numberOfTeam)
                {     
                    MessageBoxWindow wdd = new MessageBoxWindow(false, "Số lượng đội bóng đá đạt tối đa");
                    wdd.ShowDialog();
                }
                AddTeamWindow wd = new AddTeamWindow(mainWindow.league.id, mainWindow.boards, mainWindow.setting);
                wd.ShowDialog();
                LoadListTeams(mainWindow);
            }
        }
        public void OpenEditTeamWindow(MainWindow mainWindow)
        {
            if (mainWindow.currentAccount.roleLevel == 1 || mainWindow.currentAccount.roleLevel == 2)
            {
                if (mainWindow.team != null)
                {
                    AddTeamWindow wd = new AddTeamWindow(mainWindow.league.id, mainWindow.boards, mainWindow.setting, mainWindow.team);
                    wd.tblTitle.Text = "SỬA THÔNG TIN ĐỘI BÓNG";
                    wd.btnAdd.Content = "Sửa";
                    wd.tbName.Text = mainWindow.team.nameTeam;
                    wd.tbCoach.Text = mainWindow.team.coach;
                    wd.tbNational.Text = mainWindow.team.nation;
                    wd.tbStadium.Text = mainWindow.team.stadium;
                    wd.imgTeamLogo.Source = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(mainWindow.team.logo));
                    wd.btnImportTeam.Visibility = Visibility.Hidden;
                    wd.btnFileExcelTemplace.Visibility = Visibility.Hidden;
                    wd.ShowDialog();
                    LoadListTeams(mainWindow);
                    mainWindow.team = TeamDAO.Instance.GetTeamById(mainWindow.team.id);
                    LoadDetailTeam(mainWindow, mainWindow.team);
                }
            }
        }
        public void DeleteTeam(MainWindow mainWindow)
        {

            if (mainWindow.currentAccount.roleLevel == 1|| mainWindow.currentAccount.roleLevel == 2)
            {
                if (mainWindow.team == null)
                {
                }
                else
                {
                    ConfirmDialogWindow wdd = new ConfirmDialogWindow("Xác nhận xóa đội " + mainWindow.team.nameTeam+ " ?");
                    wdd.ShowDialog();
                    if (wdd.confirm == false)
                    {
                        return;
                    }
                    TeamDAO.Instance.DeleteTeam(mainWindow.team);
                    mainWindow.team = null;

                    mainWindow.tblTeamName.Text = "";
                    mainWindow.tblStadium.Text = "";
                    mainWindow.tblNational.Text = "";
                    mainWindow.tblCoach.Text = "";
                    mainWindow.tblCountOfMembers.Text = "";
                    mainWindow.tblStatus.Text = "";
                    mainWindow.imgTeamLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/software-logo.png"));
                    mainWindow.btnAddPlayer.Visibility = Visibility.Hidden;
                    mainWindow.btnExportTeam.Visibility = Visibility.Hidden;
                    MessageBoxWindow wd = new MessageBoxWindow(true, "Xóa đội bóng thành công");
                    wd.ShowDialog();
                    LoadListTeams(mainWindow);
                    LoadListPlayer(mainWindow, -1);
                }
            }
        }
        #endregion

        #region Player
        public void OpenAddPlayerWindow(MainWindow mainWindow)
        {
            if (mainWindow.currentAccount.roleLevel == 1)
            {
                if (mainWindow.setting.maxPlayerOfTeam == mainWindow.wpPlayersList.Children.Count)
                {
                    MessageBoxWindow wdd = new MessageBoxWindow(false, "Số lượng cầu thủ đá đạt tối đa");
                    wdd.ShowDialog();
                    return;
                }
                AddPlayerWindow wd = new AddPlayerWindow(mainWindow.team, mainWindow.setting, (CountNationatily(mainWindow) < mainWindow.setting.maxForeignPlayers));
                wd.ShowDialog();
                if (mainWindow.team != null)
                    LoadListPlayer(mainWindow, mainWindow.team.id);
            }
        }
        public void LoadListPlayer(MainWindow mainWindow, int idTeam)
        {
                if (teams.Count == mainWindow.setting.numberOfTeam)
                {
                    mainWindow.btnAddTeam.Visibility = Visibility.Hidden;
                    int countTeamValid = 0;
                    foreach (Team team in teams)
                    {
                        int countPlayer = PlayerDAO.Instance.Count(team.id);
                        if (countPlayer >= mainWindow.setting.minPlayerOfTeam && countPlayer <= mainWindow.setting.maxPlayerOfTeam)
                            countTeamValid++;
                    }
                    if (countTeamValid == mainWindow.setting.numberOfTeam)
                    {
                        ChangeStatus(1, mainWindow);
                    }
                    else
                        ChangeStatus(0, mainWindow);
            }
            mainWindow.wpPlayersList.Children.Clear();
            if (idTeam < 0)
                return;
            List<Player> players = PlayerDAO.Instance.GetListPlayer(idTeam);
            if (mainWindow.setting != null)
                if (players.Count == mainWindow.setting.maxPlayerOfTeam)
                {
                    mainWindow.btnAddPlayer.IsEnabled = false;
                }
            for (int i = 0; i < players.Count; i++)
            {
                ucPlayer ucPlayer = new ucPlayer(players[i], mainWindow.currentAccount.roleLevel, i + 1, mainWindow, this, mainWindow.league.status);
                mainWindow.wpPlayersList.Children.Add(ucPlayer);
            }

        }
        public void OpenEditPlayerWindow(MainWindow mainWindow, Player player)
        {
            if (mainWindow.team != null)
            {
                AddPlayerWindow wd = new AddPlayerWindow(mainWindow.team, player, mainWindow.setting, (CountNationatily(mainWindow) < mainWindow.setting.maxForeignPlayers));
                if (mainWindow.league.status == 0 && (mainWindow.currentAccount.roleLevel == 1 || mainWindow.currentAccount.roleLevel == 2))
                {
                    wd.tblTitle.Text = "SỬA THÔNG TIN CẦU THỦ";
                    wd.btnAdd.Content = "Sửa";
                }
                else
                {
                    wd.tblTitle.Text = "THÔNG TIN CẦU THỦ";
                    wd.btnAdd.Content = "Thoát";
                    wd.btnAdd.Visibility = Visibility.Hidden;
                    wd.btnUploadImage.Visibility = Visibility.Hidden;
                }
                wd.ShowDialog();
                LoadListTeams(mainWindow);
            }
        }
        #endregion

        #region Account
        public void OpenChangePasswordWindow(MainWindow mainWindow)
        {
            ChangePasswordWindow wd = new ChangePasswordWindow(mainWindow.currentAccount);
            wd.ShowDialog();
        }
        public void OpenLoginWindow(MainWindow mainWindow)
        {
            LoginWindow loginWindow = new LoginWindow();
            mainWindow.Hide();
            loginWindow.Show();
            mainWindow.Close();
        }
        List<Account> accounts;
        List<AccountView> accountViews = new List<AccountView>();
        public void LoadListAccount(MainWindow mainWindow)
        {
            accounts = AccountDAO.Instance.GetListAccount();
            accountViews = new List<AccountView>();
            int i = 1;
            foreach (Account account in accounts)
            {
                if (account.roleLevel != 1)
                {
                    AccountView accountView = new AccountView(i, account.userName, account.roleLevel);
                    accountViews.Add(accountView);
                    i++;
                }
            }
            mainWindow.dgvAccountList.ItemsSource = accountViews;
        }
        public void DeleteAccount(MainWindow mainWindow)
        {
            ConfirmDialogWindow wdd = new ConfirmDialogWindow("Xác nhận xóa tài khoản? ");
            wdd.ShowDialog();
            if (wdd.confirm == false)
            {
                return;
            }
            string accountName = mainWindow.dgvAccountList.SelectedItem.ToString();
            accounts = AccountDAO.Instance.GetListAccount();
            accountViews = new List<AccountView>();
            int i = 1;
            bool isDeleted = false;
            foreach (Account account in accounts)
            {
                if (account.roleLevel != 1)
                {
                    if (i - 1 == mainWindow.dgvAccountList.SelectedIndex && isDeleted == false)
                    {
                        AccountDAO.Instance.DeleteAtId(account.id);
                        isDeleted = true;
                    }
                    else
                    {
                        AccountView accountView = new AccountView(i, account.userName, account.roleLevel);
                        accountViews.Add(accountView);
                        i++;
                    }
                }
            }
            mainWindow.dgvAccountList.ItemsSource = accountViews;
            MessageBoxWindow wd = new MessageBoxWindow(true, "Xóa tài khoản thành công");
            wd.ShowDialog();
        }
        #endregion

        #region NockOut

        public void OpenScheduleMatch(MainWindow mainWindow)
        {
            mainWindow.grdScheduleScreen.Visibility = Visibility.Visible;
            mainWindow.grdScheduleChart.Visibility = Visibility.Hidden;
            LoadListMatch(mainWindow, 0, "Tất cả bảng");
        }
        public int GetIndexTeam(string name)
        {
            for (int i = 0; i < teamsInNockOut.Count; i++)
            {
                if (name == teamsInNockOut[i].nameTeam)
                    return i;
            }
            return -1;
        }
        public void SelectedTeamNockOutChange(ComboBox selectedComboBox)
        {
            //try
            //{
                //if (selectedComboBox.Text == selectedComboBox.Items[selectedComboBox.SelectedIndex].ToString())
                //    return;
                //foreach (ComboBox comboBox in comboBoxes)
                //{
                //    if (comboBox != selectedComboBox)
                //    {
                //        if (selectedComboBox.Text != null)
                //        {
                //            bool haveName = false;
                //            foreach (string item in comboBox.Items)
                //            {
                //                if (item == selectedComboBox.Text)
                //                    haveName = true;
                //            }
                //            if (!haveName)
                //                comboBox.Items.Add(selectedComboBox.Text);
                //        }
                //        int remove = -1;
                //        for (int i = 0; i < comboBox.Items.Count; i++)
                //        {
                //            string item = comboBox.Items[i].ToString();
                //            if (item != "Ưu tiên" && item == selectedComboBox.Items[selectedComboBox.SelectedIndex].ToString())
                //                remove = i;
                //        }
                //        if (remove != -1)
                //            comboBox.Items.RemoveAt(remove);
                //    }
                //}

                //foreach (ComboBox comboBox in comboBoxes)
                //{
                //    for (int i = 0; i < comboBox.Items.Count; i++)
                //    {
                //        if (comboBox.Items[i].ToString() == "")
                //        {
                //            comboBox.Items.RemoveAt(i);
                //            i--;
                //        }
                //    }
                //}
            //}
            //catch
            //{

            //}
        }
        public void CreateScheduleNockOut(MainWindow mainWindow)
        {
            if (mainWindow.btnCancelCreateScheduleChart.Visibility == Visibility.Hidden)
            {
                OpenScheduleMatch(mainWindow);
                return;
            }
            if (teamsInNockOut == null)
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Có lỗi xảy ra");
                wd.ShowDialog();
                return;
            }
            int size = 0;
            if (mainWindow.league.typeLeague == 2)
            {
                if (mainWindow.setting.NumberOfTeamIn <= 16)
                    size = 16;
                if (mainWindow.setting.NumberOfTeamIn <= 8)
                    size = 8;
                if (mainWindow.setting.NumberOfTeamIn <= 4)
                    size = 4;
            }
            else
            {
                if (mainWindow.league.countTeam <= 16)
                    size = 16;
                if (mainWindow.league.countTeam <= 8)
                    size = 8;
                if (mainWindow.league.countTeam <= 4)
                    size = 4;
            }
            comboBoxes.Clear();
            textBlockes.Clear();
            try
            {
                switch (size)
                {
                    case 4:
                        List<int> index4 = new List<int>();
                        int count4 = 0;
                        index4.Add(GetIndexTeam(mainWindow.cb4Team1.Items[mainWindow.cb4Team1.SelectedIndex].ToString()));
                        index4.Add(GetIndexTeam(mainWindow.cb4Team2.Items[mainWindow.cb4Team2.SelectedIndex].ToString()));
                        index4.Add(GetIndexTeam(mainWindow.cb4Team3.Items[mainWindow.cb4Team3.SelectedIndex].ToString()));
                        index4.Add(GetIndexTeam(mainWindow.cb4Team4.Items[mainWindow.cb4Team4.SelectedIndex].ToString()));

                        foreach (int i in index4)
                        {
                            if (i > -1)
                                count4++;
                        }
                        for (int i = 0; i < index4.Count; i++)
                            for (int j = 0; j < index4.Count; j++)
                            {
                                if (i != j && index4[i] == index4[j] && index4[j] != -1)
                                {
                                    MessageBoxWindow wdd = new MessageBoxWindow(false, "Trùng đội bóng");
                                    wdd.ShowDialog();
                                    return;
                                }
                            }

                        if (count4 < teamsInNockOut.Count)
                        {
                            MessageBoxWindow wdd = new MessageBoxWindow(false, "Thiếu đội bóng");
                            wdd.ShowDialog();
                            return;
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (index4[i] == -1)
                                    index4[i] = 0;
                                else
                                    index4[i] = teamsInNockOut[index4[i]].id;
                            }
                            TreeMatch treeMatch = new TreeMatch(mainWindow.league.id, 4, index4);
                            TreeMatchDAO.Instance.CreateTreeMatch(treeMatch);
                            NodeMatchDAO.Instance.SetIdTree(TreeMatchDAO.Instance.GetNewestIdTreeNode());
                        }
                        break;
                    case 8:
                        List<int> index8 = new List<int>();
                        int count8 = 0;
                        index8.Add(GetIndexTeam(mainWindow.cb8Team1.Items[mainWindow.cb8Team1.SelectedIndex].ToString()));
                        index8.Add(GetIndexTeam(mainWindow.cb8Team2.Items[mainWindow.cb8Team2.SelectedIndex].ToString()));
                        index8.Add(GetIndexTeam(mainWindow.cb8Team3.Items[mainWindow.cb8Team3.SelectedIndex].ToString()));
                        index8.Add(GetIndexTeam(mainWindow.cb8Team4.Items[mainWindow.cb8Team4.SelectedIndex].ToString()));
                        index8.Add(GetIndexTeam(mainWindow.cb8Team5.Items[mainWindow.cb8Team5.SelectedIndex].ToString()));
                        index8.Add(GetIndexTeam(mainWindow.cb8Team6.Items[mainWindow.cb8Team6.SelectedIndex].ToString()));
                        index8.Add(GetIndexTeam(mainWindow.cb8Team7.Items[mainWindow.cb8Team7.SelectedIndex].ToString()));
                        index8.Add(GetIndexTeam(mainWindow.cb8Team8.Items[mainWindow.cb8Team8.SelectedIndex].ToString()));
                        foreach (int i in index8)
                        {
                            if (i > -1)
                                count8++;
                        }
                        for (int i = 0; i < index8.Count; i++)
                            for (int j = 0; j < index8.Count; j++)
                            {
                                if (i != j && index8[i] == index8[j] && index8[j] != -1)
                                {
                                    MessageBoxWindow wdd = new MessageBoxWindow(false, "Trùng đội bóng");
                                    wdd.ShowDialog();
                                    return;
                                }
                            }
                        if (count8 < teamsInNockOut.Count)
                        {
                            MessageBoxWindow wdd = new MessageBoxWindow(false, "Thiếu đội bóng");
                            wdd.ShowDialog();
                            return;
                        }
                        else
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                if (index8[i] == -1)
                                {
                                    index8[i] = 0;
                                }
                                else
                                    index8[i] = teamsInNockOut[index8[i]].id;
                            }
                            TreeMatch treeMatch = new TreeMatch(mainWindow.league.id, 8, index8);
                            TreeMatchDAO.Instance.CreateTreeMatch(treeMatch);
                            NodeMatchDAO.Instance.SetIdTree(TreeMatchDAO.Instance.GetNewestIdTreeNode());
                        }
                        break;
                    case 16:
                        List<int> index16 = new List<int>();
                        int count16 = 0;
                        index16.Add(GetIndexTeam(mainWindow.cb16Team1.Items[mainWindow.cb16Team1.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team2.Items[mainWindow.cb16Team2.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team3.Items[mainWindow.cb16Team3.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team4.Items[mainWindow.cb16Team4.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team5.Items[mainWindow.cb16Team5.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team6.Items[mainWindow.cb16Team6.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team7.Items[mainWindow.cb16Team7.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team8.Items[mainWindow.cb16Team8.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team9.Items[mainWindow.cb16Team9.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team10.Items[mainWindow.cb16Team10.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team11.Items[mainWindow.cb16Team11.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team12.Items[mainWindow.cb16Team12.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team13.Items[mainWindow.cb16Team13.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team14.Items[mainWindow.cb16Team14.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team15.Items[mainWindow.cb16Team15.SelectedIndex].ToString()));
                        index16.Add(GetIndexTeam(mainWindow.cb16Team16.Items[mainWindow.cb16Team16.SelectedIndex].ToString()));
                        foreach (int i in index16)
                        {
                            if (i > -1)
                                count16++;
                        }
                        for (int i = 0; i < index16.Count; i++)
                            for (int j = 0; j < index16.Count; j++)
                            {
                                if (i != j && index16[i] == index16[j] && index16[j]!=-1)
                                {
                                    MessageBoxWindow wdd = new MessageBoxWindow(false, "Trùng đội bóng");
                                    wdd.ShowDialog();
                                    return;
                                }
                            }
                        if (count16 < teamsInNockOut.Count)
                        {
                            MessageBoxWindow wdd = new MessageBoxWindow(false, "Thiếu đội bóng");
                            wdd.ShowDialog();
                            return;
                        }
                        else
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (index16[i] == -1)
                                {
                                    index16[i] = 0;
                                }
                                else
                                    index16[i] = teamsInNockOut[index16[i]].id;
                            }
                            TreeMatch treeMatch = new TreeMatch(mainWindow.league.id, 16, index16);
                            TreeMatchDAO.Instance.CreateTreeMatch(treeMatch);
                            NodeMatchDAO.Instance.SetIdTree(TreeMatchDAO.Instance.GetNewestIdTreeNode());
                        }
                        break;
                }
                MessageBoxWindow wd = new MessageBoxWindow(true, "Tạo lịch thi đấu thành công");
                wd.ShowDialog();
                mainWindow.btnCreateSchedule.IsEnabled = false;
                mainWindow.btnStanding.IsEnabled = false;
                LoadCBXBoard(mainWindow);
                AddItemsForCbxRound(mainWindow);
                LoadListMatchRound(mainWindow, "Tất cả vòng", "Tất cả bảng");
                mainWindow.btnEditLeague.IsEnabled = false;
                mainWindow.btnStanding.IsEnabled = false;
                mainWindow.btnShowChart.Visibility = Visibility.Visible;
                OpenScheduleMatch(mainWindow);
            }
            catch
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Có lỗi xảy ra");
                wd.ShowDialog();
                return;
            }
        }
        List<ComboBox> comboBoxes = new List<ComboBox>();
        List<TextBlock> textBlockes = new List<TextBlock>();
        public void ViewSchedule(MainWindow mainWindow)
        {
            if (mainWindow.btnShowChart.Content.ToString() == "Bắt đầu vòng tiếp theo")
            {
                CreateBoardKnockOut(mainWindow);
                return;
            }
            TreeMatch treeMatch = TreeMatchDAO.Instance.GetTree(mainWindow.league.id);
            if (treeMatch == null)
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Chưa có biểu đồ");
                wd.ShowDialog();
                return;
            }
            int size = 0;
            if (mainWindow.league.typeLeague == 2)
            {
                if (mainWindow.setting.NumberOfTeamIn <= 16)
                    size = 16;
                if (mainWindow.setting.NumberOfTeamIn <= 8)
                    size = 8;
                if (mainWindow.setting.NumberOfTeamIn <= 4)
                    size = 4;
            }
            else
            {
                if (mainWindow.league.countTeam <= 16)
                    size = 16;
                if (mainWindow.league.countTeam <= 8)
                    size = 8;
                if (mainWindow.league.countTeam <= 4)
                    size = 4;
            }
            comboBoxes.Clear();
            textBlockes.Clear();
            switch (size)
            {
                case 4:
                    comboBoxes.Add(mainWindow.cb4Team1);
                    comboBoxes.Add(mainWindow.cb4Team2);
                    comboBoxes.Add(mainWindow.cb4Team3);
                    comboBoxes.Add(mainWindow.cb4Team4);
                    textBlockes.Add(mainWindow.tbl4Team1);
                    textBlockes.Add(mainWindow.tbl4Team2);
                    textBlockes.Add(mainWindow.tbl4Team3);
                    break;
                case 8:
                    comboBoxes.Add(mainWindow.cb8Team1);
                    comboBoxes.Add(mainWindow.cb8Team2);
                    comboBoxes.Add(mainWindow.cb8Team3);
                    comboBoxes.Add(mainWindow.cb8Team4);
                    comboBoxes.Add(mainWindow.cb8Team5);
                    comboBoxes.Add(mainWindow.cb8Team6);
                    comboBoxes.Add(mainWindow.cb8Team7);
                    comboBoxes.Add(mainWindow.cb8Team8);
                    textBlockes.Add(mainWindow.tbl8Team1);
                    textBlockes.Add(mainWindow.tbl8Team2);
                    textBlockes.Add(mainWindow.tbl8Team5);
                    textBlockes.Add(mainWindow.tbl8Team3);
                    textBlockes.Add(mainWindow.tbl8Team4);
                    textBlockes.Add(mainWindow.tbl8Team6);
                    textBlockes.Add(mainWindow.tbl8Team7);
                    break;
                case 16:
                    comboBoxes.Add(mainWindow.cb16Team1);
                    comboBoxes.Add(mainWindow.cb16Team2);
                    comboBoxes.Add(mainWindow.cb16Team3);
                    comboBoxes.Add(mainWindow.cb16Team4);
                    comboBoxes.Add(mainWindow.cb16Team5);
                    comboBoxes.Add(mainWindow.cb16Team6);
                    comboBoxes.Add(mainWindow.cb16Team7);
                    comboBoxes.Add(mainWindow.cb16Team8);
                    comboBoxes.Add(mainWindow.cb16Team9);
                    comboBoxes.Add(mainWindow.cb16Team10);
                    comboBoxes.Add(mainWindow.cb16Team11);
                    comboBoxes.Add(mainWindow.cb16Team12);
                    comboBoxes.Add(mainWindow.cb16Team13);
                    comboBoxes.Add(mainWindow.cb16Team14);
                    comboBoxes.Add(mainWindow.cb16Team15);
                    comboBoxes.Add(mainWindow.cb16Team16);
                    textBlockes.Add(mainWindow.tbl16Team1);
                    textBlockes.Add(mainWindow.tbl16Team2);
                    textBlockes.Add(mainWindow.tbl16Team9);
                    textBlockes.Add(mainWindow.tbl16Team3);
                    textBlockes.Add(mainWindow.tbl16Team4);
                    textBlockes.Add(mainWindow.tbl16Team10);
                    textBlockes.Add(mainWindow.tbl16Team13);
                    textBlockes.Add(mainWindow.tbl16Team5);
                    textBlockes.Add(mainWindow.tbl16Team6);
                    textBlockes.Add(mainWindow.tbl16Team11);
                    textBlockes.Add(mainWindow.tbl16Team7);
                    textBlockes.Add(mainWindow.tbl16Team8);
                    textBlockes.Add(mainWindow.tbl16Team12);
                    textBlockes.Add(mainWindow.tbl16Team14);
                    textBlockes.Add(mainWindow.tbl16Team15);
                    break;
            }
            TreeMatch tree = TreeMatchDAO.Instance.GetTree(mainWindow.league.id);
            NodeMatch nodeMatch = NodeMatchDAO.Instance.GetNodeById(tree.idFirstNode);
            LoadNodeToScreen(nodeMatch, comboBoxes, textBlockes);
            mainWindow.btnCancelCreateScheduleChart.Visibility = Visibility.Hidden;
            mainWindow.btnScheduleChart.Content = "Xem lịch thi đấu";
            switch (size)
            {
                case 4:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule8.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule16.Visibility = Visibility.Hidden;
                    break;
                case 8:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule8.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule16.Visibility = Visibility.Hidden;
                    break;
                case 16:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule8.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule16.Visibility = Visibility.Visible;
                    break;
            }
        }
        void LoadNodeToScreen(NodeMatch nodeMatch, List<ComboBox> comboBoxes, List<TextBlock> textBlocks)
        {
            if (nodeMatch.idNodeLeft == -1 && nodeMatch.idNodeRight == -1)
            {
                if (nodeMatch.idTeam == 0)
                {
                    if (comboBoxes.Count > 0)
                    {
                        comboBoxes[0].Items.Clear();
                        comboBoxes[0].Items.Add("Ưu tiên");
                        comboBoxes[0].SelectedIndex = 0;
                        comboBoxes[0].IsEnabled = false;
                        comboBoxes.RemoveAt(0);
                    }

                }
                else
                {
                    if (comboBoxes.Count > 0)
                    {
                        comboBoxes[0].Items.Clear();
                        Team team = TeamDAO.Instance.GetTeamById(nodeMatch.idTeam);
                        comboBoxes[0].Items.Add(team.nameTeam);
                        comboBoxes[0].SelectedIndex = 0;
                        comboBoxes[0].IsEnabled = false;
                        comboBoxes.RemoveAt(0);
                    }

                }
            }
            else
            {
                LoadNodeToScreen(NodeMatchDAO.Instance.GetNodeById(nodeMatch.idNodeLeft), comboBoxes, textBlocks);
                LoadNodeToScreen(NodeMatchDAO.Instance.GetNodeById(nodeMatch.idNodeRight), comboBoxes, textBlocks);
                if (nodeMatch.idTeam == -1)
                {
                    if (textBlocks.Count > 0)
                    {
                        textBlocks[0].Text = "";
                        textBlocks.RemoveAt(0);
                    }
                }
                else
                if (nodeMatch.idTeam == 0)
                {
                    if (textBlocks.Count > 0)
                    {
                        textBlocks[0].Text = "Ưu tiên";
                        textBlocks.RemoveAt(0);
                    }
                }
                else
                {
                    if (textBlocks.Count > 0)
                    {
                        Team team = TeamDAO.Instance.GetTeamById(nodeMatch.idTeam);
                        textBlocks[0].Text = team.nameTeam;
                        textBlocks.RemoveAt(0);
                    }
                }
            }

        }
        List<Team> teamsInNockOut = new List<Team>();
        void OpenNockOutScreen(MainWindow mainWindow)
        {
            teamsInNockOut = TeamDAO.Instance.GetListTeam("Bảng đấu loại trực tiếp", mainWindow.league.id);
            mainWindow.btnScheduleChart.Content = "Tạo lịch thi đấu";
            mainWindow.btnCancelCreateScheduleChart.Visibility = Visibility.Visible;
            mainWindow.btnCancelCreateScheduleChart.Content = "Hủy";
            int size = 0;
            if (mainWindow.league.typeLeague == 2)
            {
                if (mainWindow.setting.NumberOfTeamIn <= 16)
                    size = 16;
                if (mainWindow.setting.NumberOfTeamIn <= 8)
                    size = 8;
                if (mainWindow.setting.NumberOfTeamIn <= 4)
                    size = 4;
            }
            else
            {
                if (mainWindow.league.countTeam <= 16)
                    size = 16;
                if (mainWindow.league.countTeam <= 8)
                    size = 8;
                if (mainWindow.league.countTeam <= 4)
                    size = 4;
            }
            comboBoxes.Clear();
            textBlockes.Clear();
            switch (size)
            {
                case 4:
                    comboBoxes.Add(mainWindow.cb4Team1);
                    comboBoxes.Add(mainWindow.cb4Team2);
                    comboBoxes.Add(mainWindow.cb4Team3);
                    comboBoxes.Add(mainWindow.cb4Team4);
                    textBlockes.Add(mainWindow.tbl4Team1);
                    textBlockes.Add(mainWindow.tbl4Team2);
                    textBlockes.Add(mainWindow.tbl4Team3);
                    break;
                case 8:
                    comboBoxes.Add(mainWindow.cb8Team1);
                    comboBoxes.Add(mainWindow.cb8Team2);
                    comboBoxes.Add(mainWindow.cb8Team3);
                    comboBoxes.Add(mainWindow.cb8Team4);
                    comboBoxes.Add(mainWindow.cb8Team5);
                    comboBoxes.Add(mainWindow.cb8Team6);
                    comboBoxes.Add(mainWindow.cb8Team7);
                    comboBoxes.Add(mainWindow.cb8Team8);
                    textBlockes.Add(mainWindow.tbl8Team1);
                    textBlockes.Add(mainWindow.tbl8Team2);
                    textBlockes.Add(mainWindow.tbl8Team5);
                    textBlockes.Add(mainWindow.tbl8Team3);
                    textBlockes.Add(mainWindow.tbl8Team4);
                    textBlockes.Add(mainWindow.tbl8Team6);
                    textBlockes.Add(mainWindow.tbl8Team7);
                    break;
                case 16:
                    comboBoxes.Add(mainWindow.cb16Team1);
                    comboBoxes.Add(mainWindow.cb16Team2);
                    comboBoxes.Add(mainWindow.cb16Team3);
                    comboBoxes.Add(mainWindow.cb16Team4);
                    comboBoxes.Add(mainWindow.cb16Team5);
                    comboBoxes.Add(mainWindow.cb16Team6);
                    comboBoxes.Add(mainWindow.cb16Team7);
                    comboBoxes.Add(mainWindow.cb16Team8);
                    comboBoxes.Add(mainWindow.cb16Team9);
                    comboBoxes.Add(mainWindow.cb16Team10);
                    comboBoxes.Add(mainWindow.cb16Team11);
                    comboBoxes.Add(mainWindow.cb16Team12);
                    comboBoxes.Add(mainWindow.cb16Team13);
                    comboBoxes.Add(mainWindow.cb16Team14);
                    comboBoxes.Add(mainWindow.cb16Team15);
                    comboBoxes.Add(mainWindow.cb16Team16);
                    textBlockes.Add(mainWindow.tbl16Team1);
                    textBlockes.Add(mainWindow.tbl16Team2);
                    textBlockes.Add(mainWindow.tbl16Team9);
                    textBlockes.Add(mainWindow.tbl16Team3);
                    textBlockes.Add(mainWindow.tbl16Team4);
                    textBlockes.Add(mainWindow.tbl16Team10);
                    textBlockes.Add(mainWindow.tbl16Team13);
                    textBlockes.Add(mainWindow.tbl16Team5);
                    textBlockes.Add(mainWindow.tbl16Team6);
                    textBlockes.Add(mainWindow.tbl16Team11);
                    textBlockes.Add(mainWindow.tbl16Team7);
                    textBlockes.Add(mainWindow.tbl16Team8);
                    textBlockes.Add(mainWindow.tbl16Team12);
                    textBlockes.Add(mainWindow.tbl16Team14);
                    textBlockes.Add(mainWindow.tbl16Team15);
                    break;
            }
            switch (size)
            {
                case 4:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule8.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule16.Visibility = Visibility.Hidden;
                    mainWindow.cb4Team1.Items.Clear(); mainWindow.cb4Team1.Items.Add("Ưu tiên");
                    mainWindow.cb4Team2.Items.Clear(); mainWindow.cb4Team2.Items.Add("Ưu tiên");
                    mainWindow.cb4Team3.Items.Clear(); mainWindow.cb4Team3.Items.Add("Ưu tiên");
                    mainWindow.cb4Team4.Items.Clear(); mainWindow.cb4Team4.Items.Add("Ưu tiên");
                    mainWindow.tbl4Team1.Text = "";
                    mainWindow.tbl4Team2.Text = "";
                    mainWindow.tbl4Team3.Text = "";
                    foreach (Team team in teamsInNockOut)
                    {
                        mainWindow.cb4Team1.Items.Add(team.nameTeam);
                        mainWindow.cb4Team2.Items.Add(team.nameTeam);
                        mainWindow.cb4Team3.Items.Add(team.nameTeam);
                        mainWindow.cb4Team4.Items.Add(team.nameTeam);
                    }
                    break;
                case 8:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule8.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule16.Visibility = Visibility.Hidden;
                    mainWindow.cb8Team1.Items.Clear(); mainWindow.cb8Team1.Items.Add("Ưu tiên");
                    mainWindow.cb8Team2.Items.Clear(); mainWindow.cb8Team2.Items.Add("Ưu tiên");
                    mainWindow.cb8Team3.Items.Clear(); mainWindow.cb8Team3.Items.Add("Ưu tiên");
                    mainWindow.cb8Team4.Items.Clear(); mainWindow.cb8Team4.Items.Add("Ưu tiên");
                    mainWindow.cb8Team5.Items.Clear(); mainWindow.cb8Team5.Items.Add("Ưu tiên");
                    mainWindow.cb8Team6.Items.Clear(); mainWindow.cb8Team6.Items.Add("Ưu tiên");
                    mainWindow.cb8Team7.Items.Clear(); mainWindow.cb8Team7.Items.Add("Ưu tiên");
                    mainWindow.cb8Team8.Items.Clear(); mainWindow.cb8Team8.Items.Add("Ưu tiên");
                    mainWindow.tbl8Team1.Text = "";
                    mainWindow.tbl8Team2.Text = "";
                    mainWindow.tbl8Team3.Text = "";
                    mainWindow.tbl8Team4.Text = "";
                    mainWindow.tbl8Team5.Text = "";
                    mainWindow.tbl8Team6.Text = "";
                    mainWindow.tbl8Team7.Text = "";
                    foreach (Team team in teamsInNockOut)
                    {
                        mainWindow.cb8Team1.Items.Add(team.nameTeam);
                        mainWindow.cb8Team2.Items.Add(team.nameTeam);
                        mainWindow.cb8Team3.Items.Add(team.nameTeam);
                        mainWindow.cb8Team4.Items.Add(team.nameTeam);
                        mainWindow.cb8Team5.Items.Add(team.nameTeam);
                        mainWindow.cb8Team6.Items.Add(team.nameTeam);
                        mainWindow.cb8Team7.Items.Add(team.nameTeam);
                        mainWindow.cb8Team8.Items.Add(team.nameTeam);
                    }
                    break;
                case 16:
                    mainWindow.grdScheduleScreen.Visibility = Visibility.Hidden;
                    mainWindow.grdScheduleChart.Visibility = Visibility.Visible;
                    mainWindow.grdSchedule4.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule8.Visibility = Visibility.Hidden;
                    mainWindow.grdSchedule16.Visibility = Visibility.Visible;
                    mainWindow.cb16Team1.Items.Clear(); mainWindow.cb16Team1.Items.Add("Ưu tiên");
                    mainWindow.cb16Team2.Items.Clear(); mainWindow.cb16Team2.Items.Add("Ưu tiên");
                    mainWindow.cb16Team3.Items.Clear(); mainWindow.cb16Team3.Items.Add("Ưu tiên");
                    mainWindow.cb16Team4.Items.Clear(); mainWindow.cb16Team4.Items.Add("Ưu tiên");
                    mainWindow.cb16Team5.Items.Clear(); mainWindow.cb16Team5.Items.Add("Ưu tiên");
                    mainWindow.cb16Team6.Items.Clear(); mainWindow.cb16Team6.Items.Add("Ưu tiên");
                    mainWindow.cb16Team7.Items.Clear(); mainWindow.cb16Team7.Items.Add("Ưu tiên");
                    mainWindow.cb16Team8.Items.Clear(); mainWindow.cb16Team8.Items.Add("Ưu tiên");
                    mainWindow.cb16Team9.Items.Clear(); mainWindow.cb16Team9.Items.Add("Ưu tiên");
                    mainWindow.cb16Team10.Items.Clear(); mainWindow.cb16Team10.Items.Add("Ưu tiên");
                    mainWindow.cb16Team11.Items.Clear(); mainWindow.cb16Team11.Items.Add("Ưu tiên");
                    mainWindow.cb16Team12.Items.Clear(); mainWindow.cb16Team12.Items.Add("Ưu tiên");
                    mainWindow.cb16Team13.Items.Clear(); mainWindow.cb16Team13.Items.Add("Ưu tiên");
                    mainWindow.cb16Team14.Items.Clear(); mainWindow.cb16Team14.Items.Add("Ưu tiên");
                    mainWindow.cb16Team15.Items.Clear(); mainWindow.cb16Team15.Items.Add("Ưu tiên");
                    mainWindow.cb16Team16.Items.Clear(); mainWindow.cb16Team16.Items.Add("Ưu tiên");
                    mainWindow.tbl16Team1.Text = "";
                    mainWindow.tbl16Team2.Text = "";
                    mainWindow.tbl16Team3.Text = "";
                    mainWindow.tbl16Team4.Text = "";
                    mainWindow.tbl16Team5.Text = "";
                    mainWindow.tbl16Team6.Text = "";
                    mainWindow.tbl16Team7.Text = "";
                    mainWindow.tbl16Team8.Text = "";
                    mainWindow.tbl16Team9.Text = "";
                    mainWindow.tbl16Team10.Text = "";
                    mainWindow.tbl16Team11.Text = "";
                    mainWindow.tbl16Team12.Text = "";
                    mainWindow.tbl16Team13.Text = "";
                    mainWindow.tbl16Team14.Text = "";
                    mainWindow.tbl16Team15.Text = "";
                    foreach (Team team in teamsInNockOut)
                    {
                        mainWindow.cb16Team1.Items.Add(team.nameTeam);
                        mainWindow.cb16Team2.Items.Add(team.nameTeam);
                        mainWindow.cb16Team3.Items.Add(team.nameTeam);
                        mainWindow.cb16Team4.Items.Add(team.nameTeam);
                        mainWindow.cb16Team5.Items.Add(team.nameTeam);
                        mainWindow.cb16Team6.Items.Add(team.nameTeam);
                        mainWindow.cb16Team7.Items.Add(team.nameTeam);
                        mainWindow.cb16Team8.Items.Add(team.nameTeam);
                        mainWindow.cb16Team9.Items.Add(team.nameTeam);
                        mainWindow.cb16Team10.Items.Add(team.nameTeam);
                        mainWindow.cb16Team11.Items.Add(team.nameTeam);
                        mainWindow.cb16Team12.Items.Add(team.nameTeam);
                        mainWindow.cb16Team13.Items.Add(team.nameTeam);
                        mainWindow.cb16Team14.Items.Add(team.nameTeam);
                        mainWindow.cb16Team15.Items.Add(team.nameTeam);
                        mainWindow.cb16Team16.Items.Add(team.nameTeam);
                    }
                    break;
            }
        }
        void LoadCBXBoard(MainWindow mainWindow)
        {
            mainWindow.cbxBoard.Items.Clear();
            mainWindow.cbxBoard.Items.Add("Tất cả bảng");
            mainWindow.cbxBoard.SelectedIndex = 0;
            List<Board> boards = BoardDAO.Instance.GetListBoard(mainWindow.league.id);
            foreach (Board board in boards)
            {
                //if (board.nameBoard)
                mainWindow.cbxBoard.Items.Add(board.nameBoard);
            }
        }
        #endregion

        #region Tạo lịch thi đấu
        // Tạo lịch thi đấu
        public void CreateSchedule(MainWindow mainWindow)
        {
            // Tiến hành tạo lịch
            if (MatchDAO.Instance.HaveMatch(mainWindow.league.id))
            {
                if (mainWindow.league.typeLeague == 0 || mainWindow.league.typeLeague == 1)
                {
                    MessageBoxWindow wd = new MessageBoxWindow(false, "Đã có lịch thi đấu");
                    wd.ShowDialog();
                    return;
                }
                if (mainWindow.league.typeLeague == 2 &&
                    (!BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id) ||
                       (BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id) && TreeMatchDAO.Instance.GetTree(mainWindow.league.id) != null)))
                {
                    MessageBoxWindow wd = new MessageBoxWindow(false, "Đã có lịch thi đấu");
                    wd.ShowDialog();
                    return;
                }

            }

            ConfirmDialogWindow wdd = new ConfirmDialogWindow("Sau khi tiến hành tạo lịch, các thông tin về Câu lạc bộ, Cầu thủ sẽ không được phép thay đổi nữa!\n" +
                "Bạn có muốn tạo lịch thi đấu?");
            wdd.ShowDialog();
            if (wdd.confirm == false)
            {
                return;
            }
            else
            {

                // trường hợp đấu vòng tròn
                if (mainWindow.league.typeLeague == 0)
                {
                    // Hàm tạo lịch theo Vòng tròn tính điểm
                    CreateScheduleWithCircle(mainWindow);
                }
                // trường hợp đấu bảng

                if (mainWindow.league.typeLeague == 2)
                {
                    // Hàm tạo lịch theo bảng đấu
                    if (BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id))
                        OpenNockOutScreen(mainWindow);
                    else
                        CreateScheduleWithBoard(mainWindow);
                }

                // trường hợp Đấu loại
                if (mainWindow.league.typeLeague == 1)
                {
                    // Hàm tạo lịch theo bảng đấu
                    OpenNockOutScreen(mainWindow);
                }

                // Thay đổi status của giải đấu = 2 (Đã bắt đầu khởi tranh)
                LeagueDAO.Instance.UpdateStatusOfLeague(mainWindow.league.id, 2);

                if (mainWindow.league.typeLeague != 1 && !BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id))
                {
                    MessageBoxWindow wd = new MessageBoxWindow(true, "Tạo lịch thi đấu thành công");
                    mainWindow.btnEditLeague.IsEnabled = false;
                    wd.ShowDialog();
                    mainWindow.btnCreateSchedule.IsEnabled = false;
                }

                LoadListMatch(mainWindow, 0, mainWindow.cbxBoard.Text);
            }
        }

        static List<Match> CreateListMatch(MainWindow mainWindow, List<Team> Teams, string nameBoard)
        {
            List<Match> matches = new List<Match>();

            List<Team> Team1 = new List<Team>();
            List<Team> Team2 = new List<Team>();
            int countOfTeam = Teams.Count;
            int countOfMatch = (countOfTeam * (countOfTeam - 1)) / 2;

            if (countOfTeam <= 1)
            {
                MessageBox.Show("Số lượng đội bóng không đủ đê tạo lịch", "Lỗi");
                return matches;
            }

            int[] arr = new int[200];
            bool check = true; // check = true neu n chan va nguoc lai

            if (countOfTeam % 2 == 1)
            {
                countOfTeam++;
                check = false;
            }

            int center = countOfTeam - 1;
            for (int i = 0; i < center * 3; i++)
            {
                arr[i] = i % center;
            }

            for (int i = center; i < center * 2; i++)
            {
                if (check == true)
                {
                    Team1.Add(Teams[center]);
                    Team2.Add(Teams[arr[i]]);
                }

                int l = i - 1;
                int r = i + 1;

                for (int j = 0; j < countOfTeam / 2 - 1; j++)
                {
                    Team1.Add(Teams[arr[l]]);
                    Team2.Add(Teams[arr[r]]);
                    l--;
                    r++;
                }
            }

            int round = 0;
            if (check == false) countOfTeam--;

            // Luot di
            for (int i = 0; i < countOfMatch; i++)
            {
                if (i % (countOfTeam / 2) == 0) round++;

                Match match = new Match(mainWindow.league.id, Team1[i].id, Team2[i].id, round, Team1[i].stadium, nameBoard);
                match.allowDraw = true;
                match.date = DateTime.Now;
                match.time = DateTime.Now;

                matches.Add(match);
            }
            // Luot ve
            for (int i = 0; i < countOfMatch; i++)
            {
                if (i % (countOfTeam / 2) == 0) round++;

                Match match = new Match(mainWindow.league.id, Team2[i].id, Team1[i].id, round, Team2[i].stadium, nameBoard);
                match.allowDraw = true;
                match.date = DateTime.Now;
                match.time = DateTime.Now;

                matches.Add(match);
            }



            return matches;
        }
        // Tạo lịch thi đấu vòng tròn tại mỗi bảng đấu (Cho trường hợp giải đấu chia bảng)
        public void CreateScheduleWithBoard(MainWindow mainWindow)
        {
            List<Board> boards = BoardDAO.Instance.GetListBoard(mainWindow.league.id);

            int nBoard = boards.Count;

            // Duyệt qua mỗi bảng đấu và tạo lịch
            for (int iBoard = 0; iBoard < nBoard; iBoard++)
            {
                List<Team> teams = TeamDAO.Instance.GetListTeam(boards[iBoard].nameBoard, mainWindow.league.id);

                string nameBoard = boards[iBoard].nameBoard;

                List<Match> matches = CreateListMatch(mainWindow, teams, nameBoard);

                for (int i = 0; i < matches.Count; i++)
                {
                    MatchDAO.Instance.AddMatch(matches[i]);
                }

            }
        }

        // Tạo lịch thi đấu với cách vòng tròn tính điểm (Cho trường hợp giải đấu vòng tròn tính điểm)
        public void CreateScheduleWithCircle(MainWindow mainWindow)
        {
            List<Team> teams = TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id);

            List<Match> matches = CreateListMatch(mainWindow, teams, "Bảng đấu vòng");

            for (int i = 0; i < matches.Count; i++)
            {
                MatchDAO.Instance.AddMatch(matches[i]);
            }
        }
        public void AddItemsForCbxRound(MainWindow mainWindow)
        {
            mainWindow.cbxRound.Items.Clear();
            mainWindow.cbxRound.Items.Add("Tất cả vòng");
            List<string> rounds = MatchDAO.Instance.GetListRoundInLeague(mainWindow.league.id);
            for (int i = 0; i < rounds.Count; i++)
            {
                int tmp = int.Parse(rounds[i]);
                if (tmp > 0)
                    mainWindow.cbxRound.Items.Add("Vòng " + tmp.ToString());
            }
            //
            for (int i = 0; i < rounds.Count; i++)
            {
                int tmp = int.Parse(rounds[i]);
                if (tmp < 0)
                    switch (-tmp)
                    {
                        case 1:
                            mainWindow.cbxRound.Items.Add("Vòng Chung kết");
                            break;
                        case 2:
                            mainWindow.cbxRound.Items.Add("Vòng Bán kết");
                            break;
                        case 3:
                            mainWindow.cbxRound.Items.Add("Vòng Tứ kết");
                            break;
                        case 4:
                            mainWindow.cbxRound.Items.Add("Vòng 1/8");
                            break;
                    }
                else
                    break;
            }
            mainWindow.cbxRound.SelectedIndex = 0;
        }

        // Hủy kết quả trận đấu
        public void CancelResultMatch(MainWindow mainWindow, Match match)
        {
            ResultRecordingWindow resultWD = new ResultRecordingWindow(match);

            resultWD.DeleteOldInfor();

            LoadListMatchRound(mainWindow, mainWindow.cbxRound.SelectedItem.ToString(), mainWindow.cbxBoard.Text);
        }

        // Hiển thị danh sách trận đấu
        List<Match> listMatches;
        public void LoadListMatch(MainWindow mainWindow, int round, string board)
        {
            mainWindow.wpSchedule.Children.Clear();
            if (round == 0)
            {
                mainWindow.cbxRound.SelectedIndex = 0;
                if (board == "Tất cả bảng")
                {
                    mainWindow.cbxBoard.SelectedIndex = 0;
                    listMatches = MatchDAO.Instance.GetListMatch(mainWindow.league.id);
                }
                else
                    listMatches = MatchDAO.Instance.GetListMatchByBoard(mainWindow.league.id, board);
            }
            else
            {
                if (board == "Tất cả bảng")
                {
                    mainWindow.cbxBoard.SelectedIndex = 0;
                    listMatches = MatchDAO.Instance.GetListMatchByRound(mainWindow.league.id, round);
                }
                else
                    listMatches = MatchDAO.Instance.GetListMatchByBoardAndRound(mainWindow.league.id, round, board);
            }
            int i = 0;
            bool canEdit = true;
            if (mainWindow.league.typeLeague == 2 && BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id))
                canEdit = false;

            foreach (Match match in listMatches)
            {
                i++;
                if (mainWindow.currentAccount.roleLevel != 1 && mainWindow.currentAccount.roleLevel != 3)
                {
                    ucMatchDetail ucmatchDetail = new ucMatchDetail(i, match, mainWindow, this, false);
                    mainWindow.wpSchedule.Children.Add(ucmatchDetail);
                }
                else
                if (canEdit || match.nameBoard == "Bảng đấu loại trực tiếp")
                {
                    ucMatchDetail ucmatchDetail = new ucMatchDetail(i, match, mainWindow, this, true);
                    mainWindow.wpSchedule.Children.Add(ucmatchDetail);
                }
                else
                {
                    ucMatchDetail ucmatchDetail = new ucMatchDetail(i, match, mainWindow, this, canEdit);
                    mainWindow.wpSchedule.Children.Add(ucmatchDetail);
                }
            }

        }

        public void ChangeCbxRound(MainWindow mainWindow)
        {
            try
            {
                LoadListMatchRound(mainWindow, mainWindow.cbxRound.Items[mainWindow.cbxRound.SelectedIndex].ToString(), mainWindow.cbxBoard.Text);
            }
            catch
            {

            }
        }
        public void LoadListMatchRound(MainWindow mainWindow, string round, string board)
        {
            try
            {
                int r = Int32.Parse(round[round.Length - 1].ToString());
                if (round[round.Length - 2] != ' ')
                {
                    int a = Int32.Parse(round[round.Length - 2].ToString());
                    r = a * 10 + r;
                }

                LoadListMatch(mainWindow, r, board);
            }
            catch
            {
                switch (round)
                {
                    case "Tất cả vòng":
                        LoadListMatch(mainWindow, 0, board);
                        break;
                    case "Vòng 1/8 ":
                        LoadListMatch(mainWindow, -4, board);
                        break;
                    case "Vòng tứ kết":
                        LoadListMatch(mainWindow, -3, board);
                        break;
                    case "Vòng bán kết":
                        LoadListMatch(mainWindow, -2, board);
                        break;
                    case "Vòng chung kết":
                        LoadListMatch(mainWindow, -1, board);
                        break;
                }
            }
        }

        public void OpenEditMatchInfoWindow(MainWindow mainWindow, Match match)
        {
            EditMatchInforWindow wd = new EditMatchInforWindow(match);
            wd.ShowDialog();

            LoadListMatchRound(mainWindow, mainWindow.cbxRound.Items[mainWindow.cbxRound.SelectedIndex].ToString(), mainWindow.cbxBoard.Text);
        }
        public void OpenResultRecordingWindow(MainWindow mainWindow, Match match)
        {
            ResultRecordingWindow wd = new ResultRecordingWindow(match);
            wd.ShowDialog();
            LoadListMatchRound(mainWindow, mainWindow.cbxRound.SelectedItem.ToString(), mainWindow.cbxBoard.Text);
        }
        #endregion

        #region Setting (Quy Định)

        public void GetDetailSetting(MainWindow mainWindow)
        {
            if (mainWindow.setting != null)
            {
                mainWindow.tblCountOfTeams.Text = mainWindow.setting.numberOfTeam.ToString();
                mainWindow.tblMinCountPlayers.Text = mainWindow.setting.minPlayerOfTeam.ToString();
                mainWindow.tblMaxCountPlayers.Text = mainWindow.setting.maxPlayerOfTeam.ToString();
                mainWindow.tblMinAge.Text = mainWindow.setting.minAge.ToString();
                mainWindow.tblMaxAge.Text = mainWindow.setting.maxAge.ToString();
                mainWindow.tblMaxForeign.Text = mainWindow.setting.maxForeignPlayers.ToString();
                mainWindow.tbScoreWin.Text = mainWindow.setting.scoreWin.ToString();
                mainWindow.tbScoreDraw.Text = mainWindow.setting.scoreDraw.ToString();
                mainWindow.tbScoreLose.Text = mainWindow.setting.scoreLose.ToString();
                mainWindow.tbNumberOfTeamsIn.Text = mainWindow.setting.NumberOfTeamIn.ToString();
            }
        }
        public void LoadTypesOfGoal(MainWindow mainWindow)
        {
            List<TypeOfGoal> data = new List<TypeOfGoal>();
            data = TypeOfGoalDAO.Instance.GetListTypeOfGoal(mainWindow.league.id);
            int i = 1;
            foreach (TypeOfGoal typeG in data.ToArray())
            {
                typeG.id = i;
                i++;
            }
            mainWindow.dgvTypeOfGoal.ItemsSource = data;
        }
        public void OpenEditDialogWindow(MainWindow mainWindow)
        {
            int index = Int32.Parse(idSetting);
            int idTournament = mainWindow.league.id;
            Setting curSetting = mainWindow.setting;
            EditDialogWindow wd = new EditDialogWindow(idTournament, index, curSetting);
            wd.ShowDialog();
            List<Board> boards = BoardDAO.Instance.GetListBoard(mainWindow.league.id);

            mainWindow.setting = SettingDAO.Instance.GetSetting(idTournament);
            mainWindow.league.countTeam = mainWindow.setting.numberOfTeam;
            
            foreach (Board board in boards)
            {
                board.countTeam = mainWindow.league.countTeam / boards.Count;
                if (mainWindow.league.countTeam % boards.Count > 0)
                    board.countTeam++;
                BoardDAO.Instance.Update(board);
            }
            mainWindow.boards = boards;
            GetDetailSetting(mainWindow);
        }
        public void OpenAddGoalTypeWindow(MainWindow mainWindow)
        {
            AddGoalTypeWindow wd = new AddGoalTypeWindow(mainWindow, "");
            wd.ShowDialog();
            LoadTypesOfGoal(mainWindow);
        }
        public void OpenEditGoalTypeWindow(MainWindow mainWindow)
        {
            if (mainWindow.dgvTypeOfGoal.SelectedIndex == -1)
            {
                MessageBoxWindow msbwd = new MessageBoxWindow(false, "Vui lòng chọn loại bàn thắng cần sửa");
                msbwd.ShowDialog();
                return;
            }
            string name = (mainWindow.dgvTypeOfGoal.SelectedItem as TypeOfGoal).displayName;
            AddGoalTypeWindow wd = new AddGoalTypeWindow(mainWindow, name);
            wd.Title = "Sửa thông tin";
            wd.btnAdd.Content = "Xác nhận";
            wd.ShowDialog();
            LoadTypesOfGoal(mainWindow);
        }
        public void DeleteGoalType(MainWindow mainWindow)
        {
            if (mainWindow.dgvTypeOfGoal.SelectedIndex == -1)
            {
                MessageBoxWindow msbwd = new MessageBoxWindow(false, "Vui lòng chọn loại bàn thắng cần xoá");
                msbwd.ShowDialog();
                return;
            }
            string name = (mainWindow.dgvTypeOfGoal.SelectedItem as TypeOfGoal).displayName;
            ConfirmDialogWindow wd = new ConfirmDialogWindow("Bạn có muốn xoá loại bàn thắng \"" + name + "\" ?" );
            wd.ShowDialog();
            if (wd.confirm == false)
            {
                return;
            }
            else
            {
                try
                {
                    TypeOfGoalDAO.Instance.DeleteTypeGoal(mainWindow.league.id, name);
                    MessageBoxWindow msbwd = new MessageBoxWindow(true, "Xoá loại bàn thắng thành công");
                    msbwd.ShowDialog();
                }
                catch
                {
                    MessageBoxWindow msbwd = new MessageBoxWindow(false, "Lỗi kết nối");
                    msbwd.ShowDialog();
                    return;
                }
                LoadTypesOfGoal(mainWindow);
            }
        }
        #endregion

        #region Trợ giúp
        public void HowToUse()
        {
            var ps = new ProcessStartInfo("https://github.com/luftmensch2001/WPF-FC-Management")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }
        public void SendError()
        {
            var ps = new ProcessStartInfo("https://docs.google.com/forms/d/1XVu3sWGxHIH8NUHQW_aZ8Q6B8bX6A25LuaaZqkFpi1E/viewform?edit_requested=true")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }
        public void ContactToDeveloper()
        {
            var ps = new ProcessStartInfo("https://www.facebook.com/tam.oix")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }
        
        #endregion

        #region Ranking
        const string Pts = "Điểm";
        const string GD = "Hiệu số bàn thắng";
        const string Vs = "Hiệu số đối đầu";
        const string Fg = "Tổng số bàn thắng";
        const string Wn = "Tổng số trận thắng";
        string pathProject;

        System.Drawing.Image imgWin;
        System.Drawing.Image imgDraw;
        System.Drawing.Image imgLose;
        System.Drawing.Image imgEmpty;

        void CreateBoardKnockOut(MainWindow mainWindow)
        {
            List<Match> matches = MatchDAO.Instance.GetListMatch(mainWindow.league.id);
            if (MatchDAO.Instance.GetCountMatchWait(mainWindow.league.id) > 0 || matches.Count == 0)
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Chưa hoàn thành vòng bảng");
                wd.ShowDialog();
                return;
            }

            ConfirmDialogWindow wdd = new ConfirmDialogWindow("Sau khi bắt đầu vòng trong sẽ không cho phép sửa các trận đấu vòng bảng \n Xác nhận bắt đầu?");
            wdd.ShowDialog();
            if (wdd.confirm == false)
            {
                return;
            }
            if (mainWindow.league.typeLeague == 0 || mainWindow.league.typeLeague == 1)
                return;
            if (BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id))
            {
                return;
            }
            int cntBoard = mainWindow.league.countBoard;
            if (cntBoard == 0)
                return;
            GetDetailSetting(mainWindow);
            int teamIn = mainWindow.setting.NumberOfTeamIn;
            int teamPerGroupIn = teamIn / cntBoard;
            int slotLeft = teamIn - teamPerGroupIn * cntBoard;
            List<string> listName = new List<string>();
            List<TeamScoreDetails> listTeamCalc = new List<TeamScoreDetails>();

            try
            {
                // Get list top
                List<Board> boards = BoardDAO.Instance.GetListBoard(mainWindow.league.id);
                for (int i = 0; i < cntBoard; i++)
                {
                    string nameBoard = boards[i].nameBoard.ToString();
                    List<TeamScoreDetails> list = CalcDetails(mainWindow, nameBoard);
                    list = CalcRanking(mainWindow.league.id, list);
                    for (int ii = 0; ii < teamPerGroupIn; ii++)
                    {
                        listName.Add(list[ii].nameTeam);
                    }
                    if (list.Count > teamPerGroupIn)
                        listTeamCalc.Add(list[teamPerGroupIn]);
                }
                //Get list left
                if (slotLeft > 0)
                {
                    listTeamCalc = CalcRanking(mainWindow.league.id, listTeamCalc);
                    for (int i = 0; i < slotLeft; i++)
                        if (i < listTeamCalc.Count)
                            listName.Add(listTeamCalc[i].nameTeam);
                }
                //Add to board
                //BoardDAO.Instance.DeleteKOBoard(mainWindow.league.id);
                Board board = new Board(mainWindow.league.id, "Bảng đấu loại trực tiếp", listName.Count);
                BoardDAO.Instance.CreateBoard(board);
                for (int i = 0; i < listName.Count; i++)
                {
                    int idTeam = TeamDAO.Instance.GetTeamIDByName(mainWindow.league.id, listName[i]);
                    Team team = TeamDAO.Instance.GetTeamById(idTeam);
                    team.nameBoard = "Bảng đấu loại trực tiếp";
                    TeamDAO.Instance.UpdateTeam(team);
                }
                if (BoardDAO.Instance.HaveNockOutBoard(mainWindow.league.id))
                {
                    mainWindow.btnShowChart.Width = 140;
                    mainWindow.btnShowChart.Content = "Xem biểu đồ";
                }
                MessageBoxWindow wd = new MessageBoxWindow(true, "Tạo danh sách vào vòng loại trực tiếp thành công");
                wd.ShowDialog();

                LoadCBXBoard(mainWindow);
                AddItemsForCbxRound(mainWindow);
                LoadListMatchRound(mainWindow, "Tất cả vòng", "Tất cả bảng");
                mainWindow.btnCreateSchedule.IsEnabled = true;
            }
            catch
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Lỗi kết nối dữ liệu");
                wd.ShowDialog();
            }
        }
        void ExportRanking(MainWindow mainWindow)
        {
            if (mainWindow.cbSelectedGroupsStanding.SelectedItem == null)
                return;
            string nameBoard = "";
            if (mainWindow.league.countBoard > 1)
                nameBoard = mainWindow.cbSelectedGroupsStanding.SelectedItem.ToString();
            List<TeamScoreDetails> rank = CalcDetails(mainWindow, nameBoard);
            rank = CalcRanking(mainWindow.league.id, rank);
            PDFProcessing.Instance.ExportRankingToPdf(mainWindow.dgvRanking, rank, " " + nameBoard);
        }
        void InitCbbRanking(MainWindow mainWindow)
        {
            //Load Board
            mainWindow.cbSelectedGroupsStanding.Items.Clear();
            foreach (Board board in mainWindow.boards)
            {
                mainWindow.cbSelectedGroupsStanding.Items.Add(board.nameBoard);
            }
            if (mainWindow.boards.Count > 1)
                mainWindow.cbSelectedGroupsStanding.Visibility = Visibility.Visible;
            else
                mainWindow.cbSelectedGroupsStanding.Visibility = Visibility.Hidden;
            if (mainWindow.boards.Count >= 0)
                mainWindow.cbSelectedGroupsStanding.SelectedIndex = 0;
        }
        void LoadRanking(MainWindow mainWindow)
        {
            try
            {
                if (mainWindow.cbSelectedGroupsStanding.SelectedItem == null)
                    return;
                string nameBoard = mainWindow.cbSelectedGroupsStanding.SelectedItem.ToString();
                GetDetailSetting(mainWindow);
                List<TeamScoreDetails> rank = CalcDetails(mainWindow, nameBoard);
                rank = CalcRanking(mainWindow.league.id, rank);
                foreach (TeamScoreDetails t in rank)
                {
                    t.imageFLM = ImageProcessing.Instance.Convert(ResToImageFLM(t.fLM));
                }
                mainWindow.dgvRanking.ItemsSource = rank;
            }
            catch
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Lỗi kết nối dữ liệu");
                wd.ShowDialog();
            }
        }
        List<TeamScoreDetails> CalcDetails(MainWindow mainWindow, string nameBoard)
        {
            List<TeamScoreDetails> list = new List<TeamScoreDetails>();
            List<Team> team = new List<Team>();
            if (nameBoard == "")
                team = TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id);
            else
                team = TeamDAO.Instance.GetListTeam(nameBoard, mainWindow.league.id);
            int i = -1;
            foreach (Team t in team)
            {
                BitmapImage logoteam = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(t.logo));
                list.Add(new TeamScoreDetails(t.nameTeam, logoteam));
                i++;
                List<Match> matches = MatchDAO.Instance.GetListMatchStartedByIDTeamWithOrder(t.idTournamnt, t.id);

                //Tính 
                foreach (Match m in matches)
                {

                    int gF = 0; //Bàn thắng
                    int gA = 0; //Bàn thua

                    if (m.idTeam01 == t.id)
                    {
                        gF = m.Score1;
                        gA = m.Score2;
                    }
                    else
                    {
                        gF = m.Score2;
                        gA = m.Score1;
                    }

                    // + điểm
                    if (gF > gA)
                        list[i].pts += mainWindow.setting.scoreWin;
                    if (gF == gA)
                        list[i].pts += mainWindow.setting.scoreDraw;
                    if (gF < gA)
                        list[i].pts += mainWindow.setting.scoreLose;

                    list[i].CalcDetails(gF, gA); //Tính hiệu số

                }

                list[i].imageFLM = ImageProcessing.Instance.Convert(ResToImageFLM(list[i].fLM));
            }
            return list;
        }
        List<TeamScoreDetails> CalcRanking(int idTournament, List<TeamScoreDetails> listTeamScoreDetails)
        {
            List<string> listPriorRank = new List<string>();
            listPriorRank.Add(Pts);
            listPriorRank.Add(GD);
            listPriorRank.Add(Vs);
            listPriorRank.Add(Fg);
            listPriorRank.Add(Wn);
            TeamScoreDetails tmp;
            int index = listTeamScoreDetails.Count;
            for (int i = 0; i < index - 1; i++)
                for (int j = i + 1; j < index; j++)
                {
                    foreach (string condition in listPriorRank)
                    {
                        int res = CheckCondition(condition, listTeamScoreDetails[i], listTeamScoreDetails[j], idTournament);
                        if (res == 0)
                            continue;
                        if (res == 2)
                        {
                            tmp = listTeamScoreDetails[i];
                            listTeamScoreDetails[i] = listTeamScoreDetails[j];
                            listTeamScoreDetails[j] = tmp;
                        }
                        break;
                    }
                }
            for (int i = 0; i < listTeamScoreDetails.Count; i++)
                listTeamScoreDetails[i].rankTeam = i + 1;
            return listTeamScoreDetails;
        }
        int CheckCondition(string condition, TeamScoreDetails team1, TeamScoreDetails team2, int idTournament)
        {
            switch (condition)
            {
                case Pts:
                    if (team1.pts == team2.pts)
                        return 0;
                    if (team1.pts < team2.pts)
                        return 2;
                    break;
                case GD:
                    if (team1.gD == team2.gD)
                        return 0;
                    if (team1.gD < team2.gD)
                        return 2;
                    break;
                case Fg:
                    if (team1.f == team2.f)
                        return 0;
                    if (team1.f < team2.f)
                        return 2;
                    break;
                case Wn:
                    if (team1.w == team2.w)
                        return 0;
                    if (team1.w < team2.w)
                        return 2;
                    break;
                case Vs:
                    return CheckVersus(idTournament, team1.nameTeam, team2.nameTeam);
            }
            return 1;
        }
        int CheckVersus(int idTournament, string nameTeam1, string nameTeam2)
        {
            int idTeam1 = TeamDAO.Instance.GetTeamIDByName(idTournament, nameTeam1);
            int idTeam2 = TeamDAO.Instance.GetTeamIDByName(idTournament, nameTeam2);
            List<Match> matchid1 = MatchDAO.Instance.GetListMatchStartedByID2Team(idTournament, idTeam1, idTeam2);
            List<Match> matchid2 = MatchDAO.Instance.GetListMatchStartedByID2Team(idTournament, idTeam2, idTeam1);
            int gFH1 = 0; //Bàn thắng sân nhà team 1
            int gFH2 = 0; //Bàn thắng sân nhà team 2
            int gAH1 = 0; //Bàn thua sân nhà team 1
            int gAH2 = 0; //Bàn thua sân nhà team 2
            int W1 = 0; //Trận thắng team 1
            int W2 = 0; //Trận thắng team 2
            foreach (Match mid in matchid1) //Team 1 chủ nhà
            {
                int gF = mid.Score1;
                int gA = mid.Score2;

                if (gF > gA)
                {
                    W1++;
                }
                else
                {
                    W2++;
                }
                gFH1 += gF;
                gAH1 += gA;
            }
            foreach (Match mid in matchid2) //Team 2 chủ nhà
            {
                int gF = mid.Score1;
                int gA = mid.Score2;

                if (gF > gA)
                {
                    W2++;
                }
                else
                {
                    W1++;
                }
                gFH2 += gF;
                gAH2 += gA;
            }
            if (W1 < W2)
                return 2;
            if (W1 == W2)
            {
                if (gAH1 > gAH2)
                    return 2;
            }
            return 0;
        }
        System.Drawing.Image ResToImageFLM(string res)
        {
            if (imgWin == null)
                GetImageResultMatch();
            if (imgWin == null)
            {
                return null;
            }    
            int space = 2;
            int width = imgWin.Width * 6 + space * 2;
            int height = imgWin.Width + space * 2;

            System.Drawing.Bitmap imgres = new System.Drawing.Bitmap(width, height);
            Graphics g = Graphics.FromImage(imgres);

            for (int i = 0; i < 5; i++)
            {
                //get image
                System.Drawing.Image imgResOfmatch = imgWin;
                if (i > res.Length - 1)
                    imgResOfmatch = imgEmpty;
                else
                {
                    if (res[i] == 'V')
                        imgResOfmatch = imgWin;
                    if (res[i] == '-')
                        imgResOfmatch = imgDraw;
                    if (res[i] == 'X')
                        imgResOfmatch = imgLose;
                }
                //draw image
                g.DrawImage(imgResOfmatch, new System.Drawing.Point(i * height + space, 2));
            }

            g.Dispose();

            return imgres;
        }
        void GetImageResultMatch()
        {
            //Get Image
            pathProject = System.IO.Directory.GetCurrentDirectory();
            try
            {
                while (!File.Exists(pathProject + @"\Resource\Images\imgEmpty.png") && System.IO.Directory.GetParent(pathProject) != null)
                {
                    pathProject = System.IO.Directory.GetParent(pathProject).ToString();
                }
                imgWin = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgWin.png");
                imgDraw = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgDraw.png");
                imgLose = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgLose.png");
                imgEmpty = System.Drawing.Image.FromFile(pathProject + @"\Resource\Images\imgEmpty.png");
            }
            catch
            {

                MessageBoxWindow wd = new MessageBoxWindow(false, "Không tìm thấy file, vui lòng liên hệ hỗ trợ hoặc cài đặt lại phần mềm");
                wd.ShowDialog();
                return;
            }
        }
        #endregion

        #region Stactistic

        void FilterStatisticClick(MainWindow mainWindow)
        {
            if (mainWindow.grdSttTeams.Visibility == Visibility.Visible)
                mainWindow.dgvStatisticsTeams.ItemsSource = SttTeam(mainWindow);
            else
            if (mainWindow.grdSttPlayers.Visibility == Visibility.Visible)
                mainWindow.dgvStatisticsPlayers.ItemsSource = SttPlayer(mainWindow);
            else
            if (mainWindow.grdSttCards.Visibility == Visibility.Visible)
                mainWindow.dgvStatisticsCards.ItemsSource = SttCard(mainWindow);
        }
        void ExportStatistic(MainWindow mainWindow)
        {
            if (mainWindow.grdSttTeams.Visibility == Visibility.Visible)
                PDFProcessing.Instance.ExportTeamStatistic(mainWindow.dgvStatisticsTeams, SttTeam(mainWindow), mainWindow.cbSelectedRound.SelectedItem.ToString());
            else
            if (mainWindow.grdSttPlayers.Visibility == Visibility.Visible)
                PDFProcessing.Instance.ExportPlayerStatistic(mainWindow.dgvStatisticsPlayers, SttPlayer(mainWindow), mainWindow.cbSelectedRound.SelectedItem.ToString());
            else
            if (mainWindow.grdSttCards.Visibility == Visibility.Visible)
                PDFProcessing.Instance.ExportCardStatistic(mainWindow.dgvStatisticsCards, SttCard(mainWindow), mainWindow.cbSelectedTeam.SelectedItem.ToString());
        }
        public void SwitchTabStatistics(MainWindow mainWindow)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Tab
            mainWindow.rtStroke.Margin = new Thickness((20 + 120 * index), 0, 0, 5);

            // Reset color
            mainWindow.btnSttTeams.Foreground = white;
            mainWindow.btnSttPlayers.Foreground = white;
            mainWindow.btnSttCards.Foreground = white;

            // Hide all screens
            mainWindow.grdSttTeams.Visibility = Visibility.Hidden;
            mainWindow.grdSttPlayers.Visibility = Visibility.Hidden;
            mainWindow.grdSttCards.Visibility = Visibility.Hidden;

            switch (index)
            {
                case 0:
                    mainWindow.btnSttTeams.Foreground = lightGreen;
                    mainWindow.grdSttTeams.Visibility = Visibility.Visible;
                    //Team
                    mainWindow.cbSelectedTeam.Visibility = Visibility.Hidden;
                    //Round
                    mainWindow.cbSelectedRound.SelectedIndex = 0;
                    mainWindow.cbSelectedRound.Visibility = Visibility.Visible;
                    mainWindow.dgvStatisticsTeams.ItemsSource = SttTeam(mainWindow);
                    //Position
                    mainWindow.cbSelectedRound.Margin = new Thickness(15, 0, 0, 3);
                    mainWindow.btnFilterStatistic.Margin = new Thickness(175, 0, 0, 0);
                    break;
                case 1:
                    mainWindow.btnSttPlayers.Foreground = lightGreen;
                    mainWindow.grdSttPlayers.Visibility = Visibility.Visible;
                    //Team
                    mainWindow.cbSelectedTeam.SelectedIndex = 0;
                    mainWindow.cbSelectedTeam.Visibility = Visibility.Visible;
                    //Round
                    mainWindow.cbSelectedRound.SelectedIndex = 0;
                    mainWindow.cbSelectedRound.Visibility = Visibility.Visible;
                    mainWindow.dgvStatisticsPlayers.ItemsSource = SttPlayer(mainWindow);
                    //Position
                    mainWindow.cbSelectedRound.Margin = new Thickness(180, 0, 0, 3);
                    mainWindow.btnFilterStatistic.Margin = new Thickness(340, 0, 0, 0);
                    break;
                case 2:
                    mainWindow.btnSttCards.Foreground = lightGreen;
                    mainWindow.grdSttCards.Visibility = Visibility.Visible;
                    //Team
                    mainWindow.cbSelectedTeam.SelectedIndex = 0;
                    mainWindow.cbSelectedTeam.Visibility = Visibility.Visible;
                    //Round
                    mainWindow.cbSelectedRound.Visibility = Visibility.Hidden;
                    mainWindow.dgvStatisticsCards.ItemsSource = SttCard(mainWindow);
                    //Position
                    mainWindow.btnFilterStatistic.Margin = new Thickness(175, 0, 0, 0);
                    break;
            }
        }
        void AddToComboboxStatistic(MainWindow mainWindow)
        {
            mainWindow.cbSelectedTeam.Items.Clear();
            mainWindow.cbSelectedTeam.Items.Add("Tất cả đội");
            List<Team> teams = TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id);
            foreach (Team t in teams)
            {
                mainWindow.cbSelectedTeam.Items.Add(t.nameTeam);
            }
            mainWindow.cbSelectedTeam.SelectedIndex = 0;

            //
            mainWindow.cbSelectedRound.Items.Clear();
            mainWindow.cbSelectedRound.Items.Add("Tất cả vòng");
            List<string> rounds = MatchDAO.Instance.GetListRoundInLeague(mainWindow.league.id);
            for (int i = 0; i < rounds.Count; i++)
            {
                int tmp = int.Parse(rounds[i]);
                if (tmp > 0)
                    mainWindow.cbSelectedRound.Items.Add("Vòng " + tmp.ToString());
            }
            //
            for (int i = 0; i < rounds.Count; i++)
            {
                int tmp = int.Parse(rounds[i]);
                if (tmp < 0)
                    switch (-tmp)
                    {
                        case 1:
                            mainWindow.cbSelectedRound.Items.Add("Vòng Chung kết");
                            break;
                        case 2:
                            mainWindow.cbSelectedRound.Items.Add("Vòng Bán kết");
                            break;
                        case 3:
                            mainWindow.cbSelectedRound.Items.Add("Vòng Tứ kết");
                            break;
                        case 4:
                            mainWindow.cbSelectedRound.Items.Add("Vòng 1/8");
                            break;
                    }
                else
                    break;
            }
            mainWindow.cbSelectedRound.SelectedIndex = 0;

        }
        int GetRoundCbb(string name)
        {
            if (name == "Vòng 1/8")
                return -4;
            if (name == "Vòng Tứ kết")
                return -3;
            if (name == "Vòng Bán kết")
                return -2;
            if (name == "Vòng Chung kết")
                return -1;
            if (name == "Tất cả vòng")
                return 0;
            name = name.Remove(0, 5);
            return int.Parse(name);
        }
        List<TeamStatistic> SttTeam(MainWindow mainWindow)
        {
            List<TeamStatistic> list = new List<TeamStatistic>();
            List<Team> team = TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id);
            int round = GetRoundCbb(mainWindow.cbSelectedRound.SelectedItem.ToString());
            int i = -1;
            foreach (Team t in team)
            {
                BitmapImage logoteam = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(t.logo));
                list.Add(new TeamStatistic(t.nameTeam, logoteam));
                i++;
                List<Match> matches = MatchDAO.Instance.GetListMatchStartedByIDTeamAndRoundWithOrder(t.idTournamnt, t.id, round);

                int gF = 0; //Bàn thắng
                int gA = 0; //Bàn thua
                int rC = 0; //Thẻ đỏ
                int yC = 0; //Thẻ vàng

                foreach (Match m in matches)
                {
                    if (round != 0 && m.round != round)
                        continue;
                    list[i].m++;
                    if (m.idTeam01 == t.id)
                    {
                        gF += m.Score1;
                        gA += m.Score2;
                    }
                    else
                    {
                        gF += m.Score2;
                        gA += m.Score1;
                    }

                    List<Card> cards = CardDAO.Instance.GetListCards(m.id, t.id);
                    foreach (Card c in cards)
                    {
                        if (c.typeOfCard == "Thẻ đỏ")
                            rC++;
                        else
                            yC++;
                    }
                }

                list[i].index = i + 1;
                list[i].gf = gF;
                list[i].ga = gA;
                list[i].rc = rC;
                list[i].yc = yC;
                list[i].sumc = rC + yC;

            }
            return list;
        }
        List<PlayerStatistic> SttPlayer(MainWindow mainWindow)
        {
            string nameTeam = mainWindow.cbSelectedTeam.SelectedItem.ToString();
            List<PlayerStatistic> list = new List<PlayerStatistic>();
            List<Team> team = new List<Team>();
            int round = GetRoundCbb(mainWindow.cbSelectedRound.SelectedItem.ToString());
            if (nameTeam == "Tất cả đội")
                team = TeamDAO.Instance.GetListTeamInLeague(mainWindow.league.id);
            else
            {
                Team t = TeamDAO.Instance.GetTeamById(TeamDAO.Instance.GetTeamIDByName(mainWindow.league.id, nameTeam));
                team.Add(t);
            }
            int i = -1;
            foreach (Team t in team)
            {
                BitmapImage logoteam = ImageProcessing.Instance.Convert(ImageProcessing.Instance.ByteToImg(t.logo));
                List<Player> players = PlayerDAO.Instance.GetListPlayer(t.id);
                List<Match> matches = MatchDAO.Instance.GetListMatchStartedByIDTeamAndRoundWithOrder(t.idTournamnt, t.id, round);
                foreach (Player p in players)
                {
                    i++;
                    list.Add(new PlayerStatistic(p.namePlayer, t.nameTeam, logoteam));
                    list[i].index = i + 1;
                    list[i].number = p.uniformNumber;
                    foreach (Match m in matches)
                    {
                        list[i].rc += CardDAO.Instance.GetCountCardOfPlayerByTypeAndIdMatch(p, "Thẻ đỏ", m.id);
                        list[i].yc += CardDAO.Instance.GetCountCardOfPlayerByTypeAndIdMatch(p, "Thẻ vàng", m.id);
                        list[i].goal += GoalDAO.Instance.GetCountGoalsByIdPlayerAndIdMatch(p.id, m.id);
                        list[i].assist += GoalDAO.Instance.GetCountAssistsByIdPlayerAndIdMatch(p.id, m.id);
                    }
                }
            }

            return list;
        }
        List<CardStatistic> SttCard(MainWindow mainWindow)
        {
            string nameTeam = mainWindow.cbSelectedTeam.SelectedItem.ToString();
            int idTeam = -1;
            if (nameTeam != "Tất cả đội")
            {
                Team t = TeamDAO.Instance.GetTeamById(TeamDAO.Instance.GetTeamIDByName(mainWindow.league.id, nameTeam));
                idTeam = t.id;
            }
            List<CardStatistic> list = new List<CardStatistic>();
            int i = 0;
            int rC;
            int yC;

            //Vòng bảng
            while (true)
            {
                i++;
                rC = 0;
                yC = 0;
                List<Match> matches = MatchDAO.Instance.GetListMatchByRound(mainWindow.league.id, i);
                if (matches.Count == 0)
                    break;
                list.Add(new CardStatistic(i.ToString()));

                if (idTeam == -1)
                    foreach (Match m in matches)
                    {
                        rC += CardDAO.Instance.GetCountCardTypeByIDMatch(m.id, "Thẻ đỏ");
                        yC += CardDAO.Instance.GetCountCardTypeByIDMatch(m.id, "Thẻ vàng");
                    }
                else
                    foreach (Match m in matches)
                    {
                        rC += CardDAO.Instance.GetCountCardTypeByIDMatchAndIDTeam(m.id, idTeam, "Thẻ đỏ");
                        yC += CardDAO.Instance.GetCountCardTypeByIDMatchAndIDTeam(m.id, idTeam, "Thẻ vàng");
                    }

                list[i - 1].rc = rC;
                list[i - 1].yc = yC;
                list[i - 1].sumc = rC + yC;
            }

            //Vòng loại trực tiếp
            for (int ii = 4; ii > 0; ii--)
            {
                rC = 0;
                yC = 0;
                
                List<Match> matches = MatchDAO.Instance.GetListMatchByRound(mainWindow.league.id, -ii);
                if (matches.Count == 0)
                    continue;
                switch (ii)
                {
                    case 1:
                        list.Add(new CardStatistic("Chung kết"));
                        break;
                    case 2:
                        list.Add(new CardStatistic("Bán kết"));
                        break;
                    case 3:
                        list.Add(new CardStatistic("Tứ kết"));
                        break;
                    case 4:
                        list.Add(new CardStatistic("1/8"));
                        break;
                }

                if (idTeam == -1)
                    foreach (Match m in matches)
                    {
                        rC += CardDAO.Instance.GetCountCardTypeByIDMatch(m.id, "Thẻ đỏ");
                        yC += CardDAO.Instance.GetCountCardTypeByIDMatch(m.id, "Thẻ vàng");
                    }
                else
                    foreach (Match m in matches)
                    {
                        rC += CardDAO.Instance.GetCountCardTypeByIDMatchAndIDTeam(m.id, idTeam, "Thẻ đỏ");
                        yC += CardDAO.Instance.GetCountCardTypeByIDMatchAndIDTeam(m.id, idTeam, "Thẻ vàng");
                    }

                list[list.Count - 1].rc = rC;
                list[list.Count - 1].yc = yC;
                list[list.Count - 1].sumc = rC + yC;
            }

            return list;
        }
        #endregion
    }
}
