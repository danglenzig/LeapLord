using UnityEngine;
namespace LeapLord
{
    public class PlayerManager : MonoBehaviour
    {

        private const float POLLING_INTERVAL = 0.5f;

        private Player player;

        private float highestPlayerY = 0.0f;
        private float timeAccumulator = 0.0f;

        private int playableGems = 0;
        public int PlayableGems
        {
            get => playableGems;
            set
            {
                if (value < 0) { playableGems = 0; }
                else { playableGems = value; }
            }
        }


        private void Awake()
        {
            if (GameObject.FindGameObjectsWithTag(Tags.PLAYER_MANAGER_SINGLETON).Length > 0)
            {
                Destroy(this);
            }
            tag = Tags.PLAYER_MANAGER_SINGLETON;
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            player = GetPlayer();
        }
        void Update()
        {
            timeAccumulator += Time.deltaTime;
            if (timeAccumulator < POLLING_INTERVAL) { return; }
            timeAccumulator = 0.0f;

            if (player == null) { player = GetPlayer(); }
            if (player == null) { return; }
            if (player.transform.position.y > highestPlayerY)
            {
                highestPlayerY = player.transform.position.y;
                Debug.Log(highestPlayerY);
            }
        }

        private Player? GetPlayer()
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag(Tags.PLAYER_SINGLETON);
            if (playerGO.GetComponent<Player>() != null)
            {
                return playerGO.GetComponent<Player>();
            }
            return null;
        }
    }
}

