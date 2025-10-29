using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        private void Awake()
        {

            NarrationData.BuildNarrations();

            if (GameObject.FindGameObjectsWithTag(Tags.GAME_MANAGER_SINGLETON).Length > 0)
            {
                Destroy(gameObject);
            }
            tag = Tags.GAME_MANAGER_SINGLETON;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(mainCanvas);
            DontDestroyOnLoad(eventSystem);

            NarrationUI.OnNarrationFinished += HandleOnNarrationFinished;


        }

        private void Start()
        {
            mainMenuPanel.gameObject.SetActive(true);
            //narrationUI.gameObject.SetActive(true);
            //narrationUI.StartNarration(NarrationData.OpeningNarration);
        }

        public void OnStartButtonPressed()
        {
            ResetButtons();
            mainMenuPanel.gameObject.SetActive(false);
            narrationUI.gameObject.SetActive(true);
            narrationUI.StartNarration(NarrationData.OpeningNarration);

            //mainMenuPanel.gameObject.SetActive(false);
            //hudPanel.gameObject.SetActive(true);
            //SceneManager.LoadScene(SceneNames.LAB);
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
            // temp logic
            //narrationUI.gameObject.SetActive(false);

            switch (narrationName)
            {
                case NarrationNames.OPENING_NARRATION:

                    Debug.Log($"{narrationName} complete!");
                    narrationUI.gameObject.SetActive(false);
                    hudPanel.gameObject.SetActive(true);
                    SceneManager.LoadScene(SceneNames.LAB);

                    return;
                case NarrationNames.CLOSING_NARRATION:
                    return;
            }



        }




    }
}


