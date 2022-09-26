using UnityEngine;
using Tweens;

public class BasicUsage : MonoBehaviour
{
    [SerializeField] private float durationInSeconds;
    [SerializeField] private float startDelaySeconds;
    [SerializeField] private Vector3 endTarget;

    private EffectBuilder effectBuilder;

    void Start()
    {
        effectBuilder = new EffectBuilder(this);

        EffectData<Vector3> effectData = new EffectData<Vector3>(endTarget, durationInSeconds, startDelaySeconds);

        effectBuilder.AddEffect(new Move(transform, effectData))
            .ExecuteAll();
    }


}
