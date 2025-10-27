using UnityEngine;
namespace LeapLord
{
    public class CameraTrigger : MonoBehaviour
    {
        public static event System.Action<int> OnTriggered;

        [SerializeField] int roomID;

        private bool playerInside = false;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.PLAYER_SINGLETON)) return;
            if (playerInside) return;
            playerInside = true;
            OnTriggered.Invoke(roomID);
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.PLAYER_SINGLETON)) return;
            if (!playerInside) return;
            playerInside = false;
        }


    }
}


