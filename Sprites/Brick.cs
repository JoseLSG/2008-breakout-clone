using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout_Clone
{
    public class Brick:Sprite
    {
        const string ASSET_NAME = "brick";
        public float POINTS;
        protected Vector2 Brick_scale = new Vector2(1.3f, 1);
        public int brick_Level;
        public float reflectForce;

        public void LoadContent(ContentManager theContentManager)
        {
            LoadContent(theContentManager, ASSET_NAME);
         
        }

        public float get_Points()
        {
            return POINTS;
        }

    }
}
