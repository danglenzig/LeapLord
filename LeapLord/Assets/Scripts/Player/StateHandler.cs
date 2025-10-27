using UnityEngine;
namespace LeapLord
{
    public class StateHandler : MonoBehaviour
    {
        private Player _player = null;
        [HideInInspector] public Player player
        {
            get => _player;
            set
            {
                _player = value;
                if (_player != null)
                {
                    psm = _player.Psm;
                    playerRB = _player.GetComponent<Rigidbody>();
                }
            }
        }

        [HideInInspector] public SimpleStateMachine psm = null;
        [HideInInspector] public Rigidbody playerRB = null;

        public virtual void HandleOnEnter() { }
        public virtual void HandleOnExit() { }
        public virtual void HandleUpdate(float dTime, float moveX) { }
        public virtual void HandleFixedUpdate(float dTime) { }
        public virtual void HandleJumpPressed() { }
        public virtual void HandleJumpReleased() { }
    }
}


