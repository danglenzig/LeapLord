using UnityEngine;
using System.Collections.Generic;

namespace LeapLord
{
    public class NarrationLine
    {
        private int lineID;
        private string portraitTexturePath;
        private string lineText;

        public int LineID { get => lineID; }
        public string PortraitTexturePath { get => portraitTexturePath; }
        public string LineText { get => lineText; }

        public NarrationLine(int _lineID, string _portraitPath, string _lineText)
        {
            lineID = _lineID;
            portraitTexturePath = _portraitPath;
            lineText = _lineText;
        }
    }
    public class Narration
    {
        private string narrationName;
        private List<NarrationLine> lines;

        public string NarrationName { get => narrationName; }
        public List<NarrationLine> Lines { get => lines; }

        public Narration(string _narrationName, List<NarrationLine> _lines)
        {
            narrationName = _narrationName;
            lines = _lines;
        }

    }

    public class NarrationData
    {

        //public static List<NarrationLine> OpeningNarration = new List<NarrationLine>();
        //public static List<NarrationLine> ClosingNarration = new List<NarrationLine>();

        public static Narration OpeningNarration;
        public static Narration ClosingNarration;


        public static void BuildNarrations()
        {
            NarrationLine opening00 = new NarrationLine
                (
                    0,
                    "NarrationPortraits/Blank",
                    "Hello world. I am the first line of the opening narration"
                );
            NarrationLine opening01 = new NarrationLine
                (
                    1,
                    "NarrationPortraits/Leo_03",
                    "Hello world. I am the second line of opening narration"
                );
            List<NarrationLine> openingLines = new List<NarrationLine>();
            openingLines.Add(opening00);
            openingLines.Add(opening01);
            OpeningNarration = new Narration(NarrationNames.OPENING_NARRATION, openingLines);


            NarrationLine closing00 = new NarrationLine
                (
                    0,
                    "NarrationPortraits/Blank",
                    "Hello world. I am the first line of the closing narration"
                );
            NarrationLine closing01 = new NarrationLine
                (
                    1,
                    "NarrationPortraits/Leo_03",
                    "Hello world. I am the second line of closing narration"
                );
            List<NarrationLine> closingLines = new List<NarrationLine>();
            closingLines.Add(closing00);
            closingLines.Add(closing01);
            ClosingNarration = new Narration(NarrationNames.CLOSING_NARRATION, closingLines);

        }




        /*
        public static List<NarrationLine> BuildAndReturnNarrationLines()
        // the UI manager will grab this list, and put the appropriate 
        // narration text and portrait texture on the canvas based on
        // game events.
        {

            List<NarrationLine> lines = new List<NarrationLine>();

            NarrationLine line00 = new NarrationLine
                (
                    0,
                    "NarrationPortraits/Blank",
                    "Hello world. I am the first line of narration"
                );

            NarrationLine line01 = new NarrationLine
                (
                    1,
                    "NarrationPortraits/Leo_03",
                    "Hello world. I am the second line of narration"
                );

            lines.Add(line00);
            lines.Add(line01);

            return lines;
        }
        */
    }
        
}