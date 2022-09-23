﻿using System;
using System.Collections;

public interface ILerper
{
    public Type LerpType { get; }
    public bool IsComplete { get; set; }
    public bool IsPaused { get; set; }

    public IEnumerator Start();
    public void Restart();
    public void Reset(bool keepOriginalStartValue = false);
}
