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
        //Generate Apple Variables
        Point ApplePos = new Point();
        private Point point;
        public Point Point { get => point; }
        Canvas canvas;
        MainWindow window;
        Rectangle AppleRectangle;
        public Rect boundingBox { get => box; }
        Rect box;
        Random r = new Random(5);
        Random x_random = new Random();
        //Creates velocity based on alot of stuff that I don't want to explain
        double Velocity = -40 - (Globals.int_Difficulty * Globals.int_Difficulty) - Util.FruitVelocity();
        string movement;

        public f_Apple(Canvas c, MainWindow w)
        {
            //Generate Apple
            canvas = c;
            window = w;

            //Load image!
            ImageBrush s_Apple = new ImageBrush(new BitmapImage(new Uri(@"Images\Apple.png", UriKind.Relative)));

            //Set apple below a visible point, randomly along the x-axis between 150, and 400.
            point.Y = 690;
            point.X = 150 + x_random.Next(0, 251);

            //if its past 300
            if (point.X < 300)
            {
                //it should probably move left
                movement = "left";
            }
            //if its below 300
            else if (point.X > 300)
            {
                //it should probably move right
                movement = "right";
            }
            //More variables
            ApplePos = point;
            AppleRectangle = new Rectangle();
            AppleRectangle.Fill = s_Apple;
            AppleRectangle.Height = 64;
            AppleRectangle.Width = 64;
            canvas.Children.Add(AppleRectangle);
            box = new Rect(point, new Size(64, 64));
            int rOthernumber = r.Next();



        }



        //Tick method!!
        public void Tick()
        {
            //Movement method!
            Movement();
            //Velocity control! (lots of numbers)
            Velocity = Velocity + 1.8 + ((Globals.int_Difficulty - 1) / 2);

            //Sets visuals and hitboxes relative to "point".
            Canvas.SetTop(AppleRectangle, point.Y);
            Canvas.SetLeft(AppleRectangle, point.X);
            box.X = point.X;
            box.Y = point.Y;
        }




        //Movement method!!
        private void Movement()
        {
            //Move up based on how much fruit has already moved
            point.Y = point.Y + Velocity;
            //if fruit should move left
            if (movement == "left")
            {
                //do so 5 pixels
                point.X = point.X + 5;
            }
            //if fruit should move right
            else if (movement == "right")
            {
                //do so 5 pixels
                point.X = point.X - 5;
            }

        }

        //Check if apple has collided with sword
        public bool collidesWith(Sword sword)
        {
            //if boundingboxes overlap
            if (this.boundingBox.X > (sword.boundingBox.X - 32) && this.boundingBox.X < (sword.boundingBox.X + 80)
                && this.boundingBox.Y < (sword.boundingBox.Y + 32) && this.boundingBox.Y > sword.boundingBox.Y - 32)
            {
                //pretty obvious
                return true;
            }
            else
            {
                return false;
            }
        }

        //If called upon, remove the visuals of the apple.
        public void destroy()
        {
            canvas.Children.Remove(AppleRectangle);
         

        }

    }

}