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

        public int diveHeight;
        public int poolDepth;

        override public void create()
        {
            //load level settings
            diveHeight = 550;
            poolDepth = 400;

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


            diver = new Diver(8000, 90-64);
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

            if (diver.y > diveHeight && bubbles.canExplode < 25)
            {
                bubbles.at(diver);

                //bubbleParticle.start(false, 0.0101f, 1500);

                bubbles.start(false, 0.0025f, 12);
                bubbles.canExplode ++;


                //diver.setDrags(500, 2555);
                //diver.acceleration.Y = 0;

                diver.play("enterWater");
                diver.mode = "enterWater";


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
