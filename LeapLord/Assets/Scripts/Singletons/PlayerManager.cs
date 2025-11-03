#nullable enable
using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{
    public class PlayerManager : MonoBehaviour
    {
        public static event System.Action<bool>? OnIsNewChanged;
        public static event System.Action? GemInventoryChanged;
        public static event System.Action? OnTriedToDropAGemButDontGotNone;

        private const float POLLING_INTERVAL = 0.5f;
        public const int MAX_GEMS = 3;


        [SerializeField] private GameObject? checkpointPrefab;
        private static GameObject? staticCheckpointPrefab;


        private static float highestPlayerY = 0.0f;
        private static Vector3 lastCheckpointPos = Vector3.zero;
        
        private static int checkpointsDropped = 0;
        private static int gems = 0;
        public static int Gems { get => gems; }

        private static Player? player;
        private float timeAccumulator = 0.0f;

        private static bool isNew = true;
        public static bool IsNew
        {
            get => isNew;
            set
            {
                if (value != isNew)
                {
                    isNew = value;
                    OnIsNewChanged?.Invoke(isNew);
                }
            }
        }

        private static Vector3 lastPlayerPosition = new Vector3();
        public static Vector3 LastPlayerPosition
        {
            get => lastPlayerPosition;
            set { lastPlayerPosition = value; }
        }

        private void Awake()
        {
            if (GameObject.FindGameObjectsWithTag(Tags.PLAYER_MANAGER_SINGLETON).Length > 0)
            {
                Destroy(gameObject);
                return;
            }
            tag = Tags.PLAYER_MANAGER_SINGLETON;
            DontDestroyOnLoad(gameObject);

            InputHandler.OnDropGemPressed += DropGem;
            InputHandler.OnTeleportPressed += TeleportToCheckpoint;
            CheckpointGem.OnGemCollected += CollectGem;
            Player.AppearedOnContinue += HandleAppearedOnContinue;

        }

        void Start()
        {
            staticCheckpointPrefab = checkpointPrefab;
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
            }
        }

        private static Player? GetPlayer()
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag(Tags.PLAYER_SINGLETON);
            if (playerGO == null) { return null; }
            if (playerGO.GetComponent<Player>() != null)
            {
                return playerGO.GetComponent<Player>();
            }
            return null;
        }

        private void CollectGem(string _uuid)
        {
            if (gems + 1 > MAX_GEMS) { return; }
            gems += 1;
            GemInventoryChanged?.Invoke();
        }

        private static void DropGem()
        {
            if (player == null) { player = GetPlayer(); }
            if (player == null) { return; }

            if (gems <= 0)
            {
                //Debug.Log("You got no gems :(");
                OnTriedToDropAGemButDontGotNone?.Invoke();
                return;
            }

            gems -= 1;
            GemInventoryChanged?.Invoke();
            checkpointsDropped += 1;
            lastCheckpointPos = player.transform.position;

            GameObject[] checkpointMarkers = GameObject.FindGameObjectsWithTag(Tags.CHECKPOINT);
            foreach (GameObject cm in checkpointMarkers)
            {
                cm.gameObject.SetActive(false);
            }

            GameObject? newCheckpoint = Instantiate(staticCheckpointPrefab);
            newCheckpoint.tag = Tags.CHECKPOINT;
            newCheckpoint.transform.position = player.transform.position;
            newCheckpoint.gameObject.SetActive(true);
        }



        public static void TeleportToCheckpoint()
        {
            if (player == null) { player = GetPlayer(); }
            if (player == null) { return; }

            if (checkpointsDropped <= 0)
            {
                Debug.Log("No checkpoints dropped");
                return;
            }

            player.TeleportToPosition(lastCheckpointPos);

        }

        private void HandleAppearedOnContinue()
        {
            if (checkpointsDropped <= 0) { return; }

            GameObject[] checkpointMarkers = GameObject.FindGameObjectsWithTag(Tags.CHECKPOINT);
            foreach (GameObject cm in checkpointMarkers)
            {
                cm.gameObject.SetActive(false);
            }
            GameObject? newCheckpoint = Instantiate(staticCheckpointPrefab);
            newCheckpoint.tag = Tags.CHECKPOINT;
            newCheckpoint.transform.position = lastCheckpointPos;
            newCheckpoint.gameObject.SetActive(true);


        }
    }
}

