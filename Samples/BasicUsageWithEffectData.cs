using UnityEngine;
using Tweens;

public class BasicUsageWithEffectData : MonoBehaviour
{
    [SerializeField] private float durationInSeconds;
    [SerializeField] private float startDelaySeconds;
    [SerializeField] private Vector3 endTarget;

    private EffectBuilder effectBuilder;

    void Start()
    {
        effectBuilder = new EffectBuilder(this);

        // Acts as a container for all effect data if the data is set elsewhere.
        EffectData<Vector3> moveData = new EffectData<Vector3>(endTarget, durationInSeconds, startDelaySeconds);

        effectBuilder.AddEffect(new Move(transform, moveData))
            .ExecuteAll();
    }

}
