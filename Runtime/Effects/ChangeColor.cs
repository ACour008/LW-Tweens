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
    /// - SpriteRenderer
    /// </summary>
    public class ChangeColor : Effect
    {

        public ChangeColor(SpriteRenderer spriteRenderer, EffectData<Color> effectData) :
            this(spriteRenderer, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public ChangeColor(SpriteRenderer spriteRenderer, Color endValue, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new ColorLerper()
                .Init(() => spriteRenderer.color, (c) => spriteRenderer.color = c, endValue, durationInSeconds, startDelaySeconds);
        }

        public ChangeColor(MeshRenderer meshRenderer, EffectData<Color> effectData):
            this(meshRenderer, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public ChangeColor(MeshRenderer meshRenderer, Color endValue, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new ColorLerper()
                .Init(() => meshRenderer.material.color, (newColor) => meshRenderer.material.color = newColor, 
                    endValue, durationInSeconds, startDelaySeconds);
        }
    }

}
