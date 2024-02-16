using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FCM.ViewModel
{
    public class ResultViewModel : BaseViewModel
    {
        public ICommand SwitchTabCommand { get; set; }
        public ICommand GetUidCommand { get; set; }
        public ICommand SwitchPlayersCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand AddCardTeam1Command { get; set; }
        public ICommand AddCardTeam2Command { get; set; }
        public ICommand ChangeCbxTeamInLineupsWindowCommand { get; set; }
        public ICommand AddToOfficalLineupsCommand { get; set; }
        public ICommand AddScoreTeam1Command { get; set; }
        public ICommand AddScoreTeam2Command { get; set; }
        public ICommand EditGoalCommand { get; set; }
        public ICommand DeleteGoalCommand { get; set; }
        public ICommand EditCardInforCommand { get; set; }
        public ICommand DeleteCardCommand { get; set; }
        public ICommand EditSwitchedPlayerCommand { get; set; }
        public ICommand DeleteSwitchedPlayerCommand { get; set; }
        public ICommand AddGoalCommand { get; set; }
        public ICommand CancelGoalCommand { get; set; }
        public ICommand AddCardCommand { get; set; }
        public ICommand CancelCardCommand { get; set; }
        public ICommand OKSwitchedPlayersCommand { get; set; }
        public ICommand CancelSwitchedPlayersCommand { get; set; }

        public ICommand OpenPenaltyWindowCommand { get; set; }
        public ICommand ExitPenaltyCommand { get; set; }
        public ICommand SavePenaltyCommand { get; set; }
        
        public SolidColorBrush lightGreen = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52ff00"));
        public SolidColorBrush white = new SolidColorBrush(Colors.White);

        public string uid;
        MessageBoxWindow wd;
        public ResultViewModel()
        {
            SwitchTabCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => SwitchTab(parameter));
            ExitCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => parameter.Close());
            GetUidCommand = new RelayCommand<Button>((parameter) => true, (parameter) => uid = parameter.Uid);
            SwitchPlayersCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => OpenSwitchPlayersWindow(parameter));

            SaveCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => SaveUpdateResultInfor(parameter));
            AddCardTeam1Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddCardTeam1(parameter));
            AddCardTeam2Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddCardTeam2(parameter));
            ChangeCbxTeamInLineupsWindowCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => ChangeCbxTeamInLineupsWindow(parameter));
            AddToOfficalLineupsCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddToOfficialLineups(parameter));
            AddScoreTeam1Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddScoreTeam1(parameter));
            AddScoreTeam2Command = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => AddScoreTeam2(parameter));
            EditGoalCommand = new RelayCommand<ucGoal>((parameter) => true, (parameter) => EditGoalInfor(parameter));
            DeleteGoalCommand = new RelayCommand<ucGoal>((parameter) => true, (parameter) => DeleteGoal(parameter));
            EditCardInforCommand = new RelayCommand<ucCard>((parameter) => true, (parameter) => EditCardInfor(parameter));
            DeleteCardCommand = new RelayCommand<ucCard>((parameter) => true, (parameter) => DeleteCard(parameter));
            
            EditSwitchedPlayerCommand = new RelayCommand<ucSwitchedPlayers>((parameter) => true, (parameter) => EditSwitchedPlayer(parameter));
            DeleteSwitchedPlayerCommand = new RelayCommand<ucSwitchedPlayers>((parameter) => true, (parameter) => DeleteSwitchedPlayer(parameter));
            AddGoalCommand = new RelayCommand<AddGoalWindow>((parameter) => true, (parameter) => AddGoal(parameter));
            CancelGoalCommand = new RelayCommand<AddGoalWindow>((parameter) => true, (parameter) => CancelGoal(parameter));

            AddCardCommand = new RelayCommand<AddCardWindow>((parameter) => true, (parameter) => AddCard(parameter));
            CancelCardCommand = new RelayCommand<AddCardWindow>((parameter) => true, (parameter) => CancelCard(parameter));

            OKSwitchedPlayersCommand = new RelayCommand<SwitchPlayersWindow>((parameter) => true, (parameter) => OKSwitchedPlayers(parameter));
            CancelSwitchedPlayersCommand = new RelayCommand<SwitchPlayersWindow>((parameter) => true, (parameter) => CancelSwitchedPlayers(parameter));

            OpenPenaltyWindowCommand = new RelayCommand<ResultRecordingWindow>((parameter) => true, (parameter) => OpenPenaltyWindow(parameter));
            ExitPenaltyCommand = new RelayCommand<PenaltyWindow>((parameter) => true, (parameter) => ClosePenaltyWindow(parameter));
            SavePenaltyCommand = new RelayCommand<PenaltyWindow>((parameter) => true, (parameter) => SavePenaltyWindow(parameter));
        }

        public void SavePenaltyWindow(PenaltyWindow parameter)
        {
            int penaltyScoreTeam1 = parameter.cbScoreTeam1.SelectedIndex;
            int penaltyScoreTeam2 = parameter.cbScoreTeam2.SelectedIndex;

            if (penaltyScoreTeam1 == penaltyScoreTeam2)
            {
                wd = new MessageBoxWindow(false, "Kết quả luân lưu không thể hòa");
                wd.ShowDialog();
            }    
            else
            {
                parameter.resultWD.penaltyTeam1 = penaltyScoreTeam1;
                parameter.resultWD.penaltyTeam2 = penaltyScoreTeam2;

                parameter.resultWD.ShowPenaltyResult();

                ClosePenaltyWindow(parameter);
            }    
        }

        public void ClosePenaltyWindow(PenaltyWindow parameter)
        {
            parameter.Close();
        }

        public void OpenPenaltyWindow(ResultRecordingWindow parameter)
        {
            if (parameter.match.allowDraw == true)
            {
                wd = new MessageBoxWindow(false, "Trận đấu này được phép hòa, do đó không cần tới kết quả luân lưu");
                wd.ShowDialog();
                return;
            }    

            if (parameter.ScoreTeam1 == parameter.ScoreTeam2)
            {
                PenaltyWindow wd = new PenaltyWindow(parameter);
                wd.ShowDialog();
            }    
            else
            {
                wd = new MessageBoxWindow(false, "Bạn chỉ cần lưu kết quả luân lưu khi 2 đội hòa");
                wd.ShowDialog();
            }   
        }

        public void SwitchTab(ResultRecordingWindow parameter)
        {
            int index = int.Parse(uid); // tab index
            //Move Stroke Tab
            parameter.rtStroke.Margin = new Thickness((20 + 120 * index), 0, 0, 5);

            // Reset color
            parameter.btnGoalsTab.Foreground = white;
            parameter.btnCardsTab.Foreground = white;
            parameter.btnFormationsTab.Foreground = white;

            // Hide all screens
            parameter.grdGoalsTab.Visibility = Visibility.Hidden;
            parameter.grdCardsTab.Visibility = Visibility.Hidden;
            parameter.grdFormationsTab.Visibility = Visibility.Hidden;

            switch (index)
            {
                case 0:
                    parameter.btnGoalsTab.Foreground = lightGreen;
                    parameter.grdGoalsTab.Visibility = Visibility.Visible;
                    break;
                case 1:
                    parameter.btnCardsTab.Foreground = lightGreen;
                    parameter.grdCardsTab.Visibility = Visibility.Visible;
                    break;
                case 2:
                    parameter.btnFormationsTab.Foreground = lightGreen;
                    parameter.grdFormationsTab.Visibility = Visibility.Visible;
                    break;
            }
        }
        public void OpenSwitchPlayersWindow(ResultRecordingWindow parameter)
        {
            SwitchPlayersWindow wd = new SwitchPlayersWindow(parameter);
            wd.ShowDialog();
        }
        public void EditSwitchedPlayer(ucSwitchedPlayers parameter)
        {
            SwitchPlayersWindow wd = new SwitchPlayersWindow(parameter.resultWD);
            wd.ShowDialog();
        }
        public void DeleteSwitchedPlayer(ucSwitchedPlayers parameter)
        {
            //parameter.resultWD.ShowListSwitched();
            parameter.resultWD.DeleteSwitched(parameter.switchedPlayer);
        }
        public void SaveUpdateResultInfor(ResultRecordingWindow parameter)
        {
            if (parameter.UpdateDatabase())
            {
                wd = new MessageBoxWindow(true, "Lưu thông tin mới thành công");
                wd.ShowDialog();
                parameter.Close();
            }
        }
        public void AddCardTeam1(ResultRecordingWindow parameter)
        {
            AddCardWindow wd = new AddCardWindow(parameter, true, parameter.team1);
            wd.ShowDialog();
        }
        public void AddCardTeam2(ResultRecordingWindow parameter)
        {
            AddCardWindow wd = new AddCardWindow(parameter, false, parameter.team2);
            wd.ShowDialog();
        }
        public void EditCardInfor(ucCard parameter)
        {
            AddCardWindow wd = new AddCardWindow(parameter.card, parameter.resultWD);
            wd.ShowDialog();
        }
        public void ChangeCbxTeamInLineupsWindow(ResultRecordingWindow parameter)
        {
            parameter.LoadLineups(parameter.cbSelectedTeam.SelectedIndex);
        }
        public void AddToOfficialLineups(ResultRecordingWindow parameter)
        {
            parameter.AddPlayerToOfficialLineups();
        }
        public void AddScoreTeam1(ResultRecordingWindow parameter)
        {
            AddGoalWindow wd = new AddGoalWindow(true, parameter);
            wd.ShowDialog();
        }
        public void AddScoreTeam2(ResultRecordingWindow parameter)
        {
            AddGoalWindow wd = new AddGoalWindow(false, parameter);
            wd.ShowDialog();
        }
        public void EditGoalInfor(ucGoal parameter)
        {
            AddGoalWindow wd = new AddGoalWindow(parameter.goal, parameter.resultWD);
            wd.ShowDialog();
        }
        public void DeleteGoal(ucGoal parameter)
        {
            parameter.resultWD.DeleteGoal(new Goal(parameter.resultWD.match.id,
                                                    parameter.goal.idPlayerGoals,
                                                    parameter.goal.idPlayerAssist,
                                                    parameter.goal.idTeams,
                                                    parameter.goal.idTypeOfGoals,
                                                    parameter.tblTime.Text));

            if (parameter.resultWD.match.idTeam01 == parameter.goal.idTeams)
            {
                parameter.resultWD.ScoreTeam1--;
            }
            else
            {
                parameter.resultWD.ScoreTeam2--;
            }

            parameter.resultWD.ShowResultMatch();
        }
        public void DeleteCard(ucCard parameter)
        {
            Card c = new Card(parameter.resultWD.match.id,
                                                    parameter.card.idPlayer,
                                                    parameter.card.idTeams,
                                                    parameter.card.typeOfCard,
                                                    parameter.card.time);

            parameter.resultWD.ChangeLineupsWhenChangeCardInfor(c, true);

            parameter.resultWD.Deletecard(c);

            
        }
        public void AddGoal(AddGoalWindow parameter)
        {
            if (parameter.cbTypeOfGoal.SelectedIndex < 0)
            {
                wd = new MessageBoxWindow(false, "Thiếu thông tin về loại bàn thắng");
                wd.ShowDialog();
                return;
            }

            int maxTime = parameter.resultWD.match.allowDraw == true ? 90 : 120;

            if (!isExactTime(parameter.tbTime.Text, maxTime))
            { 
                wd = new MessageBoxWindow(false, "Thông tin về thời gian phải là định dạng số và nằm trong khoảng [0," + maxTime.ToString() + "]");
                wd.ShowDialog();
                return;
            }

            int idMatch = parameter.resultWD.match.id;
            int idPlayerScored = parameter.players[parameter.cbScored.SelectedIndex].id;
            int idPlayerAssist = parameter.cbAsssist.SelectedIndex == -1 ? idPlayerScored : parameter.players[parameter.cbAsssist.SelectedIndex].id;
            int idTeam = parameter.isTeam1 ? parameter.resultWD.team1.id : parameter.resultWD.team2.id;
            int idTypeOfGoal = parameter.typeOfGoals[parameter.cbTypeOfGoal.SelectedIndex].id;
            string timeGoal = parameter.tbTime.Text;

            Player playerScore = PlayerDAO.Instance.GetPlayerById(idPlayerScored);
            Player playerAssist = PlayerDAO.Instance.GetPlayerById(idPlayerAssist);

            int timeReveiveRedCardOfScorePlayer = parameter.resultWD.GetTimePlayerReceiveRedCard(playerScore);
            int timeReveiveRedCardOfAssistPlayer = parameter.resultWD.GetTimePlayerReceiveRedCard(playerAssist);

            if (Int32.Parse(timeGoal) > timeReveiveRedCardOfScorePlayer)
            {
                wd = new MessageBoxWindow(false, "Cầu thủ ghi bàn đã bị nhận thẻ đỏ và ra khỏi sân trước đó!\nDo đó cầu thủ này không thể ghi bàn");
                wd.ShowDialog();
                return;
            }

            if (Int32.Parse(timeGoal) > timeReveiveRedCardOfAssistPlayer)
            {
                wd = new MessageBoxWindow(false, "Cầu thủ kiến tạo đã bị nhận thẻ đỏ và ra khỏi sân trước đó!\nDo đó cầu thủ này không thể kiến tạo");
                wd.ShowDialog();
                return;
            }

            if (parameter.tbTime.Text != null)
            {
                Goal goal = new Goal(idMatch, idPlayerScored, idPlayerAssist, idTeam, idTypeOfGoal, timeGoal);
                
                if (parameter.isEdit == false)
                {
                    parameter.resultWD.AddNewGoal(parameter.isTeam1, goal);
                }
                else
                {
                    parameter.resultWD.EditGoal(parameter.goal, goal);
                }
                parameter.Close();
            }
        }
        public void CancelGoal(AddGoalWindow parameter)
        {
            parameter.Close();
        }
        public void AddCard(AddCardWindow parameter)
        {
            int maxTime = parameter.resultWD.match.allowDraw == true ? 90 : 120;

            if (!isExactTime(parameter.tbTime.Text, maxTime))
            {
                wd = new MessageBoxWindow(false, "Thông tin về thời gian phải là định dạng số và nằm trong khoảng [0," + maxTime.ToString() + "]");
                wd.ShowDialog();
                return;
            }
           
            if (parameter.cbPlayer.SelectedIndex >= 0)
            {
                string typeOfCard = parameter.rbtnYellowCard.IsChecked == true ? "Thẻ vàng" : "Thẻ đỏ";
                int idTeam;
                if (parameter.isEdit == false)
                {
                    idTeam = parameter.isTeam1 ? parameter.resultWD.team1.id : parameter.resultWD.team2.id;
                }    
                else
                {
                    idTeam = parameter.oldCard.idTeams;
                }    

                Player player = parameter.GetPlayerBySelectedIndex(parameter.cbPlayer.SelectedIndex);

                Card c = new Card(parameter.resultWD.match.id, player.id, idTeam, typeOfCard, parameter.tbTime.Text);

                if (parameter.isEdit == false)
                {
                    parameter.resultWD.AddCard(c);
                }
                else
                {
                    parameter.resultWD.EditCard(parameter.oldCard, c);
                }

                parameter.resultWD.ChangeLineupsWhenChangeCardInfor(c);

                parameter.Close();
            }
        }
        public void CancelCard(AddCardWindow parameter)
        {
            parameter.Close();
        }
        public void OKSwitchedPlayers(SwitchPlayersWindow parameter)
        {
            int maxTime = parameter.resultWD.match.allowDraw == true ? 90 : 120;

            if (!isExactTime(parameter.tbTime.Text, maxTime))
            {
                wd = new MessageBoxWindow(false, "Thông tin về thời gian phải là định dạng số và nằm trong khoảng [0," + maxTime.ToString() + "]");
                wd.ShowDialog();
                return;
            }

            if (parameter.cbPlayer2.SelectedIndex >= 0 && parameter.cbPlayer1.SelectedIndex >= 0)
            {
                parameter.resultWD.SwitchPlayer(parameter.cbPlayer2.SelectedIndex, parameter.cbPlayer1.SelectedIndex, parameter.tbTime.Text.ToString());
                parameter.Close();
            }
        }
        public void CancelSwitchedPlayers(SwitchPlayersWindow parameter)
        {
            parameter.Close();
        }

        bool isExactTime(string s, int maxTime)
        {
            if (s == "") return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9')
                {
                    return false;
                }
            }

            int time = Int32.Parse(s);
            
            

            if (time > maxTime || time < 0)
            {
                return false;
            }

            return true;
        }
    }
}
