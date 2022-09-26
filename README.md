# Tween Daddy
### A lightweight tween animation library for Unity3D


## Overview
Tween Daddy provides users a quick, useful way to animate your 2D & 3D GameObjects in Unity by building Effect objects and feeding them into the EffectBuilder. Building an Effect is straightforward: give it an end value, the duration of the animation, and the start delay time. Tween Daddy does the rest.

## Package Contents
The library is located in the Runtime folder while the basic usage sample can be found in the Samples folder.

## Requirements
As tested so far, Tween-Daddy runs on Unity version 2021.3. Tests on other versions of Unity will be documented as development continues.

## Installation Instructions
You can use install the Tween Daddy package in your Unity project through Github in two ways.
1. **The Package Manager**: In your Unity Project, open the Package Manager (Window > Package Manager) and choosing "Add Package from git URL" in the '+' dropdown menu that is located in the top-left of the Package Manager window. See [Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html) in Unity Documentation for more info.

2. **Manifest.json**: Using your computer's file manager (e.g., Windows Explorer or macOS Finder), navigate to the manifest.json file, located in *Your-Projects-Name*/Packages. Open the file in your favorite text editor, and add the following in first line of the "dependencies" entry:
 >`"com.miisko-wiiyaas.tween-daddy": "https://github.com/ACour008/Tween-Daddy.git"`

 **Remember**: add a comma after you enter the package information to avoid any errors during start up.

## How to Use Tween Daddy
`Samples/BasicUsage.cs`

```C#
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

```
More samples coming soon.

## Contributing to the project
There is a need for testing so if you would like to contribute, feel free to reach out.