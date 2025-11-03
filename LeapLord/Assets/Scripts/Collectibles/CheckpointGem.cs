using UnityEngine;
using System;
namespace LeapLord
{
    public class CheckpointGem : MonoBehaviour
    {

        public static event System.Action<string> OnGemCollected;
        public static event System.Action TriedToCollectButFull;

        private Oscillator myOscillator;
        private float startY;
        private SphereCollider myCollider;

        private string uuid = "";
        public string UUID { get => uuid; }

        private void Awake()
        {
            uuid = Guid.NewGuid().ToString();
        }

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

            int collectedGems = PlayerManager.Gems;
            if (collectedGems >= PlayerManager.MAX_GEMS)
            {
                TriedToCollectButFull?.Invoke();
                return;
            }


            myCollider.isTrigger = false;

            OnGemCollected?.Invoke(uuid);

            Destroy(gameObject);
        }

        private System.Collections.IEnumerator HandleOnCollected()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

    }
}


