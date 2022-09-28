using System;
using UnityEngine;
using UnityEngine.UI;
using Tweens.Lerpers;
using Tweens.Utils;

namespace Tweens
{
    /// <summary>
    /// Changes the opacity of a GameObject.
    /// 
    /// Currently supports the following components:
    /// - CanvasGroup
    /// - Graphic
    /// - SpriteRenderer
    /// - Button
    /// - MeshRenderer (Material needs to have a standard shader)
    /// </summary>
    public class Fade : Effect
    {
        public Fade(CanvasGroup canvasGroup, EffectData<float> effectData) :
            this(canvasGroup, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Fade(CanvasGroup canvasGroup, float endValue, float durationInSeconds, float startDelaySeconds)
        {
            _lerper = new FloatLerper()
                .Init(() => canvasGroup.alpha, (newColor) => canvasGroup.alpha = newColor, endValue, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }

        public Fade(Graphic graphic, EffectData<float> effectData) :
            this(graphic, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Fade(Graphic graphic, float endValue, float durationInSeconds, float startDelaySeconds)
        {
            Color endColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, endValue);

            _lerper = new ColorLerper()
                .Init(() => graphic.color, (newColor) => graphic.color = newColor, endColor, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }

        public Fade(SpriteRenderer spriteRenderer, EffectData<float> effectData) :
            this(spriteRenderer, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Fade(SpriteRenderer spriteRenderer, float endValue, float durationInSeconds, float startDelaySeconds)
        {
            Color endColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, endValue);

            _lerper = new ColorLerper()
                .Init(() => spriteRenderer.color, (newColor) => spriteRenderer.color = newColor, endColor, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }

        public Fade(Button button, EffectData<float> effectData) :
            this(button, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Fade(Button button, float endValue, float durationInSeconds, float startDelaySeconds)
        {
            Graphic graphic = EffectUtils.getGraphicFromButton(button);
            Color endColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, endValue);

            _lerper = new ColorLerper()
                .Init(() => graphic.color, (newColor) => graphic.color = newColor, endColor, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }

        public Fade(MeshRenderer meshRenderer, EffectData<float> effectData) :
            this(meshRenderer, effectData.EndValue, effectData.Duration, effectData.StartDelay)
        {
        }

        public Fade(MeshRenderer meshRenderer, float endValue, float durationInSeconds, float startDelaySeconds)
        {
            Color meshColor = meshRenderer.material.color;
            Color endColor = new Color(meshColor.r, meshColor.g, meshColor.b, endValue);

            StandardShaderUtils.ChangeRenderMode(meshRenderer.material, BlendMode.FADE);

            _lerper = new ColorLerper()
                .Init(() => meshRenderer.material.color, (newColor) => meshRenderer.material.color = newColor,
                    endColor, durationInSeconds, startDelaySeconds);

            SetCoroutines();
        }
    }
}