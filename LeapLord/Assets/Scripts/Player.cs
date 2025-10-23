using UnityEngine;
namespace LeapLord
{
    public class Player : MonoBehaviour
    {

        const float MOVE_THRESHOLD = 0.1f;
        const float MOVE_SPEED = 200.0f;
        const float MAX_JUMP_STRENGTH = 100.0f;
        const float JUMP_STREGTH_LERP = 0.25f;
        const float CLOSE_ENOUGH = 0.99f;
        const float GROUNDED_DISTANCE = 0.5f;

        [SerializeField] private SimpleStateMachine psm;
        [HideInInspector] public SimpleStateMachine Psm
        {
            get => psm;
        }

        [SerializeField] private QuadSpriteAnimator spriteQuad;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private JumpStrengthProgressBar progressBar;


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
        private float jumpStrength = 0.0f;
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

            //Debug.Log(isGro)

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
                    float _delta = MAX_JUMP_STRENGTH - jumpStrength;
                    _delta *= JUMP_STREGTH_LERP;
                    _delta *= Time.deltaTime * 10.0f;

                    if (jumpStrength + _delta < MAX_JUMP_STRENGTH * CLOSE_ENOUGH)
                    {
                        jumpStrength += _delta;
                        progressBar.SetProgress(jumpStrength);
                        //Debug.Log(jumpStrength);
                    }
                    else
                    {
                        //max jumpstrength
                    }

                    break;

                case PlayerStateNames.AIRBORNE:
                    // cast a downward ray

                    if (IsGrounded())
                    {
                        psm.SendEventString(toIdleTransition.EventString);
                    }
                    break;

                default:
                    return;
            }
        }

        private bool IsGrounded()
        {
            Vector3 position = transform.position;
            Vector3 direction = Vector3.down;
            return Physics.Raycast(position, direction, GROUNDED_DISTANCE, groundLayer);
            //return true;
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

                    psm.SendEventString(toJumpPrepTransition.EventString);
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


                    float xJump = jumpStrength;
                    float yJump = jumpStrength * 2.0f;
                    if (quadIsFlipped)
                    {
                        xJump *= -1.0f;
                    }
                    rb.isKinematic = false;
                    Vector3 jumpVector = new Vector3(xJump, yJump, 0.0f);
                    spriteQuad.Play(EnumLeoAnimations.JUMP_UP);
                    rb.AddForce(jumpVector * 1.5f);
                    StartCoroutine(GoToAirborneStateAfterDelay(0.1f));

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
                    progressBar.gameObject.SetActive(true);
                    spriteQuad.Play(EnumLeoAnimations.JUMP_PREP);
                    jumpStrength = 0.0f;
                    break;
                case PlayerStateNames.AIRBORNE:
                    spriteQuad.Play(EnumLeoAnimations.AIRBORNE);
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
                    progressBar.gameObject.SetActive(false);
                    jumpStrength = 0.0f;
                    break;
                case PlayerStateNames.AIRBORNE:
                    break;
                default:
                    Debug.Log("Something weird happened");
                    return;
            }
        }

        private System.Collections.IEnumerator GoToAirborneStateAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            psm.SendEventString(toAirborneTransition.EventString);
        }



    }
}

