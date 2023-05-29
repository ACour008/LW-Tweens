namespace Tweens.Easing
{
    [System.Serializable]
    public class EasingOptions
    {
        public EaseType easingType;
        public int smoothing;

        public EasingOptions(EaseType easingType, int smoothing)
        {
            this.easingType = easingType;
            this.smoothing = smoothing;
        }
    }
}
