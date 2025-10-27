using UnityEngine;
using UnityEngine.InputSystem;

namespace LeapLord
{
    public class InputHandler : MonoBehaviour
    {
        private const float DAMP = 0.25f;

        public static event System.Action<float> OnMoveXChanged;
        public static event System.Action OnJumpPressed;
        public static event System.Action OnJumpReleased;
        public static event System.Action OnDropGemPressed;
        public static event System.Action OnTeleportPressed;

        public static event System.Action OnTestButtonPressed;

        private float _moveX = 0.0f;
        private bool pressedInputsDampened = false;

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
                Destroy(gameObject);
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
                StartCoroutine(DampenPressedInputs());
            }

            if (myKB.spaceKey.wasReleasedThisFrame)
            {
                OnJumpReleased?.Invoke();
                StartCoroutine(DampenPressedInputs());
            }

            if (myKB.tKey.wasPressedThisFrame)
            {
                OnTestButtonPressed?.Invoke();
                StartCoroutine(DampenPressedInputs());
            }

            if (myKB.eKey.wasPressedThisFrame)
            {
                OnDropGemPressed?.Invoke();
                StartCoroutine(DampenPressedInputs());
            }

            if (myKB.qKey.wasPressedThisFrame)
            {
                OnTeleportPressed?.Invoke();
                StartCoroutine(DampenPressedInputs());
            }

        }

        private System.Collections.IEnumerator DampenPressedInputs()
        {
            pressedInputsDampened = true;
            yield return new WaitForSeconds(DAMP);
            pressedInputsDampened = false;
        }


    }
}

