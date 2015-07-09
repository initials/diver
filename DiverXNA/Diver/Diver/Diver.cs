/*
 * Add these to Visual Studio to quickly create new FlxSprites
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
    class Diver : FlxSprite
    {

        /// <summary>
        /// Sprite Constructor
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public Diver(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("diver_02", true, false, 64, 64);

            addAnimation("idle", new int[] { 8 }, 0, true);

            addAnimation("run1", this.generateFrameNumbersBetween(0, 5), 1, true);
            addAnimation("run2", this.generateFrameNumbersBetween(0, 5), 2, true);
            addAnimation("run4", this.generateFrameNumbersBetween(0, 5), 4, true);
            addAnimation("run8", this.generateFrameNumbersBetween(0, 5), 8, true);
            addAnimation("run16", this.generateFrameNumbersBetween(0, 5), 16, true);
            addAnimation("run24", this.generateFrameNumbersBetween(0, 5), 24, true);
            addAnimation("run32", this.generateFrameNumbersBetween(0, 5), 32, true);

            addAnimation("swan", this.generateFrameNumbersBetween(7, 18), 16, false);
            addAnimation("dive", this.generateFrameNumbersBetween(18,27), 16, false);

            addAnimation("enterWater", this.generateFrameNumbersBetween(28,31), 16, false);
            addAnimation("swim", this.generateFrameNumbersBetween(31, 64), 16, true);
            addAnimation("hitFloor", new int[] { 78,79 }, 12, true);

            addAnimationCallback(animCallback);

            //addAnimationsFromGraphicsGaleCSV("content/_.csv");

            play("idle");

            this.maxVelocity = new Vector2(15000, 15000);

            this.velocity.X = 0;
            acceleration.Y = 980;

            mode = "idle";

        }

        public void animCallback(string Name, uint Frame, int FrameIndex)
        {
            if (Name == "enterWater" && FrameIndex == 51)
            {
                //mode = "swim";
                //play("swim");

            }
        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {

            if (FlxG.keys.justPressed(Keys.X) && mode=="idle" && this.onFloor)
            {
                play("swan");
                
                velocity.Y = -300;

                mode = "swan";

            }
            else if (FlxG.keys.justPressed(Keys.X) && mode == "swan")
            {
                play("dive");

                mode = "dive";

            }
            else if (FlxG.keys.justPressed(Keys.X) && mode == "enterWater")
            {
                setDrags(0, 2555);
                acceleration.Y = 0;

                mode = "swim";
                play("swim");


            }





            if (FlxControl.LEFTJUSTPRESSED && mode == "idle")
            {
                velocity.X -= 75;

                if (this.velocity.X == 0) play("idle");
                else if (Math.Abs(this.velocity.X) < 75 * 4) play("run4");
                else if (Math.Abs(this.velocity.X) < 150 * 4) play("run8");
                else if (Math.Abs(this.velocity.X) < 225 * 4) play("run16");
                else if (Math.Abs(this.velocity.X) < 300 * 4) play("run24");
                else if (Math.Abs(this.velocity.X) < 375 * 4) play("run32");

            }
            if (FlxControl.RIGHTJUSTPRESSED && mode == "idle")
            {
                velocity.X += 75;

                if (this.velocity.X == 0) play("idle");
                else play("run16");

            }

            if (mode == "dead")
            {
                acceleration.X = 0;
                acceleration.Y = 0;
                velocity.X = 0;
                velocity.Y = 0;

            }



            base.update();
        }

        /// <summary>
        /// The render code. Add any additional render code here.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
        }

        /// <summary>
        /// Called when the sprite hit something on its bottom edge.
        /// </summary>
        /// <param name="Contact">The Object that it collided with.</param>
        /// <param name="Velocity">The Velocity that is will now have???</param>
        public override void hitBottom(FlxObject Contact, float Velocity)
        {
            if (mode == "swan")
            {
                play("hitFloor");
                mode = "dead";
            }

            base.hitBottom(Contact, Velocity);
        }

        /// <summary>
        /// Called when the sprite hits something on its left side.
        /// </summary>
        /// <param name="Contact">The Object that it collided with.</param>
        /// <param name="Velocity"></param>
        public override void hitLeft(FlxObject Contact, float Velocity)
        {
            base.hitLeft(Contact, Velocity);
        }

        /// <summary>
        /// Called when the sprite hits something on its right side
        /// </summary>
        /// <param name="Contact"></param>
        /// <param name="Velocity"></param>
        public override void hitRight(FlxObject Contact, float Velocity)
        {
            base.hitRight(Contact, Velocity);
        }

        /// <summary>
        /// Called when the sprite hits something on its side, either left or right.
        /// </summary>
        /// <param name="Contact"></param>
        /// <param name="Velocity"></param>
        public override void hitSide(FlxObject Contact, float Velocity)
        {
            base.hitSide(Contact, Velocity);
        }

        /// <summary>
        /// Called when the sprite hits something on its top
        /// </summary>
        /// <param name="Contact"></param>
        /// <param name="Velocity"></param>
        public override void hitTop(FlxObject Contact, float Velocity)
        {
            base.hitTop(Contact, Velocity);
        }

        /// <summary>
        /// Used when the sprite is damaged or hurt. Takes points off "Health".
        /// </summary>
        /// <param name="Damage">Amount of damage to take away.</param>
        public override void hurt(float Damage)
        {
            base.hurt(Damage);
        }

        /// <summary>
        /// Kill the sprite.
        /// </summary>
        public override void kill()
        {
            base.kill();
        }
    }
}
