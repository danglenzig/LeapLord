using UnityEngine;

namespace LeapLord
{
    public class TeleportEffect : MonoBehaviour
    {
        [SerializeField] private string framesFolder = "SpriteFrames/TeleportEffect";
        [Range(1.0f, 60.0f)] [SerializeField] private float frameRate = 10.0f;

        private Texture2D[] frames;
        private int currentFrame = 0;
        private Material mat;


        private void Start()
        {
            mat = GetComponent<Renderer>().material;
            LoadFrames(framesFolder);
        }

        public void Play()
        {
            StartCoroutine(PlayOneShot());
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


