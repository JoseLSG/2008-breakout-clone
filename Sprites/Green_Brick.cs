using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout_Clone
{
    class Green_Brick : Brick
    {
        public Green_Brick(float START_POS_X, float START_POS_Y)
        {
            this.Position = new Vector2(START_POS_X, START_POS_Y);
            isActive = true;
            color = Color.Green;
            POINTS = 5;
            scale = Brick_scale;
            
            brick_Level = 3;

            Type = "Brick";
        }
    }
}