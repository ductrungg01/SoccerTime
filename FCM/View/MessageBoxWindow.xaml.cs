using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace FCM.View
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        public MessageBoxWindow(bool isSuccess, string content)
        {
            InitializeComponent();
            if (isSuccess)
            {
                imgSuccess.Visibility = Visibility.Visible;
                imgError.Visibility = Visibility.Hidden;
                Title = "Thành công";
            }
            else
            {
                imgSuccess.Visibility = Visibility.Hidden;
                imgError.Visibility = Visibility.Visible;
                Title = "Lỗi";
                btnAccept.Background = new SolidColorBrush(Colors.Red);
            }
            tblMessage.Text = content;
        }
    }
}
