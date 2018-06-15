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
        public Rect boundingBox { get => box; }
        Rect box;
        Random r = new Random(5);
        int Velocity = -43;


        //Create Sprites

        public f_AppleSlice(Canvas c, MainWindow w)
        {
            //Generate Alien
            canvas = c;
            window = w;

            ImageBrush s_Apple = new ImageBrush(new BitmapImage(new Uri(@"Images\AppleSlice.png", UriKind.Relative)));

            AppleSlicePos = point;
            AppleSliceRectangle = new Rectangle();
            Canvas.SetTop(AppleSliceRectangle, point.Y);
            Canvas.SetLeft(AppleSliceRectangle, point.X);
            AppleSliceRectangle.Fill = s_Apple;
            AppleSliceRectangle.Height = 64;
            AppleSliceRectangle.Width = 64;
            canvas.Children.Add(AppleSliceRectangle);
            box = new Rect(point, new Size(64, 64));
            int rOthernumber = r.Next();



        }





        public void Tick()
        {
            Movement();
            Velocity = Velocity + 2;

            Canvas.SetTop(AppleSliceRectangle, point.Y);
            Canvas.SetLeft(AppleSliceRectangle, point.X);
            box.X = point.X;
            box.Y = point.Y;


        }





        private void Movement()
        {
            point.Y = point.Y + (Velocity);
            point.X = point.X + 5;
        }




        public void destroy()
        {
            canvas.Children.Remove(AppleSliceRectangle);
        }

        public void createslice()
        {
            canvas.Children.Add(AppleSliceRectangle);
        }
    }
 }
