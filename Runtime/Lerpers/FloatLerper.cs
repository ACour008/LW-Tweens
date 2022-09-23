using System.Collections;
using UnityEngine;

namespace Tweens.Lerpers
{
    /// <summary>
    /// A Lerper for any float-based Effects
    /// </summary>
    public class FloatLerper : Lerper<float>
    {
        /// <inheritdoc cref="Tweens.Lerpers.Lerper{T1}.Start"/>
        public override IEnumerator Start()
        {
            _isComplete = false;
            // _startValue = _getter();
            yield return _wait;

            float duration = _durationInSecs - 0.01f;

            while (_timeElapsed < duration)
            {
                float complete = _timeElapsed / duration;
                _setter(_startValue + (_endValue - _startValue) * Mathf.SmoothStep(0, 1, complete));

                _timeElapsed += Time.deltaTime;
                yield return null;
            }

            _setter(_endValue);
            _isComplete = true;
        }
    }
}