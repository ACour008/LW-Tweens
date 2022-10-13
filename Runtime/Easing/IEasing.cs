using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEasing
{
    public float Ease(float t, int smoothing = 0);
}
