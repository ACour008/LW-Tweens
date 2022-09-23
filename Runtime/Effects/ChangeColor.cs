using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tweens.Lerpers;

namespace Tweens
{
    /// <summary>
    /// Changes the color of a GameObject over time given the EffectData given
    /// to it.
    ///
    /// Currently supports the following Components:
    /// - MeshRenderer
    /// </summary>
    public class ChangeColor : Effect
    {
        public ChangeColor(MeshRenderer meshRenderer, EffectData<Color> effectData)
        {
            _lerper = new ColorLerper().
                Init(() => meshRenderer.material.color, (newColor) => meshRenderer.material.color = newColor,
                    effectData.EndValue, effectData.Duration, effectData.StartDelay);

            SetCoroutines();
        }
    }

}
