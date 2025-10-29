using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace LeapLord
{
    public class NarrationUI : MonoBehaviour
    {

        public static event System.Action<string> OnNarrationFinished;


        const float TEXT_REVEAL_INTERVAL = 0.005f;


        [SerializeField] private RawImage portraitImage;
        [SerializeField] private TMP_Text narrationText;
        [SerializeField] private Button continueButton;

        private Narration? currentNarration = null;
        private int currentNarrationIndex = -1;

        void Start()
        {
            continueButton.gameObject.SetActive(false);
        }


        public void StartNarration(Narration narration)
        {
            currentNarration = narration;
            ContinueNarration();
        }


        public void ContinueNarration()
        {
            if (currentNarration == null) { return; }
            if (currentNarrationIndex + 1 >= currentNarration.Lines.Count)
            {
                // end the narration
                OnNarrationFinished?.Invoke(currentNarration.NarrationName);
                currentNarration = null;
                currentNarrationIndex = -1;
                narrationText.text = "";
                portraitImage.texture = Resources.Load<Texture>("NarrationPortraits/Blank");
                
            }
            else
            {
                // continue the narration
                continueButton.gameObject.SetActive(false);
                currentNarrationIndex += 1;
                NarrationLine thisLine = currentNarration.Lines[currentNarrationIndex];
                string thisLineText = thisLine.LineText;
                string thisPortraitPath = thisLine.PortraitTexturePath;
                portraitImage.texture = Resources.Load<Texture>(thisPortraitPath);
                StartCoroutine(RevealText(thisLineText));

            }
        }

        private System.Collections.IEnumerator RevealText(string _text)
        {
            narrationText.text = "";
            foreach (char character in _text)
            {
                narrationText.text += character;
                yield return new WaitForSeconds(TEXT_REVEAL_INTERVAL);
            }
            continueButton.gameObject.SetActive(true);
        }


        /*
        private System.Collections.IEnumerator WaitThenTest()
        {
            yield return new WaitForSeconds(1.0f);
            TestNarrationData();
        }

        private void TestNarrationData()
        {
            List<NarrationLine> lines = NarrationData.BuildAndReturnNarrationLines();
            NarrationLine testLine = lines[1];

            string testText = testLine.LineText;
            string testTexturePath = testLine.PortraitTexturePath;

            narrationText.text = testText; // <--- This works fine.
            portraitImage.texture = Resources.Load<Texture>(testTexturePath); // This does not.
        }
        */

    }
}


