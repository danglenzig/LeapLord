using UnityEngine;
namespace LeapLord
{
    public class WalkStateHandler : StateHandler
    {
        public override void HandleOnEnter()
        {
            base.HandleOnEnter();
            player.SpriteQuad.Play(EnumLeoAnimations.WALK);
            playerRB.isKinematic = false;
            return;
        }
        public override void HandleOnExit()
        {
            base.HandleOnExit();
        }
        public override void HandleUpdate(float dTime, float moveX)
        {
            base.HandleUpdate(dTime, moveX);
            if (Mathf.Abs(moveX) < Player.MOVE_THRESHOLD)
            {
                psm.SendEventString(player.ToIdleTransition.EventString);
                return;
            }
            player.QuadIsFlipped = (moveX < 0.0f);
            float xVelocity = moveX * Player.MOVE_SPEED * dTime;
            playerRB.linearVelocity = new Vector3(xVelocity, 0.0f, 0.0f);
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

