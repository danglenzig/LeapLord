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
            mainMenuPanel.gameObject.SetActive(false);
            hudPanel.gameObject.SetActive(true);
            SceneManager.LoadScene(SceneNames.LAB);
        }

        public void OnAboutButtonPressed()
        {
            //
        }

        public void OnQuitButtonPressed()
        {
            //
        }




    }
}


