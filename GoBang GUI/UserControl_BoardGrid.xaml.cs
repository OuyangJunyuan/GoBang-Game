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
    /// UserControl_BoardGrid.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_BoardGrid : UserControl
    {
        int width =0 ;
        public UserControl_BoardGrid()
        {
            InitializeComponent();
            DrawingSqr(450, 450);
            DrawingSqr(30 + 60 * 3, 30 + 60 * 3);
            DrawingSqr(30 + 60 * 11, 30 + 60 * 3);
            DrawingSqr(30 + 60 * 3, 30 + 60 * 11);
            DrawingSqr(30 + 60 * 11, 30 + 60 * 11);
        }
        protected void DrawBoard()                                                      //画棋盘
        {
            int i = 0;


            for (i = 0; i < 15; i++)
            {
                DrawingLine(new Point(i * 60 + 30, 30), new Point(i * 60 + 30, 900 - 30));
            }

            for (i = 0; i < 15; i++)
            {
                DrawingLine(new Point(30, i * 60 + 30), new Point(900 - 30, i * 60 + 30));
            }

            DrawingSqr(450, 450);
            DrawingSqr(30 + 60 * 3, 30 + 60 * 3);
            DrawingSqr(30 + 60 * 11, 30 + 60 * 3);
            DrawingSqr(30 + 60 * 3, 30 + 60 * 11);
            DrawingSqr(30 + 60 * 11, 30 + 60 * 11);

        }
        protected void DrawingLine(Point startPt, Point endPt)
        {

            LineGeometry myLine = new LineGeometry
            {
                StartPoint = startPt,

                EndPoint = endPt
            };


            Path myPath = new Path
            {
                Stroke = Brushes.Black,

                StrokeThickness = width,

                Data = myLine
            };

            BoardGrid.Children.Add(myPath);
        }
        protected void DrawingSqr(int x, int y)
        {
            DrawingLine(new Point(x - 4 * 2, y - 4 * 1.5), new Point(x + 4 * 2, y - 4 * 1.5));
            DrawingLine(new Point(x - 4 * 2, y - 4), new Point(x + 4* 2, y -4));
            DrawingLine(new Point(x - 4 * 2, y + 4), new Point(x + 4 * 2, y + 4));
            DrawingLine(new Point(x - 4 * 2, y + 4 * 1.5), new Point(x +4 * 2, y + 4 * 1.5));
        }
        protected void DrawingScreen()
        {
            LineGeometry myLine = new LineGeometry
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 900)
            };



            Path myPath = new Path
            {
                Stroke = Brushes.Transparent,

                StrokeThickness = 3000,

                Data = myLine
            };

            BoardGrid.Children.Add(myPath);
        }
    }
}
