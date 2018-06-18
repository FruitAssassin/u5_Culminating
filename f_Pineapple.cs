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
    class f_Pineapple
    {
        //Generate Player Variables
        Point PineapplePos = new Point();
        private Point point;
        public Point Point { get => point; }
        Canvas canvas;
        MainWindow window;
        Rectangle PineappleRectangle;
        public Rect boundingBox { get => box; }
        Rect box;
        Random r = new Random(5);
        Random x_random = new Random();
        int Velocity = -40 - Util.FruitVelocity();
        string movement;

        public f_Pineapple(Canvas c, MainWindow w)
        {
            //Generate Pineapple
            canvas = c;
            window = w;

            ImageBrush s_Pineapple = new ImageBrush(new BitmapImage(new Uri(@"Images\Pineapple.png", UriKind.Relative)));

            point.Y = 690;
            point.X = 150 + x_random.Next(0, 251);
            if (point.X < 300)
            {
                movement = "left";
            }
            else if (point.X > 300)
            {
                movement = "right";
            }
            PineapplePos = point;
            PineappleRectangle = new Rectangle();
            PineappleRectangle.Fill = s_Pineapple;
            PineappleRectangle.Height = 128;
            PineappleRectangle.Width = 64;
            canvas.Children.Add(PineappleRectangle);
            box = new Rect(point, new Size(64, 128));
            int rOthernumber = r.Next();



        }



        //Controls Movement and Updates Points
        public void Tick()
        {
            Movement();
            Velocity = Velocity + 2;

            Canvas.SetTop(PineappleRectangle, point.Y);
            Canvas.SetLeft(PineappleRectangle, point.X);
            box.X = point.X;
            box.Y = point.Y;
        }




        //Moves image
        private void Movement()
        {
            point.Y = point.Y + (Velocity);
            if (movement == "left")
            {
                point.X = point.X + 5;
            }
            else if (movement == "right")
            {
                point.X = point.X - 5;
            }

        }

        //Hitbox Registration
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

        //Destroys Pineapple
        public void destroy()
        {
            canvas.Children.Remove(PineappleRectangle);


        }

    }

}
