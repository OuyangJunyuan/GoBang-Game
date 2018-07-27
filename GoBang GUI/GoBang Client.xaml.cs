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
using System.Windows.Forms;
using GoBang_Lib;
using System.Media;
using System.IO;
using System.Xml.Serialization;
namespace GoBang_GUI
{
    /// <summary>
    /// GoBang_Client.xaml 的交互逻辑
    /// </summary>
    public partial class GoBang_Client : Window
    {
        public GoBang_Client()
        {
            InitializeComponent();
            ClockInit();
            this.gameClient.Opacity = 0;
            this.gameClient.IsEnabled = false;
            this.pb_client.Stop();
            this.bgmplayer.mediaElement.Stop();
        }

        SoundPlayer soundplayer = new SoundPlayer(@"Resources\put.wav");

        Timer myTimer = new Timer();

        GoBang_Lib.Type player = GoBang_Lib.Type.Black;
        bool iswin = false, first = false, isAgainstcomputer = false;   //first表示电脑是否先手
        int times = 0, skilllevel = 0;





        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int row = (int)(e.MouseDevice.GetPosition(BoardGrid).Y / 46.666), col = (int)(e.MouseDevice.GetPosition(BoardGrid).X / 46.666);
            if (row > 14) row = 14;
            if (col > 14) col = 14;
            if (row < 0) row = 0;
            if (col < 0) row = 0;

            if (!PutChess(col, row))
            {
                return;
            }
        }

        private bool PutChess(int col, int row)
        {
            if (iswin) return false;
            //判断此处是否有棋了------------------------------------------------------------------------------------------
            if (cb_client.chessBoard.Board[col, row].type != GoBang_Lib.Type.Empety)
            {
                System.Windows.MessageBox.Show("(" + (col + 1) + "," + (row + 1) + ")已经有棋子了");
                return false;
            }

            //修改上一个棋子------------------------------------------------------------------------------------------
            if (cb_client.usercontrol_chessbutton.Count != 0)
            {
                cb_client.chessBoard.chesses_oder.Peek().isnew = false;
                cb_client.usercontrol_chessbutton.Peek().Isnew = false;
            }
            soundplayer.Play();
            //模型下棋------------------------------------------------------------------------------------------
            GoBang_Lib.Chess chess = cb_client.chessBoard.Board[col, row];

            chess.isnew = true;
            chess.number = times;
            chess.type = player;

            cb_client.chessBoard.chesses_oder.Push(chess);

            //局数+1------------------------------------------------------------------------------------------

            times++;

            //图形下棋------------------------------------------------------------------------------------------
            UserControl_ChessButton chessButton = new UserControl_ChessButton(new GoBang_Lib.Chess(player, times, true, col, row));

            cb_client.BoardGrid_usercontrol.Children.Add(chessButton);
            cb_client.usercontrol_chessbutton.Push(chessButton);

            Grid.SetRow(chessButton, row); Grid.SetColumn(chessButton, col);

            //判断是否胜利------------------------------------------------------------------------------------------
            if (GoBang_Lib.Tools.IsWin(cb_client.chessBoard, col, row))
            {
                iswin = true;
                string win = player == GoBang_Lib.Type.Black ? "黑棋获胜" : "白棋获胜";
                System.Windows.MessageBox.Show(win);
                System.Threading.Thread.Sleep(1000);

            }
            else
            {
                SwitchPlayer();
                pb_client.ResetTime();
            }
            return true;
        }

        public void RandomPutChess()
        {
            Random random = new Random();
            do
            {
                int x = random.Next(0, 15), y = random.Next(0, 15);
                if (cb_client.chessBoard.Board[x, y].type == GoBang_Lib.Type.Empety && !iswin)
                {
                    PutChess(x, y);
                    break;
                }
            } while (true);
        }

        private void SwitchPlayer()
        {
            player = player == GoBang_Lib.Type.Black ? GoBang_Lib.Type.White : GoBang_Lib.Type.Black;
        }

