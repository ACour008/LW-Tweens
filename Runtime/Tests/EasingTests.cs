using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class EasingTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void EasingExponentiateIsQuickerThanMathPow()
    {
        // Arrange
        int iterations = 3333333;

        // Act
        float powResult = PowTest(iterations);
        float expResult = ExponentiateTest(iterations);

        // Assert
        Assert.Less(expResult, powResult);
        UnityEngine.Debug.Log($"Exponentiate time (ms): {expResult}, Pow time (ms): {powResult}\nAfter {iterations} iterations.");
    }

    float PowTest(int iters)
    {
        var sw = Stopwatch.StartNew();
        float res = 0;
        for (int i = 0; i <= iters; i++)
        {
            res = Mathf.Pow(i, 30);
        }
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    float ExponentiateTest(int iters)
    {
        var sw = Stopwatch.StartNew();
        float res = 0;
        for (int i = 0; i <= iters; i++)
        {
            res = Exponentiate(i, 30);
        }
        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    // This is the same as the Exponentiate method in Easing
    // but placed here because the method is private.
    float Exponentiate(float base_, int exponent)
    {
        var result = 1f;
        while (exponent > 0)
        {
            if ((exponent & 1) == 0)
            {
                base_ *= base_;
                exponent >>= 1;
            }
            else
            {
                result *= base_;
                --exponent;
            }
        }
        return result;
    }
}
