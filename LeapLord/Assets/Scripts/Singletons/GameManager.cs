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

        private void Awake()
        {
            if (GameObject.FindGameObjectsWithTag(Tags.GAME_MANAGER_SINGLETON).Length > 0)
            {
                Destroy(this);
            }
            tag = Tags.GAME_MANAGER_SINGLETON;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(mainCanvas);
            DontDestroyOnLoad(eventSystem);

        }

        private void Start()
        {
            mainMenuPanel.gameObject.SetActive(true);
            
            

        }


        

        public void OnStartButtonPressed()
        {
            ResetButtons();
            mainMenuPanel.gameObject.SetActive(false);
            hudPanel.gameObject.SetActive(true);
            SceneManager.LoadScene(SceneNames.LAB);
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




    }
}


