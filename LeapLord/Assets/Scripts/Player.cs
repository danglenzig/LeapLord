using UnityEngine;
namespace LeapLord
{
    public class Player : MonoBehaviour
    {

        public const float MOVE_THRESHOLD = 0.1f;
        public const float MOVE_SPEED = 1.5f;
        public const float MAX_JUMP_STRENGTH = 100.0f;
        public const float JUMP_STREGTH_LERP = 0.25f;
        public const float CLOSE_ENOUGH = 0.99f;
        public const float GROUNDED_DISTANCE = 0.5f;

        [SerializeField] private SimpleStateMachine psm;
        [HideInInspector] public SimpleStateMachine Psm { get => psm; }

        
        [SerializeField] private JumpStrengthProgressBar progressBar;
        [HideInInspector] public JumpStrengthProgressBar ProgressBar { get => progressBar; }

        [SerializeField] private QuadSpriteAnimator spriteQuad;
        [HideInInspector] public QuadSpriteAnimator SpriteQuad { get => spriteQuad; }

        [SerializeField] private StateHandlers stateHandlers;
        [SerializeField] private LayerMask groundLayer;

        [Header("States")]
        [SerializeField] private State parkedState;
        [SerializeField] private State idleState;
        [SerializeField] private State walkState;
        [SerializeField] private State jumpPrepState;
        [SerializeField] private State airborneState;

        [Header("Transitions")]
        [SerializeField] private StateTransition toParkedTransition;
        [HideInInspector] public StateTransition ToParkedTransition { get => toParkedTransition; }

        [SerializeField] private StateTransition toIdleTransition;
        [HideInInspector] public StateTransition ToIdleTransition { get => toIdleTransition; }

        [SerializeField] private StateTransition toWalkTransition;
        [HideInInspector] public StateTransition ToWalkTransition { get => toWalkTransition; }

        [SerializeField] private StateTransition toJumpPrepTransition;
        [HideInInspector] public StateTransition ToJumpPrepTransition { get => toJumpPrepTransition; }

        [SerializeField] private StateTransition toAirborneTransition;
        [HideInInspector] public StateTransition ToAirborneTransition { get => toAirborneTransition; }
        

        private float jumpStrength = 0.0f;
        public float JumpStrength
        {
            get => jumpStrength;
            set
            {
                jumpStrength = value;
            }
        }

        private bool quadIsFlipped = false;
        [HideInInspector] public bool QuadIsFlipped
        {
            get => quadIsFlipped;
            set
            {
                if (value == quadIsFlipped) return;
                quadIsFlipped = value;
                SetQuadFlipped(value);
            }
        }

        private Rigidbody rb;
        private float moveInputX = 0.0f;
        private bool _isReady = false;


        private void Awake()
        {
            
            if (GameObject.FindGameObjectsWithTag(Tags.PLAYER_SINGLETON).Length > 0)
            {
                Destroy(this);
            }
            gameObject.tag = Tags.PLAYER_SINGLETON;
            //DontDestroyOnLoad(this);

            // EVENT CONNECTIONS //
            InputHandler.OnMoveXChanged += (_value) => { moveInputX = _value; };
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

            spriteQuad.OnAnimFinished += HandleAnimationFinished;

        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            psm.SendEventString(toIdleTransition.EventString);
            stateHandlers.player = this; // the stateHandlers script calls SetIsReady after it creates its dependencies
        }
        
        private void Update()
        {
            if (!_isReady) { return; }

            if (!rb.isKinematic) // stand up straight
            {
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.85f);
            }

            // handle state-dependent update behaviors
            switch (psm.CurrentState.StateName)
            {
                case PlayerStateNames.PARKED:
                    //stateHandlers.ParkedHandler.HandleUpdate(this, Time.deltaTime, moveInputX);
                    return;
                case PlayerStateNames.IDLE:
                    stateHandlers.IdleHandler.HandleUpdate(Time.deltaTime, moveInputX);
                    return;
                case PlayerStateNames.WALK:
                    stateHandlers.WalkHandler.HandleUpdate(Time.deltaTime, moveInputX);
                    return;
                case PlayerStateNames.JUMP_PREP:
                    stateHandlers.JumpPrepHandler.HandleUpdate(Time.deltaTime, moveInputX);
                    return;
                case PlayerStateNames.AIRBORNE:
                    stateHandlers.AirborneHandler.HandleUpdate(Time.deltaTime, moveInputX);
                    return;
                default:
                    return;
            }
        }

