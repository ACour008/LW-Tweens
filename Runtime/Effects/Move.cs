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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="effectData"></param>
        /// <returns></returns>
        public Move(Transform transform, EffectData<Vector3> effectData) :
            this(transform, effectData.EndValue, effectData.Duration, effectData.StartDelay, effectData.Options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="endPosition"></param>
        /// <param name="durationInSeconds"></param>
        /// <param name="startDelaySeconds"></param>
        public Move(Transform transform, Vector3 endPosition, float durationInSeconds, float startDelaySeconds, EasingOptions options = null)
        {
            IEasing easing = (options != null) ?  EasingFactory.Get(options.easingType) : EasingFactory.Get(EaseType.LINEAR);
            int smoothing = (options != null) ? options.smoothing : 2;

            _lerper = new Vector3Lerper()
                .Init(() => transform.position, (pos) => transform.position = pos, endPosition, durationInSeconds, startDelaySeconds);

            SetCoroutines(easing, smoothing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="effectData"></param>
        /// <returns></returns>
        public Move(RectTransform rectTransform, EffectData<Vector3> effectData) :
            this(rectTransform, effectData.EndValue, effectData.Duration, effectData.StartDelay, effectData.Options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="endPosition"></param>
        /// <param name="durationInSeconds"></param>
        /// <param name="startDelaySeconds"></param>
        public Move(RectTransform rectTransform, Vector3 endPosition, float durationInSeconds, float startDelaySeconds, EasingOptions options = null)
        {
            IEasing easing = (options != null) ?  EasingFactory.Get(options.easingType) : EasingFactory.Get(EaseType.LINEAR);
            int smoothing = (options != null) ? options.smoothing : 2;

            _lerper = new Vector3Lerper()
                .Init(() => rectTransform.anchoredPosition3D, (pos) => rectTransform.anchoredPosition3D = pos, endPosition, durationInSeconds, startDelaySeconds);
            SetCoroutines(easing, smoothing);
        }
    }
}