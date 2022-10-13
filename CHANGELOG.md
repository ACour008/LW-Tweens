# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keeyp a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
This section will be utilized when the project is released and will contain information for upcoming features and changes.

## [0.2.0-alpha] - Oct 13, 2022
### Added
- IEasing interface: For extending easing functions.
- Easing functions: Current ones include linear, SmoothIn & SmoothOut.
- Easing Options: contains the type of easing function and its smoothing exponent.
- Easing factory: caches all easing functions and retries easing functions with a Get method. 

### Updated
- All effects can now have easing options injected into them via their constructor.
- All lerpers also have easing functions injected into them via the Effect class.

## [0.1.1-alpha] - Sept 26, 2022
### Added
- All effects are automatically removed from the EffectBuilder once they are executed. This cleans up the main API and allows the user to maintain the the same EffectBuilder for new Effects.
- A basic usage sample in the Samples folder.

### Removed
- Restart, Reset and Clear methods in Effect & EffectBuilders due to the added features.
- Reset and Restart also removed from Lerpers.

### Fixed
- The resume method of EffectBuilder is now given the proper MonoBehaviour owner.
- ILerp interface is updated to reflect changes in Lerpers.

### Updated
- The README is now updated.


## [0.0.1-alpha] - Sept 23, 2022
### Added
- CHANGELOG.md for which all all changes to this project will be recorded.
- README.md only has the title. A full README will be authored shortly
- All project files in Unity's package layout. Files include 5 effects, 3 lerpers and the animation controller that will likely be renamed in the future.

### Fixed
- typo in package.json so projects can properly resolve this package.




[Unreleased]:
[0.1.1] https://github.com/ACour008/Tween-Daddy/releases/tag/v.0.1.1-alpha
[0.0.1-alpha] https://github.com/ACour008/Tween-Daddy/releases/tag/v.0.0.1-alpha