using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Breakout_Clone
{
    class Brick_Manager
    {
        const float brick_layout_X = 2;
        const float brick_layout_Y = 80;
        int rows = 8;
        int bricks_per_row = 10;
        Brick[,] brick_layout;
        float transition_x;
        float transition_y;

        public Brick_Manager(GraphicsDeviceManager graphics)
        {
            create_bricks();   
        }

        private void create_bricks()
        {
            brick_layout = new Brick[rows, bricks_per_row];
            
            for (int r = 0; r < rows; r++)
            {

                if (r < 2)
                {
                    for (int u = 0; u < bricks_per_row; u++)
                    {
                        brick_layout[r, u] = new Blue_Brick(brick_layout_X, brick_layout_Y);

                    }
                }

                else if (r < 4)
                {
                    for (int u = 0; u < bricks_per_row; u++)
                    {
                        brick_layout[r, u] = new Green_Brick(brick_layout_X, brick_layout_Y);

                    }
                }

                else if (r < 6)
                {
                    for (int u = 0; u < bricks_per_row; u++)
                    {
                        brick_layout[r, u] = new Red_Brick(brick_layout_X, brick_layout_Y);

                    }
                }

                else if (r < 8)
                {
                    for (int u = 0; u < bricks_per_row; u++)
                    {
                        brick_layout[r, u] = new Yellow_Brick(brick_layout_X, brick_layout_Y);

                    }
                }

            }


        }

        public void LoadContent_Bricks(ContentManager theContentManager)
        {

            for (int r = 0; r < rows; r++)
            {
                for (int u = 0; u < bricks_per_row; u++)
                {
                    brick_layout[r, u].LoadContent(theContentManager);
                }

            }

            place_bricks();


        }

        private void place_bricks()
        {
            transition_x = brick_layout[0, 0].get_Texture().Width * brick_layout[0, 0].get_Scale().X;
            transition_y = brick_layout[0, 0].get_Texture().Height * brick_layout[0, 0].get_Scale().Y;

            for (int r = 0; r < rows; r++)
            {

                for (int u = 0; u < bricks_per_row; u++)
                {
                    brick_layout[r, u].Position = new Vector2(brick_layout_X + (transition_x + 3) * u, brick_layout_Y + (transition_y + 2) * r);
                    brick_layout[r, u].set_box();
                }

            }
        }


        public void Draw_Bricks(SpriteBatch spriteBatch)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int u = 0; u < bricks_per_row; u++)
                {
                    if (brick_layout[r, u].isActive)
                        brick_layout[r, u].Draw(spriteBatch);
                }


            }
        }

        public void check_Collisions(Ball ball, bool killable)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int u = 0; u < bricks_per_row; u++)
                {

                    Collisions.Check_Sprite_colision(ball, brick_layout[r,u], killable);
                    
                }


            }


        } // END check_Colisions();



    }
    
}
