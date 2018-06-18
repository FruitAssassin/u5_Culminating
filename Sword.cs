using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace u5_Culminating
{
    class Sword
    {
        //Generate Player Variables
        Point SwordPos = new Point();
        private Point point;
        public Point Point { get => point; }
        Canvas canvas;
        MainWindow window;
        Rectangle SwordRectangle;
        public Rect boundingBox { get => box; }
        Rect box;
        Random r = new Random(5);



        //Create Sprites
        ImageBrush s_Katana = new ImageBrush(new BitmapImage(new Uri(@"Images\Katana.png", UriKind.Relative)));

        public Sword(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;

            SwordRectangle = new Rectangle();
            SwordRectangle.Fill = s_Katana;
            SwordRectangle.Height = 64;
            SwordRectangle.Width = 128;
            canvas.Children.Add(SwordRectangle);
            Canvas.SetTop(SwordRectangle, Globals.p_mouse.Y - 30);
            Canvas.SetLeft(SwordRectangle, Globals.p_mouse.X - 60);
            box = new Rect(point, new Size(128, 64));
            int rOthernumber = r.Next();

        }

        public void Tick()
        {
            Movement();
            if (Mouse.LeftButton.Equals(MouseButtonState.Pressed))
            {
                box.X = Globals.p_mouse.X -60;
                box.Y = Globals.p_mouse.Y -30;
            }

        }

        private void Movement()
        {
            if (!(Mouse.LeftButton.Equals(MouseButtonState.Pressed)))
            {
                SwordRectangle.Visibility = Visibility.Hidden;
                box.X = 900;
                box.Y = 1000;
            }
            if (Mouse.LeftButton.Equals(MouseButtonState.Pressed))
            {
                Globals.p_mouse = Mouse.GetPosition(window);
                Canvas.SetTop(SwordRectangle, Globals.p_mouse.Y - 30);
                Canvas.SetLeft(SwordRectangle, Globals.p_mouse.X - 60);
                SwordRectangle.Visibility = Visibility.Visible;
            }

        }

    }
}