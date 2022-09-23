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

        /// <summary>
        /// An EventHandler that is invoked when the last effect is finished playing.
        /// </summary>
        public event EventHandler OnExecutionCompleted;

        /// <summary>
        /// An EventHandler that is invoked when the first effect has started playing.
        /// </summary>
        public event EventHandler OnExecutionStarted;

        /// <summary>
        /// The constructor of EffectBuilder
        /// </summary>
        /// <param name="owner">The MonoBehaviour object that the Effect Builder belongs to & drives all Effect coroutines.</param>
        public EffectBuilder(MonoBehaviour owner)
        {
            Owner = owner;
        }

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
        /// Removes all Effects from the EffectBuilder.
        /// </summary>
        public void ClearAllEffects()
        {
            Owner.StartCoroutine(ClearAfterCompletion());
        }

        private IEnumerator ClearAfterCompletion()
        {
            yield return new WaitUntil( () => coroutinesRunning == 0);

            foreach (Effect effect in effects)
            {
                UnregisterEvents(effect);
            }

            effects.Clear();

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

        /// <summary>
        /// Runs every effect that has been added to the EffectBuilder.
        /// </summary>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder ExecuteAll()
        {
            coroutinesRunning = 0;
            OnExecutionStarted?.Invoke(this, EventArgs.Empty);

            foreach (Effect effect in effects)
            {
                effect.Execute(Owner);
            }

            return this;
        }

        /// <summary>
        /// Stops every effect that has been added to the EffectBuilder.
        /// </summary>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder StopAll()
        {
            foreach (Effect effect in effects)
            {
                effect.Stop(Owner);
            }

            effectsPlaying = false;
            effectsPaused = false;

            return this;
        }

        public EffectBuilder StopEffect(Type effectType)
        {
            foreach(Effect effect in effects)
            {
                if (effect.GetType() == effectType)
                {
                    effect.Stop(Owner);
                }
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
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public EffectBuilder PauseEffect(Type eventType)
        {
            foreach(Effect effect in effects)
            {
                if (effect.GetType() == eventType)
                {
                    effect.Pause(Owner);
                }
            }

            return this;
        }

        /// <summary>
        /// Resets every effect that has been added to the EffectBuilder.
        /// </summary>
        /// <param name="keepOriginalStartValue">Determines whether the Effect's start value (ie., the value of the Owner upon
        /// the first execution) is kept when reseting the Effect. If true, the start value stays the same. If false,
        /// the start value will be updated according to the Owner.</param>
        /// <returns>The current EffectBuilder object</returns>
        public EffectBuilder ResetAll(bool keepOriginalStartValue = false)
        {
            foreach(Effect effect in effects)
            {
                effect.Reset(Owner, keepOriginalStartValue);
            }

            return this;
        }

        /// <summary>
        /// Resets the specified Effect type.
        /// </summary>
        /// <param name="effectType">The System.Type of Effect(s) that should be reset.</param>
        /// <param name="keepOriginalStartValue">Determines whether the Effect's start value (ie., the value of the Owner upon
        /// the first execution) is kept when reseting the Effect. If true, the start value stays the same. If false,
        /// the start value will be updated according to the Owner.</param>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder ResetEffect(Type effectType, bool keepOriginalStartValue = false)
        {
            foreach(Effect effect in effects)
            {
                if (effect.GetType() == effectType)
                {
                    effect.Reset(Owner, keepOriginalStartValue);
                }
            }

            return this;
        }

        /// <summary>
        /// Continues all Effects if they were paused.
        /// </summary>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder ResumeAll()
        {
            foreach (Effect effect in effects)
            {
                effect.Resume(Owner);
            }

            effectsPaused = false;
            effectsPlaying = true;

            return this;
        }
        /// <summary>
        /// Resumes the specified type of Event(s) if paused.
        /// </summary>
        /// <param name="effectType">The System.Type of Effect(s) to be paused.</param>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder ResumeEvent(Type effectType)
        {
            return this;
        }

        /// <summary>
        /// Resets all Effects and immediately executes them.
        /// </summary>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder RestartAll()
        {
            foreach (Effect effect in effects)
            {
                effect.Restart(Owner);
            }

            return this;
        }

        /// <summary>
        /// Restarts the specified type of Event(s) and immediately executes it.
        /// </summary>
        /// <param name="eventType">The System.Type of Effect(s) to be restarted.</param>
        /// <returns>The current EffectBuilder object.</returns>
        public EffectBuilder RestartEvent(Type eventType)
        {
            foreach(Effect effect in effects)
            {
                if (effect.GetType() == eventType)
                {
                    effect.Restart(Owner);
                }
            }

            return this;
        }

        private void RegisterEvents(Effect effect)
        {
            Debug.Log("registered effect: " + effect);
            effect.OnEffectStarted += Effect_OnEffectStarted;
            effect.OnEffectCompleted += Effect_OnEffectCompleted;
        }

        private void UnregisterEvents(Effect effect)
        {
            effect.OnEffectStarted -= Effect_OnEffectStarted;
            effect.OnEffectCompleted -= Effect_OnEffectCompleted;
        }
    }
}
