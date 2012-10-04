using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Breakout_Clone
{
    public class Breakout : Microsoft.Xna.Framework.Game
    {
        Viewport height;
        public enum GameState { StartMenu, NewGame, Options, Help, About, Exit, EndGame };
        public enum NewGameMenu {None, Normal, Hard };

        public GameState state;
        public NewGameMenu New_Game_state;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyState;
        KeyboardState currKey, PrevKey;

        Ball ball_1;
        Ball ball_2;
        Paddle paddle;

        Brick_Manager theBrick_Manager;
        Score_Manager theScore_Manager;
        Menu_Manager theMenu_Manager;

        public static AudioEngine theAudioEngine;
        public static SoundBank theSoundBank;
        public static WaveBank theWaveBank;
       
        private static Music_Manager theMusic_Manager;

        public bool gameover;

       
        public Breakout()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            state = GameState.StartMenu;
            New_Game_state = NewGameMenu.None;
        }

        protected override void Initialize()
        {
            height = graphics.GraphicsDevice.Viewport;
            height.Height = 800;
            graphics.GraphicsDevice.Viewport = height;

            
            this.graphics.PreferredBackBufferHeight = 800;
            this.graphics.ApplyChanges();

            ball_1 = new Ball(new Vector2(400, 400),  // init Position

                                2,                      // Speed multi, multiplies final speed vector 
                                                        // after all other modifications in the game lifetime
                                new Vector2(1, 1));     // init Direction

            ball_2 = new Ball(new Vector2(400, 400),
                                2,                      
                                new Vector2(1, 1));

            paddle = new Paddle(this.graphics);
            theBrick_Manager = new Brick_Manager(this.graphics);
            theScore_Manager = new Score_Manager(this.graphics);
            theMenu_Manager = new Menu_Manager(this.graphics);

            // Initializing audio objects
            theAudioEngine = new AudioEngine("Content\\Audio\\Breakout_Audio.xgs");
            theWaveBank = new WaveBank(theAudioEngine, "Content\\Audio\\Wave Bank.xwb");
            theSoundBank = new SoundBank(theAudioEngine, "Content\\Audio\\Sound Bank.xsb");

            theMusic_Manager = new Music_Manager();
            gameover = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ball_1.LoadContent(this.Content);
            ball_2.LoadContent(this.Content);
            paddle.LoadContent(this.Content);
            theBrick_Manager.LoadContent_Bricks(this.Content);
            theScore_Manager.LoadContent_Score(this.Content);
            theMenu_Manager.LoadContent_Menu(this.Content);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //theMenu_Manager.option_selection();

            switch (state)
            {
                case GameState.StartMenu:
                    #region StartMenu sequence

                    theMusic_Manager.playTheme(state, New_Game_state);

                    theMenu_Manager.option_selection();
                    currKey = Keyboard.GetState();
                    if (currKey.IsKeyUp(Keys.Enter) && PrevKey.IsKeyDown(Keys.Enter))
                             state = (GameState)theMenu_Manager.selected;
                    PrevKey = currKey;

                        break;
                    #endregion

                case GameState.NewGame:

                        Keyboard_Listener2();

                        theMusic_Manager.playTheme(state, New_Game_state);

                        theMenu_Manager.option_Subselection();

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            New_Game_state = (NewGameMenu)theMenu_Manager.selected_sub;

                        switch (New_Game_state)
                        {
                            case NewGameMenu.Normal:
                                #region vers1Game sequence

                                Keyboard_Listener();

                                if (ball_1.isActive)
                                {
                                    Update_OneBallGame(gameTime);

                                    if (ball_1.Position.Y > paddle.Position.Y)
                                    {
                                        ball_1.isActive = false;
                                        gameover = true;

                                    }
                                }

                                else if (gameover)
                                {
                                    gameover = false;
                                    theSoundBank.PlayCue("LBMiss");
                                    Fade_Controls.beingFade = true;
                                    Fade_Controls.fadeAnim = true;
                                }

                                else if (Fade_Controls.beingFade)
                                {
                                    Fade_Controls.Dec_AlphaPerTime(gameTime);
                                }


                                break;
                                #endregion

                            case NewGameMenu.Hard:
                                #region vers2Game sequence

                                Keyboard_Listener();

                                if (ball_1.isActive && ball_2.isActive)
                                {
                                    Update_TwoBallGame(gameTime);

                                    if (ball_1.Position.Y > paddle.Position.Y)
                                    {
                                        ball_1.isActive = false;
                                        gameover = true;
                                    }

                                    if (ball_2.Position.Y > paddle.Position.Y)
                                    {
                                        ball_2.isActive = false;
                                        gameover = true;
                                    }


                                }

                                else if (gameover)
                                {
                                    gameover = false;
                                    theSoundBank.PlayCue("LBMiss");
                                    Fade_Controls.beingFade = true;
                                    Fade_Controls.fadeAnim = true;
                                }

                                else if (Fade_Controls.beingFade)
                                {
                                    Fade_Controls.Dec_AlphaPerTime(gameTime);
                                }

                                break;
                                #endregion

                        }
                        break;

                case GameState.Options:
                        Keyboard_Listener2();
                        break;

                case GameState.Help:
                        Keyboard_Listener2();
                        break;

                case GameState.About:
                        Keyboard_Listener2();
                        break;

                case GameState.Exit:
                        #region Exit sequence
                        this.Exit();
                        break;
                        #endregion

                }
        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            switch (state)
            {
                case GameState.StartMenu:
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                    theMenu_Manager.Draw_Menu(this.spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.NewGame:

                    spriteBatch.Begin();
                    switch (New_Game_state)
                    {
                        case NewGameMenu.Normal:
                            ball_1.Draw(this.spriteBatch);
                            paddle.Draw(this.spriteBatch);
                            theBrick_Manager.Draw_Bricks(this.spriteBatch);
                            theScore_Manager.Draw_Score(this.spriteBatch);
                            break;

                        case NewGameMenu.Hard:
                            ball_1.Draw(this.spriteBatch);

                            if (Collisions.ballCollided)
                                ball_2.Draw(this.spriteBatch);

                            paddle.Draw(this.spriteBatch);
                            theBrick_Manager.Draw_Bricks(this.spriteBatch);
                            theScore_Manager.Draw_Score(this.spriteBatch);
                            break;

                        default:
                            theMenu_Manager.Draw_SubMenu(this.spriteBatch);
                            break;

                    }

                    if (Fade_Controls.fade_done)
                    {
                        theMenu_Manager.Draw_FailMenu(this.spriteBatch);

                    }

                    spriteBatch.End();
                    break;


                case GameState.Options:
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                    theMenu_Manager.Draw_OptionsMenu(this.spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.Help:
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                    theMenu_Manager.Draw_HelpMenu(this.spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.About:
                    spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                    theMenu_Manager.Draw_AboutMenu(this.spriteBatch);
                    spriteBatch.End();
                    break;

                default:
                    break;

            }
            
            base.Draw(gameTime);
        }

        public void Update_OneBallGame(GameTime gameTime)
        {
            if (ball_1.Position.Y <= paddle.Position.Y) // Game stops is ball is beyond paddle Y coordinates
            {
                ball_1.Update(gameTime);
                Collisions.Check_Border_colision(graphics, ball_1);
                Collisions.upperWallHit_shrink(graphics, ball_1, paddle);
            }

            paddle.Update();
            Collisions.Check_Border_colision(graphics, paddle);
            Collisions.Check_Sprite_colision(ball_1, paddle, false);
            paddle.Rebound_from_Paddle(ball_1);
            theBrick_Manager.check_Collisions(ball_1, true);


        }

        private void Update_TwoBallGame(GameTime gameTime)
        {

                ball_1.Update(gameTime);
                Collisions.Check_Border_colision(graphics, ball_1);
                Collisions.upperWallHit_shrink(graphics, ball_1, paddle);

                Collisions.Check_Sprite_colision(ball_1, paddle, false);
                paddle.Rebound_from_Paddle(ball_1);
                theBrick_Manager.check_Collisions(ball_1, true);

                if (Collisions.ballCollided)
                {
                    ball_2.Update(gameTime);
                    Collisions.Check_Border_colision(graphics, ball_2);
                    Collisions.upperWallHit_shrink(graphics, ball_2, paddle);

                    Collisions.Check_Sprite_colision(ball_2, paddle, false);
                    paddle.Rebound_from_Paddle(ball_2);
                    theBrick_Manager.check_Collisions(ball_2, true);

                    Collisions.Check_Sprite_colision(ball_1, ball_2, false);
                }
                paddle.Update();
                Collisions.Check_Border_colision(graphics, paddle);

           
        }

        public void Keyboard_Listener()
        {
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            
        }

        public void Keyboard_Listener2()
        {
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Back))
                state = GameState.StartMenu;


        }


    }
}
