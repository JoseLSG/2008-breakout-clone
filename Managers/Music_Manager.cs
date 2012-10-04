using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Breakout_Clone
{
    class Music_Manager
    {
        private Cue MainMenu_Music, Normalscene_music, Hardscene_music;

        public Music_Manager()
        {
            MainMenu_Music = Breakout.theSoundBank.GetCue("MainTheme");
            Normalscene_music = Breakout.theSoundBank.GetCue("NormalTheme");
            Hardscene_music = Breakout.theSoundBank.GetCue("HardTheme");
        }

        public void playTheme(Breakout.GameState state, Breakout.NewGameMenu state2)
        {
            switch (state)
            {
                case Breakout.GameState.StartMenu:

                    if (!MainMenu_Music.IsPlaying)
                    {
                        MainMenu_Music.Play();
                    }
                    break;

                case Breakout.GameState.NewGame:
                     switch(state2)
                        {
                            case Breakout.NewGameMenu.Normal:
                                if (MainMenu_Music.IsPlaying)
                                {
                                    MainMenu_Music.Stop(AudioStopOptions.Immediate);
                                }
                                if (!Normalscene_music.IsPlaying)
                                {
                                    Normalscene_music.Play();
                                }
                                break;

                            case Breakout.NewGameMenu.Hard:
                                if (MainMenu_Music.IsPlaying)
                                {
                                    MainMenu_Music.Stop(AudioStopOptions.Immediate);
                                }
                                if (!Hardscene_music.IsPlaying)
                                {
                                    Hardscene_music.Play();
                                }
                                break;
                        }
                    break;

                default:
                    break;

            }

        }

    }
}
