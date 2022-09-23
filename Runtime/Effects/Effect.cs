using System;
using System.Collections;
using UnityEngine;

namespace Tweens
{
    /// <summary>
    /// The base class for all over Effects. Is abstract so cannot be instantiated.
    /// </summary>
    public abstract class Effect
    {
        protected ILerper _lerper;
        protected IEnumerator lerpCoroutine;
        protected IEnumerator stopCoroutine;

        /// <summary>
        /// An EventHandler that is invoked when the effect is first executed.
        /// </summary>
        public event EventHandler OnEffectStarted;

        /// <summary>
        /// An EventHandler that is invoked after the effect's animations are done.
        /// </summary>
        public event EventHandler OnEffectCompleted;

        /// <summary>
        /// Indicates whether the effect is playing and is not paused.
        /// </summary>
        public bool IsPlaying { get => !_lerper.IsComplete && !IsPaused; }

        /// <summary>
        /// Indicated whether the effect is currently paused.
        /// </summary>
        public bool IsPaused { get => _lerper.IsPaused; }

        /// <summary>
        /// Begins the animation of the Effect.
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to
        /// and drives all Effect coroutines.</param>
        public void Execute(MonoBehaviour owner)
        {
            OnEffectStarted?.Invoke(this, EventArgs.Empty);

            owner.StartCoroutine(stopCoroutine);
            owner.StartCoroutine(lerpCoroutine);
        }

        private IEnumerator SendCompletedMessage()
        {
            yield return new WaitUntil(() => _lerper.IsComplete);
            OnEffectCompleted?.Invoke(this, EventArgs.Empty);
        }

        protected void SetCoroutines()
        {
            lerpCoroutine = _lerper.Start();
            stopCoroutine = SendCompletedMessage();
        }

        /// <summary>
        /// Stops the Effect's animations.
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to
        /// and drives all Effect coroutines.</param>
        public void Stop(MonoBehaviour owner)
        {
            owner.StopCoroutine(lerpCoroutine);

            OnEffectCompleted?.Invoke(this, EventArgs.Empty);
            _lerper.IsComplete = true;
        }

        /// <summary>
        /// Pauses the effect.
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to
        /// and drives all Effect coroutines.</param>
        public void Pause(MonoBehaviour owner)
        {
            if (!_lerper.IsPaused) _lerper.IsPaused = true;
        }

        /// <summary>
        /// Continues the animation of the effect if was previously paused.
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to
        /// and drives all Effect coroutines.</param>
        public void Resume(MonoBehaviour owner)
        {
            if (_lerper.IsPaused) _lerper.IsPaused = false;
        }

        /// <summary>
        /// Resets the Effect's animation and promptly executes it again.
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to
        /// and drives all Effect coroutines.</param>
        public void Restart(MonoBehaviour owner)
        {
            _lerper.Restart();
            SetCoroutines();

            Execute(owner);
        }

        /// <summary>
        /// Resets the Effect's animations.
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to
        /// and drives all Effect coroutines.</param>
        /// <param name="keepOriginalStartValue">Determines whether the Effect's start value (ie., the value of the Owner upon
        /// the first execution) is kept when reseting the Effect. If true, the start value stays the same. If false,
        /// the start value will be updated according to the Owner.</param>
        public void Reset(MonoBehaviour owner, bool keepOriginalStartValue = false)
        {
            _lerper.Reset(keepOriginalStartValue);
            SetCoroutines();
        }
    }

}
