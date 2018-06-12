using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;

namespace u5_Culminating
{
    class f_Apple
    {
        //Generate Player Variables
        Point ApplePos = new Point();
        private Point point;
        public Point Point { get => point; }
        Canvas canvas;
        MainWindow window;
        Rectangle AppleRectangle;
        public Rect boundingBox { get => box; }
        Rect box;
        Random r = new Random(5);
        int Velocity = -43;


        //Create Sprites
        //ImageBrush s_f_Apple = new ImageBrush(new BitmapImage(new Uri(@"Images\Alien SP1.png", UriKind.Relative)));

        public f_Apple(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;

            ImageBrush s_Apple = new ImageBrush(new BitmapImage(new Uri(@"Images\Apple.png", UriKind.Relative)));

            ApplePos = point;
            AppleRectangle = new Rectangle();
            AppleRectangle.Fill = s_Apple;
            AppleRectangle.Height = 64;
            AppleRectangle.Width = 64;
            canvas.Children.Add(AppleRectangle);
            Canvas.SetTop(AppleRectangle, point.Y);
            Canvas.SetLeft(AppleRectangle, point.X);
            box = new Rect(point, new Size(64, 64));
            int rOthernumber = r.Next();
            point.Y = 690;
            point.X = 200;

    }




        public void Tick()
        {
            Movement();
            Velocity = Velocity + 2;

            Canvas.SetTop(AppleRectangle, point.Y);
            Canvas.SetLeft(AppleRectangle, point.X);
        }





        private void Movement()
        {
            point.Y = point.Y + (Velocity);
            point.X = point.X + 5;

        }




        public void destroy()
        {

            /* if (ApplePos == Sword)
             {
                 canvas.Children.Remove(AppleRectangle);
             }
             */




        }

    }

}
