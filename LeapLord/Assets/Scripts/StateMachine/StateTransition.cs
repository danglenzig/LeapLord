using UnityEngine;
namespace LeapLord
{
    public class StateTransition : MonoBehaviour
    {
        [SerializeField] private State toState;
        [HideInInspector] public State ToState
        {
            get => toState;
        }

        [SerializeField] private string eventString;
        [HideInInspector] public string EventString
        {
            get => eventString;
        }
    }
}

