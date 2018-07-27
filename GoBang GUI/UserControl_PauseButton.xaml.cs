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
using System.Windows.Forms;
namespace GoBang_GUI
{
    /// <summary>
    /// UserControl_PauseButton.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_PauseButton : System.Windows.Controls.UserControl
    {
        private int time = 30, min = 0, second = 0; //记录秒数
        private int isstop = 1;//计数

        private  Timer myTimer = new Timer();
        private bool isStop = false;

        public int Time
        {
            get { return time; }
        }
        public bool IsStop
        {
            get { return isStop; }
        }           

        public void Stop()
        {
            line1.Opacity = line2.Opacity = 1;
            myTimer.Stop();
        }
        public void Start()
        {
            line1.Opacity = line2.Opacity = 0;
            myTimer.Start();
        }
        public void ResetTime()
        {
            time = 30;
        }


        public UserControl_PauseButton()
        {
            InitializeComponent();
            ClockInit();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isstop++;
            if (isstop % 2 == 0)
            {
                Stop();
                isStop = true;
            }
            else { Start(); isStop = false; }

            if (isStop)
            {
                time_label.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else if (time <= 5)
            {
                time_label.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                time_label.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        private void ClockInit()
        {

            myTimer.Tick += new EventHandler(Timer1_Tick);

            myTimer.Enabled = true;

            myTimer.Interval = 1000;

            myTimer.Start();

        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            time--;
            if (time <= 0) time = 0;


            min = time / 60;
            second = time % 60;
            if(second<10)
            time_label.Content = min + ":0" + second;
            else time_label.Content = min + ":" + second;

            if (time <= 5)
            {
                time_label.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                time_label.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        


    }
}
