using UnityEngine;
using System.Collections.Generic;

namespace LeapLord
{
    public class SimpleStateMachine : MonoBehaviour
    {
        const int HISTORY_SIZE = 10;

        [SerializeField] private State initalState;
        
        private State currentState;
        [HideInInspector] public State CurrentState
        {
            get => currentState;
        }



        private readonly List<State> stateHistory = new List<State>();
        [HideInInspector] public State[] StateHistory
        {
            get => stateHistory.ToArray();
        }


        private void Awake()
        {
            currentState = initalState;
            initalState.EnterState();
        }

        private void Start()
        {
            if (initalState == null)
            {
                Debug.Log("No initial state assigned");
                return;
            }
        }

        private void UpdateStateHistory(State addedState)
        {
            stateHistory.Add(addedState);
            if (stateHistory.Count > HISTORY_SIZE)
            {
                stateHistory.RemoveAt(0);
            }
        }

        public void SendEventString(string eventString)
        {
            if (currentState == null)
            {
                Debug.Log("Something weird happened");
                return;
            }

            State nextState = null;
            foreach(StateTransition trans in currentState.Transitions)
            {
                if (trans.EventString == eventString)
                {
                    nextState = trans.ToState;
                    break;
                }
            }

            if (nextState == null)
            {
                Debug.Log($"{currentState.StateName} has no configured transition event {eventString}");
            }

            currentState.ExitState();
            UpdateStateHistory(currentState);
            currentState = nextState;
            currentState.EnterState();
        }
    }
}


