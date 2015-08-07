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
        public bool hasEnteredWater;
        private float deepestPointOfDive;

        private FlxText debugText;

        /// <summary>
        /// Sprite Constructor
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public Diver(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("diver_05", true, false, 128, 128);

            width = 48;
            height = 48;

            setOffset(32 + 8, 32 + 16);

            addAnimation("idle", new int[] { 81,82 }, 4, true);

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

            addAnimation("glide", this.generateFrameNumbersBetween(31, 52), 16, true);
            addAnimation("swim", this.generateFrameNumbersBetween(44, 64), 16, true);


            addAnimation("exitWater", this.generateFrameNumbersBetween(65, 72), 16, false);

            addAnimation("fall", new int[] { 7,26 }, 24, true);


            addAnimation("breathe", new int[] { 83,84 }, 2, true);

            addAnimation("hitFloor", new int[] { 85,86,86,86,87 }, 16, false);

            addAnimationCallback(animCallback);

            //addAnimationsFromGraphicsGaleCSV("content/_.csv");

            play("idle");

            this.maxVelocity = new Vector2(350, 600);

            this.velocity.X = 0;
            acceleration.Y = 980;

            mode = "idle";

            hasEnteredWater = false;

            //FlxG.showBounds = true;

            deepestPointOfDive = 0;

            debugText = new FlxText(xPos, yPos, 100);
            debugText.setScrollFactors(1, 1);
            debugText.alignment = FlxJustification.Left;

            

        }

        public void check()
        {
            //Console.WriteLine(_curFrame);

            if (_curFrame >= 6 && _curAnim.name == "dive")
            {
                Globals.addScore(100, "Perfect Entry");
            }
            else
            {
                FlxG.quake.start(0.02f, 0.5f);

                flicker(0.5f);

                Globals.addScore(-1000, "Failed Entry");
            }

        }

        public void animCallback(string Name, uint Frame, int FrameIndex)
        {
            //Console.WriteLine(Name);

            if (Name == "exitWater" && FrameIndex == 72)
            {
                //x -= 16;
                //y -= 16;

                //x = 900 - (width/1.5f) ;

                play("breathe");
                mode = "breathe";
                this.velocity.X = 0;
                //acceleration.Y = 980;

                float deepBonus = 1.0f / (float)((float)(Globals.diveHeight + Globals.poolDepth) - (float)deepestPointOfDive);
                deepBonus *= 10000;

                //Console.WriteLine("Deep Bonus {0}  --------->{1} {2}", deepBonus, Globals.diveHeight, deepestPointOfDive);

                Globals.addScore((int)deepBonus, "Deep Dive Bonus");
                Globals.addScore(10, "Dive Complete");
            }
        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            //Console.WriteLine("Mode: " + mode);

            debugText.x = x + 50;
            debugText.y = y;
            debugText.text = mode + "\nacc.x: " + acceleration.X + "\nvel.x: " + velocity.X + "\ndrag.x: " + drag.X;


            // check deepest point
            if (y > deepestPointOfDive)
            {
                deepestPointOfDive = y;
            }


            if (_curFrame == 11 && _curAnim.name == "swan")
            {
                Globals.addScore(5, "Swan Bonus");
            }

            if (mode == "dead")
            {
                acceleration.X = 0;
                acceleration.Y = 0;
                velocity.X = 0;
                velocity.Y = 0;
                acceleration.Y = 980;

            }
            else if (mode == "deadFloat")
            {
                acceleration.Y = 980;
            }

            if (FlxControl.ACTIONJUSTPRESSED && mode == "run" && this.onFloor)
            {
                play("swan");
                velocity.Y = -300;
                mode = "swan";

                setDrags(155, 155);

                //Console.WriteLine("Jumped! at {0} - dive Point {1} - distance {2}", x, Globals.jumpPoint, x - Globals.jumpPoint);
                Globals.addScore((int)(x - Globals.jumpPoint), "Jump Danger Bonus");
            }
            else if (mode == "idle" && this.velocity.Y > 0)
            {
                play("fall");
                mode = "fall";
            }


            else if (FlxControl.ACTIONJUSTPRESSED && mode == "swan")
            {
                play("dive");
                mode = "dive";

            }
            else if (FlxControl.ACTIONJUSTPRESSED && mode == "dive" && hasEnteredWater==true)
            {
                setDrags(5500, 5500);
                acceleration.Y = 0;

                play("enterWater");
                mode = "enterWater";

            }
            
            if (_curAnim.name == "enterWater" && _curFrame==3)
            {
                mode = "swim";
                play("swim");

                acceleration.X = 0;
                //acceleration.X = 0;
            }

            if (mode == "swim" && velocity.Y == 0)
            {
                setDrags(4500, 4500);
                acceleration.Y = -125;
            }

            if (mode == "swim")
            {
                if (velocity.X < -12)
                {
                    setDrags(333, 333);
                }
                else
                {
                    setDrags(125, 125);
                    velocity.X = -50;

                }

                if (FlxControl.ACTIONJUSTPRESSED)
                {
                    //acceleration.X /= 5;

                    ////velocity.X /= 2;

                    //if (acceleration.X > 0)
                    //{
                    //    acceleration.X = 0;
                    //}

                    if (y <= Globals.diveHeight - height / 2)
                    {
                        velocity.X -= 250;

                        setDrags(25, 25);
                    }
                }
            }

            if (FlxControl.ACTIONJUSTPRESSED && (mode == "idle" ))
            {
                mode = "run";

                acceleration.X -= 146;
                animation();
            }
            if (mode == "run")
            {
                animation();
            }

            //if (FlxControl.ACTIONJUSTPRESSED && (mode == "idle" || mode == "swim"))
            //{
            //    velocity.X -= 46;
            //    animation();
            //}
            //if (FlxControl.ACTIONJUSTPRESSED && (mode == "idle" || mode == "swim"))
            //{
            //    velocity.X += 46;
            //    animation();
            //}

            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Left;
            }
            else if (velocity.X < 0)
            {
                facing = Flx2DFacing.Right;
            }

            base.update();


        }

        public void animation()
        {
            if (this.velocity.X == 0) play("idle");
            else if (mode == "swim") play("swim");
            //else if (Math.Abs(this.velocity.X) < 75 * 4) play("run4");
            else if (Math.Abs(this.velocity.X) < 150 * 4) play("run8");
            else if (Math.Abs(this.velocity.X) < 225 * 4) play("run16");
            else if (Math.Abs(this.velocity.X) < 300 * 4) play("run24");
            else if (Math.Abs(this.velocity.X) < 375 * 4) play("run32");
        }

        /// <summary>
        /// The render code. Add any additional render code here.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void render(SpriteBatch spriteBatch)
        {
            debugText.render(spriteBatch);

            base.render(spriteBatch);
        }

        /// <summary>
        /// Called when the sprite hit something on its bottom edge.
        /// </summary>
        /// <param name="Contact">The Object that it collided with.</param>
        /// <param name="Velocity">The Velocity that is will now have???</param>
        public override void hitBottom(FlxObject Contact, float Velocity)
        {

            if (mode == "swan" || mode == "dive" || mode == "enterWater" || mode == "swim")
            {
                play("hitFloor");
                mode = "dead";
                Globals.addScore(-1000000, "Hit head");
                dead = true;
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
            Console.WriteLine("HITLEFT - Score: " + FlxG.score.ToString() + " " + Velocity);

            if (y <= Globals.diveHeight - height / 2)
            {
                play("exitWater");
                mode = "exitWater";

                acceleration.X = 0;
                acceleration.Y = 0;
                velocity.X = 0;
                velocity.Y = 0;
                setDrags(25, 25);
            }
            else
            {
                x += 3;

                acceleration.X = 0;
                acceleration.Y = 0;
                velocity.X = 0;
                velocity.Y = 0;
                setDrags(25, 25);

                mode = "deadFloat";
                play("hitFloor");

                Globals.addScore(-1000000, "Hit head on side");
            }

            dead = true;

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
