#nullable enable
using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{

    [System.Serializable]
    public class CameraZone
    {
        public Transform? playerMarker;
        public Transform? cameraMarker;
    }

    public class CameraMover : MonoBehaviour
    {
        private const float DAMP = 0.5f;
        private const float CLOSE_ENOUGH = 0.01f;
        private const float MOVE_LERP = 0.5f;

        [SerializeField] private float moveSpeed = 15.0f;
        [SerializeField] private List<CameraZone> cameraZones = new List<CameraZone>();


        private Player? player;
        private Camera? cam;
        private Transform? currentCameraMarker;

        private bool dampened = false;
        private bool beMoving = true;

        private float timeAccumulator = 0.0f;
        private float pollingInterval = 0.05f;

        private void Start()
        {
            currentCameraMarker = cameraZones[0].cameraMarker;
            cam = Camera.main;
            GameObject playerGO = GameObject.FindGameObjectWithTag(Tags.PLAYER_SINGLETON);
            player = GetPlayer();
        }

        private void Update()
        {

            if (beMoving) { MoveCamUpdate(Time.deltaTime); return; }
            if (player == null) { player = GetPlayer(); }
            if (player == null) { return; }
            if (dampened) { return; }

            // throttle player poling
            timeAccumulator += Time.deltaTime;
            if (timeAccumulator < pollingInterval) { return;  }
            timeAccumulator = 0.0f;

            float playerY = player.transform.position.y;

            foreach (CameraZone? zone in cameraZones)
            {
                if (playerY < zone.playerMarker?.position.y)
                {
                    MoveCamToMarker(zone.cameraMarker);
                    return;
                }
            }
        }

        private void MoveCamUpdate(float dTime)
        {
            float targetY = currentCameraMarker.position.y;
            float camY = cam.transform.position.y;
            float distanceToTarget = Mathf.Abs(targetY - camY);
            if (distanceToTarget <= CLOSE_ENOUGH)
            {
                beMoving = false;
                return;
            }

            float distanceThisUpdate = distanceToTarget * MOVE_LERP * moveSpeed * dTime;
            if (targetY < camY) { distanceThisUpdate *= -1; }

            Vector3 translateVector = new Vector3(0.0f, distanceThisUpdate, 0.0f);
            cam.transform.Translate(translateVector);
        }

        private void MoveCamToMarker(Transform camMarker)
        {
            StartCoroutine(Dampen());
            currentCameraMarker = camMarker;
            beMoving = true;
        }

        private Player? GetPlayer()
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag(Tags.PLAYER_SINGLETON);
            if (playerGO != null)
            {
                return playerGO.GetComponent<Player>();
            }
            return null;
        }

        private System.Collections.IEnumerator Dampen()
        {
            dampened = true;
            yield return new WaitForSeconds(DAMP);
            dampened = false;
        }
    }
}

