namespace Tweens
{
    /// <summary>
    /// A class that holds all animation information for an Effect
    /// </summary>
    /// <typeparam name="T">An appropriate type for the Effect's target end value.
    /// For example, the Move effect requires a Vector3 end value while a Scale
    /// effect requires a float.</typeparam>
    public class EffectData<T>
    {
        private T endValue;
        private float durationInSecs;
        private float startDelayInSecs;

        public T EndValue { get => endValue; set => endValue = value; }
        public float Duration { get => durationInSecs; set => durationInSecs = value; }
        public float StartDelay { get => startDelayInSecs; set => startDelayInSecs = value; }

        public EffectData(T endValue, float durationInSeconds, float startDelayInSeconds)
        {
            this.endValue = endValue;
            this.durationInSecs = durationInSeconds;
            this.startDelayInSecs = startDelayInSeconds;
        }
    }
}

