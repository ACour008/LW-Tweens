using UnityEngine;
using Tweens.Lerpers;

namespace Tweens
{
    /// <summary>
    /// Changes the scale of a GameObject over time based on the EffectData
    /// provided to the effect.
    ///
    /// Currently supports the following components:
    /// - Transform
    /// - RectTransform
    /// </summary>
    public class Scale : Effect
    {
        public Scale(Transform transform, EffectData<Vector3> effectData)
        {
            _lerper = new Vector3Lerper()
                .Init(() => transform.localScale, (newScale) => transform.localScale = newScale,
                        effectData.EndValue, effectData.Duration, effectData.StartDelay);

            SetCoroutines();
        }

        public Scale(RectTransform rectTransform, EffectData<Vector3> effectData)
        {
            _lerper = new Vector3Lerper()
                .Init(() => rectTransform.localScale, (newScale) => rectTransform.localScale = newScale,
                    effectData.EndValue, effectData.Duration, effectData.StartDelay);

            SetCoroutines();
        }
    }
}
