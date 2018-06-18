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
    class f_Banana
    {
        //Generate Player Variables
        Point BananaPos = new Point();
        private Point point;
        public Point Point { get => point; }
        Canvas canvas;
        MainWindow window;
        Rectangle BananaRectangle;
        public Rect boundingBox { get => box; }
        Rect box;
        Random r = new Random(5);
        Random x_random = new Random();
        int Velocity = -40 - Util.FruitVelocity();
        string movement;

        public f_Banana(Canvas c, MainWindow w)
        {
            //Generate Banana
            canvas = c;
            window = w;

            //Create sprite
            ImageBrush s_Banana = new ImageBrush(new BitmapImage(new Uri(@"Images\Banana.png", UriKind.Relative)));
            //Set properties
            point.Y = 690;
            point.X = 150 + x_random.Next(0, 251);
            //Borders
            if (point.X < 300)
            {
                movement = "left";
            }
            else if (point.X > 300)
            {
                movement = "right";
            }
            BananaPos = point;
            BananaRectangle = new Rectangle();
            BananaRectangle.Fill = s_Banana;
            BananaRectangle.Height = 128;
            BananaRectangle.Width = 64;
            canvas.Children.Add(BananaRectangle);
            box = new Rect(point, new Size(64, 128));
            int rOthernumber = r.Next();



        }



        //Check Points
        public void Tick()
        {
            //Movment speed
            Movement();
            Velocity = Velocity + 2;

            //Updates hitbox
            Canvas.SetTop(BananaRectangle, point.Y);
            Canvas.SetLeft(BananaRectangle, point.X);
            box.X = point.X;
            box.Y = point.Y;
        }




        //Moves Sprite
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

        //Hit Registration
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

        //Destroys Banana
        public void destroy()
        {
            canvas.Children.Remove(BananaRectangle);


        }

    }

}
