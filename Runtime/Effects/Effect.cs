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
        protected bool isMarkedForErasure;

        #region EventHandlers
        /// <summary>
        /// An EventHandler that is invoked when the effect is first executed.
        /// </summary>
        public event EventHandler OnEffectStarted;

        /// <summary>
        /// An EventHandler that is invoked after the effect's animations are done.
        /// </summary>
        public event EventHandler OnEffectCompleted;
        #endregion

        #region Fields
        /// <summary>
        /// Indicates whether the effect is playing and is not paused.
        /// </summary>
        public bool IsPlaying { get => !_lerper.IsComplete && !IsPaused; }

        /// <summary>
        /// Indicated whether the effect is currently paused.
        /// </summary>
        public bool IsPaused { get => _lerper.IsPaused; }

        /// <summary>
        /// Indicates whether the effect is marked to be destroyed. Usually after
        /// </summary>
        public bool IsMarkedForErasure { get => isMarkedForErasure; }
        #endregion

        #region MainAPI
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
        /// Stops the Effect's animations.
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to
        /// and drives all Effect coroutines.</param>
        public void Stop(MonoBehaviour owner)
        {
            owner.StopCoroutine(lerpCoroutine);

            OnEffectCompleted?.Invoke(this, EventArgs.Empty);
            isMarkedForErasure = true;
            _lerper.IsComplete = true;
        }
        #endregion

        #region Helpers
        private IEnumerator SendCompletedMessage()
        {
            yield return new WaitUntil(() => _lerper.IsComplete);

            isMarkedForErasure = true;
            OnEffectCompleted?.Invoke(this, EventArgs.Empty);
        }

        protected void SetCoroutines()
        {
            lerpCoroutine = _lerper.Start();
            stopCoroutine = SendCompletedMessage();
        }

        #endregion
    }

}
