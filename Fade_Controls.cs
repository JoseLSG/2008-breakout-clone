using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Breakout_Clone
{
    class Fade_Controls
    {
        static public bool beingFade = false;
        static public bool fadeAnim = false;
        static public bool fade_done = false;

        static double fadeDelay = 0.1;
        public static int AlphaValue = 255;
        static int fadeIncrement = 10;

        public static void Dec_AlphaPerTime(GameTime gameTime)
        {
            fadeDelay -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (AlphaValue >= 100)
            {
                if (fadeDelay <= 0)
                {
                    fadeDelay = 0.1;

                    AlphaValue -= fadeIncrement;

                }
            }
            else
            { 
                beingFade = false;
                fade_done = true;
            }

        }

    }
}
