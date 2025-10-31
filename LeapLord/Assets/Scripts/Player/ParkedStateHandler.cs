using UnityEngine;
namespace LeapLord
{
    public class ParkedStateHandler : StateHandler
    {
        public override void HandleOnEnter()
        {
            base.HandleOnEnter();
            if (player == null) return;
            playerRB.isKinematic = true;
            player.SpriteQuad.Play(EnumLeoAnimations.PARKED);
        }
        public override void HandleOnExit()
        {
            base.HandleOnExit();
            if (player != null)
            {
                player.QuadIsFlipped = false;
            }
            
        }
        public override void HandleUpdate(float dTime, float moveX)
        {
            base.HandleUpdate(dTime, moveX);
        }
        public override void HandleFixedUpdate(float dTime)
        {
            base.HandleFixedUpdate(dTime);
        }
        public override void HandleJumpPressed()
        {
            base.HandleJumpPressed();
        }
        public override void HandleJumpReleased()
        {
            base.HandleJumpReleased();
        }
    }
}

