using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace Diver
{
    public class IntroState : FlxState
    {
        Diver diver;
        Bubbles bubbles;
        FlxTileblock divingPlatform;
        FlxTileblock bgTile;
        FlxTileblock poolBottom;
        FlxTileblock poolSide;

        


        override public void create()
        {
            //load level settings


            FlxG.backColor = new Color(0,116,239);
            base.create();

            FlxLine x = new FlxLine(0, 0, new Vector2(0, Globals.diveHeight),
                new Vector2(9000, Globals.diveHeight),
                Color.White, 2);
            add(x);

            x = new FlxLine(0, 0, new Vector2(0, Globals.diveHeight + Globals.poolDepth),
                new Vector2(9000, Globals.diveHeight + Globals.poolDepth),
                Color.White, 2);
            add(x);

            poolSide = new FlxTileblock(0, Globals.diveHeight - 3, 900, 1800);
            poolSide.auto = FlxTileblock.OFF;
            poolSide.loadTiles("tile", 9, 9, 0);

            FlxTileblock poolTile = new FlxTileblock(0, Globals.diveHeight, (int)poolSide.width + Globals.poolWidth + 900, Globals.poolDepth);
            poolTile.auto = FlxTileblock.OFF;
            poolTile.loadTiles("tile", 9, 9, 0);
            poolTile.alpha = 0.5225f;
            add(poolTile);

            add(poolSide);

            bgTile = new FlxTileblock(0, 0, (int)poolSide.width + Globals.poolWidth + 900, Globals.diveHeight);
            bgTile.auto = FlxTileblock.OFF;
            bgTile.loadTiles("tile", 9, 9, 0);
            bgTile.alpha = 0.125f;
            add(bgTile);

            Globals.jumpPoint = (int)poolSide.width + Globals.poolWidth;

            divingPlatform = new FlxTileblock(Globals.jumpPoint, 90, 900, 1800);
            divingPlatform.auto = FlxTileblock.OFF;
            divingPlatform.loadTiles("tile", 9,9,0);
            add(divingPlatform);



            poolBottom = new FlxTileblock(0, Globals.diveHeight + Globals.poolDepth, 2700, 90);
            poolBottom.auto = FlxTileblock.OFF;
            poolBottom.loadTiles("tile", 9, 9, 0);
            add(poolBottom);

            diver = new Diver((int)(divingPlatform.x + divingPlatform.width - 64), 90-48);
            add(diver);

            bubbles = new Bubbles(0, 0);
            add(bubbles);

            FlxG.follow(diver, 50);

            FlxG.followBounds(0, 0, (int)(poolSide.width + Globals.poolWidth + divingPlatform.width), 9000);


        }

        override public void update()
        {
            //Console.WriteLine("Mode : {0} Diver Y {1}", diver.mode, diver.y);

            FlxU.collide(diver, divingPlatform);
            FlxU.collide(diver, poolBottom);
            FlxU.collide(diver, poolSide);

            if (diver.y > Globals.diveHeight && bubbles.canExplode < 25  && diver.mode!="swim")
            {
                bubbles.at(diver);

                bubbles.start(false, 0.0025f, 12);
                bubbles.canExplode ++;

                diver.play("enterWater");
                diver.mode = "enterWater";

            }

            if (diver.mode == "swim" && diver.y <= Globals.diveHeight - diver.height/2 )
            {
                diver.mode = "swimUp";
                diver.acceleration.Y = 0;
                diver.velocity.Y = 0;

                diver.velocity.X = -50;

                //float poolSideNumber = diver.x - 435 - poolSide.width;
                //poolSide.x = poolSideNumber;
                //float x = (float)((Globals.diveHeight + Globals.poolDepth) - diver.y);

            }

            if (FlxControl.CANCELJUSTPRESSED)
            {
                FlxG.state = new IntroState();
                return;
            }

            base.update();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            
            base.render(spriteBatch);
        }


    }
}
