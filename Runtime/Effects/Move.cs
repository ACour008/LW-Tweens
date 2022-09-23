using UnityEngine;
using Tweens.Lerpers;

namespace Tweens
{
    /// <summary>
    /// Moves the position of a GameObject based on the EffectData provided to it.
    /// 
    /// Currently supports the following components:
    /// - Transform
    /// - RectTransform
    /// </summary>
    public class Move : Effect
    {
        public Move(Transform transform, EffectData<Vector3> effectData)
        {
            _lerper = new Vector3Lerper()
                .Init(() => transform.position, (pos) => transform.position = pos, effectData.EndValue, effectData.Duration, effectData.StartDelay);

            SetCoroutines();
        }

        public Move(RectTransform rectTransform, EffectData<Vector3> effectData)
        {
            _lerper = new Vector3Lerper()
                .Init(() => rectTransform.anchoredPosition3D, (pos) => rectTransform.anchoredPosition3D = pos, effectData.EndValue, effectData.Duration, effectData.StartDelay);

            SetCoroutines();
        }

    }
}