using UnityEngine;
using System.Collections;
namespace LeapLord
{
    public class QuadSpriteAnimator : MonoBehaviour
    {
        [SerializeField] private string walkFramesFolder = "SpriteFrames/LeoWalk";
        [SerializeField] private string jumpUpFramesFolder = "SpriteFrames/LeoJumpUp";
        [SerializeField] private string idleFramesFolder = "SpriteFrames/LeoIdle";
        [SerializeField] private string airborneFramesFolder = "SpriteFrames/LeoAirborne";
        [SerializeField] private float frameRate = 10.0f;

        private Texture2D[] frames;
        private int currentFrame = 0;
        private Material mat;

        private void Start()
        {
            mat = GetComponent<Renderer>().material;
            //LoadFrames(walkFramesFolder);
            //StartCoroutine(PlayAnimation());

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

        public void Play(EnumLeoAnimations _anim)
        {
            switch (_anim)
            {
                case EnumLeoAnimations.IDLE:
                    break;
                case EnumLeoAnimations.WALK:
                    break;
                case EnumLeoAnimations.JUMP_UP:
                    break;
                case EnumLeoAnimations.AIRBORNE:
                    break;
                default:
                    return;
            }


            StopAllCoroutines();
            LoadFrames(walkFramesFolder);
            currentFrame = 0;
            StartCoroutine(PlayAnimation());
        }

        IEnumerator PlayAnimation()
        {
            while (true)
            {
                if (frames.Length <= 0) yield break;

                if (frames.Length > 1)
                {
                    mat.mainTexture = frames[currentFrame];
                    currentFrame = (currentFrame + 1) % frames.Length;
                    yield return new WaitForSeconds(1.0f / frameRate);
                }
                else
                {
                    mat.mainTexture = frames[0];
                    yield break;
                }

                
            }
        }
        


    }
}


