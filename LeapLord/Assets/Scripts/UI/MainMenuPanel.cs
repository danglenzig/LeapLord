using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace LeapLord
{
    public class MainMenuPanel : MonoBehaviour
    {




        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private TMP_Text startButtonText;

        public static event System.Action OnStartButtonPressed;
        public static event System.Action OnQuitPressed;

        private void Awake()
        {
            PlayerManager.OnIsNewChanged += (_newValue) =>
            {
                if (_newValue)
                {
                    startButtonText.text = "Start";
                }
                else
                {
                    startButtonText.text = "Continue";
                }
            };
        }

        public void HandleOnStartButtonPressed()
        {
            OnStartButtonPressed?.Invoke();
        }

        public void HandleQuitPressed()
        {
            OnQuitPressed?.Invoke();
        }

        public void ResetButtons()
        {

            if (startButton == null) { return; }
            if (quitButton == null) { return; }

            ButtonScript startBS = startButton.GetComponent<ButtonScript>();
            ButtonScript quitBS = quitButton.GetComponent<ButtonScript>();

            startBS.ResetButtonText();
            quitBS.ResetButtonText();
        }

        

    }
}


