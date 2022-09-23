using UnityEngine;
using UnityEngine.UI;
using Tweens.Lerpers;
using Tweens.Utils;

namespace Tweens
{
    /// <summary>
    /// Reduces the opacity of a GameObject based on the EffectData given to it.
    /// 
    /// Currently supports the following components:
    /// - CanvasGroup
    /// - Graphic
    /// - SpriteRenderer
    /// - Button
    /// - MeshRenderer (Note that this has not been tested with non-Standard Shader materials)
    /// </summary>
    public class Fade : Effect
    {
        public Fade(CanvasGroup canvasGroup, EffectData<float> data)
        {
            _lerper = new FloatLerper()
                .Init(() => canvasGroup.alpha, (x) => canvasGroup.alpha = x, data.EndValue, data.Duration, data.StartDelay);

            SetCoroutines();
        }

        public Fade(Graphic graphic, EffectData<float> data)
        {
            Color endColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, data.EndValue);

            _lerper = new ColorLerper()
                .Init(() => graphic.color, (x) => graphic.color = x, endColor, data.Duration, data.StartDelay);

            SetCoroutines();
        }

        public Fade(SpriteRenderer spriteRenderer, EffectData<float> data)
        {
            Color endColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, data.EndValue);

            _lerper = new ColorLerper()
                .Init(() => spriteRenderer.color, (x) => spriteRenderer.color = x, endColor, data.Duration, data.StartDelay);

            SetCoroutines();
        }

        public Fade(Button button, EffectData<float> data)
        {
            Graphic graphic = EffectUtils.getGraphicFromButton(button);

            Color endColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, data.EndValue);

            _lerper = new ColorLerper()
                .Init(() => graphic.color, (x) => graphic.color = x, endColor, data.Duration, data.StartDelay);

            SetCoroutines();
        }

        public Fade(MeshRenderer meshRenderer, EffectData<float> data)
        {
            Color meshColor = meshRenderer.material.color;
            Color endColor = new Color(meshColor.r, meshColor.g, meshColor.b, data.EndValue);

            StandardShaderUtils.ChangeRenderMode(meshRenderer.material, BlendMode.FADE);

            _lerper = new ColorLerper()
                .Init(() => meshRenderer.material.color, (newColor) => meshRenderer.material.color = newColor,
                    endColor, data.Duration, data.StartDelay);

            SetCoroutines();
        }
    }
}