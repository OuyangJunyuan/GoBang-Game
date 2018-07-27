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
using System.Runtime.InteropServices;




namespace GoBang_GUI
{
    /// <summary>
    /// UserControl_ChessBoard.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_ChessBoard : UserControl
    {

        public GoBang_Lib.ChessBoard chessBoard = new GoBang_Lib.ChessBoard();

        public Stack<UserControl_ChessButton> usercontrol_chessbutton = new Stack<UserControl_ChessButton>(225);
           
        

    

        public UserControl_ChessBoard()
        {
            InitializeComponent();
            Warning();  
        }
    
        public void Warning()
        {
            int i = 0, j = 0;
            for (i = 0; i < 15; i++)
            {
                for (j = 0; j < 15; j++)
                {
                    Warning_button(i, j);
                }
            }
        }

        public void Warning_button(int x, int y)
        {
            UserControl_ChessButton Warning = new UserControl_ChessButton(new GoBang_Lib.Chess(GoBang_Lib.Type.Empety, 0, false,x,y));
            BoardGrid_usercontrol.Children.Add(Warning);
            Grid.SetRow(Warning, x);
            Grid.SetColumn(Warning, y);
        }

    }
   
}