        public bool IsGrounded()
        {
            Vector3 position = transform.position;
            Vector3 direction = Vector3.down;
            return Physics.Raycast(position, direction, GROUNDED_DISTANCE, groundLayer);
        }

        private void SetQuadFlipped(bool flipped)
        {
            Vector3 s = spriteQuad.transform.localScale;
            s.x = Mathf.Abs(s.x) * (flipped ? -1.0f : 1.0f);
            spriteQuad.transform.localScale = s;
        }

        private void HandleOnJumpPressed()
        {
            // handle state-based on-jump-pressed behaviors
            switch (psm.CurrentState.StateName)
            {
                case PlayerStateNames.PARKED:
                    //stateHandlers.ParkedHandler.HandleJumpPressed();
                    return;
                case PlayerStateNames.IDLE:
                    stateHandlers.IdleHandler.HandleJumpPressed();
                    return;
                case PlayerStateNames.WALK:
                    //stateHandlers.WalkHandler.HandleJumpPressed();
                    return;
                case PlayerStateNames.JUMP_PREP:
                    //stateHandlers.JumpPrepHandler.HandleJumpPressed();
                    return;
                case PlayerStateNames.AIRBORNE:
                    //stateHandlers.AirborneHandler.HandleJumpPressed();
                    return;
                default:
                    return;
            }
        }
        private void HandleOnJumpReleased()
        {
            // handle state-based on-jump-released behaviors
            switch (psm.CurrentState.StateName)
            {
                case PlayerStateNames.PARKED:
                    //stateHandlers.ParkedHandler.HandleJumpReleased();
                    return;
                case PlayerStateNames.IDLE:
                    //stateHandlers.IdleHandler.HandleJumpReleased();
                    return;
                case PlayerStateNames.WALK:
                    //stateHandlers.WalkHandler.HandleJumpReleased();
                    return;
                case PlayerStateNames.JUMP_PREP:

                    stateHandlers.JumpPrepHandler.HandleJumpReleased();
                    return;
                case PlayerStateNames.AIRBORNE:
                    //stateHandlers.AirborneHandler.HandleJumpReleased();
                    return;
                default:
                    return;
            }
        }


        private void HandleOnStateEntered(State enteredState)
        {
            // handle on enter state behaviors
            switch (enteredState.StateName)
            {
                case PlayerStateNames.PARKED:
                    //stateHandlers.ParkedHandler.HandleOnEnter();
                    return;
                case PlayerStateNames.IDLE:
                    stateHandlers.IdleHandler.HandleOnEnter();
                    return;
                case PlayerStateNames.WALK:
                    stateHandlers.WalkHandler.HandleOnEnter();
                    return;
                case PlayerStateNames.JUMP_PREP:
                    stateHandlers.JumpPrepHandler.HandleOnEnter();
                    return;
                case PlayerStateNames.AIRBORNE:
                    stateHandlers.AirborneHandler.HandleOnEnter();
                    return;
                default:
                    Debug.Log("Something weird happened");
                    return;
            }
        }

        private void HandleOnStateExited(State exitedState)
        {
            // handle on exit state behaviors
            switch (exitedState.StateName)
            {
                case PlayerStateNames.PARKED:
                    //stateHandlers.ParkedHandler.HandleOnExit();
                    return;
                case PlayerStateNames.IDLE:
                    //stateHandlers.IdleHandler.HandleOnExit();
                    return;
                case PlayerStateNames.WALK:
                    //stateHandlers.WalkHandler.HandleOnExit();
                    return;
                case PlayerStateNames.JUMP_PREP:
                    stateHandlers.JumpPrepHandler.HandleOnExit();
                    return;
                case PlayerStateNames.AIRBORNE:
                    //stateHandlers.AirborneHandler.HandleOnExit();
                    return;
                default:
                    Debug.Log("Something weird happened");
                    return;
            }
        }

        private void HandleAnimationFinished(EnumLeoAnimations leoAnim)
        {
            switch (leoAnim)
            {
                case EnumLeoAnimations.JUMP_UP:
                    spriteQuad.Play(EnumLeoAnimations.AIRBORNE);
                    return;

                default:
                    return;
            }
        }

        public void SetIsReady(bool val)
        {
            _isReady = val;
        }
    }
}

