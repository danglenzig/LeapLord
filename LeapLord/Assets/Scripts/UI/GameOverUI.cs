using UnityEngine;
namespace LeapLord
{
    public class GameOverUI : MonoBehaviour
    {
        public static event System.Action OnGameOverQuitPressed;


        public void HandleButtonPress()
        {
            OnGameOverQuitPressed?.Invoke();
        }

    }

}
