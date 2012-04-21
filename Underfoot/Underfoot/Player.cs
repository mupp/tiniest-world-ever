using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Underfoot
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        float x;
        float y;
        public Vector2 Position
        {
            get { return new Vector2(x, y); }
        }

        float speed;    // pixels/s
        float footZAxisSpacing = 20;
        Vector2 heading;
        TimeSpan elapsed;
        TimeSpan timeBetweenSteps;
        TimeSpan timeBeforeLiftFoot;
        TimeSpan oneSecond;
        int lastFootPutDown;

        private Texture2D leftFoot;
        private Texture2D rightFoot;
        private Texture2D shadow;

        Vector2[] feet;
        Vector2[] footDirections;
        bool[] footInAir;

        SpriteBatch playerBatch;

        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            playerBatch = new SpriteBatch(Game.GraphicsDevice);
            x = 100;
            y = 100;
            speed = 15;
            heading = new Vector2(0, 0);
            timeBetweenSteps = new TimeSpan(0, 0, 2);
            timeBeforeLiftFoot = new TimeSpan(0, 0, 0, 0, 800);
            oneSecond = new TimeSpan(0, 0, 1);

            feet = new Vector2[2];
            footInAir = new bool[2] { false, false };
            feet[0].X = x;
            feet[0].Y = y;
            feet[1].X = x;
            feet[1].Y = y;
            footDirections = new Vector2[2];

            leftFoot = Game.Content.Load<Texture2D>("leftF");
            rightFoot = Game.Content.Load<Texture2D>("rightF");
            shadow = Game.Content.Load<Texture2D>("shadow");
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            elapsed += gameTime.ElapsedGameTime;

            // simply set heading to where mouse is
            MouseState ms = Mouse.GetState();
            heading = new Vector2(ms.X, ms.Y);

            // calculate new position
            float dist = ((float)gameTime.ElapsedGameTime.Ticks / oneSecond.Ticks) * speed;
            float dx = heading.X - x;
            float dy = heading.Y - y;
            float dist2 = (float)Math.Sqrt((dx*dx)+(dy*dy));
            x += dx * (dist / dist2);
            y += dy * (dist / dist2);


            if (elapsed >= timeBetweenSteps)
            {
                // need to take a step
                elapsed = new TimeSpan(0);
                if (footInAir[0])   // left foot
                {
                    feet[0].X = this.x - feet[1].X;
                    feet[0].Y = this.y - feet[1].Y;
                    footDirections[0].X = heading.X;
                    footDirections[0].Y = heading.Y;
                    footInAir[0] = false;
                    lastFootPutDown = 0;
                    FootCrush(0);
                }
                if (footInAir[1])   // right foot
                {
                    feet[1].X = this.x - feet[0].X;
                    feet[1].Y = this.y - feet[0].Y;
                    footDirections[1].X = heading.X;
                    footDirections[1].Y = heading.Y;
                    footInAir[1] = false;
                    lastFootPutDown = 1;
                    FootCrush(1);
                }
            }
            else if(elapsed >= timeBeforeLiftFoot)
            {
                footInAir[1 - lastFootPutDown] = true;
            }
            this.Draw(gameTime);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            
            playerBatch.Begin();
            playerBatch.Draw(shadow, new Rectangle((int)Math.Round(x-50),(int)Math.Round(y-50),100,100) , Color.Gray);
            playerBatch.End();
            base.Draw(gameTime);
        }

        void FootCrush(int foot)
        {
            // Todo
            // place the foot and kill people :)

        }
    }
}
