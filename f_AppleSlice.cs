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

namespace u5_Culminating
{
    class f_AppleSlice
    {
        //Generate Player Variables
        Point AppleSlicePos = new Point();
        public Point point;
        Canvas canvas;
        MainWindow window;
        Rectangle AppleSliceRectangle;
        Random r = new Random(5);
        Random rnum = new Random();
        string movement;
        public int rotation = 0;
        int velocity = 0;


        //Create Sprites
        ImageBrush s_AppleSlice = new ImageBrush(new BitmapImage(new Uri(@"Images\AppleSlice.png", UriKind.Relative)));
        ImageBrush s_AppleSlice2 = new ImageBrush(new BitmapImage(new Uri(@"Images\AppleSlice2.png", UriKind.Relative)));

        public f_AppleSlice(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;


            AppleSlicePos = point;
            AppleSliceRectangle = new Rectangle();
            AppleSliceRectangle.Fill = s_AppleSlice;
            AppleSliceRectangle.Height = 64;
            AppleSliceRectangle.Width = 64;
            canvas.Children.Add(AppleSliceRectangle);
            int rOthernumber = r.Next();

            if (point.X < 300)
            {
                movement = "left";
            }
            else if (point.X > 300)
            {
                movement = "right";
            }



        }





        public void Tick()
        {
            Movement();

            velocity++;
            AppleSliceRectangle.Opacity = AppleSliceRectangle.Opacity - 0.03;
            Canvas.SetTop(AppleSliceRectangle, point.Y);
            Canvas.SetLeft(AppleSliceRectangle, point.X);

            if (rotation == 180)
            {
                AppleSliceRectangle.Fill = s_AppleSlice2;
            }

        }





        private void Movement()
        {
            point.Y = point.Y + velocity;

            if (rotation == 0)
            {
                if (movement == "left")
                {
                    point.X = point.X + (Util.SliceFall() * 2);
                }
                else if (movement == "right")
                {
                    point.X = point.X - (Util.SliceFall() * 2);
                }
            }
            else if (rotation == 180)
            {
                if (movement == "left")
                {
                    point.X = point.X - (Util.SliceFall() * 2);
                }
                else if (movement == "right")
                {
                    point.X = point.X + (Util.SliceFall() * 2);
                }
            }


        }




        public void destroy()
        {
            canvas.Children.Remove(AppleSliceRectangle);
        }

    }
}