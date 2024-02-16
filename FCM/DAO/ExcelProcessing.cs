using System;
using System.Collections.Generic;
using System.Text;
using FCM.DTO;
using System.Data;
using OfficeOpenXml;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using FCM.ViewModel;
using FCM.View;
using System.IO;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;
using System.Diagnostics;
using System.Globalization;

namespace FCM.DAO
{
    class ExcelProcessing
    {
        private static ExcelProcessing instance;
        private string formatInfor;
        MessageBoxWindow wdd;

        public static ExcelProcessing Instance
        {
            get { if (instance == null) instance = new ExcelProcessing(); return instance; }
            set => instance = value;
        }
        public bool ImportTeam(AddTeamWindow parameter, string nameBoard)
        {
            int countPlayerDone = 0;
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            Team team;
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel files(*.xml;*.xlsx;*.xlsm)|*.xml;*.xlsx;*.xlsm", Multiselect = false };
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            string cauthuloi = "Số thứ tự các cầu thủ lỗi : ";
            List<int> viTriLoi = new List<int>();
            ExcelPackage package = null;
            if (path != "")
            {
                try
                {
                    try
                    {
                        package = new ExcelPackage(path);
                        ExcelWorksheet workSheet = package.Workbook.Worksheets[0];


                        string namePicture = InputFormat.Instance.FomartSpace(workSheet.Cells[3, 2].Value.ToString());
                        var pic = workSheet.Drawings[namePicture] as ExcelPicture;
                        if (pic == null)
                        {
                            wdd = new MessageBoxWindow(false, "Thông tin đôi bóng không hợp lệ");
                            wdd.ShowDialog();
                            return false;
                        }
                        int countPlayer = Int32.Parse(workSheet.Cells[4, 2].Value.ToString());
                        team = new Team(parameter.idTournament, nameBoard,
                                                                workSheet.Cells[3, 2].Value.ToString(),
                                                                workSheet.Cells[5, 2].Value.ToString(),
                                                                workSheet.Cells[6, 2].Value.ToString(),
                                                                workSheet.Cells[7, 2].Value.ToString(),
                                                                ImageProcessing.Instance.convertImgToByte(pic.Image));
                        if (team.idTournamnt == -1 || team.nameTeam == "" || team.coach == "" || team.nation == "" || team.stadium == "")
                        {
                            wdd = new MessageBoxWindow(false, "Thông tin đôi bóng không hợp lệ");
                            wdd.ShowDialog();
                            return false;
                        }
                        team.nameTeam = InputFormat.Instance.FomartSpace(team.nameTeam);
                        if (TeamDAO.Instance.IsExistTeamName(team.nameTeam, parameter.idTournament))
                        {
                            wdd = new MessageBoxWindow(false, "Tên đội bóng đã tồn tại");
                            wdd.ShowDialog();
                            return false;
                        }
                        TeamDAO.Instance.CreateTeams(team);
                        int countOutNation = 0;
                        for (int i = 13; i < 13 + countPlayer; i++)
                        {
                            try
                            {
                                if (countPlayerDone < parameter.setting.maxPlayerOfTeam)
                                {
                                    if (countOutNation < parameter.setting.maxForeignPlayers || workSheet.Cells[i, 7].Value.ToString() == team.nation)
                                    {
                                        string namePicturePlayer = workSheet.Cells[i, 3].Value.ToString();
                                        pic = workSheet.Drawings[namePicturePlayer] as ExcelPicture;

                                        if (workSheet.Cells[i, 3].Value != null &&
                                            workSheet.Cells[i, 4].Value != null &&
                                            workSheet.Cells[i, 6].Value != null &&
                                            workSheet.Cells[i, 5].Value != null &&
                                            workSheet.Cells[i, 7].Value != null &&
                                            workSheet.Cells[i, 6].Value != null &&
                                            pic != null)
                                        {
                                            //MessageBox.Show(pic.Name);
                                            //MessageBox.Show(workSheet.Cells[i, 6].Value.ToString());
                                            string date = workSheet.Cells[i, 6].Value.ToString();
                                            InputFormat.Instance.FomartSpace(date);
                                            DateTime result;

                                            if (date[1] == '/')
                                                date = "0" + date;
                                            if (date[4] == '/')
                                                date = date.Insert(3, "0");
                                            if (date.Length > 10)
                                                date = date.Remove(10);
                                            //MessageBox.Show(date);
                                            result = DateTime.ParseExact(date, "dd/MM/yyyy", null, DateTimeStyles.None);
                                            //MessageBox.Show(result.ToString("dd/MM/yyyy"));

                                            var today = DateTime.Today;
                                            var age = today.Year - result.Year;
                                            if (result > today.AddYears(-age)) age--;
                                            if (age < parameter.setting.minAge || age > parameter.setting.maxAge)
                                            {
                                                Int32.Parse(" ");
                                            }


                                            Player player = new Player(TeamDAO.Instance.GetNewestTeamid(team.idTournamnt),
                                                                        InputFormat.Instance.FomartSpace(workSheet.Cells[i, 3].Value.ToString()),
                                                                        Int32.Parse(workSheet.Cells[i, 4].Value.ToString()),
                                                                        result,
                                                                        //DateTime.Parse(workSheet.Cells[i, 6].Value.ToString()),
                                                                        workSheet.Cells[i, 5].Value.ToString(),
                                                                        workSheet.Cells[i, 7].Value.ToString(),
                                                                        workSheet.Cells[i, 8].Value == null ? "" : workSheet.Cells[i, 8].Value.ToString(),
                                                                        ImageProcessing.Instance.convertImgToByte(pic.Image));
                                            PlayerDAO.Instance.CreatePlayers(player);
                                            if (workSheet.Cells[i, 7].Value.ToString() != team.nation)
                                                countOutNation++;
                                            countPlayerDone++;
                                        }
                                        else
                                        {
                                            Int32.Parse(" ");
                                        }
                                    }
                                    else
                                    {
                                        Int32.Parse(" ");
                                    }
                                }
                                else
                                {
                                    Int32.Parse(" ");
                                }
                            }
                            catch
                            {
                                viTriLoi.Add(i - 13 + 1);
                            }
                        }
                    }
                    catch
                    {
                        wdd = new MessageBoxWindow(false, "Thông tin đội bóng lỗi");
                        wdd.ShowDialog();
                        return false;
                    }
                }


                catch
                {
                    wdd = new MessageBoxWindow(false, "File không hợp lệ, vui lòng chọn lại");
                    wdd.ShowDialog();
                    return false;
                }
            }
            if (package == null)
                return false;
            if (viTriLoi.Count == 0)
            {
                wdd = new MessageBoxWindow(true, "Thêm đội bóng thành công \n Số cầu thủ hợp lệ : " + countPlayerDone.ToString());
                wdd.ShowDialog();
            }
            else
            {
                foreach (int i in viTriLoi)
                {
                    cauthuloi += i.ToString() + ',';
                }
                cauthuloi = cauthuloi.Remove(cauthuloi.Length - 1, 1);
                wdd = new MessageBoxWindow(true, "Thêm đội bóng thành công \n Số cầu thủ hợp lệ : " + countPlayerDone.ToString() + " \n " + cauthuloi);
                wdd.ShowDialog();
            }
            return true;
        }
        public void ExportFile(Team team)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true)
                {
                    filePath = dialog.FileName;
                }
                if (filePath == "")
                    return;
                if (string.IsNullOrEmpty(filePath))
                {
                    wdd = new MessageBoxWindow(false, "Đường dẫn báo cáo không hợp lệ");
                    wdd.ShowDialog();
                    return;
                }

