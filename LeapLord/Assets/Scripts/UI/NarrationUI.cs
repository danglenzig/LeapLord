using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace LeapLord
{
    public class NarrationUI : MonoBehaviour
    {

        [SerializeField] private RawImage portraitImage;
        [SerializeField] private TMP_Text narrationText;
        void Start()
        {
            
            //StartCoroutine(WaitThenTest());
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


