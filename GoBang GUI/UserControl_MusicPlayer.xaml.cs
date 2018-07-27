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
using System.Media;
namespace GoBang_GUI
{
    /// <summary>
    /// UserControl_MusicPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_MusicPlayer : UserControl
    {
 
        public bool isplay = true;

        public UserControl_MusicPlayer()
        {
            InitializeComponent();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            mediaElement.Play();
        }

        private void Music_Click(object sender, RoutedEventArgs e)
        {
            isplay = !isplay;
            if (isplay)
            {
                mediaElement.Play();
                playerlabel.Foreground = new SolidColorBrush(Colors.Black);
                line1.Opacity = line2.Opacity = 0;
            }
            else
            {
                mediaElement.Pause();
                playerlabel.Foreground = new SolidColorBrush(Colors.Gray);
                line1.Opacity = line2.Opacity = 1;
            }
        }

    }
}
