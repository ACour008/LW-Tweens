using System;
using System.Collections;

public interface ILerper
{
    public bool IsComplete { get; set; }
    public bool IsPaused { get; set; }

    public IEnumerator Start();
}
