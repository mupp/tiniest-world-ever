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
    public class Levels
    {
        Game1 game;
        public Levels(Game1 game1)
        {
            game = game1;
        }

        public void UpdateLevel(int level, TinyHuman[] tinyHumans, House[] Houses, GameMap Map, out int humansToKill)
        {
            humansToKill = 20;

            if (level == 1)
            {
                int c;
                for (c = 0; c < 20; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].dead = false;
                    tinyHumans[c].type = (TinyHumanType)game.rnd.Next(4);
                }

                tinyHumans[21].active = true;
                tinyHumans[21].type = TinyHumanType.Cat;

                for (c = 0; c < 5; c++)
                {
                    Houses[c].active = true;
                    //tinyHumans[c].dead = false;
                    Houses[c].type = HouseType.Mormorshus;
                }

                humansToKill = 20;
            }
            else if (level == 2)
            {
                int c;
                for (c = 0; c < 20; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].dead = false;
                    tinyHumans[c].type = (TinyHumanType)game.rnd.Next(4);
                }

                for (c = 20; c < 40; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].dead = false;
                    tinyHumans[c].type = TinyHumanType.Soldier;

                    tinyHumans[c].actionSpan = TimeSpan.FromSeconds(5);
                }

                humansToKill = 40;

                for (c = 0; c < 10; c++)
                {
                    Houses[c].active = true;
                    Houses[c].type = HouseType.Mormorshus;
                }
            }
            else
            {
                int c;
                for (c = 0; c < 20; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].dead = false;
                    tinyHumans[c].type = (TinyHumanType)game.rnd.Next(4);
                }

                for (c = 20; c < 40; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].dead = false;
                    tinyHumans[c].type = TinyHumanType.Soldier;

                    tinyHumans[c].actionSpan = TimeSpan.FromSeconds(game.rnd.Next(3)+2);

                }

                humansToKill = 40;
                
                for (c = 0; c < 10; c++)
                {
                    Houses[c].active = false;
                }

                Houses[0].active = true;
                Houses[0].type = HouseType.Whitehouse1;
                Houses[0].pos.X = 20;
                Houses[0].pos.Y = 5;

                Houses[1].active = true;
                Houses[1].type = HouseType.Whitehouse2;
                Houses[1].pos.X = 22;
                Houses[1].pos.Y = 5;

                Houses[2].active = true;
                Houses[2].type = HouseType.Whitehouse3;
                Houses[2].pos.X = 24;
                Houses[2].pos.Y = 5;

            }
        
        }

    }
}
