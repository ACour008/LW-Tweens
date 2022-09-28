using UnityEngine;
using Tweens.Lerpers;

namespace Tweens
{
    /// <summary>
    /// Moves the position of a GameObject.
    /// 
    /// Currently supports the following components:
    /// - Transform
    /// - RectTransform
    /// </summary>
    public class Move : Effect
    {
        public Move(Transform transform, EffectData<Vector3> effectData) :
            this(transform, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Move(Transform transform, Vector3 endPosition, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new Vector3Lerper()
                .Init(() => transform.position, (pos) => transform.position = pos, endPosition, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }

        public Move(RectTransform rectTransform, EffectData<Vector3> effectData) :
            this(rectTransform, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Move(RectTransform rectTransform, Vector3 endPosition, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new Vector3Lerper()
                .Init(() => rectTransform.anchoredPosition3D, (pos) => rectTransform.anchoredPosition3D = pos, endPosition, durationInSeconds, startDelaySeconds);
            SetCoroutines();
        }
    }
}