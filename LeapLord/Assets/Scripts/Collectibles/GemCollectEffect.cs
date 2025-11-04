using UnityEngine;
namespace LeapLord
{
    public class GemCollectEffect : MonoBehaviour
    {
        [SerializeField] private string framesFolder = "SpriteFrames/CollectEffect";
        [Range(1.0f, 60.0f)] [SerializeField] private float frameRate = 10.0f;

        private Texture2D[] frames;
        private int currentFrame = 0;
        private Material mat;

        private void Start()
        {
            mat = GetComponent<Renderer>().material;
            LoadFrames(framesFolder);
            Play();
        }

        void LoadFrames(string folderPath)
        {
            Object[] loaded = Resources.LoadAll(folderPath, typeof(Texture2D));
            frames = new Texture2D[loaded.Length];
            for (int i = 0; i < loaded.Length; i++)
            {
                frames[i] = (Texture2D)loaded[i];
            }
        }

        public void Play()
        {
            StartCoroutine(PlayOneShot());
        }

        private System.Collections.IEnumerator PlayOneShot()
        {
            currentFrame = 0;
            if (frames.Length <= 0)
            {
                Debug.Log("No frames");
                yield break;
            }
            else
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    mat.mainTexture = frames[currentFrame];

                    if (currentFrame == frames.Length - 1)
                    {
                        Destroy(gameObject);
                        yield break;
                        
                    }
                    else
                    {
                        //not the last frame
                        yield return new WaitForSeconds(1 / frameRate);
                        currentFrame = (currentFrame + 1) % frames.Length;
                    }
                }
            }
        }
    }
}


