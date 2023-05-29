using System.Collections;
using Tweens.Easing;
using UnityEngine;

namespace Tweens.Lerpers
{
    /// <summary>
    /// A lerper for any Vector2 or Vector3 based animations.
    /// </summary>
    public class Vector3Lerper : Lerper<Vector3>
    {
        /// <inheritdoc cref="Tweens.Lerpers.Lerper{T1}.Start"/>
        public override IEnumerator Start()
        {
            _isComplete = false;

            yield return _wait;

            float duration = _durationInSecs - 0.01f;

            while(_timeElapsed < duration)
            {
                float complete = _timeElapsed / duration;
                _setter(_startValue + (_endValue - _startValue) * _easing.Ease(complete, _smoothing));
                
                if (!_isPaused) _timeElapsed += Time.deltaTime;

                yield return null;
            }

            _setter(_endValue);
            _isComplete = true;
        }
    }
}
