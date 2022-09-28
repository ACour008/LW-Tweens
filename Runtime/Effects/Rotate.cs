using UnityEngine;
using Tweens.Lerpers;

namespace Tweens
{
    /// <summary>
    /// Rotates a GameObject based on eulerAngles (Vector3).
    /// 
    /// Currently supports the following components:
    /// - Transform
    /// - RectTransform
    /// </summary>
    public class Rotate : Effect
    {
        public Rotate(Transform transform, EffectData<Vector3> effectData) :
            this(transform, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {

        }

        public Rotate(Transform transform, Vector3 endPosition, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new Vector3Lerper()
                .Init(() => transform.eulerAngles, (eulerAngles) => transform.eulerAngles = eulerAngles,
                    endPosition, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }

        public Rotate(RectTransform rectTransform, EffectData<Vector3> effectData):
            this(rectTransform, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {

        }

        public Rotate(RectTransform rectTransform, Vector3 endPosition, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new Vector3Lerper()
                .Init(() => rectTransform.eulerAngles, (eulerAngles) => rectTransform.eulerAngles = eulerAngles,
                    endPosition, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }
    }
}
