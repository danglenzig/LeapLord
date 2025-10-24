using UnityEngine;
namespace LeapLord
{
    public class IdleStateHandler : StateHandler
    {
        public override void HandleOnEnter()
        {
            base.HandleOnEnter();
            if (player == null) return;
            player.SpriteQuad.Play(EnumLeoAnimations.IDLE);
            playerRB.isKinematic = true;
            return;
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
                if (Mathf.Abs(moveX) >= Player.MOVE_THRESHOLD)
                {
                    psm.SendEventString(player.ToWalkTransition.EventString);
                    return;
                }
            }
            else
            {
                psm.SendEventString(player.ToAirborneTransition.EventString);
                player.SpriteQuad.Play(EnumLeoAnimations.AIRBORNE);
                return;
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
            psm.SendEventString(player.ToJumpPrepTransition.EventString);
            return;
        }
        public override void HandleJumpReleased()
        {
            base.HandleJumpReleased();
        }
    }
}

