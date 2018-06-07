using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Security.AccessControl;
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

namespace u5_Culminating
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    public enum GameState { MainMenu, GameOn, GameOver }


    public static class Globals
    {
        public static bool beginfade = false;
        public static bool musicPlaying = false;


        public static int ApplesCreated = 0;
        public static int PineApplesCreated = 0;
        public static int BananasCreated = 0;
        public static int WatermelonCreated = 0;

        public static ImageBrush MMBackground = new ImageBrush(new BitmapImage(new Uri(@"Images\Dojo Background.png", UriKind.Relative)));


        public static SoundPlayer musicPlayer = new SoundPlayer();
        public static MediaPlayer effectPlayer = new MediaPlayer();


    }

    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        Sword player;


        public GameState gameState;


        TextBlock txt_Begin = new TextBlock();
        public TextBox inpt_name = new TextBox();
        public TextBlock txt_name = new TextBlock();

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

            canvas_mainmenu.Background = Globals.MMBackground;
            canvas_mainmenu.Visibility = Visibility.Visible;
            gameState = GameState.MainMenu;

        }

        private void MusicEvents()
        {
            if (gameState == GameState.MainMenu)
            {
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri("mainmenu.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            }
            else if (gameState == GameState.GameOn)
            {
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri("playgame.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            }
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (gameState == GameState.GameOn)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Sword player = new Sword(canvas_battleground, this);
                }
            }
        }


        public void Click_Play(object sender, RoutedEventArgs e)
        {
            gameState = GameState.GameOn;
        }
    }
}