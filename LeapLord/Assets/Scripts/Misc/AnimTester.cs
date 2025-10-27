using UnityEngine;
using System.Collections;
namespace LeapLord
{
    public class AnimTester : MonoBehaviour
    {

        private const float DAMP = 1.0f;

        private bool _onCooldown = false;
        private bool onCooldown
        {
            get => _onCooldown;
            set
            {
                if (value != _onCooldown)
                _onCooldown = value;
                if (_onCooldown)
                {
                    StartCoroutine(InputCooldown());
                }

            }
        }

        [SerializeField] private QuadSpriteAnimator testQuad;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (onCooldown)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                onCooldown = true;
                testQuad.Play(EnumLeoAnimations.IDLE);
                return;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                onCooldown = true;
                testQuad.Play(EnumLeoAnimations.WALK);
                return;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                onCooldown = true;
                testQuad.Play(EnumLeoAnimations.JUMP_UP);
                return;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                onCooldown = true;
                testQuad.Play(EnumLeoAnimations.AIRBORNE);
                return;
            }

        }


        IEnumerator InputCooldown()
        {
            yield return new WaitForSeconds(DAMP);
            Debug.Log("input re-enabled");
            onCooldown = false;
        }
    }
}

