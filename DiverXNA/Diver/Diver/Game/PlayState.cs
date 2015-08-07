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
    public class PlayState : FlxState
    {
        Diver diver;
        Bubbles bubbles;
        FlxTileblock divingPlatform;
        FlxTileblock bgTile;
        FlxTileblock poolBottom;
        FlxTileblock poolSide;

        InformationText score;

        override public void create()
        {
            //load level settings

            Globals.purgeScoreHistory();

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

            poolSide = new FlxTileblock(0, Globals.diveHeight, 900, 1800);
            poolSide.auto = FlxTileblock.OFF;
            poolSide.loadTiles("tile", 9, 9, 0);

            FlxTileblock poolTile = new FlxTileblock(0, Globals.diveHeight, (int)poolSide.width + Globals.poolWidth + 900, Globals.poolDepth + 9);
            poolTile.auto = FlxTileblock.OFF;
            poolTile.loadTiles("tile", 9, 9, 0);
            poolTile.alpha = 0.3225f;
            add(poolTile);

            poolTile = new FlxTileblock(0, Globals.diveHeight + Globals.poolDepth - 90, (int)poolSide.width + Globals.poolWidth + 900, Globals.poolDepth + 9);
            poolTile.auto = FlxTileblock.OFF;
            poolTile.loadTiles("tile", 9, 9, 0);
            poolTile.alpha = 0.5225f;
            add(poolTile);

            

            bgTile = new FlxTileblock(0, 0, (int)poolSide.width + Globals.poolWidth + 900, Globals.diveHeight);
            bgTile.auto = FlxTileblock.OFF;
            bgTile.loadTiles("tile", 9, 9, 3);
            bgTile.alpha = 0.125f;
            add(bgTile);

            Globals.jumpPoint = (int)poolSide.width + Globals.poolWidth;

            divingPlatform = new FlxTileblock(Globals.jumpPoint, 90, 900, 1800);
            divingPlatform.auto = FlxTileblock.OFF;
            divingPlatform.loadTiles("tile", 9,9,0);
            add(divingPlatform);

            poolBottom = new FlxTileblock(0, Globals.diveHeight + Globals.poolDepth, (int)poolSide.width + Globals.poolWidth + 900, 180);
            poolBottom.auto = FlxTileblock.OFF;
            poolBottom.loadTiles("tile", 9, 9, 0);
            add(poolBottom);
            add(poolSide);

            diver = new Diver((int)(divingPlatform.x + divingPlatform.width - 64), 90-48);
            add(diver);

            bubbles = new Bubbles(0, 0);
            add(bubbles);

            FlxG.follow(diver, 50);

            FlxG.followBounds(0, 0, (int)(poolSide.width + Globals.poolWidth + divingPlatform.width), 9000);

            FlxG.score = 0;

            score = new InformationText(0, 30, FlxG.width);
            score.setFormat(null, 2, Color.White, FlxJustification.Center, Color.Black);
            score.visible = false;
            score.setScrollFactors(0, 0);
            add(score);

        }

        override public void update()
        {
            if (FlxG.debug)
            {
                if (FlxControl.LEFT)
                {
                    FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxHud.xboxDPadLeft, 0, 0);
                    FlxG.showHud();
                }
                else if (FlxControl.RIGHT)
                {
                    FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxHud.xboxDPadRight, 0, 0);
                    FlxG.showHud();
                }
                else if (FlxControl.ACTION)
                {
                    FlxG._game.hud.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxHud.xboxButtonA, 0, 0);
                    FlxG.showHud();
                }
                else
                {
                    FlxG.hideHud();
                }
            }

            FlxU.collide(diver, divingPlatform);
            FlxU.collide(diver, poolBottom);
            FlxU.collide(diver, poolSide);

            if (diver.y > Globals.diveHeight && bubbles.canExplode < 35  && diver.mode!="swim")
            {
                bubbles.at(diver);
                bubbles.x -= 32;
                bubbles.y -= 32;

                bubbles.start(false, 0.0025f, 5);
                bubbles.canExplode ++;


                if (diver.hasEnteredWater==false)
                    diver.check();

                diver.hasEnteredWater = true;

                //if (diver.mode!="enterWater")
                //    diver.check();

                //diver.play("enterWater");
                //diver.mode = "enterWater";

            }

            if (diver.mode == "swim" && diver.y <= (Globals.diveHeight - diver.height/2)-2 )
            {
                diver.acceleration.Y = 0;
                diver.velocity.Y = 10;
            }

            if (diver.mode == "breathe" || diver.dead==true)
            {
                score.visible = true;
                //score.text = string.Format("Score {0}", FlxG.score);
                //Console.WriteLine("SCORE");

                score.displayScoreHistory();

            }

            if (FlxControl.CANCELJUSTPRESSED)
            {
                FlxG.state = new PlayState();
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
