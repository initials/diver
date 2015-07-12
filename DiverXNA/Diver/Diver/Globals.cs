using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;

namespace Diver
{
    public class Globals
    {
        // ---------- Set these to build level -------------
        public static int diveHeight = 1000; 
        public static int poolWidth = 1500;
        public static int poolDepth = 700;

        // ----------- calculate based on level

        public static int jumpPoint;


        public Globals()
        {

        }

        public static void addScore(int Score, string Message)
        {
            FlxG.score += Score;

            if (FlxG.score < 0)
                FlxG.score = 0;

            string msg = string.Format("{0} Your score is now: {1}", Message, FlxG.score);

            Console.WriteLine(msg);
            FlxG.log(msg);

        }


    }
}
