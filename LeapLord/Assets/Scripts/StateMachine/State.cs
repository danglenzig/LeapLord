using UnityEngine;
namespace LeapLord
{
    public class State : MonoBehaviour
    {
        public event System.Action<State> OnStateEntered;
        public event System.Action<State> OnStateExited;

        [SerializeField] private string stateName;
        [HideInInspector] public string StateName
        {
            get => stateName;
        }

        [SerializeField] private StateTransition[] transitions;
        [HideInInspector] public StateTransition[] Transitions
        {
            get => transitions;
        }

        public void EnterState()
        {
            OnStateEntered?.Invoke(this);
        }
        public void ExitState()
        {
            OnStateExited?.Invoke(this);
        }




    }
}

