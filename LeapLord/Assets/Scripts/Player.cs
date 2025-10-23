using UnityEngine;
namespace LeapLord
{
    public class Player : MonoBehaviour
    {
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


        private void Awake()
        {
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
            psm.SendEventString(toIdleTransition.EventString);
        }

        private void HandleOnStateEntered(State enteredState)
        {
            switch (enteredState.StateName)
            {
                case PlayerStateNames.PARKED:
                    break;

                case PlayerStateNames.IDLE:
                    if (!spriteQuad.isActiveAndEnabled)
                    {
                        spriteQuad.gameObject.SetActive(true);
                    }
                    spriteQuad.Play(EnumLeoAnimations.IDLE);
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

