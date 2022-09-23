using UnityEngine.UI;

namespace Tweens.Utils
{
    public static class EffectUtils
    {
        /// <summary>
        /// Grabs the Graphic component from a Button object.
        /// </summary>
        /// <param name="button">The button to get the Graphic component from.</param>
        /// <returns>The Graphic component from the button's child GameObject
        /// (like a TMPro or Text Component.</returns>
        public static Graphic getGraphicFromButton(Button button)
        {
            return button.transform.GetChild(0).GetComponent<Graphic>();
        }
    }
}

