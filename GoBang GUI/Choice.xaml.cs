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
using System.Windows.Shapes;

namespace GoBang_GUI
{
    /// <summary>
    /// Choice.xaml 的交互逻辑
    /// </summary>
    public partial class Choice : Window
    {
        public Choice()
        {
            InitializeComponent();
        }

        private bool first = false, computer = false, isover = false, isback = false;

        public int computerlevel = 1;

        public bool IsFirst
        {
            get { return first; }
        }
        public bool IsAgainstcomputer
        {
            get { return computer; }
        }

        public bool IsOk
        {
            get { return isover; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isback = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            isover = true;
            Close();
        }

        private void Firstbutton_Unchecked(object sender, RoutedEventArgs e)
        {
            first = false;
        }

        private void Computerbutton_Unchecked(object sender, RoutedEventArgs e)
        {
            computer = false;
        }

        private void Stupid_Checked(object sender, RoutedEventArgs e)
        {
            computerlevel = 1;
        }

        private void Command_Checked(object sender, RoutedEventArgs e)
        {
            computerlevel = 2;
        }

        private void Normal_Checked(object sender, RoutedEventArgs e)
        {
            computerlevel = 3;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            computerlevel = 4;
        }

        public bool IsBack
        {
            get { return isback; }
        }

        private void Computerbutton_Checked(object sender, RoutedEventArgs e)
        {
                computer = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
                first = true;
        }


    }
}
