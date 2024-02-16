using FCM.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using FCM.DAO;
using FCM.DTO;
using FCM.UserControls;

namespace FCM.ViewModel
{
    class AddPlayerViewModel : BaseViewModel
    {
        MessageBoxWindow wd;

        public ICommand CancelAddPlayerCommand { get; set; }
        public ICommand AddPlayerCommand { get; set; }
        public ICommand AddImagePlayerCommand { get; set; }
        private System.Drawing.Image imaged;

        public AddPlayerViewModel()
        {
            CancelAddPlayerCommand = new RelayCommand<AddPlayerWindow>((parameter) => true, (parameter) => parameter.Close());
            AddPlayerCommand = new RelayCommand<AddPlayerWindow>((parameter) => true, (parameter) => AddPlayer(parameter));
            AddImagePlayerCommand = new RelayCommand<AddPlayerWindow>((parameter) => true, (parameter) => AddImagePlayer(parameter));
        }
        void AddImagePlayer(AddPlayerWindow parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Image files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png", Multiselect = false };
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            if (path != "")
                try
                {
                    imaged = Image.FromFile(path);
                    parameter.imgPlayerImage.Source = ImageProcessing.Instance.Convert(imaged);
                }
                catch
                {
                    wd = new MessageBoxWindow(false, "File không hợp lệ, vui lòng chọn lại");
                    wd.ShowDialog();
                }
        }

       // Kiểm tra xem đã đủ số lượng cầu thủ/đội bóng tối thiếu hay chưa
        bool IsReady(AddPlayerWindow parameter)
        {
            League leagueNow = LeagueDAO.Instance.GetLeagueById(parameter.team.idTournamnt);

            bool isEnoughTeams = true;
            bool isEnoughPlayers = true;

            if (leagueNow.countTeam != parameter.setting.numberOfTeam)
            {
                isEnoughTeams = false;
            }

            List<Team> teamsList = TeamDAO.Instance.GetListTeamInLeague(leagueNow.id);
            foreach (Team team in teamsList)
            {
                int numberPlayerOfTeams = TeamDAO.Instance.GetPlayerCountOfTeam(team.id);

                if (numberPlayerOfTeams < parameter.setting.minPlayerOfTeam)
                {
                    //MessageBox.Show(parameter.setting.minPlayerOfTeam.ToString());
                    isEnoughPlayers = false;
                    break;
                }
            }

            return (isEnoughPlayers && isEnoughTeams);
        }
        void AddPlayer(AddPlayerWindow parameter)
        {
            string name = InputFormat.Instance.FomartSpace(parameter.tbName.Text);
            string unformNumber = InputFormat.Instance.FomartSpace(parameter.tbNumber.Text);
            string position = InputFormat.Instance.FomartSpace(parameter.tbPosition.Text);
            string nation = InputFormat.Instance.FomartSpace(parameter.tbNationality.Text);
            string note = InputFormat.Instance.FomartSpace(parameter.tbNote.Text);

            if (name == "" || unformNumber == "" ||parameter.dpDoB.SelectedDate.ToString()==""|| nation == "" || position == "" || parameter.imgPlayerImage.Source.ToString() == "pack://application:,,,/Resource/Images/NoImageSelected.png")
            {
                wd = new MessageBoxWindow(false, "Thiếu thông tin");
                wd.ShowDialog();
                return;
            }
            if (!InputFormat.Instance.isNumber(unformNumber) || Int32.Parse(unformNumber) < 0)
            {
                wd = new MessageBoxWindow(false, "Số áo chỉ nhận giá trị là số nguyên dương >= 0");
                wd.ShowDialog();
                return;
            }
            if ((parameter.player == null || parameter.player.uniformNumber != Int32.Parse(unformNumber))
                && PlayerDAO.Instance.IsHaveNumber(Int32.Parse(unformNumber), parameter.team.id))
            {
                wd = new MessageBoxWindow(false, "Số áo đã tồn tại");
                wd.ShowDialog();
                return;
            }
            var today = DateTime.Today;
            var age = today.Year -  DateTime.Parse(parameter.dpDoB.ToString()).Year;
            if ( DateTime.Parse(parameter.dpDoB.ToString()).Date > today.AddYears(-age)) age--;
            if (age<parameter.setting.minAge || age>parameter.setting.maxAge)
            {
                wd = new MessageBoxWindow(false, "Tuổi của cầu thủ phải >" + parameter.setting.minAge + " và <= " + parameter.setting.maxAge);
                wd.ShowDialog();
                return;
            }    
            if (parameter.player == null)
            {
                if (!parameter.CanGetOutNation && parameter.team.nation!=nation)
                {
                    wd = new MessageBoxWindow(false, "Số cầu thủ ngước ngoài đã đạt tối đa");
                    wd.ShowDialog();
                    return;
                }    
                Player player = new Player(parameter.team.id,name,Int32.Parse(unformNumber), DateTime.Parse(parameter.dpDoB.ToString()), position,nation,note, ImageProcessing.Instance.convertImgToByte(imaged));
                PlayerDAO.Instance.CreatePlayers(player);

                wd = new MessageBoxWindow(true, "Thêm cầu thủ thành công");
                wd.ShowDialog();


                parameter.Close();
            }
            else
            {
                Player player;
                if (imaged == null)
                    player = new Player(parameter.team.id, name, Int32.Parse(unformNumber), DateTime.Parse(parameter.dpDoB.ToString()), position, nation, note, parameter.player.image);
                else 
                    player = new Player(parameter.team.id, name, Int32.Parse(unformNumber), DateTime.Parse(parameter.dpDoB.ToString()), position, nation, note, ImageProcessing.Instance.convertImgToByte(imaged));
                player.id = parameter.player.id;
                if (!parameter.CanGetOutNation && parameter.team.nation != nation &&parameter.player.nationality!=nation)
                {
                    wd = new MessageBoxWindow(false, "Số cầu thủ ngước ngoài đã đạt tối đa");
                    wd.ShowDialog();
                    return;
                }
                PlayerDAO.Instance.UpdatePlayer(player);
                wd = new MessageBoxWindow(true, "Sửa cầu thủ thành công");
                wd.ShowDialog();
                parameter.Close();
            }
        }
    }
}
