using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FCM.View;
using FCM.DAO;

namespace FCM.ViewModel
{
    class AddGoalTypeViewModel : BaseViewModel
    {
        public ICommand CancelAddGoalTypeCommand { get; set; }
        public ICommand AddGoalTypeCommand { get; set; }

        public AddGoalTypeViewModel()
        {
            AddGoalTypeCommand = new RelayCommand<AddGoalTypeWindow>((parameter) => true, (parameter) => AddGoalType(parameter));
            CancelAddGoalTypeCommand = new RelayCommand<AddGoalTypeWindow>((parameter) => true, (parameter) => parameter.Close());
        }

        public void AddGoalType(AddGoalTypeWindow parameter)
        {
            if (parameter.isAdd)
            {
                try
                {
                    string name = InputFormat.Instance.FomartSpace(parameter.tbName.Text);
                    if (name == "")
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Tên bàn thắng không được để trống");
                        wd.ShowDialog();
                        return;
                    }
                    if (TypeOfGoalDAO.Instance.IsExistNameTypeGoal(parameter.idTournament, name))
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Loại bàn thắng đã tồn tại");
                        wd.ShowDialog();
                        return;
                    }
                    else
                    {
                        TypeOfGoalDAO.Instance.AddTypeGoal(parameter.idTournament, name);
                        MessageBoxWindow wd = new MessageBoxWindow(true, "Thêm loại bàn thắng thành công");
                        wd.ShowDialog();
                        parameter.Close();
                    }
                }
                catch
                {
                    MessageBoxWindow wd = new MessageBoxWindow(false, "Lỗi kết nối");
                    wd.ShowDialog();
                    return;
                }
            }
            else
            {
                try
                {
                    string oldName = parameter.oldName;
                    string name = InputFormat.Instance.FomartSpace(parameter.tbName.Text);
                    if (name == "")
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Tên bàn thắng không được để trống");
                        wd.ShowDialog();
                        return;
                    }
                    if (name != oldName && TypeOfGoalDAO.Instance.IsExistNameTypeGoal(parameter.idTournament, name))
                    {
                        MessageBoxWindow wd = new MessageBoxWindow(false, "Loại bàn thắng đã tồn tại");
                        wd.ShowDialog();
                        return;
                    }
                    else
                    {
                        TypeOfGoalDAO.Instance.EditNameTypeGoal(parameter.idTournament, name, oldName);
                        MessageBoxWindow wd = new MessageBoxWindow(true, "Sửa loại bàn thắng thành công");
                        wd.ShowDialog();
                        parameter.Close();
                    }
                }
                catch
                {
                    MessageBoxWindow wd = new MessageBoxWindow(false, "Lỗi kết nối");
                    wd.ShowDialog();
                    return;
                }
            }
        }

    }
}
