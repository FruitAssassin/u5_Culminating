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
    class f_PineappleSlice
    {
        //Generate Player Variables
        Point PineappleSlicePos = new Point();
        public Point point;
        Canvas canvas;
        MainWindow window;
        Rectangle PineappleSliceRectangle;
        Random r = new Random(5);
        Random rnum = new Random();
        string movement;
        public int rotation = 0;
        int velocity = 0;


        //Create Sprites
        ImageBrush s_PineappleSlice = new ImageBrush(new BitmapImage(new Uri(@"Images\PineappleSlice.png", UriKind.Relative)));
        ImageBrush s_PineappleSlice2 = new ImageBrush(new BitmapImage(new Uri(@"Images\PineappleSlice2.png", UriKind.Relative)));

        public f_PineappleSlice(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;


            PineappleSlicePos = point;
            PineappleSliceRectangle = new Rectangle();
            PineappleSliceRectangle.Fill = s_PineappleSlice;
            PineappleSliceRectangle.Height = 128;
            PineappleSliceRectangle.Width = 64;
            canvas.Children.Add(PineappleSliceRectangle);
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
            PineappleSliceRectangle.Opacity = PineappleSliceRectangle.Opacity - 0.03;
            Canvas.SetTop(PineappleSliceRectangle, point.Y);
            Canvas.SetLeft(PineappleSliceRectangle, point.X);

            if (rotation == 180)
            {
                PineappleSliceRectangle.Fill = s_PineappleSlice2;
            }

        }





        private void Movement()
        {
            point.Y = point.Y + velocity;

            if (rotation == 0)
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
            else if (rotation == 180)
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


        }




        public void destroy()
        {
            canvas.Children.Remove(PineappleSliceRectangle);
        }

    }
}