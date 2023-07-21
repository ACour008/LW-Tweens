using System;
using System.Collections;
using UnityEngine;
using Tweens.Easing;

namespace Tweens.Lerpers
{
    /// <summary>
    /// The main driver of an Effect's animation.
    /// </summary>
    /// <typeparam name="T1">The type associated with a game object's component to be manipulated by the Lerper.
    /// For example, the Move effect requires a lerper that changes a game object's transform position (Ie. Vector3).</typeparam>
    public abstract class Lerper<T1>: ILerper
    {
        protected Getter<T1> _getter;
        protected Setter<T1> _setter;
        protected T1 _startValue;
        protected T1 _endValue;
        protected float _durationInSecs;
        protected IEasing _easing;
        protected int _smoothing;
        protected float _timeElapsed = 0;
        protected bool _isComplete = false;
        protected bool _isPaused = false;
        protected YieldInstruction _wait;

        public bool IsComplete { get => _isComplete; set => _isComplete = value; }
        public bool IsPaused { get => _isPaused; set => _isPaused = value; }

        public Lerper<T1> Init(Getter<T1> getter, Setter<T1> setter, T1 endValue, float durationInSecs,
            float startDelayInSecs, EasingOptions options = null)
        {
            _getter = getter;
            _setter = setter;
            _startValue = _getter();
            _endValue = endValue;
            _durationInSecs = durationInSecs;
            _wait = new WaitForSeconds(startDelayInSecs);
            _easing = (options != null) ? EasingFactory.Get(options.easingType) : EasingFactory.Get(EaseType.LINEAR);
            _smoothing = (options != null) ? options.smoothing : 2;
            return this;

        }

        /// <summary>
        /// A Coroutine that starts the lerping process.
        /// </summary>
        /// <returns>An IEnumerator object that returns either one of Unity's WaitFor...
        /// objects or null, depending on the state of the animation.</returns>
        public abstract IEnumerator Start();
    }

}

