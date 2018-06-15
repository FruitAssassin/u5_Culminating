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



    public enum GameState { MainMenu, GameOn, GameOver, Settings }


    public static class Globals
    {
        public static bool beginfade = false;
        public static bool musicPlaying = false;


        public static int ApplesCreated = 0;
        public static int PineApplesCreated = 0;
        public static int BananasCreated = 0;
        public static int WatermelonCreated = 0;
        public static int Difficulty = 1;

        public static Point p_mouse;

        public static ImageBrush MMBackground = new ImageBrush(new BitmapImage(new Uri(@"Images\Dojo Background.png", UriKind.Relative)));
        public static ImageBrush RBackground = new ImageBrush(new BitmapImage(new Uri(@"Images\Dojo Wall.png", UriKind.Relative)));

        public static SoundPlayer musicPlayer = new SoundPlayer();
        public static MediaPlayer effectPlayer = new MediaPlayer();


    }

    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        Sword player;


        public GameState gameState;


        TextBlock txt_Begin = new TextBlock();
        public ComboBox CB_Difficulty = new ComboBox();
        public TextBlock txt_Difficulty = new TextBlock();
        Button btn_Back = new Button();

        List<f_Apple> applelist = new List<f_Apple>();
        List<Sword> swordlist = new List<Sword>();
        List<f_Apple> appletodestroy = new List<f_Apple>();
        List<f_AppleSlice> appleslicecreate = new List<f_AppleSlice>();

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Fruit Assasian";

            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

            canvas_mainmenu.Background = Globals.MMBackground;
            canvas_battleground.Background = Globals.RBackground;
            canvas_mainmenu.Visibility = Visibility.Visible;
            gameState = GameState.MainMenu;
            Globals.p_mouse = Mouse.GetPosition(this);
            Console.WriteLine(Mouse.GetPosition(this));


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

                CheckCollision();

                foreach (f_Apple a in applelist)
                {
                        a.Tick();
                }
                foreach (Sword s in swordlist)
                {
                    s.Tick();
                }

            }



            if (gameState == GameState.Settings)
            {
                if (CB_Difficulty.Text == "Easy")
                {
                    Globals.Difficulty = 1;
                }
                else if (CB_Difficulty.Text == "Medium")
                {
                    Globals.Difficulty = 2;
                }
                else if (CB_Difficulty.Text == "Hard")
                {
                    Globals.Difficulty = 3;
                }
            }

        }

        public void CheckCollision()
        {
            foreach (f_Apple a in applelist)
            {
                foreach (Sword s in swordlist)
                {
                        if (a.collidesWith(s))
                        {
                            a.destroy();
                            appletodestroy.Add(a);

                            f_AppleSlice appleSlice = new f_AppleSlice(canvas_battleground, this);
                            appleslicecreate.Add(appleSlice);
                            appleSlice.point = a.Point;
                        }
                }
            }
        }


        public void Click_Play(object sender, RoutedEventArgs e)
        {

            if (gameState == GameState.MainMenu)
            {
                canvas_mainmenu.Visibility = Visibility.Hidden;
                canvas_battleground.Visibility = Visibility.Visible;
                f_Apple apple = new f_Apple(canvas_battleground, this);
                applelist.Add(apple);

                Sword player = new Sword(canvas_battleground, this);
                swordlist.Add(player);
            }

            Console.WriteLine(Globals.Difficulty);
            gameState = GameState.GameOn;
        }
        public void Click_Settings(object sender, RoutedEventArgs e)
        {
            canvas_settings.Children.Clear();

            txt_Difficulty.Text = "Difficulty:";
            canvas_settings.Children.Add(txt_Difficulty);

            btn_Back.Content = "Back";
            btn_Back.Width = 90;
            Canvas.SetLeft(btn_Back, 10);
            Canvas.SetTop(btn_Back, 619);
            btn_Back.Click += new RoutedEventHandler(Click_Back);
            canvas_settings.Children.Add(btn_Back);


            CB_Difficulty.Width = 100;
            CB_Difficulty.DropDownOpened += new EventHandler(CB_Difficulty_Down);
            CB_Difficulty.Items.Add("Easy");
            CB_Difficulty.Items.Add("Medium");
            CB_Difficulty.Items.Add("Hard");
            canvas_settings.Children.Add(CB_Difficulty);
            Canvas.SetTop(CB_Difficulty, 20);




            if (gameState == GameState.MainMenu)
            {
                canvas_mainmenu.Visibility = Visibility.Hidden;
                canvas_settings.Visibility = Visibility.Visible;
            }

            gameState = GameState.Settings;
        }

        public void CB_Difficulty_Down(object Sender, EventArgs e)
        {
        }

        public void Click_Back(object sender, RoutedEventArgs e)
        {
            if (gameState == GameState.Settings)
            {
                canvas_mainmenu.Visibility = Visibility.Visible;
                canvas_settings.Visibility = Visibility.Hidden;
            }

            gameState = GameState.MainMenu;
        }
    }
}
