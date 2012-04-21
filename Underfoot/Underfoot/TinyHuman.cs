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
    public enum TinyHumanType
    {
        Zorf = 0,
        Mupp = 1,
        Girl1 = 2,
        Girl2 = 3,
        Soldier = 4,
        Cat     = 5,
        Homer   = 6,
        FatBob  = 7
    }
    
    
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TinyHuman : Microsoft.Xna.Framework.GameComponent
    {

        public Vector2 pos; // Where we at

        public Vector2 dir; // Run for the hills!
        public float angle;

        private float speed;

        public TinyHumanType type;
        public bool dead;

        Game1 game;

        public TinyHuman(Game1 game1)
            : base(game1)
        {
            // TODO: Construct any child components here

            game = game1;

            pos = new Vector2(game.rnd.Next(40), game.rnd.Next(28));
            dir = new Vector2(0, 0);
            speed = 0.2f;
            angle = 0.0f;
            type = (TinyHumanType)game.rnd.Next(7);

            dead = false;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here



            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update2(GameTime gameTime, Vector2 foot)
        {
            // TODO: Add your update code here

            if (dead)
                return;

            float dx = pos.X - foot.X;
            float dy = pos.Y - foot.Y;
            float dist = (float)Math.Sqrt((double)(dx * dx + dy * dy));

            if (Math.Abs(dx) < 1 && Math.Abs(dy) < 1)
                dead = true;

            //angle = (float)Math.Atan((double)dy / (double)dx);

            //// r�kna ut riktning
            //pos.X += speed * (float)Math.Cos(angle);
            //pos.Y += speed * (float)Math.Sin(angle);

            if (type == TinyHumanType.Soldier)
            {
                if (dx >= 0)
                    pos.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dy >= 0)
                    pos.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dx <= 0)
                    pos.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dy <= 0)
                    pos.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (dist < 10 && dx < 10 && dx >= 0)
                    pos.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dist < 10 && dy < 10 && dy >= 0)
                    pos.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dist < 10 && dx > -10 && dx <= 0)
                    pos.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dist < 10 && dy > -10 && dy <= 0)
                    pos.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (pos.X > game.maxx)
                pos.X = game.maxx;
            if (pos.Y > game.maxy)
                pos.Y = game.maxx;
            if (pos.X < 1)
                pos.X = 1;
            if (pos.Y < 1)
                pos.Y = 1;

            base.Update(gameTime);
        }
    }
}
