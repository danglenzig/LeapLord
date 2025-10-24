using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{
    public class CameraMover : MonoBehaviour
    {

        //private const float DAMP = 0.1f;

        private const float MOVE_SPEED = 1.0f;
        private const float CLOSE_ENOUGH = 0.01f;
        private const float MOVE_LERP = 4.0f;

        [SerializeField] GameObject levelMarker01;
        [SerializeField] GameObject levelMarker02;

        [SerializeField] float Level01TopPlayerY;
        [SerializeField] float Level02TopPlayerY;

        private float camY_01;
        private float camY_02;

        private Camera cam;
        private Player player;
        private bool isDamp = false;
        
        private bool isMoving = false;
        private List<float> camYList = new List<float>();

        private float _currentCamY;
        private float currentCamY
        {
            get => _currentCamY;
            set
            {
                if (value == _currentCamY) return;
                _currentCamY = value;
            }
        }

        


        private void Awake()
        {
            cam = Camera.main;
            
            camY_01 = levelMarker01.transform.position.y;
            camY_02 = levelMarker02.transform.position.y;

            camYList.Add(camY_01);
            camYList.Add(camY_02);

        }

        private void Start()
        {
            transform.position = Vector3.zero;
            cam.transform.position = new Vector3(cam.transform.position.x, camY_01, cam.transform.position.z);
            currentCamY = camY_01;

            player = GetPlayer();

        }

        private void Update()
        {
            //Debug.Log(isMoving);

            
            if (player == null)
            {
                player = GetPlayer();
            }

            if (player == null) return;

            float playerY = player.transform.position.y;


            // this is all fucked up


            if (playerY < Level01TopPlayerY)
            {
                currentCamY = camY_01;
                MoveToNewCurrentCamY();
                return;
                /*
                if (currentCamY == camY_01) return;
                else
                {
                    currentCamY = camY_01;
                    return;
                }
                */
            }
            if (playerY < Level02TopPlayerY)
            {
                currentCamY = camY_02;
                MoveToNewCurrentCamY();
                return;
                /*
                if (currentCamY == camY_02) return;
                else
                {   
                    currentCamY = camY_02;
                    return;
                }
                */
            }



            if (!isMoving) return;


            //Debug.Log(currentCamY);

            

            float _y = cam.transform.position.y;
            float distanceToTargetY = Mathf.Abs(currentCamY - _y);

            Debug.Log(distanceToTargetY);


            if (distanceToTargetY <= CLOSE_ENOUGH)
            {
                isMoving = false;
                //Debug.Log("hkjhjkhkjhkj");
                return;
            }

            float moveDistance = distanceToTargetY * MOVE_LERP * Time.deltaTime;
            if (currentCamY < _y) { moveDistance *= -1.0f; }
            cam.transform.Translate(new Vector3(0.0f, moveDistance, 0.0f));

        }


        private void MoveToNewCurrentCamY()
        {
            isMoving = true;
        }

        private Player? GetPlayer()
        {
            Player _player = null;
            GameObject playerGo = GameObject.FindGameObjectWithTag(Tags.PLAYER_SINGLETON);
            _player = playerGo.GetComponent<Player>();
            return _player;
        }
        

        private void HandleOnTriggered(int roomID)
        {
            if (isMoving) return;
            float newCamY = camYList[roomID - 1];
            if (newCamY == currentCamY) return;
            currentCamY = newCamY;
        }

    }

}