        public void Pause()
        {
            BoardGrid.IsEnabled = false;
        }
        public void Continue()
        {
            BoardGrid.IsEnabled = true;
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            Pause();

            if (ChoiceGame())
            {
                Delete_n_Chess(times);
                iswin = false;
                player = GoBang_Lib.Type.Black;
                pb_client.ResetTime();
            }
            Continue();
        }
        private void Retract(object sender, RoutedEventArgs e)
        {
            if (isAgainstcomputer)
                Delete_n_Chess(2);
            else
                Delete_a_Chess();
        }

        private void Delete_a_Chess()
        {
            if (cb_client.chessBoard.chesses_oder.Count > 0 && cb_client.usercontrol_chessbutton.Count > 0)
            {
                //获取最后一颗棋子的模型
                GoBang_Lib.Chess peekchess = cb_client.chessBoard.chesses_oder.Pop();

                int x = peekchess.x, y = peekchess.y;
                //在棋盘模型中清除
                cb_client.chessBoard.Board[x, y].type = GoBang_Lib.Type.Empety;

                //视图中移除
                UserControl_ChessButton cb = cb_client.usercontrol_chessbutton.Pop();
                cb_client.BoardGrid_usercontrol.Children.Remove(cb);

                //把上一个棋子标记为新
                if (cb_client.usercontrol_chessbutton.Count != 0)
                {
                    cb_client.chessBoard.chesses_oder.Peek().isnew = true;
                    cb_client.usercontrol_chessbutton.Peek().Isnew = true;
                }
                //对局次数减一
                times--;
                SwitchPlayer();
            }
        }
        private void Delete_n_Chess(int n)
        {
            for (int i = n; i > 0; i--)
            {
                Delete_a_Chess();
            }
        }

        private void ClockInit()
        {
            myTimer.Tick += new EventHandler(Timer1_Tick);

            myTimer.Enabled = true;

            myTimer.Interval = 100;

            myTimer.Start();
        }

        public void Timer1_Tick(object sender, EventArgs e)
        {

            if (iswin || pb_client.IsStop)
                Pause();
            else
                Continue();

            if (pb_client.Time <= 0 && !iswin)
            {
                RandomPutChess();
                if (isAgainstcomputer && !iswin)
                {
                    Computer();
                }
            }





            if (first && times == 0 && isAgainstcomputer)
            {
                PutChess(7, 7);
            }

            GoBang_Lib.Type type_computer = first == true ? GoBang_Lib.Type.Black : GoBang_Lib.Type.White;

            if (isAgainstcomputer == true && iswin == false && player == type_computer)
            {
                System.DateTime current = new System.DateTime();
                current = System.DateTime.Now;

                Computer();

                double timeSpan = (System.DateTime.Now - current).TotalSeconds;
                label1.Content = "搜索节点个数:" + GoBang_Lib.Tools.count + "\n剪枝节点个数：" + GoBang_Lib.Tools.cut + "\n思考时间：" + timeSpan + "秒";
            }

        }

        private void Begin_startbutton_Click(object sender, RoutedEventArgs e)
        {

            if (beginlabel.Content.ToString() == "返回游戏")
            {
                this.BeginGrid.Opacity = 0;
                this.BeginGrid.IsEnabled = false;
                this.BeginGrid.IsHitTestVisible = false;

                this.bgmplayer.mediaElement.Play();
                this.gameClient.Opacity = 1;
                this.gameClient.IsEnabled = true;
                this.pb_client.Start();
                return;
            }
            if (ChoiceGame())
            {
                this.BeginGrid.Opacity = 0;
                this.BeginGrid.IsEnabled = false;
                this.BeginGrid.IsHitTestVisible = false;

                this.bgmplayer.mediaElement.Play();
                this.gameClient.Opacity = 1;
                this.gameClient.IsEnabled = true;

                this.pb_client.Start();

                if (this.gameClient.Opacity == 1)
                {
                    beginlabel.Content = "返回游戏";
                }
            }
            else return;
        }

