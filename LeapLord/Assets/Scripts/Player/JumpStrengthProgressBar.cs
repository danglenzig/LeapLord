using UnityEngine;
namespace LeapLord
{
    public class JumpStrengthProgressBar : MonoBehaviour
    {

        private const float HEIGHT = 0.05f;
        private const float DEPTH = 1.0f;
        private const float MAX_WIDTH = 0.5f;


        [SerializeField] private GameObject backgroundQuad;
        [SerializeField] private GameObject foregroundQuad;


        private void Start()
        {
            gameObject.SetActive(false);
            foregroundQuad.transform.localScale = new Vector3(0.0f, HEIGHT, DEPTH);
            
        }

        public void SetProgress(float progress)
        {
            progress /= 100.0f;
            float width = MAX_WIDTH * progress;
            foregroundQuad.transform.localScale = new Vector3(width, HEIGHT, DEPTH);

            float diff = MAX_WIDTH - width;




        }




    }
}

