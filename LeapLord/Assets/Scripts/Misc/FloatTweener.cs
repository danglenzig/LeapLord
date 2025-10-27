using UnityEngine;
using System.Collections.Generic;
namespace LeapLord
{
    public class FloatTweener : MonoBehaviour
    {
        public const int MAX_TWEENS = 10;

        public static event System.Action<FloatTweenData> TweenUpdated;
        
        private List<FloatTweenData> activeTweens = new List<FloatTweenData>();


        private void Start()
        {
            if (GameObject.FindGameObjectsWithTag(Tags.FLOAT_TWEEN_SINGLETON).Length > 0)
            {
                Destroy(this);
            }
            gameObject.tag = Tags.FLOAT_TWEEN_SINGLETON;
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            foreach (FloatTweenData _tween in activeTweens)
            {
                // TODO:
                // calculate and set its currentValue (???)
                // increment its ticks
                // if currentValue == endValue:
                //     set isFinished to true
                //     remove it from the activeTweens list

                TweenUpdated?.Invoke(_tween);
            }
        }

        public bool StartTween
            (
            string _tweenName,
            float _startValue,
            float _endValue,
            float _duration,
            FloatTweenData.EaseType _easeType,
            FloatTweenData.TransDirection _transDirection
            )
        {
            if (activeTweens.Count >= MAX_TWEENS)
            {
                Debug.Log("Too many tweens");
                return false;
            }

            FloatTweenData newTween = new FloatTweenData();
            newTween.tweenName = _tweenName;
            newTween.startValue = _startValue;
            newTween.endValue = _endValue;
            newTween.duration = _duration;
            newTween.easeType = _easeType;
            newTween.transDirection = _transDirection;

            newTween.currentValue = _startValue;
            newTween.ticks = 0;
            newTween.isFinished = false;
            activeTweens.Add(newTween);
            return true;
        }
    }
    public struct FloatTweenData
    {
        public enum EaseType
        {
            LINEAR,
            QUAD,
            CUBIC,
            SINE
        }
        public enum TransDirection
        {
            IN,
            OUT,
            BOTH
        }

        public string tweenName;
        public float startValue;
        public float endValue;
        public float duration;
        public EaseType easeType;
        public TransDirection transDirection;

        public float currentValue;
        public int ticks;
        public bool isFinished;
    }
}

