using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;
using FCM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FCM.ViewModel
{
    public class EditMatchInforViewModel : BaseViewModel
    {
        public ICommand OpenEditMatchWindowCommand { get; set; }
        public ICommand OpenResultRecordWindowCommand { get; set; }
        public ICommand CancelResultCommand { get; set; }

        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        MessageBoxWindow wd;

        public EditMatchInforViewModel()
        {
            OpenEditMatchWindowCommand = new RelayCommand<ucMatchDetail>((parameter) => true, (parameter) => OpenEditMatchWindow(parameter));
            OpenResultRecordWindowCommand = new RelayCommand<ucMatchDetail>((parameter) => true, (parameter) => OpenResultRecordWindow(parameter));
            CancelResultCommand = new RelayCommand<ucMatchDetail>((parameter) => true, (parameter) => CancelResult(parameter));

            ExitCommand = new RelayCommand<EditMatchInforWindow>((parameter) => true, (parameter) => parameter.Close());
            SaveCommand = new RelayCommand<EditMatchInforWindow>((parameter) => true, (parameter) => SaveNewInfor(parameter));
        }

        void OpenEditMatchWindow(ucMatchDetail parameter)
        {
            parameter.main.OpenEditMatchInfoWindow(parameter.mainWindow, parameter.match);
        }
        void OpenResultRecordWindow(ucMatchDetail parameter)
        {
            if (parameter.match.date.ToString("dd/MM/yyyy") == "11/11/2000")
            {
                wd = new MessageBoxWindow(false, "Chưa chọn thời gian");
                wd.ShowDialog();
                return;
            }
            parameter.main.OpenResultRecordingWindow(parameter.mainWindow, parameter.match);
        }

        void CancelResult(ucMatchDetail parameter)
        {
            ConfirmDialogWindow wdd = new ConfirmDialogWindow("Bạn thực sự muốn xóa kết quả của trận đấu này?");
            wdd.ShowDialog();
            if (wdd.confirm == false)
            {
                return;
            }
            {
                if (!parameter.match.allowDraw)
                {
                    TreeMatch tree = TreeMatchDAO.Instance.GetTree(parameter.match.idTournaments);
                    tree.DeleteNode(NodeMatchDAO.Instance.GetNodeById(tree.idFirstNode), parameter.match.id);
                }
                parameter.match.Score1 = -1;
                parameter.match.Score2 = -1;
                MatchDAO.Instance.UpdateMatch(parameter.match);
                // MessageBox.Show(parameter.mainWindow.cbxRound.Items[parameter.mainWindow.cbxRound.SelectedIndex].ToString());
                // parameter.main.LoadListMatchRound(parameter.mainWindow, parameter.mainWindow.cbxRound.Items[parameter.mainWindow.cbxRound.SelectedIndex].ToString(), "Tất cả bảng đấu");
                parameter.main.CancelResultMatch(parameter.mainWindow, parameter.match);
            }
        }

        void SaveNewInfor(EditMatchInforWindow parameter)
        {

            League league = LeagueDAO.Instance.GetLeagueById(parameter.match.idTournaments);
            if (DateTime.Compare(league.dateTime, DateTime.Parse(parameter.dpDate.Text)) > 0)
            {
                if (league.dateTime.TimeOfDay > DateTime.Parse(parameter.tpTime.Text).TimeOfDay)
                {
                    wd = new MessageBoxWindow(false, "Thời gian trận đấu phải sau thời gian giải khởi tranh");
                    wd.ShowDialog();
                    return;
                }
            }
            if (league.typeLeague == 1)
            {
                DateTime date = MatchDAO.Instance.MaxTimeNockOut(parameter.match);

                date = date.AddHours(2);
                DateTime date1 = DateTime.Parse(parameter.dpDate.Text);
                DateTime time = DateTime.Parse(parameter.tpTime.Text);
                date1 = date1.AddHours(time.Hour - date1.Hour);
                date1 = date1.AddMinutes(time.Minute - date1.Minute);
                //MessageBox.Show(time.Hour.ToString());
                //   MessageBox.Show(date1 + "             " + date);
                if (DateTime.Compare(date1, date) < 0)
                {
                    // MessageBox.Show(date1 + "             " + date);
                    wd = new MessageBoxWindow(false, "Thời gian trận đấu phải sau thời gian vòng đấu trước");
                    wd.ShowDialog();
                    return;
                }
            }
            if (league.typeLeague == 2)
            {
                DateTime date = MatchDAO.Instance.MaxTimeBoard(parameter.match);
                if (BoardDAO.Instance.HaveNockOutBoard(parameter.match.idTournaments))
                {
                    date = date.AddHours(1);
                    date = date.AddMinutes(30);
                    DateTime date1 = DateTime.Parse(parameter.dpDate.Text);
                    DateTime time = DateTime.Parse(parameter.tpTime.Text);
                    date1 = date1.AddHours(time.Hour - date1.Hour);
                    date1 = date1.AddMinutes(time.Minute - date1.Minute);
                    //MessageBox.Show(time.Hour.ToString());
                    //   MessageBox.Show(date1 + "             " + date);

                    if (DateTime.Compare(date1, date) < 0)
                    {
                        // MessageBox.Show(date1 + "             " + date);
                        wd = new MessageBoxWindow(false, "Thời gian trận đấu phải sau thời gian vòng bảng");
                        wd.ShowDialog();
                        return;
                    }
                }
                else
                {
                    date = MatchDAO.Instance.MaxTimeNockOut(parameter.match);

                    date = date.AddHours(2);
                    DateTime date1 = DateTime.Parse(parameter.dpDate.Text);
                    DateTime time = DateTime.Parse(parameter.tpTime.Text);
                    date1 = date1.AddHours(time.Hour - date1.Hour);
                    date1 = date1.AddMinutes(time.Minute - date1.Minute);
                    //MessageBox.Show(time.Hour.ToString());
                    //   MessageBox.Show(date1 + "             " + date);
                    if (DateTime.Compare(date1, date) < 0)
                    {
                        // MessageBox.Show(date1 + "             " + date);
                        wd = new MessageBoxWindow(false, "Thời gian trận đấu phải sau thời gian vòng đấu trước");
                        wd.ShowDialog();
                        return;
                    }
                }    
            }



            parameter.match.statium = parameter.cbStadium.Text;
            parameter.match.date = DateTime.Parse(parameter.dpDate.Text);
            parameter.match.time = DateTime.Parse(parameter.tpTime.Text);

            if (MatchDAO.Instance.IsExistTimeMatch(parameter.match))
            {
                wd = new MessageBoxWindow(false, "Trùng thời gian");
                wd.ShowDialog();
                return;
            }
            MatchDAO.Instance.UpdateMatch(parameter.match);

            wd = new MessageBoxWindow(true, "Sửa thông tin trận đấu thành công");
            wd.ShowDialog();
            parameter.Close();
        }


    }
}
