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


    public static class Util
    {
        //Creates a random integer
        private static Random rnd = new Random();
        public static int GetRandomFruitKind()
        {
            return rnd.Next(0, 5);
        }
        public static int ChanceForFruit()
        {
            return rnd.Next(0, 51);
        }
        public static int FruitVelocity()
        {
            return rnd.Next(0, 8);
        }
        public static double SliceFall()
        {
            return rnd.NextDouble();
        }
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
        List<f_Watermelon> watermelonlist = new List<f_Watermelon>();
        List<f_Pineapple> pineapplelist = new List<f_Pineapple>();
        List<f_Apple> bananalist = new List<f_Apple>();
        List<Sword> swordlist = new List<Sword>();

        List<f_Apple> appletodestroy = new List<f_Apple>();
        List<f_Watermelon> watermelontodestroy = new List<f_Watermelon>();
        List<f_Pineapple> pineappletodestroy = new List<f_Pineapple>();

        List<f_AppleSlice> appleslicelist = new List<f_AppleSlice>();
        List<f_WatermelonSlice> watermelonslicelist = new List<f_WatermelonSlice>();
        List<f_PineappleSlice> pineappleslicelist = new List<f_PineappleSlice>();

        List<f_AppleSlice> appleslicetodestroy = new List<f_AppleSlice>();
        List<f_WatermelonSlice> watermelonslicetodestroy = new List<f_WatermelonSlice>();
        List<f_PineappleSlice> pineappleslicetodestroy = new List<f_PineappleSlice>();

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
            
            
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri(@"Sounds\mainmenu.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            
   
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //plays music specific to gamestates
            MusicEvents();
            if (gameState == GameState.GameOn)
            {

                CreateFruits();
                CheckCollision();
                RemoveInstances();

                foreach (f_Apple a in applelist)
                {
                    a.Tick();
                }
                foreach (f_AppleSlice aslice in appleslicelist)
                {
                    aslice.Tick();
                }
                foreach (f_Watermelon w in watermelonlist)
                {
                    w.Tick();
                }
                foreach (f_WatermelonSlice wSlice in watermelonslicelist)
                {
                    wSlice.Tick();
                }
                foreach (f_Pineapple pA in pineapplelist)
                {
                    pA.Tick();
                }
                foreach (f_PineappleSlice pAslice in pineappleslicelist)
                {
                    pAslice.Tick();
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

        public void CreateFruits()
        {
            int chance = Util.ChanceForFruit();

            if (chance == 10)
            {
                int fruitkind = Util.GetRandomFruitKind();

                if (fruitkind == 4)
                {
                    f_Apple apple = new f_Apple(canvas_battleground, this);
                    applelist.Add(apple);
                }
                else if (fruitkind == 3)
                {
                    f_Watermelon watermelon = new f_Watermelon(canvas_battleground, this);
                    watermelonlist.Add(watermelon);
                }
                else if (fruitkind == 2)
                {
                    f_Pineapple pineapple = new f_Pineapple(canvas_battleground, this);
                    pineapplelist.Add(pineapple);
                }
                else if (fruitkind == 1)
                {
                    f_Watermelon watermelon = new f_Watermelon(canvas_battleground, this);
                    watermelonlist.Add(watermelon);
                }
            }
        }

        public void CheckCollision()
        {
            foreach (f_Apple a in applelist)
            {
                if (a.Point.Y > 691)
                {
                    a.destroy();
                    appletodestroy.Add(a);
                }
                foreach (Sword s in swordlist)
                {
                    if (a.collidesWith(s))
                    {
                        a.destroy();
                        appletodestroy.Add(a);

                        f_AppleSlice appleSlice = new f_AppleSlice(canvas_battleground, this);
                        appleslicelist.Add(appleSlice);
                        appleSlice.point = a.Point;

                        f_AppleSlice appleSlicemirror = new f_AppleSlice(canvas_battleground, this);
                        appleslicelist.Add(appleSlicemirror);
                        appleSlicemirror.point = a.Point;
                        appleSlicemirror.rotation = 180;
                    }
                }
            }
            foreach (f_AppleSlice aSlice in appleslicelist)
            {
                if (aSlice.point.Y > 691)
                {
                    aSlice.destroy();
                    appleslicetodestroy.Add(aSlice);
                }
            }


            foreach (f_Watermelon w in watermelonlist)
            {
                if (w.Point.Y > 691)
                {
                    w.destroy();
                    watermelontodestroy.Add(w);
                }
                foreach (Sword s in swordlist)
                {
                    if (w.collidesWith(s))
                    {
                        w.destroy();
                        watermelontodestroy.Add(w);

                        f_WatermelonSlice watermelonSlice = new f_WatermelonSlice(canvas_battleground, this);
                        watermelonslicelist.Add(watermelonSlice);
                        watermelonSlice.point = w.Point;
                        watermelonSlice.point.X = w.Point.X + 64;

                        f_WatermelonSlice watermelonSlicemirror = new f_WatermelonSlice(canvas_battleground, this);
                        watermelonslicelist.Add(watermelonSlicemirror);
                        watermelonSlicemirror.point = w.Point;
                        watermelonSlicemirror.rotation = 180;
                    }
                }
            }
            foreach (f_WatermelonSlice wSlice in watermelonslicelist)
            {
                if (wSlice.point.Y > 691)
                {
                    wSlice.destroy();
                    watermelonslicetodestroy.Add(wSlice);
                }
            }


            foreach (f_Pineapple pA in pineapplelist)
            {
                if (pA.Point.Y > 691)
                {
                    pA.destroy();
                    pineappletodestroy.Add(pA);
                }
                foreach (Sword s in swordlist)
                {
                    if (pA.collidesWith(s))
                    {
                        pA.destroy();
                        pineappletodestroy.Add(pA);

                        f_PineappleSlice pineappleSlice = new f_PineappleSlice(canvas_battleground, this);
                        pineappleslicelist.Add(pineappleSlice);
                        pineappleSlice.point = pA.Point;

                        f_PineappleSlice pineappleSlicemirror = new f_PineappleSlice(canvas_battleground, this);
                        pineappleslicelist.Add(pineappleSlicemirror);
                        pineappleSlicemirror.point = pA.Point;
                        pineappleSlicemirror.rotation = 180;
                    }
                }
            }
            foreach (f_PineappleSlice pASlice in pineappleslicelist)
            {
                if (pASlice.point.Y > 691)
                {
                    pASlice.destroy();
                    pineappleslicetodestroy.Add(pASlice);
                }
            }
        }

        public void RemoveInstances()
        {
            foreach (f_Apple a in appletodestroy)
            {
                applelist.Remove(a);
            }
            foreach (f_AppleSlice aSlice in appleslicetodestroy)
            {
                appleslicelist.Remove(aSlice);
            }
            foreach (f_Watermelon w in watermelontodestroy)
            {
                watermelonlist.Remove(w);
            }
            foreach (f_WatermelonSlice wSlice in watermelonslicetodestroy)
            {
                watermelonslicelist.Remove(wSlice);
            }
            foreach (f_Pineapple pA in pineappletodestroy)
            {
                pineapplelist.Remove(pA);
            }
            foreach (f_PineappleSlice pASlice in pineappleslicetodestroy)
            {
                pineappleslicelist.Remove(pASlice);
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