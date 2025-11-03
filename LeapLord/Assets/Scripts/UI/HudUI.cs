using UnityEngine;
using TMPro;
namespace LeapLord
{
    public class HudUI : MonoBehaviour
    {

        private const float DEFAULT_FONT_SIZE = 40.0f;
        private const float FLASH_FONT_SIZE = 60.0f;
        private const float FLASH_DURATION = 0.1f;
        private const int FLASH_REPEAT = 4;

        public static event System.Action OnMainPressed;
        public static event System.Action OnPausePressed;
        public static event System.Action OnQuitPressed;


        [SerializeField] private TMP_Text gemsUIText;

        private bool isFlashing = false;


        private void Awake()
        {
            PlayerManager.GemInventoryChanged += UpdateGemsUI;
            PlayerManager.OnTriedToDropAGemButDontGotNone += HandleGotNoGems;
        }

        private void Start()
        {
            UpdateGemsUI();
        }

        public void HandleMainMenuPressed()
        {
            OnMainPressed?.Invoke();
        }

        public void HandlePausePressed()
        {
            OnMainPressed?.Invoke();
        }

        public void HandleQuitPressed()
        {
            OnQuitPressed?.Invoke();
        }

        private void UpdateGemsUI()
        {
            int _gems = PlayerManager.Gems;
            if (_gems >= PlayerManager.MAX_GEMS)
            {
                gemsUIText.text = ($"Gems: {PlayerManager.Gems} (Full) ");
            }
            else
            {
                gemsUIText.text = ($"Gems: {PlayerManager.Gems}");
            }

            
            
        }

        private void HandleGotNoGems()
        {
            Debug.Log("You got no gems");
            if (isFlashing) { return; }
            isFlashing = true;
            StartCoroutine(FlashGemsText());

        }

        private System.Collections.IEnumerator FlashGemsText()
        {
            for (int i = 0; i < FLASH_REPEAT; i++)
            {

                gemsUIText.fontSize = FLASH_FONT_SIZE;
                yield return new WaitForSeconds(FLASH_DURATION);
                gemsUIText.fontSize = DEFAULT_FONT_SIZE;
                yield return new WaitForSeconds(FLASH_DURATION);

            }
            isFlashing = false;
        }


        // highest Y
        // Gems collected PlayerManager.MAX_GEMS
        // Main button
        // Pause button
        // quit button
    }
}


