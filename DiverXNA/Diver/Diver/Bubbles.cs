/*
            bubbleParticle = new FlxEmitter();
            bubbleParticle.delay = 3;
            bubbleParticle.setXSpeed(-150, 150);
            bubbleParticle.setYSpeed(-40, 100);
            bubbleParticle.setRotation(-720, 720);
            bubbleParticle.gravity = Lemonade_Globals.GRAVITY * -0.25f;
            bubbleParticle.createSprites(FlxG.Content.Load<Texture2D>("Lemonade/bubble"), 200, true, 1.0f, 0.65f);
            add(bubbleParticle);
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Diver
{
    class Bubbles : FlxEmitter
    {
        public int canExplode;

        public Bubbles(int xPos, int yPos)
            : base(xPos, yPos)
        {
            this.delay = 3;
            this.setSize(64, 64);
            this.setXSpeed(-15, 15);
            this.setYSpeed(-40, 15);
            this.setRotation(0, 0);
            
            this.gravity = -9.8f;
            this.createSprites(FlxG.Content.Load<Texture2D>("bubble"), 1200, true, 1.0f, 0.65f);

            
            this.canExplode = 0;
        }

        override public void update()
        {
            base.update();
        }

        
    }
}
