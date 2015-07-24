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
        private int counter;

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
            text = Globals.scoreHistory[counter].Item2 + ": " + Globals.scoreHistory[counter / 10].Item1.ToString();
            if (counter < Globals.scoreHistory.Count * 10)
            {
                counter++;
            }

        }

    }
}
