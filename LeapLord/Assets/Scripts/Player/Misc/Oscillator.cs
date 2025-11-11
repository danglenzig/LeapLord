using UnityEngine;

// Stick this on to any game object as a component
// The value of CurrentValue will cycle between min and max
// values, based on the configured Amplitute, Period, and Ground
// e.g. if Amplitude = 10, Period = 1, and Ground = 0, then it will
// cycle between -5 and +5 every 1 second.
// The wave type determines how it moves...
//   Sine: back and forth like a pendulum -- uses Mathf.Sin()
//   Triangle: back and forth like a billiard ball  -- uses Mathf.PingPong()
//   Square: alternatesdirectly  between the min and max values,
//           without going through the values in between
// Usage example...

//   void Update()
//        {
//            float currentOscVal = myOscillator.GetCurrentValue();
//            Vector3 newPos = new Vector3(0.0f, startPos.y , startPos.z + currentOscVal);
//            transform.position = newPos;
//
//        }


namespace LeapLord
{
    public class Oscillator : MonoBehaviour
    {

        public enum EnumWaveType { SINE, TRIANGLE, SQUARE }

        public EnumWaveType WaveType;

        public float Amplitude = 10.0f;
        public float Period = 1.0f;
        public float Ground = 0.0f;

        private float _currentValue;
        public float CurrentValue
        {
            get => _currentValue;
            private set
            {
                _currentValue = value;
            }
        }

        private float timeAccumulator = 0.0f;
        private bool squareWaveUp = false;


        void Start()
        {
            Period = Mathf.Abs(Period);
            if (Period <= 0)
            {
                Period = 0.05f;
            }

            if (WaveType == EnumWaveType.SQUARE)
            {
                float startValue = (Amplitude * 0.5f) + Ground;
                CurrentValue = startValue;
            }

        }

        void Update()
        {

            Period = Mathf.Abs(Period);
            if (Period <= 0)
            {
                Period = 0.05f;
            }

            switch (WaveType)
            {
                case EnumWaveType.SINE:
                    SineUpdate();
                    break;
                case EnumWaveType.TRIANGLE:
                    TriangleUpdate();
                    break;
                case EnumWaveType.SQUARE:
                    SquareUpdate();
                    break;
            }
        }

        void SineUpdate()
        {
            float angleRadians = ((Time.time % Period) / Period) * Mathf.PI * 2.0f;
            angleRadians -= Mathf.PI / 2.0f; // rotate half a circle, so we start at zero
            float rawSine = Mathf.Sin(angleRadians);
            float rawOffset = rawSine * Amplitude * 0.5f;
            float transposedToGround = rawOffset + Ground;
            CurrentValue = transposedToGround;
        }

        void TriangleUpdate()
        {
            float rawTriangle = Mathf.PingPong(Time.time, (Period / 2.0f)) / (Period / 2.0f);
            float distanceFromZero = Amplitude * rawTriangle;
            distanceFromZero -= Amplitude * 0.5f;
            distanceFromZero += Ground;
            CurrentValue = distanceFromZero;

        }

        void SquareUpdate()
        {
            timeAccumulator += Time.deltaTime;
            if (timeAccumulator < Period * 0.5f)
            {
                return;
            }
            timeAccumulator = 0.0f;

            float distanceFromGround = Amplitude * 0.5f;
            if (!squareWaveUp)
            {
                distanceFromGround *= -1;
            }
            squareWaveUp = !squareWaveUp;
            CurrentValue = distanceFromGround + Ground;
        }

        public float GetCurrentValue()
        {
            return CurrentValue;
        }
    }
}


