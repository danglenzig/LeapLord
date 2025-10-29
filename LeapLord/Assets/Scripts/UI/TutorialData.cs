using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{

    public class TutorialSlide
    {
        private int slideID;
        private string slideImageResourcePath;
        private string slideText;

        public int SlideID { get => slideID; }
        public string SlideImageResourcePath { get => slideImageResourcePath; }
        public string SlideText { get => slideText; }

        public TutorialSlide(int _slideID, string _slideImageResourcePath, string _slideText)
        {
            slideID = _slideID;
            slideImageResourcePath = _slideImageResourcePath;
            slideText = _slideText;
        }
    }

    public class TutorialData
    {
        public static List<TutorialSlide> TutorialSlides = new List<TutorialSlide>();

        public static void BuildTutorial()
        {
            TutorialSlide slide00 = new TutorialSlide
                (
                    0,
                    "TutorialImages/TutorialWalk",
                    "Press A/D or Left/Right Arrow keys to walk  left & right."
                );

            TutorialSlide slide01 = new TutorialSlide
                (
                    1,
                    "TutorialImages/TutorialWalk", // placeholder
                    "Press SPACE to begin charging your leap."
                );

            TutorialSlide slide02 = new TutorialSlide
                (
                    2,
                    "TutorialImages/TutorialWalk", // placeholder
                    "Release SPACE to unleash your leap."
                );

            TutorialSlide slide03 = new TutorialSlide
                (
                    3,
                    "TutorialImages/TutorialWalk", // placeholder
                    "The longer you charge your leap, the higher and further it will be."
                );

            TutorialSlide slide04 = new TutorialSlide
                (
                    4,
                    "TutorialImages/TutorialWalk", // placeholder
                    "Collect teleportation gems. Use them to create checkpoints."
                );

            TutorialSlide slide05 = new TutorialSlide
                (
                    5,
                    "TutorialImages/TutorialWalk", // placeholder
                    "Press E to drop a collected gem and create a checkpoint."
                );

            TutorialSlide slide06 = new TutorialSlide
                (
                    6,
                    "TutorialImages/TutorialWalk", // placeholder
                    "Press Q to teleport to your last checkpoint. This is useful of you fall."
                );

            TutorialSlide slide07 = new TutorialSlide
                (
                    7,
                    "TutorialImages/TutorialWalk", // placeholder
                    "Press ESC at any time to pause the game and review this tutorial."
                );

            TutorialSlide slide08 = new TutorialSlide
                (
                    8,
                    "TutorialImages/TutorialWalk", // placeholder
                    "Godspeed, Your Majesty!"
                );

            TutorialSlides.Add(slide00);
            TutorialSlides.Add(slide01);
            TutorialSlides.Add(slide02);
            TutorialSlides.Add(slide03);
            TutorialSlides.Add(slide04);
            TutorialSlides.Add(slide05);
            TutorialSlides.Add(slide06);
            TutorialSlides.Add(slide07);
            TutorialSlides.Add(slide08);
        }
    }
}

