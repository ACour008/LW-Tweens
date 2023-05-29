using System;
using System.Collections.Generic;

namespace Tweens.Easing
{
    /// <summary>
    /// 
    /// </summary>
    public enum EaseType
    {
        LINEAR,
        SMOOTH_START,
        SMOOTH_END
    }

    public abstract class Easing : IEasing
    {
        public abstract float Ease(float t, int smoothing = 0);
        
        protected float Exponentiate(float t, int exp)
        {
            var result = 1f;
            while (exp > 0)
            {
                if ((exp & 1) == 0)
                {
                    t *= t;
                    exp >>= 1;
                }
                else
                {
                    result *= t;
                    --exp;
                }
            }
            return result;
        }
    }

    public sealed class Linear : Easing
    {
        public override float Ease(float t, int smoothing = 0) => t;
    }

    public sealed class SmoothIn : Easing
    {
        public override float Ease(float t, int smoothing = 2) => Exponentiate(t, smoothing);
    }

    public sealed class SmoothOut: Easing
    {
        public override float Ease(float t, int smoothing = 2) => 1 - Exponentiate(t - 1, smoothing);
    }
}
