using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout_Clone
{
    public class Ball:Sprite
    {
        const string ASSET_NAME = "ball";
        private float Speed_Multi;
        public float Old_deviation = 0;

        

        public Ball(Vector2 Start_Position, float Speed_Multi, Vector2 Direction)
        {
            this.Position = Start_Position;
            this.Speed_Multi = Speed_Multi;
            this.Direction = Direction;
            Speed = new Vector2(100, 100);
            isActive = true;
            color = Color.White;
            Type = "Ball";

        }

        public void LoadContent(ContentManager theContentManager)
        {
            LoadContent(theContentManager, ASSET_NAME);
        }

        public void Update(GameTime theGameTime)
        {
            // Save previous position
            save_Position();

            // Compute next position
            Position += Direction * Speed * Speed_Multi * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            set_box();
          
        }


        public void set_Speed_Multi(float Speed_Multi)
        {
            this.Speed_Multi = Speed_Multi;
        }



    }
}
