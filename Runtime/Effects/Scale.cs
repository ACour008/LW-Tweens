using UnityEngine;
using Tweens.Lerpers;

namespace Tweens
{
    /// <summary>
    /// Changes the scale of a GameObject.
    ///
    /// Currently supports the following components:
    /// - Transform
    /// - RectTransform
    /// </summary>
    public class Scale : Effect
    {
        public Scale(Transform transform, EffectData<Vector3> effectData) :
            this(transform, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Scale(Transform transform, Vector3 endScale, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new Vector3Lerper()
                .Init(() => transform.localScale, (newScale) => transform.localScale = newScale,
                    endScale, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }

        public Scale(RectTransform rectTransform, EffectData<Vector3> effectData) :
            this(rectTransform, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Scale(RectTransform rectTransform, Vector3 endScale, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new Vector3Lerper()
               .Init(() => rectTransform.localScale, (newScale) => rectTransform.localScale = newScale,
                   endScale, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }
    }
}
