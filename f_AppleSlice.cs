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
        //Generate Apple Variables
        Point AppleSlicePos = new Point();
        public Point point;
        Canvas canvas;
        MainWindow window;
        Rectangle AppleSliceRectangle;
        string movement;
        public int rotation = 0;
        int velocity = 0;


        //Create Sprites - Hooray!
        ImageBrush s_AppleSlice = new ImageBrush(new BitmapImage(new Uri(@"Images\AppleSlice.png", UriKind.Relative)));
        ImageBrush s_AppleSlice2 = new ImageBrush(new BitmapImage(new Uri(@"Images\AppleSlice2.png", UriKind.Relative)));

        public f_AppleSlice(Canvas c, MainWindow w)
        {
            //Generate Slices
            canvas = c;
            window = w;

            //More variables! Neat
            AppleSlicePos = point;
            AppleSliceRectangle = new Rectangle();
            AppleSliceRectangle.Fill = s_AppleSlice;
            AppleSliceRectangle.Height = 64;
            AppleSliceRectangle.Width = 64;
            canvas.Children.Add(AppleSliceRectangle);

            if (point.X < 300)
            {
                movement = "left";
            }
            else if (point.X > 300)
            {
                movement = "right";
            }



        }




        //Tick method!
        public void Tick()
        {
            //Movement method!!!!
            Movement();

            //Increase velocity
            velocity++;
            //Face slice
            AppleSliceRectangle.Opacity = AppleSliceRectangle.Opacity - 0.03;
            //Set visuals relative to point
            Canvas.SetTop(AppleSliceRectangle, point.Y);
            Canvas.SetLeft(AppleSliceRectangle, point.X);

            //if slice should be horizontally flipped
            if (rotation == 180)
            {
                //horizontally flip it
                AppleSliceRectangle.Fill = s_AppleSlice2;
            }

        }




        //Controls movement, was this even a surprise? It was to me
        private void Movement()
        {
            //Set point along y-axis relative to velocity
            point.Y = point.Y + velocity;

            //If self isn't horizontally flipped
            if (rotation == 0)
            {
                //if slice should move left
                if (movement == "left")
                {
                    //move right
                    point.X = point.X + (Util.SliceFall() * 2);
                }
                //if slice should move right
                else if (movement == "right")
                {
                    //move left
                    point.X = point.X - (Util.SliceFall() * 2);
                }
            }
            // if self is horizontally flipped
            else if (rotation == 180)
            {
                //and shold move left
                if (movement == "left")
                {
                    //move left
                    point.X = point.X - (Util.SliceFall() * 2);
                }
                //and should move right
                else if (movement == "right")
                {
                    //move right
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