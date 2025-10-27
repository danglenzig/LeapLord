using UnityEngine;
namespace LeapLord
{
    public class CheckpointGem : MonoBehaviour
    {

        private Oscillator myOscillator;
        private float startY;
        private SphereCollider myCollider;

        private void Start()
        {
            myOscillator = GetComponent<Oscillator>();
            myCollider = GetComponent<SphereCollider>();
            myCollider.isTrigger = true;
            startY = transform.position.y;
        }

        private void FixedUpdate()
        {
            if (myOscillator == null) { return; }
            float oscValue = myOscillator.CurrentValue;
            Vector3 newPos = new Vector3(transform.position.x, startY +  oscValue, transform.position.z);
            gameObject.transform.position = newPos;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.PLAYER_SINGLETON)) { return; }
            myCollider.isTrigger = false;
            PlayerManager.CollectGem();
            
            Destroy(gameObject);
        }

        private System.Collections.IEnumerator HandleOnCollected()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

    }
}


