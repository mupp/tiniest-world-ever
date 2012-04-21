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
    public class TinyHuman : Microsoft.Xna.Framework.GameComponent
    {

        public Vector2 pos; // Where we at

        public Vector2 dir; // Run for the hills!

        private float speed;

        public int type;

        public TinyHuman(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here

            

            pos = new Vector2(game.rnd.Next(40), game.rnd.Next(28));
            dir = new Vector2(0, 0);
            speed = 1.0f;
            type = game.rnd.Next(2);
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
        public void Update2(GameTime gameTime, double footx, double footy)
        {
            // TODO: Add your update code here

            // räkna ut riktning
            //pos.X = pos.X + speed;

            base.Update(gameTime);
        }
    }
}
