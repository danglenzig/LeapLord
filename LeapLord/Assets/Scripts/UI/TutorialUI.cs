using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace LeapLord
{
    public class TutorialUI : MonoBehaviour
    {
        public static event System.Action OnTutorialClosed;
        public static event System.Action OnMainPressed;

        [SerializeField] private RawImage slideImage;
        [SerializeField] private TMP_Text slideText;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button leftButton;

        private List<TutorialSlide> slides;
        private int currentSlideIdx = -1;

        public void StartTutorial()
        {
            if (slides != TutorialData.TutorialSlides)
            {
                slides = TutorialData.TutorialSlides;
            }
            currentSlideIdx = 0;
            HandleCurrentSlide();
        }

        private void HandleCurrentSlide()
        {
            leftButton.gameObject.SetActive(currentSlideIdx != 0);
            rightButton.gameObject.SetActive(currentSlideIdx != slides.Count - 1);
            TutorialSlide slide = slides[currentSlideIdx];
            slideImage.texture = Resources.Load<Texture>(slide.SlideImageResourcePath);
            slideText.text = slide.SlideText;
        }

        public void ResetTutorial()
        {
            currentSlideIdx = -1;
            slideText.text = "";
        }

        public void HandleOnLeftArrowPressed()
        {
            if (currentSlideIdx - 1 < 0)
            {
                currentSlideIdx = slides.Count - 1;
                HandleCurrentSlide();
            }
            else
            {
                currentSlideIdx -= 1;
                HandleCurrentSlide();
            }
        }

        public void HandleOnRightArrowPressed()
        {
            currentSlideIdx = (currentSlideIdx + 1) % slides.Count;
            HandleCurrentSlide();
        }

        public void HandleOnCloseButtonPressed()
        {
            OnTutorialClosed?.Invoke();
        }

        public void HandleOnMainPressed()
        {
            OnMainPressed?.Invoke();
        }


    }
}

