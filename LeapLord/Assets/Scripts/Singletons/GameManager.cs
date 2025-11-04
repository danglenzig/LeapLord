#nullable enable
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace LeapLord
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private Canvas? mainCanvas;
        [SerializeField] private GameObject? eventSystem;

        [SerializeField] private GameObject? mainMenuPrefab;
        [SerializeField] private GameObject? hudPrefab;
        [SerializeField] private GameObject? narrationPrefab;
        [SerializeField] private GameObject? tutorialPrefab;

        private MainMenuPanel? mainMenuPanel = null;
        private HudUI? hudPanel = null;
        private NarrationUI? narrationUI = null;
        private TutorialUI? tutorialUI = null;

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
            //Time.timeScale = 0.5f;

            eventSystem?.gameObject.SetActive(false);
            NarrationData.BuildNarrations();
            TutorialData.BuildTutorial();

            if (GameObject.FindGameObjectsWithTag(Tags.EVENT_SYSTEM_SINGLETON).Length > 0) { Destroy(eventSystem?.gameObject); }
            if (GameObject.FindGameObjectsWithTag(Tags.MAIN_CANVAS_SINGLETON).Length > 0) { Destroy(mainCanvas?.gameObject); }
            if (GameObject.FindGameObjectsWithTag(Tags.GAME_MANAGER_SINGLETON).Length > 0)
            {
                Destroy(gameObject);
                return;
            }

            tag = Tags.GAME_MANAGER_SINGLETON;
            mainCanvas.tag = Tags.MAIN_CANVAS_SINGLETON;
            eventSystem.tag = Tags.EVENT_SYSTEM_SINGLETON;

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(mainCanvas.gameObject);
            DontDestroyOnLoad(eventSystem.gameObject);

            GameObject? mmGo = Instantiate(mainMenuPrefab, mainCanvas.transform);
            mainMenuPanel = mmGo?.GetComponent<MainMenuPanel>();

            GameObject? hudGo = Instantiate(hudPrefab, mainCanvas.transform);
            hudPanel = hudGo?.GetComponent<HudUI>();

            GameObject? narrGo = Instantiate(narrationPrefab, mainCanvas.transform);
            narrationUI = narrGo?.GetComponent<NarrationUI>();

            GameObject? tutGo = Instantiate(tutorialPrefab, mainCanvas.transform);
            tutorialUI = tutGo?.GetComponent<TutorialUI>();

            InputHandler.OnPausePressed += HandleOnPausePressed;

            MainMenuPanel.OnStartButtonPressed += HandleOnStartButtonPressed;
            MainMenuPanel.OnQuitPressed += HandleOnQuitButtonPressed;
            NarrationUI.OnNarrationFinished += HandleOnNarrationFinished;
            TutorialUI.OnTutorialClosed += HandleOnCloseTutorialPressed;
            TutorialUI.OnMainPressed += HandleOnMainMenuPressed;
            TutorialUI.OnTutorialEnabledChanged += (_value) =>
            {
                hudPanel.gameObject.SetActive(!_value);
            };
            HudUI.OnMainPressed += HandleOnMainMenuPressed;
            HudUI.OnPausePressed += HandleOnPausePressed;
            HudUI.OnQuitPressed += HandleOnQuitButtonPressed;

            mmGo?.SetActive(false);
            hudGo?.SetActive(false);
            narrGo?.SetActive(false);
            tutGo?.SetActive(false);

        }

        private void Start()
        {
            eventSystem?.gameObject.SetActive(true);
            mainMenuPanel?.gameObject.SetActive(true);
            hudPanel?.gameObject.SetActive(false);
            narrationUI?.gameObject.SetActive(false);
            tutorialUI?.gameObject.SetActive(false);
        }
        
        private void HandleOnStartButtonPressed()
        {

            //Debug.Log("Start button pressed");
            mainMenuPanel?.ResetButtons();
            mainMenuPanel?.gameObject.SetActive(false);
            narrationUI?.gameObject.SetActive(true);

            if (PlayerManager.IsNew)
            {
                if (!skipIntro)
                {
                    narrationUI?.StartNarration(NarrationData.OpeningNarration);
                }
                else
                {
                    HandleOnNarrationFinished(NarrationNames.OPENING_NARRATION);
                }
            }
            else
            {
                HandleOnNarrationFinished(NarrationNames.OPENING_NARRATION);
            }

            
            
        }

        private void HandleOnGameWon()
        {
            Debug.Log("You win");
        }

        public void OnAboutButtonPressed()
        {
            //
        }

        public void HandleOnQuitButtonPressed()
        {
            Debug.Log("FOO");
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        private void HandleOnNarrationFinished(string narrationName)
        {
            
            switch (narrationName)
            {
                case NarrationNames.OPENING_NARRATION:

                    //Debug.Log($"{narrationName} complete!");
                    narrationUI?.gameObject.SetActive(false);
                    hudPanel?.gameObject.SetActive(true);
                    SceneManager.LoadScene(SceneNames.LAB);

                    if (PlayerManager.IsNew)
                    {
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

        public void HandleOnPausePressed()
        {
            if (GetPlayer() == null) { return; }
            isPaused = !isPaused;
        }



        private void HandleOnCloseTutorialPressed()
        {
            if (GetPlayer() == null) { return; }
            isPaused = false;
        }

        private void HandleOnMainMenuPressed()
        {

            narrationUI?.gameObject.SetActive(false);
            hudPanel?.gameObject.SetActive(false);
            tutorialUI?.gameObject.SetActive(false);

            Player? player = GetPlayer();
            if (player != null)
            {
                PlayerManager.LastPlayerPosition = player.transform.position;
            }

            SceneManager.LoadScene(SceneNames.START);
            mainMenuPanel?.gameObject.SetActive(true);
        }

        private void HideTutorial()
        {
            
            tutorialUI?.ResetTutorial();
            tutorialUI?.gameObject.SetActive(false);

            Player? player = GetPlayer();
            if (player != null)
            {
                player.UnPark();
            }
            
        }

        private void DisplayTutorial()
        {

            tutorialUI?.gameObject.SetActive(true);
            tutorialUI?.StartTutorial();

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


