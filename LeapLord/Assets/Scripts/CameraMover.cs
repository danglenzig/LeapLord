using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{
    public class CameraMover : MonoBehaviour
    {
        private const float DAMP = 0.25f;
        private const float CLOSE_ENOUGH = 0.01f;
        private const float MOVE_LERP = 0.5f;

        [SerializeField] float moveSpeed = 15.0f;

        [Header("MARKERS")]
        // these are all just empty game objects, whose
        // only value is their position.
        [SerializeField] private GameObject cameraMarker00;
        [SerializeField] private GameObject playerMarker00;

        [SerializeField] private GameObject cameraMarker01;
        [SerializeField] private GameObject playerMarker01;

        [SerializeField] private GameObject cameraMarker02;
        [SerializeField] private GameObject playerMarker02;

        // ...and so on

        private Player player;
        private Camera cam;
        private GameObject currentCameraMarker;

        private bool dampened = false;
        private bool beMoving = true;

        private void Start()
        {
            currentCameraMarker = cameraMarker00;
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

            float playerY = player.transform.position.y;

            if (playerY < playerMarker00.transform.position.y)
            {
                if (currentCameraMarker != cameraMarker00)
                {
                    MoveCamToMarker(cameraMarker00);
                    return;
                }
                return;
            }
            else if (playerY < playerMarker01.transform.position.y)
            {
                if (currentCameraMarker != cameraMarker01)
                {
                    MoveCamToMarker(cameraMarker01);
                    return;
                }
                return;
            }
            else if (playerY < playerMarker02.transform.position.y)
            {
                if (currentCameraMarker != cameraMarker02)
                {
                    MoveCamToMarker(cameraMarker02);
                    return;
                }
                return;
            }

            // and so on...
        }



        private void MoveCamUpdate(float dTime)
        {
            float targetY = currentCameraMarker.transform.position.y;
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

        private void MoveCamToMarker(GameObject camMarker)
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

