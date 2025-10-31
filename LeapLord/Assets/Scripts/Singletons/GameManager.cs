using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace LeapLord
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private GameObject eventSystem;

        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject hudPanel;

        [SerializeField] private Button startButton;
        [SerializeField] private Button aboutButton;
        [SerializeField] private Button quitButton;

        [SerializeField] private NarrationUI narrationUI;
        [SerializeField] private TutorialUI tutorialUI;

        [SerializeField] private bool skipIntro = false;

        private bool _isPaused = false;
        private bool isPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                if (_isPaused)
                {
                    DisplayTutorial();
                }
                else
                {
                    HideTutorial();
                }
            }
        }

        private void Awake()
        {

            NarrationData.BuildNarrations();
            TutorialData.BuildTutorial();

            if (GameObject.FindGameObjectsWithTag(Tags.GAME_MANAGER_SINGLETON).Length > 0)
            {
                Destroy(gameObject);
            }
            tag = Tags.GAME_MANAGER_SINGLETON;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(mainCanvas);
            DontDestroyOnLoad(eventSystem);

            NarrationUI.OnNarrationFinished += HandleOnNarrationFinished;
            InputHandler.OnPausePressed += HandleOnPausePressed;
            Crown.OnCrownGot += HandleOnGameWon;

        }

        private void Start()
        {
            HandleIsNewChanged(PlayerManager.IsNew);
            mainMenuPanel.gameObject.SetActive(true);
        }

        public void OnStartButtonPressed()
        {
            ResetButtons();
            mainMenuPanel.gameObject.SetActive(false);
            narrationUI.gameObject.SetActive(true);

            if (skipIntro == false)
            {
                narrationUI.StartNarration(NarrationData.OpeningNarration);
            }
            else
            {
                HandleOnNarrationFinished(NarrationNames.OPENING_NARRATION);
            }
        }

        private void ResetButtons()
        {
            ButtonScript startBS = startButton.GetComponent<ButtonScript>();
            ButtonScript aboutBS = aboutButton.GetComponent<ButtonScript>();
            ButtonScript quitBS = quitButton.GetComponent<ButtonScript>();

            startBS.ResetButtonText();
            aboutBS.ResetButtonText();
            quitBS.ResetButtonText();
        }

        private void HandleOnGameWon()
        {
            Debug.Log("You win");
        }

        public void OnAboutButtonPressed()
        {
            //
        }

        public void OnQuitButtonPressed()
        {
            //
        }

        private void HandleOnPointerEnterButton()
        {

        }

        private void HandleOnNarrationFinished(string narrationName)
        {
            switch (narrationName)
            {
                case NarrationNames.OPENING_NARRATION:

                    Debug.Log($"{narrationName} complete!");
                    narrationUI.gameObject.SetActive(false);
                    hudPanel.gameObject.SetActive(true);
                    SceneManager.LoadScene(SceneNames.LAB);

                    if (PlayerManager.IsNew)
                    {
                        // play tutorial
                        PlayerManager.IsNew = false;
                        StartCoroutine(WaitASecThenDisplayTutorial());
                    }

                    return;

                case NarrationNames.CLOSING_NARRATION:
                    return;
            }
        }

        private Player? GetPlayer()
        {
            GameObject? playerGO = GameObject.FindGameObjectWithTag(Tags.PLAYER_SINGLETON);
            if (playerGO == null) { return null; }

            Player? _player = playerGO.GetComponent<Player>();
            if (_player != null) { return _player; }

            return null;
        }

        private void HandleOnPausePressed()
        {
            if (GetPlayer() == null) { return; }
            isPaused = !isPaused;
        }

        public void HandleOnCloseTutorialPressed()
        {
            if (GetPlayer() == null) { return; }
            isPaused = false;
        }

        public void HandleOnMainMenuPressed()
        {
            //
        }

        private void HandleIsNewChanged(bool _isNew)
        {
            GameObject startButtonTextGO = startButton.transform.GetChild(0).gameObject;
            TMP_Text startButtonText = startButtonTextGO.GetComponent<TMP_Text>();
            if (_isNew == true)
            {
                startButtonText.text = "Start";
            }
            else
            {
                startButtonText.text = "Continue";
                skipIntro = true;
            }
        }

        private void HideTutorial()
        {

            tutorialUI.ResetTutorial();
            tutorialUI.gameObject.SetActive(false);

            Player? player = GetPlayer();
            if (player != null)
            {
                player.UnPark();
            }
        }

        private void DisplayTutorial()
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.StartTutorial();

            Player? player = GetPlayer();
            if (player != null)
            {
                player.Park();
            }
        }

        private System.Collections.IEnumerator WaitASecThenDisplayTutorial()
        {
            yield return new WaitForSeconds(0.5f);
            isPaused = true;
        }
    }
}


