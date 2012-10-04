using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout_Clone
{
    class Score_Manager
    {
        private SpriteFont font;
        private Vector2 scorePosition;
        static float score = 0;

        public Score_Manager(GraphicsDeviceManager graphics)
        {
            scorePosition = new Vector2(10, graphics.GraphicsDevice.Viewport.Height - 30);

        }

        public void LoadContent_Score(ContentManager theContentManager)
        {
            font = theContentManager.Load<SpriteFont>("Arial");
        }

        public void Draw_Score(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.DrawString(font, "Score: " + score, scorePosition, Color.White);
        }

        public static void add_points(float points)
        {
            score += points;
        }
    }

}
