using UnityEngine;
using System.Collections;
namespace LeapLord
{
    public class QuadSpriteAnimator : MonoBehaviour
    {
        
        public event System.Action<EnumLeoAnimations> OnAnimFinished;
        public event System.Action<EnumLeoAnimations> OnAnimLooped;


        //[SerializeField] private EnumLeoAnimations startingAnimation = EnumLeoAnimations.IDLE;

        [SerializeField] private string walkFramesFolder = "SpriteFrames/LeoWalk";
        [SerializeField] private string jumpUpFramesFolder = "SpriteFrames/LeoJumpUp";
        [SerializeField] private string idleFramesFolder = "SpriteFrames/LeoIdle";
        [SerializeField] private string airborneFramesFolder = "SpriteFrames/LeoAirborne";
        [Range(1.0f, 60.0f)][SerializeField] private float frameRate = 10.0f;

        private Texture2D[] frames;
        private int currentFrame = 0;
        private Material mat;

        private EnumLeoAnimations currentLeoAnimation = EnumLeoAnimations.IDLE;


        private void Start()
        {
            mat = GetComponent<Renderer>().material;
            mat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
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

        void LoadOneRandomFrameFromFolder(string folderPath)
        {
            Object[] loaded = Resources.LoadAll(folderPath, typeof(Texture2D));
            frames = new Texture2D[1];
            int randoInt = Random.Range(0, loaded.Length);
            frames[0] = (Texture2D)loaded[randoInt];
        }

        public void Play(EnumLeoAnimations _anim)
        {
            StopAllCoroutines();

            switch (_anim)
            {
                case EnumLeoAnimations.IDLE:
                    currentLeoAnimation = EnumLeoAnimations.IDLE;
                    LoadFrames(idleFramesFolder);
                    StartCoroutine(PlayLoopingAnimation());
                    break;
                case EnumLeoAnimations.WALK:
                    currentLeoAnimation = EnumLeoAnimations.WALK;
                    LoadFrames(walkFramesFolder);
                    StartCoroutine(PlayLoopingAnimation());
                    break;
                case EnumLeoAnimations.JUMP_UP:
                    currentLeoAnimation = EnumLeoAnimations.JUMP_UP;
                    LoadFrames(jumpUpFramesFolder);
                    StartCoroutine(PlayOneShotAnimation());
                    break;
                case EnumLeoAnimations.AIRBORNE:
                    currentLeoAnimation = EnumLeoAnimations.AIRBORNE;
                    //LoadFrames(airborneFramesFolder);
                    LoadOneRandomFrameFromFolder(airborneFramesFolder);
                    StartCoroutine(PlayLoopingAnimation());
                    break;
                default:
                    Debug.Log($"Invalid animation: {_anim}");
                    return;
            }
        }

        IEnumerator PlayLoopingAnimation()
        {

            currentFrame = 0;
            while (true)
            {
                if (frames.Length <= 0)
                {
                    Debug.Log($"{currentLeoAnimation} has no frames");
                    yield break;
                }
                else if (frames.Length > 1)
                {
                    mat.mainTexture = frames[currentFrame];

                    currentFrame = (currentFrame + 1) % frames.Length;
                    if (currentFrame == 0)
                    {
                        //Debug.Log($"Loop at {Time.time}");
                        // invoke the anim looped event
                        OnAnimLooped?.Invoke(currentLeoAnimation); // the ? is important if there are no listeners
                        
                    }
                    yield return new WaitForSeconds(1.0f / frameRate);
                }
                else
                {
                    StartCoroutine(PlayOneShotAnimation()); // there is only one frame
                    yield break;
                }
            }
        }

        IEnumerator PlayOneShotAnimation()
        {
            currentFrame = 0;
            if (frames.Length <= 0)
            {
                Debug.Log($"{currentLeoAnimation} has no frames");
                yield break;
            }
            else
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    mat.mainTexture = frames[currentFrame];
                    
                    if (currentFrame == frames.Length - 1)
                    {
                        // last frame
                        //Debug.Log("Last frame");
                        OnAnimFinished?.Invoke(currentLeoAnimation);
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


