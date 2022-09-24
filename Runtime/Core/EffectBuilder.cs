using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tweens
{
    /// <summary>
    /// The animator class that controls all the Effects given to it.
    /// </summary>
    public class EffectBuilder
    {
        private List<Effect> effects = new List<Effect>();
        private bool effectsPlaying = false;
        private bool effectsPaused = false;
        private int coroutinesRunning = 0;

        #region Fields
        /// <summary>
        /// The MonoBehaviour object that the Effect Builder belongs to & drives all Effect coroutines.
        /// </summary>
        public MonoBehaviour Owner { get; private set; }
        /// <summary>
        /// Returns true if any of the Effects are playing.
        /// </summary>
        public bool EffectsPlaying { get => effectsPlaying; }
        /// <summary>
        /// Returns true if any Effects are paused.
        /// </summary>
        public bool EffectsPaused { get => effectsPaused; }
        /// <summary>
        /// Returns the total count of effects that have been added.
        /// </summary>
        public int CurrentAnimations { get => effects.Count; }
        /// <summary>
        /// Returns the total count of effects currently playing.
        /// </summary>
        public int AnimationsRunning { get => coroutinesRunning; }
        #endregion

        #region EventHandlers
        /// <summary>
        /// An EventHandler that is invoked when the last effect is finished playing.
        /// </summary>
        public event EventHandler OnExecutionCompleted;
        /// <summary>
        /// An EventHandler that is invoked when the first effect has started playing.
        /// </summary>
        public event EventHandler OnExecutionStarted;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectBuilder" class/>
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the EffectBuilder utilizes to run Effect animations.</param>
        public EffectBuilder(MonoBehaviour owner)
        {
            Owner = owner;
        }

        #region mainAPI
        /// <summary>
        /// Adds an Effect to the EffectBuilder
        /// </summary>
        /// <param name="effect"></param>
        /// <returns>The current EffectBuilder object</returns>
        /// <seealso cref="Tweens.Effect"/>
        public EffectBuilder AddEffect(Effect effect)
        {
            effects.Add(effect);
            RegisterEvents(effect);
            return this;
        }

        /// <summary>
        /// Adds an array of Effects.
        /// </summary>
        /// <param name="effects">An array full of Effects.</param>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder AddEffects(Effect[] effects)
        {
            for(int i = 0; i < effects.Length; i++)
            {
                AddEffect(effects[i]);
            }

            return this;
        }

        /// <summary>
        /// Runs every effect that has been added to the EffectBuilder. All effects are marked for detruction and removed
        /// after the have completed their animation.
        /// </summary>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder ExecuteAll()
        {
            coroutinesRunning = 0;
            OnExecutionStarted?.Invoke(this, EventArgs.Empty);

            foreach (Effect effect in effects)
            {
                effect.Execute(Owner);
                Owner.StartCoroutine(DestroyAfterCompletion(effect));
            }

            return this;
        }

        /// <summary>
        /// Pauses every effect that has been added to the EffectBuilder.
        /// </summary>
        /// <returns>The current EffectBuilder object</returns>
        public EffectBuilder PauseAll()
        {
            foreach (Effect effect in effects)
            {
                effect.Pause(Owner);
            }

            effectsPlaying = false;
            effectsPaused = true;

            return this;
        }

        /// <summary>
        /// Searches for and pauses the given Effect. If the effect is exists, it is not marked for destruction while paused.
        /// </summary>
        /// <param name="effect">The effect that was previously supplied to the EffectBuilder object.</param>
        /// <returns></returns>
        public EffectBuilder PauseEffect(Effect effect)
        {
            foreach (Effect eff in effects)
            {
                if (eff == effect) eff.Pause(Owner);
            }

            return this;
        }

        /// <summary>
        /// Resumes every effect that has been paused.
        /// </summary>
        /// <returns>The current EffectBuilder object</returns>
        public EffectBuilder ResumeAll()
        {
            foreach(Effect effect in effects)
            {
                effect.Resume(Owner);
            }

            return this;
        }

        /// <summary>
        /// Searches for and resumes the given Effect if it is paused.
        /// </summary>
        /// <param name="effect">An effect that was previously supplied to the EffectBuilder object.</param>
        /// <returns>The current EffectBuilder object</returns>
        public EffectBuilder ResumeEffect(Effect effect)
        {
            foreach(Effect eff in effects)
            {
                if (eff == effect) eff.Resume(Owner);
            }

            return this;
        }

        /// <summary>
        /// Stops every effect, marks them for destruction, and removes them from the EffectBuilder object.
        /// </summary>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder StopAll()
        {
            foreach (Effect effect in effects)
            {
                effect.Stop(Owner);
                effects.Remove(effect);
            }

            effectsPlaying = false;
            effectsPaused = false;

            return this;
        }

        /// <summary>
        /// Searches for the given effect and stops it if found. It is marked it for destruction and removed
        /// from the EffectBuilder object.
        /// </summary>
        /// <param name="effect">The effect that was previously supplied tot he EffectBuilder object.</param>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder StopEffect(Effect effect)
        {
            foreach (Effect eff in effects)
            {
                if (eff == effect)
                {
                    eff.Stop(Owner);
                    effects.Remove(eff);
                }
            }

            return this;
        }
        #endregion

        #region Events
        private void RegisterEvents(Effect effect)
        {
            effect.OnEffectStarted += Effect_OnEffectStarted;
            effect.OnEffectCompleted += Effect_OnEffectCompleted;
        }

        private void UnregisterEvents(Effect effect)
        {
            effect.OnEffectStarted -= Effect_OnEffectStarted;
            effect.OnEffectCompleted -= Effect_OnEffectCompleted;
        }

        private void Effect_OnEffectStarted(object sender, EventArgs e)
        {
            coroutinesRunning++;
            if (coroutinesRunning >= 1)
            {
                OnExecutionStarted?.Invoke(this, EventArgs.Empty);
                effectsPlaying = true;
            }
        }
        private void Effect_OnEffectCompleted(object sender, EventArgs e)
        {
            coroutinesRunning--;
            if (coroutinesRunning <= 0)
            {
                OnExecutionCompleted?.Invoke(this, EventArgs.Empty);
                effectsPlaying = false;
            }
        }
        #endregion

        #region Helpers
        private IEnumerator DestroyAfterCompletion(Effect effect)
        {
            yield return new WaitUntil(() => effect.IsMarkedForErasure);

            UnregisterEvents(effect);
            effects.Remove(effect);
        }
        #endregion
    }
}
