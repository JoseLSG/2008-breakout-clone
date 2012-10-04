using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout_Clone
{
    public class Sprite
    {
        //these public var could be easily set to private or protected like mSpriteTexture with its corresponding set and get methods
        public Vector2 Position;
        public Vector3 Prev_Pos_min;
        public Vector3 Prev_Pos_max;
        public Vector2 Direction;
        public Vector2 Speed;
        protected Texture2D mSpriteTexture;
        protected Vector2 scale = new Vector2(1,1);
        protected Color color;

        public String Type;

        public bool isActive;

        public BoundingBox box;

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            set_box();
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            if (!Fade_Controls.fadeAnim)
                theSpriteBatch.Draw(mSpriteTexture, Position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
         
            else
                theSpriteBatch.Draw(mSpriteTexture, Position, null, new Color(255, 255, 255, (byte)MathHelper.Clamp(Fade_Controls.AlphaValue, 0, 255)), 0, Vector2.Zero, scale, SpriteEffects.None, 0);
         
        }

        //Boundingbox must follow sprite texture position
        public void set_box()
        {
            box.Min.X = Position.X;
            box.Min.Y = Position.Y;
            box.Max.X = (Position.X + mSpriteTexture.Width * scale.X);
            box.Max.Y = (Position.Y + mSpriteTexture.Height * scale.Y);
        }

        public void save_Position()
        {
            Prev_Pos_min = box.Min;
            Prev_Pos_max = box.Max;
        }

        public void Kill()
        {
            isActive = false;
           
        }

        public Texture2D get_Texture()
        {
            return mSpriteTexture;
        }

        public Vector2 get_Scale()
        {
            return scale;
        }

        public void set_Scale(Vector2 newScale)
        {
            scale = newScale;
        }

    }
}