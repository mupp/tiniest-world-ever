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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont myFont;

        // vars
        Color WaterColor = Color.Blue;
        Color LandColor = Color.Green;
        Color FootColor = Color.Yellow;
        Color GuyColor = Color.Black;
        Color SplatColor = Color.Red;

        GameMap Map;
        Player player;

        TinyHuman[] tinyHumans;

        private Texture2D zorf;
        private Texture2D mupp;
        private Texture2D blood1;
        private Texture2D blood2;
        private Texture2D ground;
        private Texture2D foot;

        public Random rnd;

        private foot Foot;

        public int blockSize
        {
            get;
            set;
        }

        public int maxx, maxy;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rnd = new Random();
            maxx = 39;
            maxy = 24;

            Map = new GameMap(this, 300, 200);
            player = new Player(this);
            Foot = new foot(this);

            tinyHumans = new TinyHuman[100];

            int c;
            for (c = 0; c < 100; c++)
                tinyHumans[c] = new TinyHuman(this);


            Components.Add(player);
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            blockSize = 20;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myFont = Content.Load<SpriteFont>("SpriteFont1");

            ground = Content.Load<Texture2D>("ground");
            zorf = Content.Load<Texture2D>("zorf");
            mupp = Content.Load<Texture2D>("mupp");
            blood1 = Content.Load<Texture2D>("blood1");
            blood2 = Content.Load<Texture2D>("blood2");
            foot = Content.Load<Texture2D>("foot");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            bool up, right, left, down;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                up = true;
            else
                up = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                right = true;
            else
                right = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                down = true;
            else
                down = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                left = true;
            else
                left = false;

            Foot.Update2(gameTime, up, down, left, right);

            int c;
            for (c = 0; c < 100; c++)
                tinyHumans[c].Update2(gameTime, Foot.pos);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(WaterColor);

            // TODO: Add your drawing code here
            int x, y, c;

            spriteBatch.Begin();

            for (x = 0; x < 40; x++)
                for (y = 0; y < 24; y++ )
                    spriteBatch.Draw(ground, new Rectangle(x * blockSize, y * blockSize, blockSize, blockSize), Color.White);

            for (c = 0; c < 100; c++ )
                {
                    Texture2D texture;
                    if (tinyHumans[c].type == 0) 
                        texture = zorf;
                    else
                        texture = mupp;

                    spriteBatch.Draw(texture, new Rectangle((int)(tinyHumans[c].pos.X * blockSize),
                    (int)(tinyHumans[c].pos.Y * blockSize), blockSize, blockSize), Color.White);

                    if (tinyHumans[c].dead)
                        spriteBatch.Draw(blood1, new Rectangle((int)(tinyHumans[c].pos.X * blockSize),
(int)(tinyHumans[c].pos.Y * blockSize), blockSize, blockSize), Color.White);

                }

            spriteBatch.Draw(foot, new Rectangle((int)Foot.pos.X * blockSize,
(int)Foot.pos.Y * blockSize, blockSize, blockSize), Color.White);

            spriteBatch.DrawString(myFont, "Time : " + DateTime.Now.ToString(), new Vector2(blockSize, blockSize), Color.White);
            spriteBatch.End();

            Map.Update(gameTime);

            //DrawGuys(gameTime);

            //DrawFeet(gameTime);

            base.Draw(gameTime);
        }

        private void DrawGuys(GameTime time)
        {
            // TODO
        }
        private void DrawFeet(GameTime time)
        {
            // TODO
        }
    }
}