        private bool ChoiceGame()
        {
            Choice choice = new Choice();
            choice.ShowDialog();

            if (choice.IsFirst) //人先手;
            {
                first = false;
            }
            else first = true;


            if (choice.IsAgainstcomputer)
            {
                isAgainstcomputer = true;
            }
            else isAgainstcomputer = false;

            skilllevel = choice.computerlevel;

            return choice.IsOk;
        }

        private void Goback(object sender, RoutedEventArgs e)
        {
            var client = this.gameClient;
            client.Opacity = 0;
            client.IsEnabled = false;

            var beginpage = this.BeginGrid;
            beginpage.Opacity = 1;
            beginpage.IsEnabled = true;
            beginpage.IsHitTestVisible = true;
            this.bgmplayer.mediaElement.Pause();


            this.pb_client.Stop();
        }

        private void About_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new About();
            window.ShowDialog();

        }

        private void Computer()
        {
            if (skilllevel == 1)
            {
                GoBang_Lib.Point com = GoBang_Lib.Tools.AI_1(cb_client.chessBoard, player);
                PutChess(com.X, com.Y);
            }
            else if (skilllevel == 2)
            {
                GoBang_Lib.Point com = GoBang_Lib.Tools.AI_3(cb_client.chessBoard, player);
                PutChess(com.X, com.Y);
            }
            else if (skilllevel == 3)
            {
                GoBang_Lib.Tools.deepMax = 4;
                GoBang_Lib.Point com = GoBang_Lib.Tools.MaxMin(cb_client.chessBoard, player);
                PutChess(com.X, com.Y);
            }
            else if (skilllevel == 4)
            {
                GoBang_Lib.Tools.deepMax = 6;
                GoBang_Lib.Point com = GoBang_Lib.Tools.MaxMin(cb_client.chessBoard, player);
                PutChess(com.X, com.Y);
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Recover_button_Click(object sender, RoutedEventArgs e)
        {
            Window recoverwindow = new Recover();

            recoverwindow.ShowDialog();

            GameSave savegame;


            if (File.Exists("GameSave.xml"))
            {
                using (var stream = File.OpenRead("GameSave.xml"))
                {
                    var serializer = new XmlSerializer(typeof(GameSave));
                    savegame = serializer.Deserialize(stream) as GameSave;
                }
            }
            else savegame = new GameSave();

            if (savegame.SelectedGames == null || savegame.SelectedGames.Chesses == null)
            {
                return;
            }

            Game game = savegame.SelectedGames;
            Chess[] chesses = game.Chesses;

            //清空棋盘
            Delete_n_Chess(times);
            iswin = false;
            player = GoBang_Lib.Type.Black;
            pb_client.ResetTime();

            //复盘
            if (chesses.Length > 0)
            {
                player = chesses[chesses.Length - 1].type;

                for (int i = chesses.Length - 1; i >= 0; i--)
                {
                    PutChess(chesses[i].x, chesses[i].y);
                }
            }

            //清空选中状态
            savegame.SelectedGames = null;

            using (var stream = File.Open("GameSave.xml", FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(GameSave));
                serializer.Serialize(stream, savegame);
            }



            //显示
            this.bgmplayer.mediaElement.Play();
            this.BeginGrid.Opacity = 0;
            this.BeginGrid.IsEnabled = false;
            this.BeginGrid.IsHitTestVisible = false;

            this.gameClient.Opacity = 1;
            this.gameClient.IsEnabled = true;

            this.pb_client.Start();

            if (this.gameClient.Opacity == 1)
            {
                beginlabel.Content = "返回游戏";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pause();
            Stack<GoBang_Lib.Chess> chesses = cb_client.chessBoard.chesses_oder;
            Window window = new Save(chesses.ToArray(), player);
            window.ShowDialog();
        }


    }
}
