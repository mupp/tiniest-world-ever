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

        public void UpdateLevel(int level, TinyHuman[] tinyHumans, House[] Houses, GameMap Map)
        {
            if (level == 1)
            {
                int c;
                for (c = 0; c < 20; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].type = (TinyHumanType)game.rnd.Next(4);
                }

                tinyHumans[21].active = true;
                tinyHumans[21].type = TinyHumanType.Cat;

                for (c = 0; c < 5; c++)
                {
                    Houses[c].active = true;
                    Houses[c].type = HouseType.Mormorshus;
                }
            }
            else if (level == 2)
            {
                int c;
                for (c = 0; c < 20; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].type = (TinyHumanType)game.rnd.Next(4);
                }

                for (c = 21; c < 40; c++)
                {
                    tinyHumans[c].active = true;
                    tinyHumans[c].type = TinyHumanType.Soldier;
                }

                for (c = 0; c < 10; c++)
                {
                    Houses[c].active = true;
                    Houses[c].type = HouseType.Blommor;
                }
            }
            { 
            
            }
        
        }

    }
}
