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
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace GoBang_GUI
{
    /// <summary>
    /// Recover.xaml 的交互逻辑
    /// </summary>
    public partial class Recover : Window
    {
        public static GameSave savegame;
        public static Game selectedgame;
        public Recover()
        {

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
            plaerNamesListBox.SelectionMode = SelectionMode.Single;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = (string)plaerNamesListBox.SelectedItem;
            ObservableCollection<string> names = savegame.GamesName;
            ObservableCollection<Game> games = savegame.Games;
            int i = names.IndexOf(name);
            if (i >= 0)
            {
                selectedgame = games[i];
                savegame.SelectedGames = selectedgame; //获取选中项

                using (var stream = File.Open("GameSave.xml", FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(GameSave));
                    serializer.Serialize(stream, savegame);
                }
                Close();
            }
            else
            {
                MessageBox.Show("未选择任何记录");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = (string)plaerNamesListBox.SelectedItem;
            ObservableCollection<string> names = savegame.GamesName;
            ObservableCollection<Game> games = savegame.Games;
            int i = names.IndexOf(name);

            if (i >= 0)
            {
                selectedgame = games[i];
                savegame.Games.RemoveAt(i);
                savegame.GamesName.RemoveAt(i);
                using (var stream = File.Open("GameSave.xml", FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(GameSave));
                    serializer.Serialize(stream, savegame);
                }
            }
            else
            {
                MessageBox.Show("未选择任何记录");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            savegame = null;
            Close();
        }
    }
}
