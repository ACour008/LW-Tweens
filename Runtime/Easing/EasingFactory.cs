using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tweens.Easing
{
    public static class EasingFactory
    {
        private static Dictionary<EaseType, IEasing> easingFunctions = new Dictionary<EaseType, IEasing>()
        {
            {EaseType.LINEAR, new Linear()},
            {EaseType.SMOOTH_END, new SmoothOut()},
            {EaseType.SMOOTH_START, new SmoothIn()}
        };

        public static IEasing Get(EaseType easingType)
        {
            return easingFunctions[easingType];
        }
    }

}