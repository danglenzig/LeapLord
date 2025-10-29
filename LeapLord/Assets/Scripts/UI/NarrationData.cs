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

    public class NarrationData
    {
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
    }
}