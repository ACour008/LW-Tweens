using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tweens.Utils
{
    /// <summary>
    /// An enum containing the BlendModes for a standard shader material.
    /// </summary>
    public enum BlendMode
    {
        OPAQUE,
        CUTOUT,
        FADE,
        TRANSPARENT
    }

    public static class StandardShaderUtils
    {
        /// <summary>
        /// Changes the Render Mode of a material with a standard shader.
        /// </summary>
        /// <param name="standardShaderMaterial">The material to change the render mode</param>
        /// <param name="blendMode">A Tweens.Util.BlendMode enum.</param>
        public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
        {
            switch(blendMode)
            {
                case BlendMode.OPAQUE:
                    ChangeRenderModeToOpaque(standardShaderMaterial);
                    break;
                case BlendMode.CUTOUT:
                    ChangeRenderModeToCutout(standardShaderMaterial);
                    break;
                case BlendMode.FADE:
                    ChangeRenderModeToFade(standardShaderMaterial);
                    break;
                case BlendMode.TRANSPARENT:
                    ChangeRenderModeToTransparent(standardShaderMaterial);
                    break;
                default:
                    throw new UnityException("Blend Mode not found");

            }
        }

        private static void ChangeRenderModeToOpaque(Material mat)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAMULTIPLY_ON");
            mat.renderQueue = -1;
            mat.SetOverrideTag("RenderType", "Opaque");
            mat.SetFloat("_Mode", 0);
        }

        private static void ChangeRenderModeToCutout(Material mat)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.EnableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAMULTIPLY_ON");
            mat.renderQueue = 2450;
            mat.SetOverrideTag("RenderType", "Cutout");
            mat.SetFloat("_Mode", 1);

        }

        private static void ChangeRenderModeToFade(Material mat)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAMULTIPLY_ON");
            mat.renderQueue = 3000;
            mat.SetOverrideTag("RenderType", "Fade");
            mat.SetFloat("_Mode", 2);
        }

        private static void ChangeRenderModeToTransparent(Material mat)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.EnableKeyword("_ALPHAMULTIPLY_ON");
            mat.renderQueue = 3000;
            mat.SetOverrideTag("RenderType", "Transparent");
            mat.SetFloat("_Mode", 3);
        }
    }

}