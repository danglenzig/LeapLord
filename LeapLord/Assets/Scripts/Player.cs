using UnityEngine;
namespace LeapLord
{
    public class Player : MonoBehaviour
    {

        const float MOVE_THRESHOLD = 0.1f;
        const float MOVE_SPEED = 200.0f;

        [SerializeField] private SimpleStateMachine psm;
        [HideInInspector] public SimpleStateMachine Psm
        {
            get => psm;
        }

        [SerializeField] private QuadSpriteAnimator spriteQuad;
        [Header("States")]
        [SerializeField] private State parkedState;
        [SerializeField] private State idleState;
        [SerializeField] private State walkState;
        [SerializeField] private State jumpPrepState;
        [SerializeField] private State airborneState;

        [Header("Transitions")]
        [SerializeField] private StateTransition toParkedTransition;
        [SerializeField] private StateTransition toIdleTransition;
        [SerializeField] private StateTransition toWalkTransition;
        [SerializeField] private StateTransition toJumpPrepTransition;
        [SerializeField] private StateTransition toAirborneTransition;

        private Rigidbody rb;
        private float moveInputX = 0.0f;

        private bool _quadIsFlipped = false;
        private bool quadIsFlipped
        {
            get => _quadIsFlipped;
            set
            {
                if (value != _quadIsFlipped)
                {
                    _quadIsFlipped = value;
                    SetQuadFlipped(_quadIsFlipped);
                }
            }
        }


        private void Awake()
        {

            if (GameObject.FindGameObjectsWithTag(Tags.PLAYER_SINGLETON).Length > 0)
            {
                Destroy(this);
            }
            gameObject.tag = Tags.PLAYER_SINGLETON;
            DontDestroyOnLoad(this);

            InputHandler.OnMoveXChanged += (_value) =>
            {
                moveInputX = _value;
            };
            InputHandler.OnJumpPressed += HandleOnJumpPressed;
            InputHandler.OnJumpReleased += HandleOnJumpReleased;

            parkedState.OnStateEntered += HandleOnStateEntered;
            idleState.OnStateEntered += HandleOnStateEntered;
            walkState.OnStateEntered += HandleOnStateEntered;
            jumpPrepState.OnStateEntered += HandleOnStateEntered;
            airborneState.OnStateEntered += HandleOnStateEntered;

            parkedState.OnStateExited += HandleOnStateExited;
            idleState.OnStateExited += HandleOnStateExited;
            walkState.OnStateExited += HandleOnStateExited;
            jumpPrepState.OnStateExited += HandleOnStateExited;
            airborneState.OnStateExited += HandleOnStateExited;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            psm.SendEventString(toIdleTransition.EventString);
        }

        private void Update()
        {

            if (!rb.isKinematic)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            }
           
            switch (psm.CurrentState.StateName)
            {
                case PlayerStateNames.PARKED:
                    break;

                case PlayerStateNames.IDLE:
                    if (Mathf.Abs(moveInputX) >= MOVE_THRESHOLD)
                    {
                        psm.SendEventString(toWalkTransition.EventString);
                        return;
                    }
                    break;

                case PlayerStateNames.WALK:
                    if (Mathf.Abs(moveInputX) < MOVE_THRESHOLD)
                    {
                        psm.SendEventString(toIdleTransition.EventString);
                        return;
                    }
                    quadIsFlipped = (moveInputX < 0.0f);
                    float _moveX = moveInputX * MOVE_SPEED * Time.deltaTime;
                    rb.linearVelocity = new Vector3(_moveX, 0.0f, 0.0f);
                    break;

                case PlayerStateNames.JUMP_PREP:
                    break;

                case PlayerStateNames.AIRBORNE:
                    break;

                default:
                    return;
            }
        }

        private void SetQuadFlipped(bool flipped)
        {
            Vector3 s = spriteQuad.transform.localScale;
            s.x = Mathf.Abs(s.x) * (flipped ? -1.0f : 1.0f);
            spriteQuad.transform.localScale = s;
        }

        private void HandleOnJumpPressed()
        {
            switch (psm.CurrentState.StateName)
            {
                case PlayerStateNames.PARKED:
                    break;
                case PlayerStateNames.IDLE:
                    break;
                case PlayerStateNames.WALK:
                    break;
                case PlayerStateNames.JUMP_PREP:
                    break;
                case PlayerStateNames.AIRBORNE:
                    break;
                default:
                    return;
            }
        }
        private void HandleOnJumpReleased()
        {
            switch (psm.CurrentState.StateName)
            {
                case PlayerStateNames.PARKED:
                    break;
                case PlayerStateNames.IDLE:
                    break;
                case PlayerStateNames.WALK:
                    break;
                case PlayerStateNames.JUMP_PREP:
                    break;
                case PlayerStateNames.AIRBORNE:
                    break;
                default:
                    return;
            }
        }


        private void HandleOnStateEntered(State enteredState)
        {
            switch (enteredState.StateName)
            {
                case PlayerStateNames.PARKED:
                    break;

                case PlayerStateNames.IDLE:
                    spriteQuad.Play(EnumLeoAnimations.IDLE);
                    rb.isKinematic = true;
                    //rb.linearVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                case PlayerStateNames.WALK:
                    spriteQuad.Play(EnumLeoAnimations.WALK);
                    rb.isKinematic = false;
                    break;
                case PlayerStateNames.JUMP_PREP:
                    break;
                case PlayerStateNames.AIRBORNE:
                    break;
                default:
                    Debug.Log("Something weird happened");
                    return;
            }
        }

        private void HandleOnStateExited(State exitedState)
        {
            switch (exitedState.StateName)
            {
                case PlayerStateNames.PARKED:
                    break;
                case PlayerStateNames.IDLE:
                    break;
                case PlayerStateNames.WALK:
                    break;
                case PlayerStateNames.JUMP_PREP:
                    break;
                case PlayerStateNames.AIRBORNE:
                    break;
                default:
                    Debug.Log("Something weird happened");
                    return;
            }
        }



    }
}

