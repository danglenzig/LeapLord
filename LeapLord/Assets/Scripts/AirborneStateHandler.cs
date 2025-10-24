using UnityEngine;
namespace LeapLord
{
    public class AirborneStateHandler : StateHandler
    {
        public override void HandleOnEnter()
        {
            base.HandleOnEnter();
            player.SpriteQuad.Play(EnumLeoAnimations.AIRBORNE);
        }
        public override void HandleOnExit()
        {
            base.HandleOnExit();
        }
       
        public override void HandleUpdate(float dTime, float moveX)
        {
            base.HandleUpdate(dTime, moveX);
            if (player.IsGrounded())
            {
                psm.SendEventString(player.ToIdleTransition.EventString);
            }
            return;
            
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
