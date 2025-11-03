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

        [SerializeField] private GameObject gemPrefab;
        [SerializeField] private int numberOfGems = 4;

        private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

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


            for (int j = 0; j < numberOfGems; j++)
            {
                int aRando = Random.Range(0, indexes.Count);
                indexes.Remove(aRando);
                chosenSpawnPoints.Add(spawnPoints[aRando]);
            }

            foreach(SpawnPoint sp in chosenSpawnPoints)
            {
                GameObject gemGO = Instantiate(gemPrefab);
                gemGO.transform.position = sp.marker.position;
                sp.gemUUID = gemGO.GetComponent<CheckpointGem>().UUID;
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

            Debug.Log(chosenSpawnPoint.gemUUID);

        }



        private void HandleOnGemCollected(string uuid)
        {
            foreach (SpawnPoint sp in spawnPoints)
            {
                if (sp.gemUUID == uuid)
                {
                    sp.gemUUID = "";
                    ReplenishGem(sp);
                    return;
                }
            }
        }


    }
}

