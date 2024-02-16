using System;
using System.Windows;
using System.Windows.Input;
using FCM.DAO;
using FCM.View;

namespace FCM.ViewModel
{
    class EditDialogViewModel : BaseViewModel
    {
        public ICommand CancelEditDialogCommand { get; set; }
        public ICommand EditDialogCommand { get; set; }

        public EditDialogViewModel()
        { 
            CancelEditDialogCommand = new RelayCommand<EditDialogWindow>((parameter) => true, (parameter) => parameter.Close());
            EditDialogCommand = new RelayCommand<EditDialogWindow>((parameter) => true, (parameter) => EditDialog(parameter));
        }

        void EditDialog(EditDialogWindow parameter)
        {
            int value;
            try
            {
                value = int.Parse(parameter.tbValue.Text);
            }
            catch
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Định dạng không hợp lệ");
                wd.ShowDialog();
                return;
            }


            if (value > 60)
            {
                if (parameter.idSetting == 4)
                {
                    if (value > 100)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số tuổi tối đa là 100");
                        wd.ShowDialog();
                        return;
                    }
                }
                else
                {
                    MessageBoxWindow wd = new MessageBoxWindow(false, "Giá trị vượt quá quy định (60)");
                    wd.ShowDialog();
                    return;
                }
            }
            //Logic
            switch (parameter.idSetting)
            {
                case 0:
                    if (value < 2)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng đội bóng phải lớn hơn 1");
                        wd.ShowDialog();
                        return;
                    }
                    if (value <= parameter.curSetting.NumberOfTeamIn)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng đội bóng tham gia phải lớn hơn số đội vào vòng trong");
                        wd.ShowDialog();
                        return;
                    }
                    if (value / BoardDAO.Instance.GetListBoard(parameter.idTournament).Count < 2)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số đội trong 1 bảng tối thiểu là 2");
                        wd.ShowDialog();
                        return;
                    }
                    if (value > 16 && LeagueDAO.Instance.GetLeagueById(parameter.idTournament).typeLeague==1)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng đội tối đa của thể loại thi đấu loại trực tiếp là 16");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 1:
                    if (parameter.curSetting.minPlayerOfTeam < 1)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng cầu thủ tối thiểu phải lớn hơn 0");
                        wd.ShowDialog();
                        return;
                    }
                    if (value > parameter.curSetting.maxPlayerOfTeam)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng cầu thủ tối thiểu không được lớn hơn số lượng cầu thủ tối đa");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 2:
                    if (parameter.curSetting.maxPlayerOfTeam < 1)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng cầu thủ tối đa phải lớn hơn 0");
                        wd.ShowDialog();
                        return;
                    }
                    if (value < parameter.curSetting.minPlayerOfTeam)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng cầu thủ tối đa không được nhỏ hơn số lượng cầu thủ tối thiểu");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 3:
                    if (value < 1)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số tuổi phải lớn hơn 0");
                        wd.ShowDialog();
                        return;
                    }
                    if (value > parameter.curSetting.maxAge)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Tuổi tối thiểu không được lớn hơn tuổi tối đa");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 4:
                    if (value < 1)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số tuổi phải lớn hơn 0");
                        wd.ShowDialog();
                        return;
                    }
                    if (value < parameter.curSetting.minAge)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Tuổi tối đa không được nhỏ hơn tuổi tối thiểu");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 5:
                    if (value < 0)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng tối thiểu là 0");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 6:
                    if (value < 2)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng tối thiểu là 2");
                        wd.ShowDialog();
                        return;
                    }
                    if (value >= parameter.curSetting.numberOfTeam)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng đội vào vòng trong phải nhỏ hơn số đội tham gia giải");
                        wd.ShowDialog();
                        return;
                    }
                    if (value > 16)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Số lượng đội vào vòng trong tối đa là 16 đội");
                        wd.ShowDialog();
                        return;
                    }  
                    break;
                case 7:
                    if (value <= parameter.curSetting.scoreDraw || value <= parameter.curSetting.scoreLose)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Điểm thắng > Điểm Hoà > Điểm Thua");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 8:
                    if (value <= parameter.curSetting.scoreLose || value >= parameter.curSetting.scoreWin)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Điểm thắng > Điểm Hoà > Điểm Thua");
                        wd.ShowDialog();
                        return;
                    }
                    break;
                case 9:
                    if (value >= parameter.curSetting.scoreDraw || value >= parameter.curSetting.scoreWin)
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Điểm thắng > Điểm Hoà > Điểm Thua");
                        wd.ShowDialog();
                        return;
                    }
                    break;
            }

            string nameCol = "";

            //Edit
            try
            {
                switch(parameter.idSetting)
                {
                    case 0:
                        nameCol = "NumberofTeams";
                        break;
                    case 1:
                        nameCol = "MinplayerofTeams";
                        break;
                    case 2:
                        nameCol = "MaxplayerofTeams";
                        break;
                    case 3:
                        nameCol = "MinAge";
                        break;
                    case 4:
                        nameCol = "MaxAge";
                        break;
                    case 5:
                        nameCol = "MaxNumberForeignPlayers";
                        break;
                    case 6:
                        nameCol = "NumberOfTeamsIn";
                        break;
                    case 7:
                        nameCol = "Score_Win";
                        break;
                    case 8:
                        nameCol = "Score_Draw";
                        break;
                    case 9:
                        nameCol = "Score_Lose";
                        break;
                }
                SettingDAO.Instance.EditSetting(parameter.idTournament, nameCol, parameter.tbValue.Text);

                //
                if (parameter.idSetting == 0)
                {
                    LeagueDAO.Instance.UpdateNumberOfTeams(parameter.idTournament, value);
                }


                MessageBoxWindow wd = new MessageBoxWindow(true, "Cập nhật quy định thành công");
                wd.ShowDialog();
                parameter.Close();
            }
            catch
            {
                MessageBoxWindow wd = new MessageBoxWindow(false, "Lỗi kết nối dữ liệu");
                wd.ShowDialog();
                return;
            }
        }
    }
}
