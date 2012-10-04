

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Breakout_Clone;

namespace Breakout_Clone
{
    public class Menu_Manager
    {
        #region Variables
        private SpriteFont font;
        private Vector2 
            menu_item1_Pos,
            menu_item2_Pos,
            menu_item3_Pos,
            menu_item4_Pos,
            menu_item5_Pos,
            New_menu_item1_Pos,
            New_menu_item2_Pos;

        private Vector2 CenterPos;

        private string New_menu_item1 = "Normal";
        private string New_menu_item2 = "Hard";
        
        private string menu_item1 = "New Game";
        private string menu_item2 = "Options";
        private string menu_item3 = "Help";
        private string menu_item4 = "About";
        private string menu_item5 = "Exit";

        private int num_options = 5;
        public int selected = 1;

        private int num_options_sub = 2;
        public int selected_sub = 1;

        private OptionsMenu optionsmenu;
        private HelpMenu helpmenu;
        private AboutMenu aboutmenu;
        private FailMenu failmenu;


        KeyboardState currKey, PrevKey;

        #endregion

        /// <summary>
        /// Instantiates Menu_Manager and setups the menu item positions.
        /// </summary>
        /// <param name="graphics"></param>
        public Menu_Manager(GraphicsDeviceManager graphics)
        {
            CenterPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                                    graphics.GraphicsDevice.Viewport.Height / 2);

            
            menu_item1_Pos = new Vector2(CenterPos.X - 100, CenterPos.Y - 100);
            menu_item2_Pos = new Vector2(CenterPos.X - 100, CenterPos.Y - 50);
            menu_item3_Pos = new Vector2(CenterPos.X - 100, CenterPos.Y + 0);
            menu_item4_Pos = new Vector2(CenterPos.X - 100, CenterPos.Y + 50);
            menu_item5_Pos = new Vector2(CenterPos.X - 100, CenterPos.Y + 100);

            New_menu_item1_Pos = new Vector2(CenterPos.X - 100, CenterPos.Y - 50);
            New_menu_item2_Pos = new Vector2(CenterPos.X - 100, CenterPos.Y + 0);

            aboutmenu = new AboutMenu(CenterPos);
            optionsmenu = new OptionsMenu(CenterPos);
            helpmenu = new HelpMenu(CenterPos);
            failmenu = new FailMenu(CenterPos);

        }

        /// <summary>
        /// Loads the menu font to content manager.
        /// </summary>
        /// <param name="theContentManager"></param>
        public void LoadContent_Menu(ContentManager theContentManager)
        {
            font = theContentManager.Load<SpriteFont>("Menufont");
        }

        /// <summary>
        /// Draws the appropriate menu display depending on the use selected item.
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        public void Draw_Menu(SpriteBatch theSpriteBatch)
        {
           
            switch(selected)
            {
                case 1:
                    theSpriteBatch.DrawString(font, menu_item1, menu_item1_Pos, Color.White);
                    theSpriteBatch.DrawString(font, menu_item2, menu_item2_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item3, menu_item3_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item4, menu_item4_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item5, menu_item5_Pos, Color.BlueViolet);

                    break;
            
                case 2:
                    theSpriteBatch.DrawString(font, menu_item1, menu_item1_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item2, menu_item2_Pos, Color.White);
                    theSpriteBatch.DrawString(font, menu_item3, menu_item3_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item4, menu_item4_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item5, menu_item5_Pos, Color.BlueViolet);
                  
                    break;

                case 3:
                    theSpriteBatch.DrawString(font, menu_item1, menu_item1_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item2, menu_item2_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item3, menu_item3_Pos, Color.White);
                    theSpriteBatch.DrawString(font, menu_item4, menu_item4_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item5, menu_item5_Pos, Color.BlueViolet);
                    
                    break;

                case 4:
                    theSpriteBatch.DrawString(font, menu_item1, menu_item1_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item2, menu_item2_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item3, menu_item3_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item4, menu_item4_Pos, Color.White);
                    theSpriteBatch.DrawString(font, menu_item5, menu_item5_Pos, Color.BlueViolet);
                    
                    break;

                case 5:
                    theSpriteBatch.DrawString(font, menu_item1, menu_item1_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item2, menu_item2_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item3, menu_item3_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item4, menu_item4_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, menu_item5, menu_item5_Pos, Color.White);
                    
                    break;

                default:
                    break;
            }
        }

        public void Draw_SubMenu(SpriteBatch theSpriteBatch)
        {

            switch (selected_sub)
            {
                case 1:
                    theSpriteBatch.DrawString(font, New_menu_item1, New_menu_item1_Pos, Color.White);
                    theSpriteBatch.DrawString(font, New_menu_item2, New_menu_item2_Pos, Color.BlueViolet);

                    break;

                case 2:
                    theSpriteBatch.DrawString(font, New_menu_item1, New_menu_item1_Pos, Color.BlueViolet);
                    theSpriteBatch.DrawString(font, New_menu_item2, New_menu_item2_Pos, Color.White);

                    break;

                default:
                    break;
            }
        }

        public void Draw_AboutMenu(SpriteBatch theSpriteBatch)
        {
           aboutmenu.DrawText(theSpriteBatch, font);
        }

        public void Draw_OptionsMenu(SpriteBatch theSpriteBatch)
        {
           optionsmenu.DrawText(theSpriteBatch, font);
        }

        public void Draw_HelpMenu(SpriteBatch theSpriteBatch)
        {
            helpmenu.DrawText(theSpriteBatch, font);
        }

        public void Draw_FailMenu(SpriteBatch theSpriteBatch)
        {
            failmenu.DrawText(theSpriteBatch, font);
        }

        /// <summary>
        /// Figures out what the user has selected and sets the selection
        /// to match it.
        /// </summary>
        public void option_selection()
        {
            
            currKey = Keyboard.GetState();

            if (currKey.IsKeyDown(Keys.Down) && PrevKey.IsKeyUp(Keys.Down))
            {
                selected++;
                if (selected > num_options)
                    selected = 1;
            }

            if (currKey.IsKeyDown(Keys.Up) && PrevKey.IsKeyUp(Keys.Up))
            {
                selected--;
                if (selected <= 0)
                    selected = num_options;
            }

            PrevKey = currKey;

        }

        public void option_Subselection()
        {

            currKey = Keyboard.GetState();

            if (currKey.IsKeyDown(Keys.Down) && PrevKey.IsKeyUp(Keys.Down))
            {
                selected_sub++;
                if (selected_sub > num_options_sub)
                    selected_sub = 1;
            }

            if (currKey.IsKeyDown(Keys.Up) && PrevKey.IsKeyUp(Keys.Up))
            {
                selected_sub--;
                if (selected_sub <= 0)
                    selected_sub = num_options_sub;
            }

            PrevKey = currKey;

        }



    }
}
