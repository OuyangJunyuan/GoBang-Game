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
using System.IO;
using System.Xml.Serialization;
namespace GoBang_GUI
{
    /// <summary>
    /// Save.xaml 的交互逻辑
    /// </summary>
    public partial class Save : Window
    {
        public GameSave savegame;
        public DateTime dateTime;

        private GoBang_Lib.Chess[] newchessBoard;
        private GoBang_Lib.Type newplayer;



        public Save(GoBang_Lib.Chess[] chessboard, GoBang_Lib.Type player)
        {
            

            newchessBoard = chessboard;
            newplayer = player;


            if (savegame == null)
            {
                if (File.Exists("GameSave.xml"))
                {
                    using (var stream = File.OpenRead("GameSave.xml"))
                    {
                        var serializer = new XmlSerializer(typeof(GameSave));
                        savegame = serializer.Deserialize(stream) as GameSave;
                    }
                }
                else savegame = new GameSave();
            }

            DataContext = savegame;

            InitializeComponent();
            
            dateTime = new DateTime();
            dateTime = DateTime.Now;
            newPlaerTextBox.Text = dateTime.ToShortDateString() + dateTime.ToShortTimeString()+"."+dateTime.Second.ToString();
            plaerNamesListBox.SelectionMode = SelectionMode.Single;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {                    
            using (var stream = File.Open("GameSave.xml", FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(GameSave));
                serializer.Serialize(stream, savegame);
            }
            Close();
        }

        private void AddNewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if(savegame.GamesName.Contains(newPlaerTextBox.Text))
            {
                MessageBox.Show("重名");
                return;
            }
            if(!string.IsNullOrWhiteSpace(newPlaerTextBox.Text))
            {
                Game game = new Game(newPlaerTextBox.Text, newchessBoard, newplayer);
                savegame.AddGame(game );
                newPlaerTextBox.Text = string.Empty;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            savegame = null;
            Close();
        }
    }
}
