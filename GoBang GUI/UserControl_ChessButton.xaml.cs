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

namespace GoBang_GUI
{
    /// <summary>
    /// UserControl_ChessButton.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_ChessButton : UserControl
    {
        

        private UserControl_ChessButton()
        {
            InitializeComponent();
        }

        public UserControl_ChessButton(GoBang_Lib.Chess chess_lib)
        {
            InitializeComponent();
            Chess_control = chess_lib;
        }





        //——————————————————————————————————————————————————————
       private GoBang_Lib.Chess _chess_control;
       public GoBang_Lib.Chess Chess_control
        {
            get { return _chess_control; }
            set { _chess_control = value; Type = _chess_control.type;Number = _chess_control.number; Isnew=_chess_control.isnew;}        //用棋模型来修改控件的依赖属性，从在将绑定在依赖属性上的图片地址修改成对应的类型
        }
        //——————————————————————————————————————————————————————
        public static DependencyProperty TypeProperty = DependencyProperty.Register(
                "Type",
                typeof(GoBang_Lib.Type),
                typeof(UserControl_ChessButton),
                new PropertyMetadata(GoBang_Lib.Type.Empety,new PropertyChangedCallback(OnTypeChanged)));
        public GoBang_Lib.Type Type
        {
            get { return (GoBang_Lib.Type)this.GetValue(TypeProperty); }
            set { this.SetValue(TypeProperty, value); }
        }
        private static void OnTypeChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var control = source as UserControl_ChessButton;
            var color = (control.Type == GoBang_Lib.Type.Black) ? new SolidColorBrush(Colors.White ) : new SolidColorBrush(Colors.Black);
            control.NumberLabel.FontWeight = FontWeights.Normal;
            if (control.Isnew)
            {
                color = new SolidColorBrush(Colors.Red);
                control.NumberLabel.FontWeight = FontWeights.Bold;
            }
            control.NumberLabel.Foreground = color;
        }
        //——————————————————————————————————————————————————————
        public static DependencyProperty NumberProperty = DependencyProperty.Register(
            "Number",
            typeof(int),
            typeof(UserControl_ChessButton),
            new PropertyMetadata(0, new PropertyChangedCallback(OnNumberChanged)));
        public int Number
        {
            get { return (int)this.GetValue(NumberProperty); }
            set { this.SetValue(NumberProperty, value); }
        }
        private static void OnNumberChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            String number;
            var control = source as UserControl_ChessButton;
            number = control.Number <= 9 ? "0" + control.Number : control.Number + "";
            control.NumberLabel.Content = number;

        }
        //——————————————————————————————————————————————————————
        public static DependencyProperty IsnewProperty = DependencyProperty.Register(
            "Isnew",
            typeof(bool),
            typeof(UserControl_ChessButton),
            new PropertyMetadata(true , new PropertyChangedCallback(OnTypeChanged)));
        public bool Isnew
        {
            get { return (bool)this.GetValue(IsnewProperty); }
            set { this.SetValue(IsnewProperty, value); }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            RingImage.Opacity = 1;
        }


        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
         RingImage.Opacity = 0;
        }
    }

}
 