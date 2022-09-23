using UnityEngine;
using Tweens.Lerpers;

namespace Tweens
{
    /// <summary>
    /// Rotates a GameObject based on eulerAngles (Vector3) as set in the
    /// EffectData given to it.
    /// 
    /// Currently supports the following components:
    /// - Transform
    /// - RectTransform
    /// </summary>
    public class Rotate : Effect
    {
        public Rotate(Transform transform, EffectData<Vector3> effectData)
        {
            _lerper = new Vector3Lerper()
                .Init(() => transform.eulerAngles, (eulerAngles) => transform.eulerAngles = eulerAngles,
                      effectData.EndValue, effectData.Duration, effectData.StartDelay);

            SetCoroutines();
        }

        public Rotate(RectTransform rectTransform, EffectData<Vector3> effectData)
        {
            _lerper = new Vector3Lerper()
                .Init(() => rectTransform.eulerAngles, (eulerAngles) => rectTransform.eulerAngles = eulerAngles,
                    effectData.EndValue, effectData.Duration, effectData.StartDelay);

            SetCoroutines();
        }
    }
}
