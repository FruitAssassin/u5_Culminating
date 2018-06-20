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
        //Booleans
        public static bool btn_SubmitClicked = false;
        public static bool areStatsEntered = false;
        public static bool musicPlaying = false;
        public static bool isMainMenuCreated = false;
        public static bool isHUDCreated = false;
        public static bool isLeaderboardCreated = false;
        public static bool areStatsWriten = false;

        //Integers
        public static int int_Difficulty = 1;
        public static int p_score = 0;
        public static int p_lives = 3;
        public static int p_combo = 0;
        public static int p_bestcombo = 0;
        public static int first_p_score = 0;
        public static int first_p_combo = 0;
        public static int second_p_score = 0;
        public static int second_p_combo = 0;
        public static int third_p_score = 0;
        public static int third_p_combo = 0;
        public static int fourth_p_score = 0;
        public static int fourth_p_combo = 0;
        public static int fifth_p_score = 0;
        public static int fifth_p_combo = 0;
        public static int yourPlace = 0;

        //Strings
        public static string str_Difficulty = "Easy";
        public static string str_Song = "Sensoki";
        public static string first_p_name = "name";
        public static string first_p_stats = "Score: " + first_p_score.ToString() + "   Combo: " + first_p_combo.ToString() + "\nDifficulty: " + first_p_difficulty;
        public static string second_p_name = "name";
        public static string second_p_stats = "Score: " + second_p_score.ToString() + "   Combo: " + second_p_combo.ToString() + "\nDifficulty: " + second_p_difficulty;
        public static string third_p_name = "name";
        public static string third_p_stats = "Score: " + third_p_score.ToString() + "   Combo: " + third_p_combo.ToString() + "\nDifficulty: " + third_p_difficulty;
        public static string fourth_p_name = "name";
        public static string fourth_p_stats = "Score: " + fourth_p_score.ToString() + "   Combo: " + fourth_p_combo.ToString() + "\nDifficulty: " + fourth_p_difficulty;
        public static string fifth_p_name = "name";
        public static string fifth_p_stats = "Score: " + fifth_p_score.ToString() + "   Combo: " + fifth_p_combo.ToString() + "\nDifficulty: " + fifth_p_difficulty;
        public static string yourStats = "Score: " + p_score.ToString() + "   Combo: " + p_combo.ToString() + "\nDifficulty: " + str_Difficulty;
        public static string yourName;
        public static string[] censoredwords = new string[34];

        public static string first_p_difficulty;
        public static string second_p_difficulty;
        public static string third_p_difficulty;
        public static string fourth_p_difficulty;
        public static string fifth_p_difficulty;

        //Misc
        public static Point p_mouse;

        public static ImageBrush MMBackground = new ImageBrush(new BitmapImage(new Uri(@"Images\Dojo Background.png", UriKind.Relative)));
        public static ImageBrush RBackground = new ImageBrush(new BitmapImage(new Uri(@"Images\Dojo Wall.png", UriKind.Relative)));
        public static ImageBrush sprite_Leaderboard = new ImageBrush(new BitmapImage(new Uri(@"Images\Leaderboard.png", UriKind.Relative)));

        public static SoundPlayer musicPlayer = new SoundPlayer();
        public static MediaPlayer effectPlayer = new MediaPlayer();

        //Creates directory in order to download and upload stats
        public static Assembly assembly = Assembly.GetExecutingAssembly();
        public static string path = System.IO.Path.GetDirectoryName(assembly.Location);
        public static Uri statspath = new Uri("ftp://ftp.ezyro.com/htdocs/FNstats.txt");


    }


    public static class Util
    {
        //Creates a random integer
        private static Random rnd = new Random();
        public static int GetRandomFruitKind()
        {
            return rnd.Next(0,6);
        }
        public static int ChanceForFruit()
        {
            return rnd.Next(0, (33 / Globals.int_Difficulty + (3 * Globals.int_Difficulty)));
        }
        public static int FruitVelocity()
        {
            return rnd.Next(0, 9);
        }
        public static double SliceFall()
        {
            return rnd.NextDouble();
        }
    }

    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer comboTimer = new System.Windows.Threading.DispatcherTimer();
        Sword player;


        public GameState gameState;


        Button btn_Play = new Button();
        Button btn_Settings = new Button();
        TextBlock txt_Begin = new TextBlock();
        ComboBox CB_Difficulty = new ComboBox();
        TextBlock txt_Difficulty = new TextBlock();
        TextBlock txt_Score = new TextBlock();
        TextBlock txt_Lives = new TextBlock();
        TextBlock txt_Combo = new TextBlock();
        ComboBox CB_Song = new ComboBox();
        TextBlock txt_Song = new TextBlock();
        Button btn_Back = new Button();
        TextBox inpt_name = new TextBox();
        TextBlock txt_name = new TextBlock();
        Button btn_submit = new Button();
        TextBlock first_name = new TextBlock();
        TextBlock first_stats = new TextBlock();
        TextBlock second_name = new TextBlock();
        TextBlock second_stats = new TextBlock();
        TextBlock third_name = new TextBlock();
        TextBlock third_stats = new TextBlock();
        TextBlock fourth_name = new TextBlock();
        TextBlock fourth_stats = new TextBlock();
        TextBlock fifth_name = new TextBlock();
        TextBlock fifth_stats = new TextBlock();
        TextBlock your_stats = new TextBlock();
        Button leave_Leaderboard = new Button();
        Rectangle darkenLeaderboard = new Rectangle();


        List<f_Apple> applelist = new List<f_Apple>();
        List<f_Watermelon> watermelonlist = new List<f_Watermelon>();
        List<f_Pineapple> pineapplelist = new List<f_Pineapple>();
        List<f_Banana> bananalist = new List<f_Banana>();
        List<Sword> swordlist = new List<Sword>();
        List<Bomb> bomblist = new List<Bomb>();

        List<f_Apple> appletodestroy = new List<f_Apple>();
        List<f_Watermelon> watermelontodestroy = new List<f_Watermelon>();
        List<f_Pineapple> pineappletodestroy = new List<f_Pineapple>();
        List<f_Banana> bananatodestroy = new List<f_Banana>();
        List<Bomb> bombtodestroy = new List<Bomb>();
        List<Sword> swordtodestroy = new List<Sword>();

        List<f_AppleSlice> appleslicelist = new List<f_AppleSlice>();
        List<f_WatermelonSlice> watermelonslicelist = new List<f_WatermelonSlice>();
        List<f_PineappleSlice> pineappleslicelist = new List<f_PineappleSlice>();
        List<f_BananaSlice> bananaslicelist = new List<f_BananaSlice>();

        List<f_AppleSlice> appleslicetodestroy = new List<f_AppleSlice>();
        List<f_WatermelonSlice> watermelonslicetodestroy = new List<f_WatermelonSlice>();
        List<f_PineappleSlice> pineappleslicetodestroy = new List<f_PineappleSlice>();
        List<f_BananaSlice> bananaslicetodestroy = new List<f_BananaSlice>();

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Fruit Assasian";

            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

            comboTimer.Tick += comboTimer_Tick;
            comboTimer.Interval = new TimeSpan(0, 0, 0, 3);//combotimer
            comboTimer.Start();

            //If the directory in the debug folder doesn't exist, create it.
            if (!Directory.Exists(Globals.path))
            {
                Directory.CreateDirectory(Globals.path);
            }
            Uri stats = new Uri(Globals.path + @"\Stats.txt");


            CreateMainmenu();

            //Reads stats method
            ReadStats();
            //Refresh stats method
            RefreshStats();
            //Set censored words method
            SetCensoredWords();


        }

        private void CreateMainmenu()
        {
            canvas_mainmenu.Background = Globals.MMBackground;
            canvas_battleground.Background = Globals.RBackground;
            canvas_mainmenu.Visibility = Visibility.Visible;
            gameState = GameState.MainMenu;
            Globals.p_mouse = Mouse.GetPosition(this);
            Console.WriteLine(Mouse.GetPosition(this));

            canvas_mainmenu.Children.Add(btn_Play);
            canvas_mainmenu.Children.Add(btn_Settings);
            btn_Play.Click += new RoutedEventHandler(Click_Play); btn_Play.Content = "Play!"; btn_Play.Width = 100; btn_Play.Height = 35; btn_Play.FontSize = 20; Canvas.SetTop(btn_Play, 136); Canvas.SetLeft(btn_Play, 286);
            btn_Settings.Click += new RoutedEventHandler(Click_Settings); btn_Settings.Content = "Settings"; btn_Settings.Width = 100; btn_Settings.Height = 35; btn_Settings.FontSize = 20; Canvas.SetTop(btn_Settings, 176); Canvas.SetLeft(btn_Settings, 286);

            Globals.isMainMenuCreated = true;
        }

        private void MusicEvents()
        {

            if (Globals.str_Song == "Sensoki")
            {
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri(@"Sounds\Sensoki.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            }
            else if (Globals.str_Song == "Tokyo")
            {
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri(@"Sounds\Tokyo.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            }
            else if (Globals.str_Song == "Uchigatana")
            {
                if (Globals.musicPlaying == false)
                {
                    Globals.musicPlayer.Stop();
                    Uri music = new Uri(@"Sounds\Uchigatana.wav", UriKind.Relative);
                    Globals.musicPlayer.SoundLocation = music.ToString();
                    Globals.musicPlayer.PlayLooping();

                    Globals.musicPlaying = true;
                }
            }


        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if(gameState == GameState.MainMenu)
            {
                this.Title = "Fruit Assasian V0.9 - Current Top Player: " + Globals.first_p_name + ", Score: " + Globals.first_p_score;
                if (Globals.isMainMenuCreated == false)
                {
                    CreateMainmenu();
                }

                //Check and set values
                if (Globals.areStatsWriten == true)
                {
                    Globals.areStatsWriten = false;
                }

                //Check and set values
                if (Globals.isLeaderboardCreated == true)
                {
                    Globals.isLeaderboardCreated = false;
                }

            }

            //plays music specific to gamestates
            MusicEvents();
            if (gameState == GameState.GameOn)
            {
                this.Title = "Fruit Assasian V0.9 - Current Top Player: " + Globals.first_p_name + ", Score: " + Globals.first_p_score;

                HUD();
                CreateFruits();
                CheckCollision();
                RemoveInstances();
                InstancesTick();

            }



            else if (gameState == GameState.Settings)
            {
                if (CB_Difficulty.Text == "Easy")
                {
                    Globals.int_Difficulty = 1;
                    Globals.str_Difficulty = "Easy";
                }
                else if (CB_Difficulty.Text == "Medium")
                {
                    Globals.int_Difficulty = 2;
                    Globals.str_Difficulty = "Medium";
                }
                else if (CB_Difficulty.Text == "Hard")
                {
                    Globals.int_Difficulty = 3;
                    Globals.str_Difficulty = "Hard";
                }
            }

            //end game tick events
            else if (gameState == GameState.GameOver)
            {
                //Clear previous canvas
                canvas_mainmenu.Children.Clear();
                Globals.isMainMenuCreated = false;

                //Set title
                this.Title = "Game Over!";
                canvas_leaderboard.Visibility = Visibility.Visible;

                //Create leaderboard
                Rectangle leaderboard = new Rectangle();
                leaderboard.Height = 480;
                leaderboard.Width = 380;
                Canvas.SetTop(leaderboard, 100);
                Canvas.SetLeft(leaderboard, 150);

                //Create leaderboard instance/objects
                if (Globals.isLeaderboardCreated == false)
                {
                    Globals.yourStats = "Score: " + Globals.p_score.ToString() + "   Best Combo:" + Globals.p_bestcombo.ToString() + "\nDifficulty:" + Globals.str_Difficulty;
                    CreateLeaderboard(leaderboard);
                    leaderboard.Fill = Globals.sprite_Leaderboard;
                }

                if (Globals.btn_SubmitClicked == true)
                {
                    SubmitClicked(leaderboard);
                }

            }
        }

        private void HUD()
        {
            if (Globals.isHUDCreated == false)
            {
                canvas_battleground.Children.Add(txt_Lives);
                canvas_battleground.Children.Add(txt_Score);
                canvas_battleground.Children.Add(txt_Combo);


                txt_Lives.FontSize = 18;
                txt_Lives.FontWeight = FontWeights.Bold;
                txt_Lives.Foreground = Brushes.White;
                Canvas.SetLeft(txt_Lives, 5);

                txt_Score.FontSize = 18;
                txt_Score.FontWeight = FontWeights.Bold;
                txt_Score.Foreground = Brushes.White;
                Canvas.SetLeft(txt_Score, 5);
                Canvas.SetTop(txt_Score, 25);

                txt_Combo.FontSize = 18;
                txt_Combo.FontWeight = FontWeights.Bold;
                txt_Combo.Foreground = Brushes.White;
                Canvas.SetLeft(txt_Combo, 5);
                Canvas.SetTop(txt_Combo, 50);

                Globals.isHUDCreated = true;
            }

            txt_Lives.Text = "Lives: " + Globals.p_lives;
            txt_Score.Text = "Score: " + Globals.p_score;
            txt_Combo.Text = "Combo: " + Globals.p_combo;
        }

        private void comboTimer_Tick(object sender, EventArgs e)
        {
            if (Globals.p_combo > Globals.p_bestcombo)
            {
                Globals.p_bestcombo = Globals.p_combo;
            }

            Globals.p_combo = 0;
            comboTimer.Stop();
            comboTimer.Start();
        }

        private void InstancesTick()
        {
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
            foreach (f_Banana b in bananalist)
            {
                b.Tick();
            }
            foreach (f_BananaSlice bslice in bananaslicelist)
            {
                bslice.Tick();
            }
            foreach (Bomb b in bomblist)
            {
                b.Tick();
            }
            foreach (Sword s in swordlist)
            {
                s.Tick();
            }
        }

        public void CreateFruits()
        {
            int chance = Util.ChanceForFruit();

            if (chance == 10)
            {
                int fruitkind = Util.GetRandomFruitKind();

                if (fruitkind == 1)
                {
                    if (Globals.int_Difficulty == 1)
                    {
                        int.TryParse(Util.FruitVelocity().ToString(), out int x);
                        if (x > 2)
                        {
                            Bomb bomba = new Bomb(canvas_battleground, this);
                            bomblist.Add(bomba);
                        }
                    }
                    else
                    {
                        Bomb bomba = new Bomb(canvas_battleground, this);
                        bomblist.Add(bomba);
                    }
                }
                if (fruitkind == 2)
                {
                    f_Apple apple = new f_Apple(canvas_battleground, this);
                    applelist.Add(apple);
                }
                else if (fruitkind == 5)
                {
                    if (Globals.int_Difficulty == 3)
                    {
                        Bomb bomba = new Bomb(canvas_battleground, this);
                        bomblist.Add(bomba);
                    }
                    else
                    {
                        f_Watermelon watermelon = new f_Watermelon(canvas_battleground, this);
                        watermelonlist.Add(watermelon);
                    }
                }
                else if (fruitkind == 4)
                {
                    f_Pineapple pineapple = new f_Pineapple(canvas_battleground, this);
                    pineapplelist.Add(pineapple);
                }
                else if (fruitkind == 3)
                {
                    f_Banana banana = new f_Banana(canvas_battleground, this);
                    bananalist.Add(banana);
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

                        Globals.p_score = Globals.p_score + ((3 * Globals.int_Difficulty) * (1 + (Globals.p_combo/25)));
                        Globals.p_combo++;
                        comboTimer.Stop();
                        comboTimer.Start();

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

                        Globals.p_score = Globals.p_score + ((2 * Globals.int_Difficulty) * (1 + (Globals.p_combo / 25)));
                        Globals.p_combo++;
                        comboTimer.Stop();
                        comboTimer.Start();

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

                        Globals.p_score = Globals.p_score + ((2 * Globals.int_Difficulty) * (1 + (Globals.p_combo / 25)));
                        Globals.p_combo++;
                        comboTimer.Stop();
                        comboTimer.Start();

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

            foreach (f_Banana b in bananalist)
            {
                if (b.Point.Y > 691)
                {
                    b.destroy();
                    bananatodestroy.Add(b);
                }
                foreach (Sword s in swordlist)
                {
                    if (b.collidesWith(s))
                    {
                        b.destroy();
                        bananatodestroy.Add(b);

                        Globals.p_score = Globals.p_score + ((4 * Globals.int_Difficulty) * (1 + (Globals.p_combo / 25)));
                        Globals.p_combo++;
                        comboTimer.Stop();
                        comboTimer.Start();

                        f_BananaSlice bananaSlice = new f_BananaSlice(canvas_battleground, this);
                        bananaslicelist.Add(bananaSlice);
                        bananaSlice.point = b.Point;

                        f_BananaSlice bananaSlicemirror = new f_BananaSlice(canvas_battleground, this);
                        bananaslicelist.Add(bananaSlicemirror);
                        bananaSlicemirror.point = b.Point;
                        bananaSlicemirror.rotation = 180;
                    }
                }
            }
            foreach (f_BananaSlice bSlice in bananaslicelist)
            {
                if (bSlice.point.Y > 691)
                {
                    bSlice.destroy();
                    bananaslicetodestroy.Add(bSlice);
                }
            }

            foreach (Bomb bo in bomblist)
            {
                if (bo.Point.Y > 691)
                {
                    bo.destroy();
                    bombtodestroy.Add(bo);
                }
                foreach (Sword s in swordlist)
                {
                    if (bo.collidesWith(s))
                    {
                        bo.destroy();
                        bombtodestroy.Add(bo);

                        if (Globals.p_lives > 1)
                        {
                            MessageBox.Show("oof");
                            Globals.p_lives = Globals.p_lives - 1;

                            if (Globals.p_combo > Globals.p_bestcombo)
                            {
                                Globals.p_bestcombo = Globals.p_combo;
                            }

                            Globals.p_combo = 0;
                            comboTimer.Stop();
                            comboTimer.Start();
                        }
                        else if (Globals.p_lives == 1)
                        {
                            MessageBox.Show("KAPOOYAH! You're accuracy is a bit rusty there Mr.Assasian. I hope improvement is on your bucket list. You've lost all lives.");
                            gameState = GameState.GameOver;
                        }
                    }
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
            foreach (f_Banana b in bananatodestroy)
            {
                bananalist.Remove(b);
            }
            foreach (f_BananaSlice bSlice in bananaslicetodestroy)
            {
                bananaslicelist.Remove(bSlice);
            }
            foreach (Sword s in swordtodestroy)
            {
                swordlist.Remove(s);
            }
            foreach (Bomb bo in bombtodestroy)
            {
                bomblist.Remove(bo);
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

            Console.WriteLine(Globals.int_Difficulty);
            Console.WriteLine(Globals.str_Difficulty);
            gameState = GameState.GameOn;
        }

        public void Click_Settings(object sender, RoutedEventArgs e)
        {
            canvas_settings.Children.Clear();
            canvas_settings.Background = Globals.RBackground;

            CreateSettings();

            if (gameState == GameState.MainMenu)
            {
                canvas_mainmenu.Visibility = Visibility.Hidden;
                canvas_settings.Visibility = Visibility.Visible;
            }

            gameState = GameState.Settings;
        }

        private void CreateSettings()
        {
            txt_Difficulty.Text = "Difficulty:";
            txt_Difficulty.FontSize = 24;
            txt_Difficulty.FontWeight = FontWeights.Bold;
            txt_Difficulty.Foreground = Brushes.White;
            canvas_settings.Children.Add(txt_Difficulty);
            Canvas.SetLeft(txt_Difficulty, 5);

            btn_Back.Content = "Back";
            btn_Back.Width = 90;
            Canvas.SetLeft(btn_Back, 5);
            Canvas.SetTop(btn_Back, 619);
            btn_Back.Click += new RoutedEventHandler(Click_Back);
            canvas_settings.Children.Add(btn_Back);

            CB_Difficulty.Items.Clear();
            CB_Difficulty.Width = 100;
            CB_Difficulty.SelectedItem = Globals.str_Difficulty;
            CB_Difficulty.Items.Add("Easy");
            CB_Difficulty.Items.Add("Medium");
            CB_Difficulty.Items.Add("Hard");
            canvas_settings.Children.Add(CB_Difficulty);
            Canvas.SetTop(CB_Difficulty, 35);
            Canvas.SetLeft(CB_Difficulty, 5);


            txt_Song.Text = "Song:";
            txt_Song.FontSize = 24;
            txt_Song.FontWeight = FontWeights.Bold;
            txt_Song.Foreground = Brushes.White;
            canvas_settings.Children.Add(txt_Song);
            Canvas.SetLeft(txt_Song, 5);
            Canvas.SetTop(txt_Song, 70);

            CB_Song.Items.Clear();
            CB_Song.Width = 100;
            CB_Song.SelectedItem = Globals.str_Song;
            CB_Song.Items.Add("Sensoki");
            CB_Song.Items.Add("Uchigatana");
            CB_Song.Items.Add("Tokyo");
            canvas_settings.Children.Add(CB_Song);
            Canvas.SetTop(CB_Song, 105);
            Canvas.SetLeft(CB_Song, 5);
        }

        public void Click_Back(object sender, RoutedEventArgs e)
        {
            if (gameState == GameState.Settings)
            {
                canvas_mainmenu.Visibility = Visibility.Visible;
                canvas_settings.Visibility = Visibility.Hidden;

                if (CB_Song.Text != Globals.str_Song)
                {
                    Globals.musicPlaying = false;

                    if (CB_Song.Text == "Sensoki")
                    {
                        Globals.str_Song = "Sensoki";
                    }
                    else if (CB_Song.Text == "Uchigatana")
                    {
                        Globals.str_Song = "Uchigatana";
                    }
                    else if (CB_Song.Text == "Tokyo")
                    {
                        Globals.str_Song = "Tokyo";
                    }
                    else if (CB_Song.Text == "Sakura")
                    {
                        Globals.str_Song = "Sakura";
                    }
                }
                Console.WriteLine("Currently Playing: " + Globals.str_Song);


            }

            gameState = GameState.MainMenu;
        }

        public void CreateLeaderboard(Rectangle leaderboard)
        {
            //Reads stats from stats.txt
            ReadStats();

            //Adds objects
            if (Globals.areStatsEntered == false)
            {
                UpdateLeaderboard();

                canvas_leaderboard.Children.Add(darkenLeaderboard);
                canvas_leaderboard.Children.Add(leaderboard);
                canvas_leaderboard.Children.Add(txt_name);
                canvas_leaderboard.Children.Add(inpt_name);
                canvas_leaderboard.Children.Add(btn_submit);
                canvas_leaderboard.Children.Add(first_name);
                canvas_leaderboard.Children.Add(first_stats);
                canvas_leaderboard.Children.Add(second_name);
                canvas_leaderboard.Children.Add(second_stats);
                canvas_leaderboard.Children.Add(third_name);
                canvas_leaderboard.Children.Add(third_stats);
                canvas_leaderboard.Children.Add(fourth_name);
                canvas_leaderboard.Children.Add(fourth_stats);
                canvas_leaderboard.Children.Add(fifth_name);
                canvas_leaderboard.Children.Add(fifth_stats);
                canvas_leaderboard.Children.Add(your_stats);
            }

            //Removes objects and prepares for switch to main menu
            else if (Globals.areStatsEntered == true)
            {
                leave_Leaderboard.Height = 25; leave_Leaderboard.Width = 291; leave_Leaderboard.Content = "Main Menu"; Canvas.SetTop(leave_Leaderboard, 620); Canvas.SetLeft(leave_Leaderboard, 195); leave_Leaderboard.Click += new RoutedEventHandler(click_leaveLeaderboard);

                canvas_leaderboard.Children.Remove(txt_name);
                canvas_leaderboard.Children.Remove(inpt_name);
                canvas_leaderboard.Children.Remove(btn_submit);
                canvas_leaderboard.Children.Add(leave_Leaderboard);
            }


            Globals.isLeaderboardCreated = true;
        }

        private void UpdateLeaderboard()
        {
            if (Globals.areStatsEntered == false)
            {
                //Set leaderboard text object attributes
                darkenLeaderboard.Height = 800; darkenLeaderboard.Width = 800; darkenLeaderboard.Fill = Brushes.Black; darkenLeaderboard.Opacity = 0.5;
                txt_name.Height = 250; txt_name.Width = 200; txt_name.Text = "Enter your name"; txt_name.TextAlignment = TextAlignment.Center; txt_name.FontSize = 24; txt_name.FontFamily = new FontFamily("Times New Roman"); Canvas.SetTop(txt_name, 10); Canvas.SetLeft(txt_name, 240); txt_name.Foreground = Brushes.White;
                inpt_name.Height = 25; inpt_name.Width = 291; inpt_name.Text = "Enter your name here! (Max 10 letters)"; inpt_name.TextAlignment = TextAlignment.Center; inpt_name.FontSize = 12; inpt_name.FontFamily = new FontFamily("Times New Roman"); Canvas.SetTop(inpt_name, 50); Canvas.SetLeft(inpt_name, 195);
                btn_submit.Height = 25; btn_submit.Width = 291; btn_submit.Content = "Submit"; Canvas.SetTop(btn_submit, 75); Canvas.SetLeft(btn_submit, 195); btn_submit.Click += new RoutedEventHandler(click_btnSubmit);
                first_name.Height = 30; first_name.Width = 200; first_name.Text = Globals.first_p_name; Canvas.SetTop(first_name, 165); Canvas.SetLeft(first_name, 273); first_name.FontSize = 24; first_name.FontFamily = new FontFamily("Times New Roman"); first_name.Foreground = Brushes.Gold; first_name.FontWeight = FontWeights.Bold;
                first_stats.Height = 50; first_stats.Width = 200; first_stats.Text = Globals.first_p_stats; Canvas.SetTop(first_stats, 200); Canvas.SetLeft(first_stats, 273); first_stats.FontSize = 18; first_stats.FontFamily = new FontFamily("Times New Roman");
                second_name.Height = 25; second_name.Width = 200; second_name.Text = Globals.second_p_name; Canvas.SetTop(second_name, 265); Canvas.SetLeft(second_name, 273); second_name.FontSize = 12; second_name.FontFamily = new FontFamily("Times New Roman"); second_name.Foreground = Brushes.White;
                second_stats.Height = 30; second_stats.Width = 200; second_stats.Text = Globals.second_p_stats; Canvas.SetTop(second_stats, 300); Canvas.SetLeft(second_stats, 273); second_stats.FontSize = 12; second_stats.FontFamily = new FontFamily("Times New Roman");
                third_name.Height = 25; third_name.Width = 200; third_name.Text = Globals.third_p_name; Canvas.SetTop(third_name, 340); Canvas.SetLeft(third_name, 273); third_name.FontSize = 12; third_name.FontFamily = new FontFamily("Times New Roman"); third_name.Foreground = Brushes.White;
                third_stats.Height = 30; third_stats.Width = 200; third_stats.Text = Globals.third_p_stats; Canvas.SetTop(third_stats, 375); Canvas.SetLeft(third_stats, 273); third_stats.FontSize = 12; third_stats.FontFamily = new FontFamily("Times New Roman");
                fourth_name.Height = 25; fourth_name.Width = 200; fourth_name.Text = Globals.fourth_p_name; Canvas.SetTop(fourth_name, 415); Canvas.SetLeft(fourth_name, 273); fourth_name.FontSize = 12; fourth_name.FontFamily = new FontFamily("Times New Roman"); fourth_name.Foreground = Brushes.Black;
                fourth_stats.Height = 30; fourth_stats.Width = 200; fourth_stats.Text = Globals.fourth_p_stats; Canvas.SetTop(fourth_stats, 432); Canvas.SetLeft(fourth_stats, 273); fourth_stats.FontSize = 12; fourth_stats.FontFamily = new FontFamily("Times New Roman");
                fifth_name.Height = 25; fifth_name.Width = 200; fifth_name.Text = Globals.fifth_p_name; Canvas.SetTop(fifth_name, 470); Canvas.SetLeft(fifth_name, 273); fifth_name.FontSize = 12; fifth_name.FontFamily = new FontFamily("Times New Roman"); fifth_name.Foreground = Brushes.Black;
                fifth_stats.Height = 30; fifth_stats.Width = 200; fifth_stats.Text = Globals.fifth_p_stats; Canvas.SetTop(fifth_stats, 488); Canvas.SetLeft(fifth_stats, 273); fifth_stats.FontSize = 12; fifth_stats.FontFamily = new FontFamily("Times New Roman");
                your_stats.Height = 25; your_stats.Width = 200; your_stats.Text = Globals.yourStats; Canvas.SetTop(your_stats, 525); Canvas.SetLeft(your_stats, 273); your_stats.FontSize = 12; your_stats.FontFamily = new FontFamily("Times New Roman");
                
            }

            else if (Globals.areStatsEntered == true)
            {
                //Set leaderboard text object attributes
                darkenLeaderboard.Height = 800; darkenLeaderboard.Width = 800; darkenLeaderboard.Fill = Brushes.Black; darkenLeaderboard.Opacity = 0.5;
                txt_name.Height = 250; txt_name.Width = 200; txt_name.Text = "Enter your name"; txt_name.TextAlignment = TextAlignment.Center; txt_name.FontSize = 24; txt_name.FontFamily = new FontFamily("Times New Roman"); Canvas.SetTop(txt_name, 10); Canvas.SetLeft(txt_name, 240); txt_name.Foreground = Brushes.White;
                inpt_name.Height = 25; inpt_name.Width = 291; inpt_name.Text = "Enter your name here! (Max 10 letters)"; inpt_name.TextAlignment = TextAlignment.Center; inpt_name.FontSize = 12; inpt_name.FontFamily = new FontFamily("Times New Roman"); Canvas.SetTop(inpt_name, 50); Canvas.SetLeft(inpt_name, 195);
                btn_submit.Height = 25; btn_submit.Width = 291; btn_submit.Content = "Submit"; Canvas.SetTop(btn_submit, 75); Canvas.SetLeft(btn_submit, 195); btn_submit.Click += new RoutedEventHandler(click_btnSubmit);
                first_name.Height = 30; first_name.Width = 200; first_name.Text = Globals.first_p_name; Canvas.SetTop(first_name, 165); Canvas.SetLeft(first_name, 273); first_name.FontSize = 24; first_name.FontFamily = new FontFamily("Times New Roman"); first_name.Foreground = Brushes.Gold; first_name.FontWeight = FontWeights.Bold;
                first_stats.Height = 50; first_stats.Width = 200; first_stats.Text = Globals.first_p_stats; Canvas.SetTop(first_stats, 200); Canvas.SetLeft(first_stats, 273); first_stats.FontSize = 18; first_stats.FontFamily = new FontFamily("Times New Roman"); 
                second_name.Height = 25; second_name.Width = 200; second_name.Text = Globals.second_p_name; Canvas.SetTop(second_name, 265); Canvas.SetLeft(second_name, 273); second_name.FontSize = 12; second_name.FontFamily = new FontFamily("Times New Roman"); second_name.Foreground = Brushes.White;
                second_stats.Height = 25; second_stats.Width = 200; second_stats.Text = Globals.second_p_stats; Canvas.SetTop(second_stats, 300); Canvas.SetLeft(second_stats, 273); second_stats.FontSize = 12; second_stats.FontFamily = new FontFamily("Times New Roman");
                third_name.Height = 25; third_name.Width = 200; third_name.Text = Globals.third_p_name; Canvas.SetTop(third_name, 340); Canvas.SetLeft(third_name, 273); third_name.FontSize = 12; third_name.FontFamily = new FontFamily("Times New Roman"); third_name.Foreground = Brushes.White;
                third_stats.Height = 25; third_stats.Width = 200; third_stats.Text = Globals.third_p_stats; Canvas.SetTop(third_stats, 375); Canvas.SetLeft(third_stats, 273); third_stats.FontSize = 12; third_stats.FontFamily = new FontFamily("Times New Roman");
                fourth_name.Height = 25; fourth_name.Width = 200; fourth_name.Text = Globals.fourth_p_name; Canvas.SetTop(fourth_name, 415); Canvas.SetLeft(fourth_name, 273); fourth_name.FontSize = 12; fourth_name.FontFamily = new FontFamily("Times New Roman"); fourth_name.Foreground = Brushes.Black;
                fourth_stats.Height = 25; fourth_stats.Width = 200; fourth_stats.Text = Globals.fourth_p_stats; Canvas.SetTop(fourth_stats, 432); Canvas.SetLeft(fourth_stats, 273); fourth_stats.FontSize = 12; fourth_stats.FontFamily = new FontFamily("Times New Roman");
                fifth_name.Height = 25; fifth_name.Width = 200; fifth_name.Text = Globals.fifth_p_name; Canvas.SetTop(fifth_name, 470); Canvas.SetLeft(fifth_name, 273); fifth_name.FontSize = 12; fifth_name.FontFamily = new FontFamily("Times New Roman"); fifth_name.Foreground = Brushes.Black;
                fifth_stats.Height = 25; fifth_stats.Width = 200; fifth_stats.Text = Globals.fifth_p_stats; Canvas.SetTop(fifth_stats, 488); Canvas.SetLeft(fifth_stats, 273); fifth_stats.FontSize = 12; fifth_stats.FontFamily = new FontFamily("Times New Roman");
                your_stats.Height = 25; your_stats.Width = 200; your_stats.Text = Globals.yourStats; Canvas.SetTop(your_stats, 525); Canvas.SetLeft(your_stats, 273); your_stats.FontSize = 12; your_stats.FontFamily = new FontFamily("Times New Roman"); 
                leave_Leaderboard.Height = 25; leave_Leaderboard.Width = 291; leave_Leaderboard.Content = "Main Menu"; Canvas.SetTop(leave_Leaderboard, 590); Canvas.SetLeft(leave_Leaderboard, 195); leave_Leaderboard.Click += new RoutedEventHandler(click_leaveLeaderboard);

                //Remove unnecessary objects
                canvas_leaderboard.Children.Remove(txt_name);
                canvas_leaderboard.Children.Remove(inpt_name);
                canvas_leaderboard.Children.Remove(btn_submit);
                canvas_leaderboard.Children.Add(leave_Leaderboard);
            }
        }

        public void click_btnSubmit(object sender, RoutedEventArgs e)
        {
            //Plot twist
            Globals.btn_SubmitClicked = true;
        }

        public void click_leaveLeaderboard(object sender, RoutedEventArgs e)
        {
            //Changes visibility
            canvas_mainmenu.Visibility = Visibility.Visible;
            canvas_leaderboard.Visibility = Visibility.Hidden;
            canvas_battleground.Visibility = Visibility.Hidden;
            //Clears unnecessary objects
            canvas_battleground.Children.Clear();
            canvas_leaderboard.Children.Clear();
            canvas_mainmenu.Children.Clear();
            //Prepares for mainmenu creation
            Globals.isMainMenuCreated = false;
            ResetGame();
            gameState = GameState.MainMenu;
        }

        public void SubmitClicked(Rectangle leaderboard)
        {
            //Check for validation of entered name / compare with censored words
            for (int i = 0; i <= 32; i++)
            {
                //If censored word appears, change name to something stupid
                if (inpt_name.Text.ToUpper() == Globals.censoredwords[i]) { inpt_name.Text = "QT #" + i; MessageBox.Show("That is a Banned word. Your name will now be " + inpt_name.Text); }
            }
            if (inpt_name.Text.Contains('%'))
            {
                //Allows for secret names that only people who know the trick, can use.
                if (inpt_name.Text.Length < 15 && inpt_name.Text.Length > 1)
                {
                    //Fix name
                    string fixed_name = inpt_name.Text.Replace('%',' ');
                    //Set name and stats
                    Globals.areStatsEntered = true;
                    Globals.yourName = fixed_name;
                    //Methods to update and refresh. Stay relevant
                    RefreshStats();
                    UpdateLeaderboard();
                    WriteStats();
                    //Tell player that the game is functioning properly
                    MessageBox.Show("Thanks " + Globals.yourName + ", for entering your name. The leaderboards should now be updated.");
                }
                else
                {
                    //Give the player no clue as to what they did wrong.
                    MessageBox.Show("Oops, something went wrong. Please try again.");
                }
            }
            //Standard names
            else if (!inpt_name.Text.Contains('%'))
            {
                if (inpt_name.Text.Length < 13 && inpt_name.Text.Length > 2 && !inpt_name.Text.Contains('.') && !inpt_name.Text.Contains('_'))
                {
                    Globals.areStatsEntered = true;
                    Globals.yourName = inpt_name.Text;
                    RefreshStats();
                    UpdateLeaderboard();
                    WriteStats();
                    MessageBox.Show("Thanks " + Globals.yourName + ", for entering your name. The leaderboards should now be updated.");
                }
                else
                {
                    MessageBox.Show("Oops, something went wrong. Please try again.");
                }
            }
            Globals.btn_SubmitClicked = false;
        }

        //Enter at your own risk. These are in place to prevent people from abusing the leaderboard system. No harm is meant in writing this code.
        private static void SetCensoredWords()
        {
            //NOTE: These are in place so that the leaderboard system doesn't get abused.
            //You have been warned.

            Globals.censoredwords[0] = "FUCK";
            Globals.censoredwords[1] = "SHIT";
            Globals.censoredwords[2] = "BITCH";
            Globals.censoredwords[3] = "CUNT";
            Globals.censoredwords[4] = "CUM";
            Globals.censoredwords[5] = "NIGGER";
            Globals.censoredwords[6] = "NIGGA";
            Globals.censoredwords[7] = "FVCK";
            Globals.censoredwords[8] = "SH1T";
            Globals.censoredwords[9] = "SH!T";
            Globals.censoredwords[10] = "B1TCH";
            Globals.censoredwords[11] = "B!TCH";
            Globals.censoredwords[12] = "SLAVE";
            Globals.censoredwords[13] = "WHORE";
            Globals.censoredwords[14] = "MOLESTER";
            Globals.censoredwords[15] = "RAPE";
            Globals.censoredwords[16] = "RAPIST";
            Globals.censoredwords[17] = "FUCKER";
            Globals.censoredwords[18] = "FVCKER";
            Globals.censoredwords[19] = "FVCK3R";
            Globals.censoredwords[20] = "FUCK3R";
            Globals.censoredwords[21] = "SLUT";
            Globals.censoredwords[22] = "5LUT";
            Globals.censoredwords[23] = "SKANK";
            Globals.censoredwords[24] = "SHITHEAD";
            Globals.censoredwords[25] = "FUCKA";
            Globals.censoredwords[26] = "POT";
            Globals.censoredwords[27] = "METH";
            Globals.censoredwords[28] = "CRACK";
            Globals.censoredwords[29] = "ASS";
            Globals.censoredwords[30] = "DUMBASS";
            Globals.censoredwords[31] = "THOT";
            Globals.censoredwords[32] = "TH0T";
            Globals.censoredwords[33] = "BIG NIGGA";
        }

        public void ResetGame()
        {
            //Methods that help reset the game!
            ResetGlobals();

            //Reset lists
            foreach (f_Apple a in applelist)
            {
                a.destroy();
                appletodestroy.Add(a);
            }
            foreach (f_Banana b in bananalist)
            {
                b.destroy();
                bananatodestroy.Add(b);
            }
            foreach (f_Pineapple pA in pineapplelist)
            {
                pA.destroy();
                pineappletodestroy.Add(pA);
            }
            foreach (f_Watermelon w in watermelonlist)
            {
                w.destroy();
                watermelontodestroy.Add(w);
            }
            foreach (Sword s in swordlist)
            {
                s.destroy();
                swordtodestroy.Add(s);
            }
            foreach (Bomb ba in bomblist)
            {
                ba.destroy();
                bombtodestroy.Add(ba);
            }

            gameState = GameState.MainMenu;

        }

        private static void ResetGlobals()
        {
            //Reset important global variables
            Globals.musicPlaying = false;
            Globals.isLeaderboardCreated = false;
            Globals.btn_SubmitClicked = false;
            Globals.areStatsEntered = false;

            Globals.p_score = 0;
            Globals.p_lives = 3;
        }

        public static void ReadStats()
        {

            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(@"ezyro_22162395".Normalize(), @"Icecream5*".Normalize());
            wc.DownloadFile(Globals.statspath, Globals.path + @"\Stats.txt");

            using (StreamReader StatsReader = new StreamReader(Globals.path + @"\Stats.txt"))
            {
                using (StreamReader StatLineReader = new StreamReader(Globals.path + @"\Stats.txt"))
                {
                    string allLines = StatsReader.ReadToEnd();
                    for (int x = allLines.Count(i => i == '\n'); x > 0; x--)
                    {
                        string line = StatLineReader.ReadLine();
                        if (line.Contains("1st"))
                        {
                            Globals.first_p_name = StatLineReader.ReadLine();
                            int.TryParse(StatLineReader.ReadLine(), out Globals.first_p_score);
                            int.TryParse(StatLineReader.ReadLine(), out Globals.first_p_combo);
                            Globals.first_p_difficulty = StatLineReader.ReadLine();
                            x = x - 4;
                        }
                        if (line.Contains("2nd"))
                        {
                            Globals.second_p_name = StatLineReader.ReadLine();
                            int.TryParse(StatLineReader.ReadLine(), out Globals.second_p_score);
                            int.TryParse(StatLineReader.ReadLine(), out Globals.second_p_combo);
                            Globals.second_p_difficulty = StatLineReader.ReadLine();
                            x = x - 4;
                        }
                        if (line.Contains("3rd"))
                        {
                            Globals.third_p_name = StatLineReader.ReadLine();
                            int.TryParse(StatLineReader.ReadLine(), out Globals.third_p_score);
                            int.TryParse(StatLineReader.ReadLine(), out Globals.third_p_combo);
                            Globals.third_p_difficulty = StatLineReader.ReadLine();
                            x = x - 4;
                        }
                        if (line.Contains("4th"))
                        {
                            Globals.fourth_p_name = StatLineReader.ReadLine();
                            int.TryParse(StatLineReader.ReadLine(), out Globals.fourth_p_score);
                            int.TryParse(StatLineReader.ReadLine(), out Globals.fourth_p_combo);
                            Globals.fourth_p_difficulty = StatLineReader.ReadLine();
                            x = x - 4;
                        }
                        if (line.Contains("5th"))
                        {
                            Globals.fifth_p_name = StatLineReader.ReadLine();
                            int.TryParse(StatLineReader.ReadLine(), out Globals.fifth_p_score);
                            int.TryParse(StatLineReader.ReadLine(), out Globals.fifth_p_combo);
                            Globals.fifth_p_difficulty = StatLineReader.ReadLine();
                            x = x - 4;
                        }
                    }
                }
                StatsReader.Close();
                Console.WriteLine(Globals.first_p_name);
                Console.WriteLine(Globals.first_p_score.ToString());
                Console.WriteLine(Globals.second_p_name);
                Console.WriteLine(Globals.second_p_score.ToString());
                Console.WriteLine(Globals.third_p_name);
                Console.WriteLine(Globals.third_p_score.ToString());
                Console.WriteLine(Globals.fourth_p_name);
                Console.WriteLine(Globals.fourth_p_score.ToString());
                Console.WriteLine(Globals.fifth_p_name);
                Console.WriteLine(Globals.fifth_p_score.ToString());
            }
        }

        public static void RefreshStats()
        {

            if (Globals.p_score > Globals.first_p_score)
            {
                Globals.fifth_p_name = Globals.fourth_p_name;
                Globals.fifth_p_score = Globals.fourth_p_score;
                Globals.fifth_p_combo = Globals.fourth_p_combo;
                Globals.fifth_p_difficulty = Globals.fourth_p_difficulty;
                Globals.fourth_p_name = Globals.third_p_name;
                Globals.fourth_p_score = Globals.third_p_score;
                Globals.fourth_p_combo = Globals.third_p_combo;
                Globals.fourth_p_difficulty = Globals.third_p_difficulty;
                Globals.third_p_name = Globals.second_p_name;
                Globals.third_p_score = Globals.second_p_score;
                Globals.third_p_combo = Globals.second_p_combo;
                Globals.third_p_difficulty = Globals.second_p_difficulty;
                Globals.second_p_name = Globals.first_p_name;
                Globals.second_p_score = Globals.first_p_score;
                Globals.second_p_combo = Globals.first_p_combo;
                Globals.second_p_difficulty = Globals.first_p_difficulty;
                Globals.first_p_score = Globals.p_score;
                Globals.first_p_name = Globals.yourName;
                Globals.first_p_combo = Globals.p_bestcombo;
                Globals.first_p_difficulty = Globals.str_Difficulty;
            }
            else if (Globals.p_score > Globals.second_p_score)
            {
                Globals.fifth_p_name = Globals.fourth_p_name;
                Globals.fifth_p_score = Globals.fourth_p_score;
                Globals.fifth_p_combo = Globals.fourth_p_combo;
                Globals.fifth_p_difficulty = Globals.fourth_p_difficulty;
                Globals.fourth_p_name = Globals.third_p_name;
                Globals.fourth_p_score = Globals.third_p_score;
                Globals.fourth_p_combo = Globals.third_p_combo;
                Globals.fourth_p_difficulty = Globals.third_p_difficulty;
                Globals.third_p_name = Globals.second_p_name;
                Globals.third_p_score = Globals.second_p_score;
                Globals.third_p_combo = Globals.second_p_combo;
                Globals.third_p_difficulty = Globals.second_p_difficulty;
                Globals.second_p_score = Globals.p_score;
                Globals.second_p_name = Globals.yourName;
                Globals.second_p_combo = Globals.p_bestcombo;
                Globals.second_p_difficulty = Globals.str_Difficulty;
            }
            else if (Globals.p_score > Globals.third_p_score)
            {
                Globals.fifth_p_name = Globals.fourth_p_name;
                Globals.fifth_p_score = Globals.fourth_p_score;
                Globals.fifth_p_combo = Globals.fourth_p_combo;
                Globals.fifth_p_difficulty = Globals.fourth_p_difficulty;
                Globals.fourth_p_name = Globals.third_p_name;
                Globals.fourth_p_score = Globals.third_p_score;
                Globals.fourth_p_combo = Globals.third_p_combo;
                Globals.fourth_p_difficulty = Globals.third_p_difficulty;
                Globals.third_p_score = Globals.p_score;
                Globals.third_p_name = Globals.yourName;
                Globals.third_p_combo = Globals.p_bestcombo;
                Globals.third_p_difficulty = Globals.str_Difficulty;
            }
            else if (Globals.p_score > Globals.fourth_p_score)
            {
                Globals.fifth_p_name = Globals.fourth_p_name;
                Globals.fifth_p_score = Globals.fourth_p_score;
                Globals.fifth_p_combo = Globals.fourth_p_combo;
                Globals.fifth_p_difficulty = Globals.fourth_p_difficulty;
                Globals.fourth_p_score = Globals.p_score;
                Globals.fourth_p_name = Globals.yourName;
                Globals.fourth_p_combo = Globals.p_bestcombo;
                Globals.fourth_p_difficulty = Globals.str_Difficulty;
            }
            else if (Globals.p_score > Globals.fifth_p_score)
            {
                Globals.fifth_p_score = Globals.p_score;
                Globals.fifth_p_name = Globals.yourName;
                Globals.fifth_p_combo = Globals.p_bestcombo;
                Globals.fifth_p_difficulty = Globals.str_Difficulty;
            }

            Globals.yourStats = "Score: " + Globals.p_score.ToString() + "   Best Combo: " + Globals.p_bestcombo.ToString() + "\nDifficulty: " + Globals.str_Difficulty;
            Globals.first_p_stats = "Score: " + Globals.first_p_score.ToString() + "   Best Combo: " + Globals.first_p_combo.ToString() + "\nDifficulty: " + Globals.first_p_difficulty;
            Globals.second_p_stats = "Score: " + Globals.second_p_score.ToString() + "   Best Combo: " + Globals.second_p_combo.ToString() + "\nDifficulty: " + Globals.second_p_difficulty;
            Globals.third_p_stats = "Score: " + Globals.third_p_score.ToString() + "   Best Combo: " + Globals.third_p_combo.ToString() + "\nDifficulty: " + Globals.third_p_difficulty;
            Globals.fourth_p_stats = "Score: " + Globals.fourth_p_score.ToString() + "   Best Combo: " + Globals.fourth_p_combo.ToString() + "\nDifficulty: " + Globals.fourth_p_difficulty;
            Globals.fifth_p_stats = "Score: " + Globals.fifth_p_score.ToString() + "   Best Combo: " + Globals.fifth_p_combo.ToString() + "\nDifficulty: " + Globals.fifth_p_difficulty;
        }

        public static void WriteStats()
        {
            ReadStats();
            RefreshStats();

            using (StreamWriter StatWriter = new StreamWriter(Globals.path + @"\Stats.txt"))
            {
                StatWriter.WriteLine("1st");
                StatWriter.WriteLine(Globals.first_p_name);
                StatWriter.WriteLine(Globals.first_p_score);
                StatWriter.WriteLine(Globals.first_p_combo);
                StatWriter.WriteLine(Globals.first_p_difficulty);
                StatWriter.WriteLine("");
                StatWriter.WriteLine("2nd");
                StatWriter.WriteLine(Globals.second_p_name);
                StatWriter.WriteLine(Globals.second_p_score);
                StatWriter.WriteLine(Globals.second_p_combo);
                StatWriter.WriteLine(Globals.second_p_difficulty);
                StatWriter.WriteLine("");
                StatWriter.WriteLine("3rd");
                StatWriter.WriteLine(Globals.third_p_name);
                StatWriter.WriteLine(Globals.third_p_score);
                StatWriter.WriteLine(Globals.third_p_combo);
                StatWriter.WriteLine(Globals.third_p_difficulty);
                StatWriter.WriteLine("");
                StatWriter.WriteLine("4th");
                StatWriter.WriteLine(Globals.fourth_p_name);
                StatWriter.WriteLine(Globals.fourth_p_score);
                StatWriter.WriteLine(Globals.fourth_p_combo);
                StatWriter.WriteLine(Globals.fourth_p_difficulty);
                StatWriter.WriteLine("");
                StatWriter.WriteLine("5th");
                StatWriter.WriteLine(Globals.fifth_p_name);
                StatWriter.WriteLine(Globals.fifth_p_score);
                StatWriter.WriteLine(Globals.fifth_p_combo);
                StatWriter.WriteLine(Globals.fifth_p_difficulty);
            }

            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(@"ezyro_22162395".Normalize(), @"Icecream5*".Normalize());
            wc.UploadFile(Globals.statspath, Globals.path + @"\Stats.txt");

        }
    }
}
