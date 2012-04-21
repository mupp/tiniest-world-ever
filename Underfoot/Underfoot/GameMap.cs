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
    public enum TerrainType
    {
        Water = 0,
        Land = 1
    }
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameMap : Microsoft.Xna.Framework.GameComponent
    {

        public TerrainType[,] Map
        {
            get;
            set;
        }

        public int[] WindowSize // width, height
        {
            get;
            set;
        }

        public GameMap(Game game, int width, int height)
            : base(game)
        {
            // TODO: Construct any child components here
            this.WindowSize = new int[] { width, height };
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            this.Map = new TerrainType[WindowSize[0], WindowSize[1]];
            this.Map[WindowSize[0] / 2, WindowSize[1] / 2] = TerrainType.Land;


            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public TerrainType TerrainAt(int x, int y)
        {
            int xc = (int)Math.Round((double)x / WindowSize[0]);
            int yc = (int)Math.Round((double)y / WindowSize[1]);
            return Map[x, y];
        }


    }
}
