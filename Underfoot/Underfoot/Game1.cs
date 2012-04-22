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

        public TinyHuman[] tinyHumans;
        public House[] Houses;

        private Texture2D zorf;
        private Texture2D mupp;
        private Texture2D blood1;
        private Texture2D blood2;
        private Texture2D ground;
        private Texture2D foot;

        private Texture2D cat;
        private Texture2D girl1;
        private Texture2D girl2;
        private Texture2D fatbob;
        private Texture2D soldier;
        private Texture2D soldier2;
        private Texture2D homer;
        private Texture2D tinypresident;

        private Texture2D mormorshus;
        private Texture2D mormorshus2;
        private Texture2D flowers1;
        private Texture2D flowers2;

        private Texture2D splash1;
        private Texture2D splash2;
        private Texture2D gameOver;

        private Texture2D whitehouse1;
        private Texture2D whitehouse2;
        private Texture2D whitehouse3;

        private Texture2D whitehouse1b;
        private Texture2D whitehouse2b;
        private Texture2D whitehouse3b;

        public SoundEffect soundSplatt1;
        public SoundEffect soundSplatt2;
        public SoundEffect soundSplatt3;
        public SoundEffect soundKrash;
        public SoundEffect soundStep;
        public SoundEffect soundShot;
        public SoundEffect music;
        public SoundEffectInstance musicLoop;
        public Song introSong;
        public Song sadSong;

        public Random rnd;

        private Levels levels;
        private int level;

        public int endgame;
        TimeSpan endgameTime;

        public int life;

        public int blockSize
        {
            get;
            set;
        }

        public int maxx, maxy;

        public int MAXHOUSE = 20;
        public int MAXHUMAN = 100;
        public int MAXX = 40;
        public int MAXY = 24;

        public int humansToKill;

        TimeSpan gameOverScreenTime;
        bool showGameOver;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rnd = new Random();
            maxx = 39;
            maxy = 23;

            Map = new GameMap(this, 300, 200);
            player = new Player(this);
            levels = new Levels(this);

            tinyHumans = new TinyHuman[MAXHUMAN];
            Houses = new House[MAXHOUSE];

            int c;
            for (c = 0; c < MAXHUMAN; c++)
            {
                tinyHumans[c] = new TinyHuman(this);
                tinyHumans[c].active = false;
            }

            for (c = 0; c < MAXHOUSE; c++)
            {
                Houses[c] = new House(this);
                Houses[c].active = false;
            }

            Components.Add(player);
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();

            showGameOver = false;
            gameOverScreenTime = TimeSpan.FromSeconds(5);

            life = 100;
            endgame = 0;

            level = 3;
            levels.UpdateLevel(level, tinyHumans, Houses, Map, out humansToKill);
            
        }

        protected override void EndRun()
        {
            MediaPlayer.Stop();
            base.EndRun();
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
            cat = Content.Load<Texture2D>("cat");
            girl1 = Content.Load<Texture2D>("girl1");
            girl2 = Content.Load<Texture2D>("girl2");
            soldier = Content.Load<Texture2D>("soldier");
            soldier2 = Content.Load<Texture2D>("soldier2");
            homer = Content.Load<Texture2D>("homer");
            fatbob = Content.Load<Texture2D>("fatbob");
            tinypresident = Content.Load<Texture2D>("tinypresident");

            blood1 = Content.Load<Texture2D>("blood1");
            blood2 = Content.Load<Texture2D>("blood2");
            foot = Content.Load<Texture2D>("foot");

            mormorshus= Content.Load<Texture2D>("mormorshus");
            mormorshus2 = Content.Load<Texture2D>("mormorshus2");
            flowers1 = Content.Load<Texture2D>("flowers1");
            flowers2 = Content.Load<Texture2D>("flowers2");

            splash1 = Content.Load<Texture2D>("Splash");
            splash2 = Content.Load<Texture2D>("Splash2");
            gameOver = Content.Load<Texture2D>("gameover");

            whitehouse1 = Content.Load<Texture2D>("tinywhitehouse1");
            whitehouse2 = Content.Load<Texture2D>("tinywhitehouse2");
            whitehouse3 = Content.Load<Texture2D>("tinywhitehouse3");

            whitehouse1b = Content.Load<Texture2D>("tinywhitehouse1b");
            whitehouse2b = Content.Load<Texture2D>("tinywhitehouse2b");
            whitehouse3b = Content.Load<Texture2D>("tinywhitehouse3b");

            soundSplatt1 = Content.Load<SoundEffect>("splurt1");
            soundSplatt2 = Content.Load<SoundEffect>("splurt2");
            soundSplatt3 = Content.Load<SoundEffect>("splurt3");
            soundKrash = Content.Load<SoundEffect>("Explosion");
            soundStep = Content.Load<SoundEffect>("Step");
            soundShot = Content.Load<SoundEffect>("shot");
            music = Content.Load<SoundEffect>("music");
            musicLoop = music.CreateInstance();
            musicLoop.IsLooped = true;
            musicLoop.Volume = 0.2F;

            introSong = Content.Load<Song>("intromusic");
            sadSong = Content.Load<Song>("sad");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(introSong);
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

            if (endgame == 1)
            {
                tinyHumans[41].active = true;
                tinyHumans[41].dead = false;
                tinyHumans[41].type = TinyHumanType.President;
                tinyHumans[41].pos.X = 22;
                tinyHumans[41].pos.Y = 6;
                endgame = 2;
            }

            if (endgame > 2)
                return;

            if (showGameOver)
            {
                musicLoop.Stop();
                return;
            }
            if (musicLoop.State != SoundState.Playing)
                musicLoop.Play();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            Vector2 foot2;
            foot2.X = player.Position.X / blockSize;
            foot2.Y = player.Position.Y / blockSize;

            int c;
            for (c = 0; c < 100; c++)
                tinyHumans[c].Update2(gameTime, foot2);

            for (c = 0; c < 5; c++)
                Houses[c].Update2(gameTime, foot2);

            // TODO: Add your update logic here

            if (humansToKill < 1)
            {
                // Go to next level
                level++;

                levels.UpdateLevel(level, tinyHumans, Houses, Map, out humansToKill);
            }

            if (life < 1)
            {
                life = 100;

                levels.UpdateLevel(level, tinyHumans, Houses, Map, out humansToKill);

                showGameOver = true;
                gameOverScreenTime = gameTime.TotalGameTime;
            }

            if (level == 3 && endgame == 0 && Houses[0].destroyed && Houses[1].destroyed && Houses[2].destroyed)
                endgame = 1;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(WaterColor);

            if (gameTime.TotalGameTime < TimeSpan.FromSeconds(2))
            {
                spriteBatch.Begin();
                spriteBatch.Draw(splash1, new Rectangle(0,
                         0, 800, 480), Color.White);
                spriteBatch.End();
                return;
            }
            else
                if (gameTime.TotalGameTime < TimeSpan.FromSeconds(8))
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(splash2, new Rectangle(0, 0, 800, 480), Color.White);
                    spriteBatch.End();
                    return;
                }

            if (endgame == 2 && tinyHumans[41].pos.Y < 10)
            {
                tinyHumans[41].pos.Y += 0.8f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                tinyHumans[41].pos.X -= 0.8f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }

            if (endgame == 2 && tinyHumans[41].pos.Y >= 10)
                endgame = 3;

            // TODO: Add your drawing code here
            int x, y, c;

            spriteBatch.Begin();

            for (x = 0; x < MAXX; x++)
                for (y = 0; y < MAXY; y++ )
                    spriteBatch.Draw(ground, new Rectangle(x * blockSize, y * blockSize, blockSize, blockSize), Color.White);

            for (c = 0; c < 100; c++ )
                {
                    if (!tinyHumans[c].active)
                        continue;
                    Texture2D texture;

                    if (tinyHumans[c].type == TinyHumanType.Zorf)
                        texture = zorf;
                    else if (tinyHumans[c].type == TinyHumanType.Mupp)
                        texture = mupp;
                    else if (tinyHumans[c].type == TinyHumanType.Girl1)
                        texture = girl1;
                    else if (tinyHumans[c].type == TinyHumanType.Girl2)
                        texture = girl2;
                    else if (tinyHumans[c].type == TinyHumanType.Soldier && tinyHumans[c].doingAction)
                        texture = soldier2;
                    else if (tinyHumans[c].type == TinyHumanType.Soldier)
                        texture = soldier;
                    else if (tinyHumans[c].type == TinyHumanType.FatBob)
                        texture = fatbob;
                    else if (tinyHumans[c].type == TinyHumanType.Homer)
                        texture = homer;
                    else if (tinyHumans[c].type == TinyHumanType.Cat)
                        texture = cat;
                    else if (tinyHumans[c].type == TinyHumanType.President)
                        texture = tinypresident;
                    else
                        texture = mupp;

                    if (!tinyHumans[c].dead)
                    {
                        spriteBatch.Draw(texture, new Rectangle((int)(tinyHumans[c].pos.X * blockSize),
                        (int)(tinyHumans[c].pos.Y * blockSize), blockSize, blockSize), Color.White);
                    }
                    if (tinyHumans[c].dead)
                        spriteBatch.Draw(blood1, new Rectangle((int)(tinyHumans[c].pos.X * blockSize),
(int)(tinyHumans[c].pos.Y * blockSize), blockSize, blockSize), Color.White);

                }

            for (c = 0; c < 5; c++)
            {
                if (!Houses[c].active)
                    continue;

                Texture2D texture;

                if (Houses[c].type == HouseType.Mormorshus)
                {
                    if (Houses[c].destroyed)
                        texture = mormorshus2;
                    else
                        texture = mormorshus;
                }
                else if (Houses[c].type == HouseType.Whitehouse1)
                {
                    if (Houses[c].destroyed)
                        texture = whitehouse1b;
                    else
                        texture = whitehouse1;
                }
                else if (Houses[c].type == HouseType.Whitehouse2)
                {
                    if (Houses[c].destroyed)
                        texture = whitehouse2b;
                    else
                        texture = whitehouse2;
                }
                else if (Houses[c].type == HouseType.Whitehouse3)
                {
                    if (Houses[c].destroyed)
                        texture = whitehouse3b;
                    else
                        texture = whitehouse3;
                }
                else
                {
                    if (Houses[c].destroyed)
                        texture = flowers2;
                    else
                        texture = flowers1;
                }
                spriteBatch.Draw(texture, new Rectangle((int)(Houses[c].pos.X * blockSize),
                     (int)(Houses[c].pos.Y * blockSize), blockSize*2, blockSize*2), Color.White);
            }


            if (endgame == 3)
            {
                endgame = 4;
                endgameTime = gameTime.TotalGameTime;
            }

            if (endgame >= 4)
            {
                spriteBatch.DrawString(myFont, "TINY PRESIDENT: Hey! Why did you", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize), Color.Yellow);
                spriteBatch.DrawString(myFont, "destroy my tiny white house?", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize), Color.Yellow);

                endgame = 5;
            }

            if (endgame >= 5 && gameTime.TotalGameTime > endgameTime + TimeSpan.FromSeconds(3))
            {
                spriteBatch.DrawString(myFont, "GIANT: Oh sorry I didn't mean to", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 2), Color.LightBlue);

                endgame = 6;
            }

            if (endgame >= 6 && gameTime.TotalGameTime > endgameTime + TimeSpan.FromSeconds(6))
            {
                spriteBatch.DrawString(myFont, "TINY PRESIDENT: From now on I want you", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 3), Color.Yellow);
                spriteBatch.DrawString(myFont, "to only use your powers for good!", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 4), Color.Yellow);

                endgame = 7;
            }

            if (endgame == 7 && gameTime.TotalGameTime > endgameTime + TimeSpan.FromSeconds(9))
            {
                spriteBatch.DrawString(myFont, "GIANT: OK", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 5), Color.LightBlue);

                endgame = 8;
            }

            if (endgame == 8 && gameTime.TotalGameTime > endgameTime + TimeSpan.FromSeconds(12))
            {
                spriteBatch.DrawString(myFont, "TINY PRESIDENT: I will now use my magic", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 6), Color.Yellow);
                spriteBatch.DrawString(myFont, "powers to bring everyone you killed back", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 7), Color.Yellow);
                spriteBatch.DrawString(myFont, "to life again!", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 8), Color.Yellow);

                endgame = 9;
            }

            if (endgame == 9 && gameTime.TotalGameTime > endgameTime + TimeSpan.FromSeconds(15))
            {
                spriteBatch.DrawString(myFont, "GIANT: Hurray!", new Vector2(tinyHumans[41].pos.X * blockSize + blockSize * 2, tinyHumans[41].pos.Y * blockSize + blockSize * 9), Color.LightBlue);


                endgame = 10;
            }

            if (endgame == 10 && gameTime.TotalGameTime > endgameTime + TimeSpan.FromSeconds(22))
            {
                showGameOver = true;
                endgame = 11;

            }

            spriteBatch.DrawString(myFont, "Humans To Kill : " + humansToKill.ToString(), new Vector2(blockSize, blockSize), Color.White);

            spriteBatch.DrawString(myFont, "Life : " + life.ToString(), new Vector2(700, blockSize), Color.White);
            

            if (showGameOver)
            {
 
                spriteBatch.Draw(gameOver, new Rectangle(0, 0, 800, 480), Color.White);

                if (gameTime.TotalGameTime > gameOverScreenTime + TimeSpan.FromSeconds(5))
                {
                    showGameOver = false;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
