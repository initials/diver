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
        FlxTileblock tile;
        FlxTileblock bgTile;
        FlxTileblock poolBottom;
        FlxSprite poolSide;

        public int diveHeight;
        public int poolDepth;

        override public void create()
        {
            //load level settings
            diveHeight = 1550;
            poolDepth = 700;

            FlxG.backColor = new Color(0,116,239);
            base.create();

            bgTile = new FlxTileblock(5000, 0, 2700, 900);
            bgTile.auto = FlxTileblock.OFF;
            bgTile.loadTiles("tile", 9, 9, 0);
            bgTile.alpha = 0.125f;
            add(bgTile);

            tile = new FlxTileblock(7000, 90, 1800, 1800);
            tile.auto = FlxTileblock.OFF;
            tile.loadTiles("tile", 9,9,0);
            add(tile);

            bgTile = new FlxTileblock(5000, diveHeight, 2700, poolDepth);
            bgTile.auto = FlxTileblock.OFF;
            bgTile.loadTiles("tile", 9, 9, 0);
            bgTile.alpha = 0.225f;
            add(bgTile);

            poolBottom = new FlxTileblock(5000, diveHeight + poolDepth, 2700, 90);
            poolBottom.auto = FlxTileblock.OFF;
            poolBottom.loadTiles("tile", 9, 9, 0);
            add(poolBottom);

            poolSide = new FlxSprite(-900, diveHeight-3);
            poolSide.loadGraphic("poolSide", false, false, 180, 180);
            poolSide.@fixed = true;
            poolSide.solid = true;
            //poolSide.moves = false;
            add(poolSide);

            diver = new Diver(8000, 90-48);
            add(diver);

            bubbles = new Bubbles(0, 0);
            add(bubbles);

            FlxG.follow(diver, 50);
            
            FlxG.followBounds(0, 0, 9000, 9000);

            FlxLine x = new FlxLine(0, 0, new Vector2(0, diveHeight), new Vector2(9000, diveHeight), Color.White, 2);
            add(x);

            x = new FlxLine(0, 0, new Vector2(0, diveHeight + poolDepth), new Vector2(9000, diveHeight + poolDepth), Color.White, 2);
            add(x);
        }

        override public void update()
        {

            FlxU.collide(diver, tile);
            FlxU.collide(diver, poolBottom);
            FlxU.collide(diver, poolSide);

            if (diver.y > diveHeight && bubbles.canExplode < 25  && diver.mode!="swim")
            {
                bubbles.at(diver);

                bubbles.start(false, 0.0025f, 12);
                bubbles.canExplode ++;

                diver.play("enterWater");
                diver.mode = "enterWater";

            }

            if (diver.mode == "swim" && diver.y <= diveHeight - diver.height/2 && poolSide.x==-900)
            {
                diver.mode = "swimUp";
                diver.acceleration.Y = 0;
                diver.velocity.Y = 0;

                float poolSideNumber = diver.x - 435 - poolSide.width;
                //Console.WriteLine(poolSideNumber + "   " + Convert.ToInt32(1.0f / ((diveHeight + poolDepth) - diver.y) * 1000).ToString());

                poolSide.x = poolSideNumber;

                float x = (float)((diveHeight + poolDepth) - diver.y);
                //Console.WriteLine(x);

                //FlxG.score += Convert.ToInt32(1.0f / (float)(((diveHeight + poolDepth) - diver.y)) * 1000);

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
