using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout_Clone
{
    public class Paddle:Sprite
    {
        const string ASSET_NAME = "paddle2";
        const float STEP = 10;
        private float START_POS_X;
        private float START_POS_Y;
        private float Speed_Multi;
        private KeyboardState keyState;
        private float deviation;
        const float DEV_MAGNITUDE = 2.5f;

        public bool isNotShrunk = true;


        public Paddle(GraphicsDeviceManager graphics)
        {
            START_POS_X = graphics.GraphicsDevice.Viewport.Width + 50;
            START_POS_Y = graphics.GraphicsDevice.Viewport.Height + 50;
            this.Position = new Vector2(START_POS_X, START_POS_Y);
            this.Speed_Multi = 1;
            isActive = true;
            deviation = 0;
            scale.X = 1;
            color = Color.White;
            Type = "Paddle";
        }

        public void LoadContent(ContentManager theContentManager)
        {
            LoadContent(theContentManager, ASSET_NAME);
        }

        public void Update()
        {
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left))
            {
                Position.X -= STEP * Speed_Multi;
            }

            else if (keyState.IsKeyDown(Keys.Right))
            {
                Position.X += STEP * Speed_Multi;
            }
            set_box();
        }

        public void set_Speed_Multi(float Speed_Multi)
        {
            this.Speed_Multi = Speed_Multi;
        }

        public void Rebound_from_Paddle(Ball ball)
        {
            if (this.box.Intersects(ball.box))
            {
                deviation = (box.Min.X + mSpriteTexture.Width / 2) -(ball.box.Min.X + ball.get_Texture().Width / 2);

                if (ball.Direction.X > 0)
                {
                    ball.Speed.X -= ball.Old_deviation;
                    ball.Speed.X += (deviation * -1 * DEV_MAGNITUDE);
                    ball.Old_deviation = (deviation * -1 * DEV_MAGNITUDE);
                }

                else
                {
                    ball.Speed.X -= ball.Old_deviation;
                    ball.Speed.X += (deviation * DEV_MAGNITUDE);
                    ball.Old_deviation = (deviation * DEV_MAGNITUDE);
                }
            }
        }
    }
}
