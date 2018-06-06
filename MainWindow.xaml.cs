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


        public static bool areAliensCreated = false;
        public static bool areAliensCreated2 = false;
        public static bool areAliensCreated3 = false;
        public static bool areAliensCreated4 = false;
        public static bool beginfade = false;
        public static bool musicPlaying = false;


        public static int SP1AliensCreated = 0;
        public static int SP2AliensCreated = 0;
        public static int SP3AliensCreated = 0;
        public static int SP4AliensCreated = 0;


        public static ImageBrush Fake_Apple = new ImageBrush(new BitmapImage(new Uri("FakeApple.png", UriKind.Relative)));
        public static ImageBrush Fake_Banana = new ImageBrush(new BitmapImage(new Uri("FakeBanana.png", UriKind.Relative)));
        public static ImageBrush Fake_Pineapple = new ImageBrush(new BitmapImage(new Uri("FakePineapple.png", UriKind.Relative)));
        public static ImageBrush Fake_Watermelon = new ImageBrush(new BitmapImage(new Uri("FakeWatermelon.png", UriKind.Relative)));
        public static ImageBrush Fake_Bomb = new ImageBrush(new BitmapImage(new Uri("FakeBomb.png", UriKind.Relative)));

        public static SoundPlayer musicPlayer = new SoundPlayer();
        public static MediaPlayer effectPlayer = new MediaPlayer();
    }

    public partial class MainWindow : Window
    {

        public GameState gameState;

        Rectangle FakeApple = new Rectangle();
        Rectangle FakeBanana = new Rectangle();
        Rectangle FakePineapple = new Rectangle();
        Rectangle FakeWatermelon = new Rectangle();
        Rectangle FakeBomb = new Rectangle();


        TextBlock txt_Begin = new TextBlock();
        public TextBox inpt_name = new TextBox();
        public TextBlock txt_name = new TextBlock();

        public MainWindow()
        {


            InitializeComponent();

            gameState = GameState.MainMenu;
            CreateMainMenu();



        }

        private void CreateMainMenu()
        {


            FakeApple.Fill = Globals.Fake_Apple; FakeApple.Height = 128; FakeApple.Width = 128; Canvas.SetTop(FakeApple, 84); Canvas.SetLeft(FakeApple, 276);
            FakeBanana.Fill = Globals.Fake_Banana; FakeBanana.Height = 128; FakeBanana.Width = 128; Canvas.SetTop(FakeBanana, 148); Canvas.SetLeft(FakeBanana, 176);
            FakePineapple.Fill = Globals.Fake_Pineapple; FakePineapple.Height = 128; FakePineapple.Width = 128; Canvas.SetTop(FakePineapple, 148); Canvas.SetLeft(FakePineapple, 101);
            FakeWatermelon.Fill = Globals.Fake_Watermelon; FakeWatermelon.Height = 128; FakeWatermelon.Width = 128; Canvas.SetTop(FakeWatermelon, 148); Canvas.SetLeft(FakeWatermelon, 451);
            FakeBomb.Fill = Globals.Fake_Bomb; FakeBomb.Height = 128; FakeBomb.Width = 128; Canvas.SetTop(FakeBomb, 148); Canvas.SetLeft(FakeBomb, 376);



            canvas_mainmenu.Children.Add(FakeApple);
            canvas_mainmenu.Children.Add(FakeBanana);
            canvas_mainmenu.Children.Add(FakePineapple);
            canvas_mainmenu.Children.Add(FakeWatermelon);
            canvas_mainmenu.Children.Add(FakeBomb);

        }


        private void FadeBeginText()
        {
            if (gameState == GameState.MainMenu)
            {
                if (Globals.beginfade == false)
                {
                    if (txt_Begin.Opacity != 0 || txt_Begin.Opacity >= 1)
                    {
                        txt_Begin.Opacity = txt_Begin.Opacity - 0.025;
                    }
                    if (txt_Begin.Opacity <= 0)
                    {
                        Globals.beginfade = true;
                    }
                }
                if (Globals.beginfade == true)
                {
                    if (txt_Begin.Opacity != 1 || txt_Begin.Opacity >= 0)
                    {
                        txt_Begin.Opacity = txt_Begin.Opacity + 0.025;
                    }
                    if (txt_Begin.Opacity >= 1)
                    {
                        Globals.beginfade = false;
                    }
                }
            }
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
    }
}
        

