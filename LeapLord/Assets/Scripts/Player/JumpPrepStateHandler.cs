using UnityEngine;

namespace LeapLord
{
    public class JumpPrepStateHandler : StateHandler
    {
        public override void HandleOnEnter()
        {
            base.HandleOnEnter();
            player.ProgressBar.gameObject.SetActive(true);
            player.SpriteQuad.Play(EnumLeoAnimations.JUMP_PREP);
            player.JumpStrength = 0.0f;
            return;
        }
        public override void HandleOnExit()
        {
            base.HandleOnExit();
            player.ProgressBar.gameObject.SetActive(false);
            player.JumpStrength = 0.0f;
            return;
        }
        public override void HandleUpdate(float dTime, float moveX)
        {
            base.HandleUpdate(dTime, moveX);
            float _delta = Player.MAX_JUMP_STRENGTH - player.JumpStrength;
            _delta *= Player.JUMP_STREGTH_LERP * dTime * 10f;
            if (player.JumpStrength + _delta < Player.MAX_JUMP_STRENGTH * Player.CLOSE_ENOUGH)
            {
                player.JumpStrength += _delta;
                player.ProgressBar.SetProgress(player.JumpStrength);
            }
            else
            {
                // we're at max jump strength
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
            float xJump = player.JumpStrength;
            float yJump = xJump * 2.0f;
            if (player.QuadIsFlipped) { xJump *= -1; }
            playerRB.isKinematic = false;
            Vector3 jumpVector = new Vector3(xJump, yJump, 0.0f)  *  1.5f  ;
            player.SpriteQuad.Play(EnumLeoAnimations.JUMP_UP);
            playerRB.AddForce(jumpVector);
            StartCoroutine(GoToAirborneStateAfterDelay(0.1f));
        }


        private System.Collections.IEnumerator GoToAirborneStateAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            psm.SendEventString(player.ToAirborneTransition.EventString);
        }


    }
}