                using (ExcelPackage p = new ExcelPackage())
                {
                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "Group6";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Đội bóng";

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add(team.nameTeam);

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];

                    // đặt tên cho sheet
                    ws.Name = team.nameTeam;
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 11;
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    /////////////////////////////////////////Bảng đội bóng /////////////////////////////////////////////////////////


                    ///Tiêu đề đội bóng
                    ///
                    List<Player> players = PlayerDAO.Instance.GetListPlayer(team.id);
                    ws.Cells[2, 1, 2, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[2, 1, 2, 2].Merge = true;
                    ws.Cells[2, 1, 2, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    ws.Cells[2, 1, 2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[2, 1, 2, 2].Value = "Thông tin đội bóng";
                    ws.Cells[2, 1, 2, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    ///Tên đội
                    ws.Cells[3, 1].Value = "Tên đội bóng";
                    ws.Cells[3, 2].Value = team.nameTeam;
                    ws.Cells[3, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    ///
                    ws.Cells[4, 1].Value = "Số cầu thủ";
                    ws.Cells[4, 2].Value = players.Count;
                    ///Huấn luyện viên
                    ws.Cells[5, 1].Value = "Huấn luyện viên";
                    ws.Cells[5, 2].Value = team.coach;
                    ///Sân đấu
                    ws.Cells[6, 1].Value = "Sân nhà";
                    ws.Cells[6, 2].Value = team.stadium;
                    ///Quốc gia
                    ws.Cells[7, 1].Value = "Quốc gia";
                    ws.Cells[7, 2].Value = team.nation;
                    ///Hình ảnh đội
                    ws.Cells[8, 1].Value = "Logo";
                    ws.Cells[8, 2].Value = team.logo;
                    var pic = ws.Drawings.AddPicture(team.nameTeam, ImageProcessing.Instance.ByteToImg(team.logo));
                    ws.Rows[8].Height = 60;
                    ws.Columns.Width = 60;
                    pic.SetPosition(7, 10, 1, 80);
                    pic.SetSize((int)ws.Columns[2].Width, (int)ws.Rows[8].Height);

                    for (int i = 1; i < 8; i++)
                    {
                        ws.Cells[i + 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        ws.Cells[i + 1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                    ws.Cells[2, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[2, 8].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    // Auto fill
                    ws.Columns[1, 10].AutoFit(30);

                    // Border Table
                    ws.Cells[2, 1, 8, 2].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    /////////////////////////////////////////Danh sách cầu thủ /////////////////////////////////////////////////////////

                    ///Tiêu đề cầu thủ
                    ws.Cells[11, 1, 11, 8].Value = "Danh sách cầu thủ";
                    ws.Cells[11, 1, 11, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[11, 1, 11, 8].Merge = true;
                    ws.Cells[11, 1, 11, 8].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    ws.Cells[11, 1, 11, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    ws.Cells[12, 1, 12, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[12, 1, 12, 8].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                    ///STT
                    ws.Cells[12, 1].Value = "STT";
                    ///Tên cầu thủ
                    ws.Cells[12, 2].Value = "Hình ảnh";
                    ///Tên cầu thủ
                    ws.Cells[12, 3].Value = "Tên";
                    ///Số áo
                    ws.Cells[12, 4].Value = "Số áo";
                    ///Vị trí
                    ws.Cells[12, 5].Value = "Vị trí";
                    ///Ngày sinh
                    ws.Cells[12, 6].Value = "Ngày sinh(Ngày/Tháng/Năm)";
                    ///Quốc gia
                    ws.Cells[12, 7, 12, 7].Value = "Quốc tịch";
                    ///Chú ý
                    ws.Cells[12, 8, 12, 8].Value = "Ghi chú";

                    /////////////////////////////////////////////
                    /////////////////List Player/////////////////
                    /////////////////////////////////////////////

                    for (int i = 0; i < players.Count; i++)
                    {
                        ws.Cells[12 + i + 1, 1].Value = i + 1;
                        ws.Cells[12 + i + 1, 3].Value = players[i].namePlayer;
                        ws.Cells[12 + i + 1, 4].Value = players[i].uniformNumber;
                        ws.Cells[12 + i + 1, 5].Value = players[i].position;
                        ws.Cells[12 + i + 1, 6].Value = players[i].birthDay.ToString("dd/MM/yyyy");
                        ws.Cells[12 + i + 1, 7].Value = players[i].nationality;
                        ws.Cells[12 + i + 1, 8].Value = players[i].note;
                        pic = ws.Drawings.AddPicture(players[i].namePlayer, ImageProcessing.Instance.ByteToImg(players[i].image));
                        ws.Rows[12 + i + 1].Height = 60;
                        ws.Columns[2].Width = 60;
                        pic.SetPosition(12 + i, 10, 1, 80);
                        pic.SetSize((int)ws.Columns[2].Width, (int)ws.Rows[12 + i + 1].Height);
                        for (int j = 1; j <= 8; j++)
                        {
                            ws.Cells[12 + i + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    for (int j = 1; j <= 8; j++)
                    {
                        ws.Cells[12, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }



                    /////////////////////////////////////////////
                    /////////////////////////////////////////////
                    /////////////////////////////////////////////

                    // Border Table
                    ws.Columns[1, 10].AutoFit(30);
                    int count = players.Count;
                    ws.Cells[11, 1, 12 + count, 8].Style.Border.BorderAround(ExcelBorderStyle.Thick);
                    ws.Cells[1, 1, 12 + count, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[1, 1, 12 + count, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);

                    var proc = new Process();
                    proc.StartInfo = new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    };
                    wdd = new MessageBoxWindow(true, "Xuất tệp tin thành công");
                    wdd.ShowDialog();
                    proc.Start();
                }
            }
            catch
            {
                wdd = new MessageBoxWindow(false, "Xuất tệp tin thất bại");
                wdd.ShowDialog();
            }
        }
        public void OpenTemPlace()
        {
            try
            {
                string pathProject = System.IO.Directory.GetCurrentDirectory();
                string filePath = @"\Resource\TemplaceTeam.xlsx";

                while (!File.Exists(pathProject + filePath) && System.IO.Directory.GetParent(pathProject) != null)
                {
                    pathProject = System.IO.Directory.GetParent(pathProject).ToString();
                }
                var proc = new Process();
                proc.StartInfo = new ProcessStartInfo(pathProject + filePath)
                {
                    UseShellExecute = true
                };
                proc.Start();
            }
            catch
            {
                wdd = new MessageBoxWindow(false, "Tệp tin mẫu không tồn tại");
                wdd.ShowDialog();
            }
        }
    }
}
