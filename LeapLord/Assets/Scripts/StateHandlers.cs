using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{
    public class StateHandlers : MonoBehaviour
    {

        //[SerializeField] private Player player;
        private Player _player;
        [HideInInspector] public Player player
        {
            get => _player;
            set
            {
                //Debug.Log("FOO");
                _player = value;
                if (_player != null)
                {
                    //Debug.Log("BAR");
                    OnPlayerSet();
                }
            }
        }

        //private List<StateHandler> handlers;

        [SerializeField] private ParkedStateHandler parkedHandler;
        [HideInInspector] public ParkedStateHandler ParkedHandler
        {
            get => parkedHandler;
        }

        [SerializeField] private IdleStateHandler idleHandler;
        [HideInInspector]
        public IdleStateHandler IdleHandler
        {
            get => idleHandler;
        }

        [SerializeField] private WalkStateHandler walkHandler;
        [HideInInspector]
        public WalkStateHandler WalkHandler
        {
            get => walkHandler;
        }

        [SerializeField] private JumpPrepStateHandler jumpPrepHandler;
        [HideInInspector]
        public JumpPrepStateHandler JumpPrepHandler
        {
            get => jumpPrepHandler;
        }

        [SerializeField] private AirborneStateHandler airborneHandler;
        [HideInInspector]
        public AirborneStateHandler AirborneHandler
        {
            get => airborneHandler;
        }

        private void OnPlayerSet()
        {
            ParkedHandler.player = player;
            IdleHandler.player = player;
            WalkHandler.player = player;
            JumpPrepHandler.player = player;
            AirborneHandler.player = player;

            player.SetIsReady(true);
        }


    }
}

