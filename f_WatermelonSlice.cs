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
    class f_WatermelonSlice
    {
        //Generate Player Variables
        Point WatermelonSlicePos = new Point();
        public Point point;
        Canvas canvas;
        MainWindow window;
        Rectangle WatermelonSliceRectangle;
        Random r = new Random(5);
        Random rnum = new Random();
        string movement;
        public int rotation = 0;
        int velocity = 0;


        //Create Sprites
        ImageBrush s_WatermelonSlice = new ImageBrush(new BitmapImage(new Uri(@"Images\WatermelonSlice.png", UriKind.Relative)));
        ImageBrush s_WatermelonSlice2 = new ImageBrush(new BitmapImage(new Uri(@"Images\WatermelonSlice2.png", UriKind.Relative)));

        public f_WatermelonSlice(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;


            WatermelonSlicePos = point;
            WatermelonSliceRectangle = new Rectangle();
            WatermelonSliceRectangle.Fill = s_WatermelonSlice;
            WatermelonSliceRectangle.Height = 64;
            WatermelonSliceRectangle.Width = 64;
            canvas.Children.Add(WatermelonSliceRectangle);
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
            WatermelonSliceRectangle.Opacity = WatermelonSliceRectangle.Opacity - 0.03;
            Canvas.SetTop(WatermelonSliceRectangle, point.Y);
            Canvas.SetLeft(WatermelonSliceRectangle, point.X);

            if (rotation == 180)
            {
                WatermelonSliceRectangle.Fill = s_WatermelonSlice2;
                
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
            canvas.Children.Remove(WatermelonSliceRectangle);
        }

    }
}