using UnityEngine;
namespace LeapLord
{
    public class Crown : MonoBehaviour
    {
        public static event System.Action OnCrownGot;

        [SerializeField] private GameObject crownVisual;

        private Oscillator myOscillator = null;
        private bool isGot = false;

        private float startY = 0.0f;

        void Start()
        {
            startY = transform.position.y;
            myOscillator = GetComponent<Oscillator>();
        }

        private void FixedUpdate()
        {
            if (myOscillator == null) { return; }
            float oscVal = myOscillator.CurrentValue;
            float newY = transform.position.y + oscVal;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.PLAYER_SINGLETON)) { return; }
            if (isGot == true) { return; }
            isGot = true;
            crownVisual.SetActive(false);
            //Debug.Log("You got the crown!!");
            OnCrownGot?.Invoke();
            Destroy(gameObject);

        }

    }
}

