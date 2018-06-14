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
        Rectangle AppleSlice;
        public Rect boundingBox { get => box; }
        Rect box;
        Random r = new Random(5);
        int Velocity = -43;


        //Create Sprites

        public f_Apple(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;

            ImageBrush s_Apple = new ImageBrush(new BitmapImage(new Uri(@"Images\Apple.png", UriKind.Relative)));
            ImageBrush s_AppleSlice = new ImageBrush(new BitmapImage(new Uri(@"Images\AppleSlice.png", UriKind.Relative)));

            point.Y = 690;
            point.X = 200;
            ApplePos = point;
            AppleRectangle = new Rectangle();
            AppleSlice = new Rectangle();
            Canvas.SetTop(AppleRectangle, point.Y);
            Canvas.SetLeft(AppleRectangle, point.X);
            AppleRectangle.Fill = s_Apple;
            AppleSlice.Fill = s_AppleSlice;
            AppleRectangle.Height = 64;
            AppleRectangle.Width = 64;
            canvas.Children.Add(AppleRectangle);
            box = new Rect(point, new Size(64, 64));
            int rOthernumber = r.Next();

            

        }



        

        public void Tick()
        {
            Movement();
            Velocity = Velocity + 2;

            Canvas.SetTop(AppleRectangle, point.Y);
            Canvas.SetLeft(AppleRectangle, point.X);
            box.X = point.X;
            box.Y = point.Y;


        }

        



        private void Movement()
        {
            point.Y = point.Y + (Velocity);
            point.X = point.X + 5;
        }

        public bool collidesWith(Sword sword)
        {
            if (this.boundingBox.X > (sword.boundingBox.X) && this.boundingBox.X < (sword.boundingBox.X + 128)
                && this.boundingBox.Y < (sword.boundingBox.Y + 128) && this.boundingBox.Y > sword.boundingBox.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        public void destroy()
        {

            canvas.Children.Remove(AppleRectangle);




        }

 

    }

}
