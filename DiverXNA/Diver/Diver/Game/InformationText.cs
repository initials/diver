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
    class InformationText : FlxText
    {
        private float counter;

        public InformationText(int xPos, int yPos, int Width)
            : base(xPos, yPos, Width)
        {
            counter = 0;
        }

        override public void update()
        {
            base.update();
        }

        public void displayScoreHistory()
        {

            visible = true;
            //text = Globals.scoreHistory[Convert.ToInt32(counter)].Item2 + ": " + Globals.scoreHistory[Convert.ToInt32(counter)].Item1.ToString();

            //if (counter % 1.0f == 0.0f)
            //{
            //    text = "";
            //}

            //if (counter < Globals.scoreDict.Count - 1)
            //{
            //    counter+=0.025f;
            //}
            //else
            //{
            //    text = "Total Score: " + FlxG.score;

            //}

            text = "";

            foreach (KeyValuePair<string, int> entry in Globals.scoreDict)
            {
                // do something with entry.Value or entry.Key
                text += entry.Key + ": " + entry.Value + "\n";



            }

            text += "Final Score: " + FlxG.score;



        }

    }
}
