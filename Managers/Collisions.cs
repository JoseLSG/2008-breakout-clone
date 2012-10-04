using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout_Clone
{
    public class Collisions
    {
        #region Brick Reflective Force Variables
        static float yellow_reflectForce = 10;
        static float red_reflectForce    = 30;
        static float green_reflectForce  = 60;
        static float blue_reflectForce   = 90;
        #endregion

        #region Vars for Force application
        static float applied_oldForce;

        static int apply_Level = 0;
        static int currentHit;
        static int prevHit = 0;

        public static bool ballCollided = false;
        #endregion


        /// <summary>
        /// Determines if the obj passed is hitting the borders of the game world screen
        /// and re-sets the position of the obj and its bounding box.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="obj"></param>
        public static void Check_Border_colision(GraphicsDeviceManager graphics , Sprite obj)
        {
            float MaxX = graphics.GraphicsDevice.Viewport.Width - obj.get_Texture().Width * obj.get_Scale().X;
            float MinX = 0;
            float MaxY = graphics.GraphicsDevice.Viewport.Height - obj.get_Texture().Height * obj.get_Scale().Y;
            float MinY = 0;

            

            //Check for bounce
            if (obj.Position.X > MaxX)          // Right border check
            {
                obj.save_Position();

                obj.Direction.X *= -1;
                obj.Position.X = MaxX; // back up  to meet colision surface
                obj.set_box(); // allow boundingbox to follow sprite

                if (obj.Type == "Ball") // Only produce sound if the obj is a Ball
                  Breakout.theSoundBank.PlayCue("BorderHit");
                
            }

            else if (obj.Position.X < MinX)     // Left border check
            {
                obj.save_Position();

                obj.Direction.X *= -1;
                obj.Position.X = MinX;
                obj.set_box();
                if (obj.Type == "Ball") // Only produce sound if the obj is a Ball
                 Breakout.theSoundBank.PlayCue("BorderHit");
            }

            if (obj.Position.Y > MaxY)          // Bottom border check
            {
                obj.save_Position();

                obj.Direction.Y *= -1;
                obj.Position.Y = MaxY;
                obj.set_box();
                if (obj.Type == "Ball") // Only produce sound if the obj is a Ball
                   Breakout.theSoundBank.PlayCue("BorderHit");
               
            }

            else if (obj.Position.Y < MinY)     // Top border check
            {
                obj.save_Position();

                obj.Direction.Y *= -1;
                obj.Position.Y = MinY;
                obj.set_box();
                if (obj.Type == "Ball") // Only produce sound if the obj is a Ball
                   Breakout.theSoundBank.PlayCue("BorderHit");
            }

        }

        /// <summary>
        /// Determines if the obj colide, re-sets obj and bounding box positions, 
        /// and checks what should be killed and what audio to play.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <param name="isKillable"></param>
        public static void Check_Sprite_colision(Sprite obj1, Sprite obj2, bool isKillable)
        {
            if (obj2.isActive)
            {
                if (obj1.box.Intersects(obj2.box))
                {

                    if (obj1.Type == "Ball" || obj2.Type == "Ball")
                        ballCollided = true; // Flag for release of second ball in game version 2
                
                    if (obj1.Type == "Paddle" || obj2.Type == "Paddle") // Only produce sound if the obj is a Ball
                        Breakout.theSoundBank.PlayCue("PaddleHit");
                    

                    if (isKillable)
                    {
                        Score_Manager.add_points(convertToBrick(obj2).get_Points());
                        brick_applyReflectPower((Ball)obj1, (Brick)obj2);
                        obj2.Kill();
                        Breakout.theSoundBank.PlayCue("BrickHit");
                    }

                    // Find Left/Right side
                    if (obj1.Prev_Pos_max.X < obj2.box.Min.X)
                    {
                        // Do left side computation
                        obj1.Direction.X *= -1;
                        obj1.Position.X = obj2.box.Min.X - obj1.get_Texture().Width;
                        obj1.box.Min.X = obj1.Position.X;
                        obj1.box.Max.X = obj2.box.Min.X;

                    }
                    else if (obj1.Prev_Pos_min.X > obj2.box.Max.X)
                    {
                        // Do right side computation
                        obj1.Direction.X *= -1;
                        obj1.Position.X = obj2.box.Max.X;
                        obj1.box.Min.X = obj1.Position.X;
                        obj1.box.Max.X = obj2.box.Max.X + obj2.get_Texture().Width;
                    }

                    // Find Top/Bottom side
                    if (obj1.Prev_Pos_max.Y < obj2.box.Min.Y)
                    {
                        // Do top side computation
                        obj1.Direction.Y *= -1;
                        obj1.Position.Y = obj2.box.Min.Y - obj1.get_Texture().Height;
                        obj1.box.Min.Y = obj1.Position.Y;
                        obj1.box.Max.Y = obj2.box.Min.Y;
                    }
                    else if (obj1.Prev_Pos_min.Y > obj2.box.Max.Y)
                    {
                        // Do bottom side computation
                        obj1.Direction.Y *= -1;
                        obj1.Position.Y = obj2.box.Max.Y;
                        obj1.box.Min.Y = obj1.Position.Y;
                        obj1.box.Max.Y = obj2.box.Max.Y + obj1.get_Texture().Height;
                    }
                } 
            }
        }

        /// <summary>
        /// Converts an obj to a Brick obj.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Brick convertToBrick(Sprite obj)
        {
            return (Brick) obj;
        }

        /// <summary>
        /// Determines if ball hit top wall and shrinks the paddle by 1/3.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="ball"></param>
        /// <param name="paddle"></param>
        public static void upperWallHit_shrink(GraphicsDeviceManager graphics, Ball ball, Paddle paddle)
        {
            if (paddle.isNotShrunk)
            {
                if (ball.Prev_Pos_min.Y < 0)          // Top border check
                {
                    paddle.set_Scale(new Vector2(paddle.get_Scale().X * 0.333f, 1)); // 0.333f  = 1/3 scale
                    paddle.isNotShrunk = false; // Shrinking only happens once, subsequent top wall colisions will no shrink it further
                }
            }
        }

        public static void brick_applyReflectPower(Ball ball, Brick brick)
        {

            currentHit = brick.brick_Level;

            if (currentHit != prevHit)  // if the same brick was previously hit, don't increase ball velocity
            {
                if (currentHit > prevHit) // if a higher point brick is struck, apply that brick's reflective force
                {
                    apply_Level++;
                    apply_ForceLevel(ball);
                    prevHit = currentHit;
                }
                else //if a lower point brick is struck, use 2 levels lower reflective force 
                {
                    apply_Level = prevHit - 2;
                    apply_ForceLevel(ball);
                    prevHit = currentHit;
                }
            }

            else if (currentHit != apply_Level) // in normal success scenario, both levels should be the same
            {                                   // if not, then each hit of the same brick will use the next upper level of force.
                apply_Level++;
                apply_ForceLevel(ball);
            }

        }

        private static void apply_ForceLevel(Ball ball)
        {
            ball.Speed.X -= applied_oldForce;
            ball.Speed.Y -= applied_oldForce;

            switch (apply_Level)
            {
                case 0:
                    applied_oldForce = 0;
                    ball.Speed.X += 0;
                    ball.Speed.Y += 0;
                    break;

                case 1:
                    ball.Speed.X += yellow_reflectForce;
                    ball.Speed.Y += yellow_reflectForce;
                    applied_oldForce = yellow_reflectForce;
                    break;

                case 2:
                    ball.Speed.X += red_reflectForce;
                    ball.Speed.Y += red_reflectForce;
                    applied_oldForce = red_reflectForce;
                    break;

                case 3:
                    ball.Speed.X += green_reflectForce;
                    ball.Speed.Y += green_reflectForce;
                    applied_oldForce = green_reflectForce;
                    break;

                case 4:
                    ball.Speed.X += blue_reflectForce;
                    ball.Speed.Y += blue_reflectForce;
                    applied_oldForce = blue_reflectForce;
                    break;

                default:
                    break;
            }

        }


    }

}
