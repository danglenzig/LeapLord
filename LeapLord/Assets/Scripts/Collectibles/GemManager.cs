using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{

    public class SpawnPoint
    {
        public Transform marker;
        public string gemUUID;
    }

    public class GemManager : MonoBehaviour
    {
        public static event System.Action<string> GMOnGemCollected;


        private const float DAMP = 1.0f;

        [SerializeField] private GameObject gemPrefab;
        [SerializeField] private int numberOfGems = 4;
        [SerializeField] private GameObject vfxPrefab;

        private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        
        public static bool Dampened = false;


        private void Awake()
        {
            Transform[] gemMarkers = GetComponentsInChildren<Transform>();

            for (int i = 0; i < gemMarkers.Length; i++)
            {
                if (gemMarkers[i] != transform)
                {
                    SpawnPoint newSpawnPoint = new SpawnPoint();
                    newSpawnPoint.marker = gemMarkers[i];
                    newSpawnPoint.gemUUID = "";
                    spawnPoints.Add(newSpawnPoint);
                }
            }

            if (numberOfGems > gemMarkers.Length)
            {
                numberOfGems = gemMarkers.Length;
            }

            CheckpointGem.OnGemCollected += HandleOnGemCollected;

        }

        private void Start()
        {
            SpawnGems();
        }

        private void SpawnGems()
        {
            List<SpawnPoint> chosenSpawnPoints = new List<SpawnPoint>();
            List<int> indexes = new List<int>();

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                indexes.Add(i);
            }

            // Do this numberOfGems times...
            for (int i = 0; i < numberOfGems; i++)
            {
                // create an empty eligible list
                List<SpawnPoint> eligibleSpawnPoints = new List<SpawnPoint>();
                
                
                // iterate through the spawnpoints list
                foreach (SpawnPoint sp in spawnPoints)
                {
                    // if this sp is unoccupied, add it to the eligible list
                    if (sp.gemUUID == "") { eligibleSpawnPoints.Add(sp); }
                }

                // pick a random eligible spawn point
                int aRando = Random.Range(0, eligibleSpawnPoints.Count);
                SpawnPoint chosenSP = eligibleSpawnPoints[aRando];

                // instantiate a gem and put it in this spawn point
                GameObject newGemGO = Instantiate(gemPrefab);
                newGemGO.transform.position = chosenSP.marker.transform.position;

                // set the sp's gemUUID, so it doesn't appear in the eligible list next time around
                chosenSP.gemUUID = newGemGO.GetComponent<CheckpointGem>().UUID;
            } 
        }

        private void ReplenishGem(SpawnPoint excludedSpawnPoint)
        {
            List<SpawnPoint> eligibleSpwnPoints = new List<SpawnPoint>();
            foreach (SpawnPoint sp in spawnPoints)
            {
                if(sp.gemUUID == "" && sp != excludedSpawnPoint)
                {
                    eligibleSpwnPoints.Add(sp);
                }
            }
            int aRando = Random.Range(0, eligibleSpwnPoints.Count);

            SpawnPoint chosenSpawnPoint = eligibleSpwnPoints[aRando];

            GameObject gemGO = Instantiate(gemPrefab);
            gemGO.transform.position = chosenSpawnPoint.marker.transform.position;
            chosenSpawnPoint.gemUUID = gemGO.GetComponent<CheckpointGem>().UUID;
        }

        private void HandleOnGemCollected(string uuid, Transform gemTransform)
        {
            foreach(SpawnPoint sp in spawnPoints)
            {
                if (sp.gemUUID == uuid)
                {
                    sp.gemUUID = ""; // this SpawnPoint is now empty
                    ReplenishGem(sp); // put a new one anywhere but here
                    break; // we're done
                }
            }
            GMOnGemCollected?.Invoke(uuid); // The PlayerManager hears this and manages the gem inventory accordingly
            PlayCollectVfxAtPosition(gemTransform.position);
        }

        private void PlayCollectVfxAtPosition(Vector3 pos)
        {
            GameObject vfxGO = Instantiate(vfxPrefab);
            vfxGO.transform.position = pos;
            // it destroys itself when it's done.
        }

    }
}

