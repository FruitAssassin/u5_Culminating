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
    class f_BananaSlice
    {
        //Generate Player Variables
        Point BananaSlicePos = new Point();
        public Point point;
        Canvas canvas;
        MainWindow window;
        Rectangle BananaSliceRectangle;
        Random r = new Random(5);
        Random rnum = new Random();
        string movement;
        public int rotation = 0;


        //Create Sprites
        ImageBrush s_BananaSlice = new ImageBrush(new BitmapImage(new Uri(@"Images\BananaSlice.png", UriKind.Relative)));
        ImageBrush s_BananaSlice2 = new ImageBrush(new BitmapImage(new Uri(@"Images\BananaSlice2.png", UriKind.Relative)));

        public f_BananaSlice(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;


            BananaSlicePos = point;
            BananaSliceRectangle = new Rectangle();
            BananaSliceRectangle.Fill = s_BananaSlice;
            BananaSliceRectangle.Height = 64;
            BananaSliceRectangle.Width = 64;
            canvas.Children.Add(BananaSliceRectangle);
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

            Canvas.SetTop(BananaSliceRectangle, point.Y);
            Canvas.SetLeft(BananaSliceRectangle, point.X);

            if (rotation == 180)
            {
                BananaSliceRectangle.Fill = s_BananaSlice2;
            }

        }





        private void Movement()
        {
            point.Y = point.Y + 10;

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
            canvas.Children.Remove(BananaSliceRectangle);
        }

    }
}