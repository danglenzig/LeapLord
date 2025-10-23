using UnityEngine;
using UnityEngine.InputSystem;

namespace LeapLord
{
    public class InputHandler : MonoBehaviour
    {

        public static event System.Action<float> OnMoveXChanged;
        public static event System.Action OnJumpPressed;
        public static event System.Action OnJumpReleased;

        private float _moveX = 0.0f;


        //private void Start()
        //{
        //    Time.timeScale = 0.5f;
        //}

        private float moveX
        {
            get => _moveX;
            set
            {
                if (value != _moveX)
                {
                    _moveX = value;
                    OnMoveXChanged?.Invoke(_moveX);

                }
            }
        }

        private Keyboard myKB;


        private void Awake()
        {
            if (GameObject.FindGameObjectsWithTag(Tags.INPUT_HANDLER_SINGLETON).Length > 0)
            {
                Destroy(this);
            }
            gameObject.tag = Tags.INPUT_HANDLER_SINGLETON;
            DontDestroyOnLoad(this);
            myKB = Keyboard.current;
        }

        private void Update()
        {
            float moveRight = 0.0f;
            float moveLeft = 0.0f;
            if (myKB.dKey.isPressed)
            {
                moveRight = 1.0f;
            }
            else
            {
                moveRight = 0.0f;
            }

            if (myKB.aKey.isPressed)
            {
                moveLeft = -1.0f;
            }
            else
            {
                moveLeft = 0.0f;
            }

            moveX = moveRight + moveLeft;

            if (myKB.spaceKey.wasPressedThisFrame)
            {
                OnJumpPressed?.Invoke();
            }

            if (myKB.spaceKey.wasReleasedThisFrame)
            {
                OnJumpReleased?.Invoke();
            }

        }




    }
}

