﻿using System;
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

        public static List<Tuple<int, string>> scoreHistory = new List<Tuple<int, string>>();

        public static Dictionary<string, int> scoreDict = new Dictionary<string, int>();



        public Globals()
        {
            scoreHistory = new List<Tuple<int, string>>();

            scoreDict = new Dictionary<string, int>();
        }

        public static void purgeScoreHistory()
        {
            scoreHistory = new List<Tuple<int, string>>();

            scoreDict = new Dictionary<string, int>();
        }

        public static void addScore(int Score, string Message)
        {
            FlxG.score += Score;

            if (FlxG.score < 0)
                FlxG.score = 0;

            string msg = string.Format("{0} Your score is now: {1}", Message, FlxG.score);

            Console.WriteLine(msg);
            FlxG.log(msg);

            scoreHistory.Add(new Tuple<int, string>(Score, Message));

            if (scoreDict.ContainsKey(Message))
            {

                int existingScore = scoreDict[Message];
                int newScore = existingScore + Score;

                scoreDict[Message] = newScore ;
            }
            else
            {
                scoreDict[Message] = Score;

            }


        }


    }
}
