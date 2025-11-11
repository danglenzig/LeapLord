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
        //private bool useBackground = false;
        //public bool UseBackground { get => useBackground; }

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

        public static Narration OpeningNarration;
        public static Narration ClosingNarration;


        public static void BuildNarrations()
        {
            ///////////////////////
            // OPENING NARRATION //
            ///////////////////////
            List<NarrationLine> openingLines = new List<NarrationLine>();
            NarrationLine opening00 = new NarrationLine
                (
                    0,
                    "NarrationPortraits/Blank",
                    "Once upon a time, there was a mighty and benevolent king—well, mighty anyway—who, through sheer royal negligence, misplaced his crown."
                ); openingLines.Add(opening00);

            NarrationLine opening01 = new NarrationLine
                (
                    1,
                    "NarrationPortraits/Leo_03",
                    "KING: Alas! Our crown is gone! The very symbol of our sovereign power and boundless virility! How can we face our filthy peasantry bare-headed?"
                ); openingLines.Add(opening01);

            NarrationLine opening02 = new NarrationLine
                (
                    2,
                    "NarrationPortraits/Leo_02",
                    "You there! Servant! Have you seen the royal crown?"
                ); openingLines.Add(opening02);

            NarrationLine opening03 = new NarrationLine
                (
                    3,
                    "NarrationPortraits/Peasant",
                    "SERVANT: Yes, my Lord. You left it by the sink in the royal privy... atop this very tower."
                ); openingLines.Add(opening03);

            NarrationLine opening04 = new NarrationLine
                (
                    4,
                    "NarrationPortraits/Leo_00",
                    "Ah! You have earned the gratitude of your King. We shall take the ROYAL ELEVATOR and reclaim our glory!"
                ); openingLines.Add(opening04);

            NarrationLine opening05 = new NarrationLine
                (
                    5,
                    "NarrationPortraits/Peasant",
                    "Er—unfortunately, Your Majesty, the Royal Elevator is... temporarily out of order. The repairman’s scheduled to arrive in two days. Give or take a fortnight."
                ); openingLines.Add(opening05);

            NarrationLine opening06 = new NarrationLine
                (
                    6,
                    "NarrationPortraits/Leo_00",
                    "Then we shall take the ROYAL STAIRS!"
                ); openingLines.Add(opening06);

            NarrationLine opening07 = new NarrationLine
                (
                    7,
                    "NarrationPortraits/Peasant",
                    "Humbly, my Lord, you had the Royal Stairs removed after the Royal Elevator was installed."
                ); openingLines.Add(opening07);

            NarrationLine opening08 = new NarrationLine
                (
                    8,
                    "NarrationPortraits/Leo_02",
                    "Removed!? Why in the realm would we do such a foolish thing?"
                ); openingLines.Add(opening08);

            NarrationLine opening09 = new NarrationLine
                (
                    9,
                    "NarrationPortraits/Peasant",
                    "Your Majesty said stairs were a “legacy technology”, no longer worth maintaining. Like when you closed the scriptorium after we got that printing press..."
                ); openingLines.Add(opening09);

            NarrationLine opening10 = new NarrationLine
                (
                    10,
                    "NarrationPortraits/Peasant",
                    "...and dismissed all the programmers after we got the Royal AI."
                ); openingLines.Add(opening10);

            NarrationLine opening11 = new NarrationLine
                (
                   11,
                    "NarrationPortraits/Leo_01",
                    "..."
                ); openingLines.Add(opening11);

            NarrationLine opening12 = new NarrationLine
                (
                    12,
                    "NarrationPortraits/Peasant",
                    "Perhaps Your Majesty could... leap to the top of the tower?"
                ); openingLines.Add(opening12);

            NarrationLine opening13 = new NarrationLine
                (
                    13,
                    "NarrationPortraits/Leo_00",
                    "Leap? Hah! A royal leap it shall be! Sound the trumpets! Fetch the...wait, how exactly does that work?"
                ); openingLines.Add(opening13);

            OpeningNarration = new Narration(NarrationNames.OPENING_NARRATION, openingLines);


            ///////////////////////
            // CLOSING NARRATION //
            ///////////////////////
            NarrationLine closing00 = new NarrationLine
                (
                    0,
                    "NarrationPortraits/Blank",
                    "And so, after an afternoon of furious leaping, undignified falling, and inexplicable teleporting, His Humble Majesty at last reach the top of the tower."
                );
            NarrationLine closing01 = new NarrationLine
                (
                    1,
                    "NarrationPortraits/Leo_04",
                    "Behold! Our crown! The sacred symbol of divine authority and virile majesty!"
                );

            NarrationLine closing02 = new NarrationLine
                (
                    2,
                    "NarrationPortraits/Leo_04",
                    "Once again, the realm shall tremble before our shining manliness!"
                );

            List<NarrationLine> closingLines = new List<NarrationLine>();
            closingLines.Add(closing00);
            closingLines.Add(closing01);
            closingLines.Add(closing02);
            ClosingNarration = new Narration(NarrationNames.CLOSING_NARRATION, closingLines);

        }



    }
        
}