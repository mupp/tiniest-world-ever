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

    public enum HouseType
    {
        Mormorshus = 0,
        Blommor = 1

    }

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class House : Microsoft.Xna.Framework.GameComponent
    {

        public Vector2 pos; // Where we at
        public HouseType type;
        public bool destroyed;

        public bool active;

        public House(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here

            type = HouseType.Mormorshus;
            destroyed = false;

            pos = new Vector2(game.rnd.Next(game.MAXX), game.rnd.Next(game.MAXY));
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
            float dx = pos.X - foot.X;
            float dy = pos.Y - foot.Y;

            if (Math.Abs(dx) < 1 && Math.Abs(dy) < 1)
                destroyed = true;

            base.Update(gameTime);
        }
    }
}
