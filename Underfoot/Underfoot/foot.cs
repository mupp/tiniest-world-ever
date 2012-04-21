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
    public class foot : Microsoft.Xna.Framework.GameComponent
    {

        public Vector2 pos;
        float speed;

        public foot(Game game)
            : base(game)
        {
            pos.X = 10;
            pos.Y = 10;
            speed = 0.1f;


            // TODO: Construct any child components here
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
        public void Update2(GameTime gameTime, bool up, bool down, bool left, bool right)
        {
            // TODO: Add your update code here          

            if (up)
                pos.Y -= speed;
            if (down)
                pos.Y += speed;
            if (left)
                pos.X -= speed;
            if (right)
                pos.X += speed;

            base.Update(gameTime);
        }
    }
}
