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

        Game1 game;

        float speed;    // pixels/s
        float footZAxisSpacing = 30;
        Vector2 target;
        Vector2 heading;
        TimeSpan elapsed;
        TimeSpan timeBetweenSteps;
        TimeSpan timeBeforeLiftFoot;
        TimeSpan oneSecond;
        int lastFootPutDown;

        private Texture2D leftFoot;
        private Texture2D rightFoot;
        private Texture2D shadow;

        private Rectangle footSizeRect;
        private float angle;

        Vector2[] feet;
        Vector2[] footDirections;
        float[] footAngles;
        bool[] footInAir;

        SpriteBatch playerBatch;

        public Player(Game1 game1)
            : base(game1)
        {
            // TODO: Construct any child components here
            this.game = game1;
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
            speed = 25;
            heading = new Vector2(0, 0);
            target = new Vector2(0, 0);
            timeBetweenSteps = new TimeSpan(0, 0, 3);
            timeBeforeLiftFoot = new TimeSpan(0, 0, 0, 0, 800);
            oneSecond = new TimeSpan(0, 0, 1);
            footSizeRect = new Rectangle(0,0,100,200);

            feet = new Vector2[2];
            footInAir = new bool[2] { false, false };
            feet[0].X = x;
            feet[0].Y = y;
            feet[1].X = x;
            feet[1].Y = y;
            footDirections = new Vector2[2];
            footAngles = new float[] { 0.3F, 0.3F };

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

            angle = (float)Math.Atan2(heading.X, -heading.Y);

            // simply set heading to where mouse is
            MouseState ms = Mouse.GetState();
            target.X = ms.X;
            target.Y = ms.Y;

            heading = new Vector2(ms.X - x, ms.Y - y);
            

            // calculate new position
            float dist = ((float)gameTime.ElapsedGameTime.Ticks / oneSecond.Ticks) * speed;
            float dx = target.X - x;
            float dy = target.Y - y;
            float dist2 = (float)Math.Sqrt((dx*dx)+(dy*dy));
            x += dx * (dist / dist2);
            y += dy * (dist / dist2);


            if (elapsed >= timeBetweenSteps)
            {
                // need to take a step
                elapsed = new TimeSpan(0);

                if (footInAir[0])   // left foot
                {

                    Vector2 pos = new Vector2((float)(Math.Sin(angle - (Math.PI/8)) * footZAxisSpacing), -(float)(Math.Cos(angle - (Math.PI/8)) * footZAxisSpacing));
                    feet[0].X = x + pos.X;
                    feet[0].Y = y + pos.Y;
                    footInAir[0] = false;
                    footAngles[0] = angle;
                    lastFootPutDown = 0;
                    FootCrush(0);
                }
                if (footInAir[1])   // right foot
                {
                    Vector2 pos = new Vector2((float)(Math.Sin(angle + (Math.PI/8)) * footZAxisSpacing), -(float)(Math.Cos(angle + (Math.PI/8)) * footZAxisSpacing));
                    feet[1].X = x + pos.X;
                    feet[1].Y = y + pos.Y;
                    footInAir[1] = false;
                    footAngles[1] = angle;
                    lastFootPutDown = 1;
                    FootCrush(1);
                }
            }
            else if(elapsed >= timeBeforeLiftFoot)
            {
                footInAir[1 - lastFootPutDown] = true;
            }
            //this.Draw(gameTime);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            playerBatch.Begin();
            if (!footInAir[0])
            {
                playerBatch.Draw(leftFoot,
                    new Rectangle((int)(feet[0].X), (int)(feet[0].Y), 30, 60),
                    footSizeRect,
                    Color.BlanchedAlmond,
                    footAngles[0],
                    new Vector2(50, 100),
                    SpriteEffects.None, 0.9F);
            }
            if (!footInAir[1])
            {
                playerBatch.Draw(rightFoot,
                    new Rectangle((int)(feet[1].X), (int)(feet[1].Y), 30, 60),
                    footSizeRect,
                    Color.BlanchedAlmond,
                    footAngles[1],
                    new Vector2(50, 100),
                    SpriteEffects.None, 0.9F);
            }
            playerBatch.Draw(shadow, new Rectangle((int)Math.Round(x-50),(int)Math.Round(y-50),100,100) , Color.Gray);
            playerBatch.End();
            base.Draw(gameTime);
        }

        void FootCrush(int foot)
        {
            // Todo
            // place the foot and kill people :)

            float killRadius = 35;
            Vector2 footVector = new Vector2((float)(Math.Sin(footAngles[foot])), -(float)(Math.Cos(footAngles[foot])));
            float fvLength = (float)Math.Sqrt((footVector.X * footVector.X) + (footVector.Y * footVector.Y));

            List<float> killRadii = new List<float>();
            List<float> distances = new List<float>();

            foreach (TinyHuman human in game.tinyHumans)
            {
                Vector2 localPos = new Vector2(human.pos.X * game.blockSize, human.pos.Y * game.blockSize);
                float dx = localPos.X - feet[foot].X;
                float dy = localPos.Y - feet[foot].Y;
                //Vector2 vector = new Vector2(dx, dy);
                //float vLength = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
                // dot product A*B = |A|*|B|*cos(a) 
                //float dotProduct = Microsoft.Xna.Framework.Vector2.Dot(vector, footVector);
                //float cosa = Math.Abs(dotProduct / (vLength * fvLength));
                //float kr = (2*killRadius / 3) + (cosa * killRadius / 3);

                float dist = (float)Math.Sqrt((dx * dx) + (dy * dy));
                if (dist <= killRadius)
                {
                    human.dead = true;
                }
            }
        }
    }
}
