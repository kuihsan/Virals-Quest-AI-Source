using System;
using System.Collections;
using UnityEngine;

/// <summary>
///     This class is used to control the fade in and fade out animations of a CanvasGroup.
/// </summary>
public class FadeCanvas : MonoBehaviour
{
    // Current alpha value of the CanvasGroup
    private float _currentAlpha;

    // Event called when the Active Fade is completed.
    public Action OnCurrentFadeCompleted;

    /// <summary>
    ///     Starts the fade in animation for the given CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to fade in.</param>
    /// <param name="duration">The duration of the fade in animation.</param>
    public void StartFadeIn(CanvasGroup canvasGroup, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn(canvasGroup, duration));
    }

    /// <summary>
    ///     Starts the fade out animation for the given CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to fade out.</param>
    /// <param name="duration">The duration of the fade out animation.</param>
    public void StartFadeOut(CanvasGroup canvasGroup, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(canvasGroup, duration));
    }

    /// <summary>
    ///     Starts a sequence of fade in and fade out animations with a gap in between for the given CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to animate.</param>
    /// <param name="fadeInDuration">The duration of the fade in animation.</param>
    /// <param name="fadeOutDuration">The duration of the fade out animation.</param>
    /// <param name="gapDuration">The duration of the gap between the fade in and fade out animations.</param>
    public void StartFadeInFadeOutWithGap(CanvasGroup canvasGroup, float fadeInDuration, float fadeOutDuration,
        float gapDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInFadeOutWithGap(canvasGroup, fadeInDuration, fadeOutDuration, gapDuration));
    }

    /// <summary>
    ///     Starts a sequence of fade out and fade in animations with a gap in between for the given CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to animate.</param>
    /// <param name="fadeInDuration">The duration of the fade in animation.</param>
    /// <param name="fadeOutDuration">The duration of the fade out animation.</param>
    /// <param name="gapDuration">The duration of the gap between the fade out and fade in animations.</param>
    public void StartFadeOutFadeInWithGap(CanvasGroup canvasGroup, float fadeInDuration, float fadeOutDuration,
        float gapDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutFadeInWithGap(canvasGroup, fadeInDuration, fadeOutDuration, gapDuration));
    }

    /// <summary>
    ///     Sets the alpha value of the given CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to set the alpha value for.</param>
    /// <param name="value">The alpha value to set.</param>
    private void SetAlpha(CanvasGroup canvasGroup, float value)
    {
        _currentAlpha = Mathf.Clamp01(value);
        canvasGroup.alpha = _currentAlpha;
    }

    /// <summary>
    ///     Coroutine for the fade in animation.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to fade in.</param>
    /// <param name="duration">The duration of the fade in animation.</param>
    private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0.0f;

        // Disable raycasts during fade-in
        canvasGroup.blocksRaycasts = false;

        // Gradually increase alpha from 0 to 1
        while (elapsedTime < duration)
        {
            SetAlpha(canvasGroup, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(canvasGroup, 1.0f);  // Ensure alpha is set to 1 at the end

        // Enable raycasts after fade-in is complete
        canvasGroup.blocksRaycasts = true;

        OnCurrentFadeCompleted?.Invoke();
    }

    /// <summary>
    ///     Coroutine for the fade out animation.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to fade out.</param>
    /// <param name="duration">The duration of the fade out animation.</param>
    private IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0.0f;

        // Disable raycasts during fade-out
        canvasGroup.blocksRaycasts = false;

        // Gradually decrease alpha from 1 to 0
        while (elapsedTime < duration)
        {
            SetAlpha(canvasGroup, 1 - (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(canvasGroup, 0.0f);  // Ensure alpha is set to 0 at the end

        // Enable raycasts after fade-out is complete
        canvasGroup.blocksRaycasts = false;

        OnCurrentFadeCompleted?.Invoke();
    }

    /// <summary>
    ///     Coroutine for a sequence of fade in and fade out animations with a gap in between.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to animate.</param>
    /// <param name="fadeInDuration">The duration of the fade in animation.</param>
    /// <param name="fadeOutDuration">The duration of the fade out animation.</param>
    /// <param name="gapDuration">The duration of the gap between the fade in and fade out animations.</param>
    private IEnumerator FadeInFadeOutWithGap(CanvasGroup canvasGroup, float fadeInDuration, float fadeOutDuration,
        float gapDuration)
    {
        yield return FadeIn(canvasGroup, fadeInDuration);

        // Pause for a specified gap duration
        yield return new WaitForSeconds(gapDuration);

        yield return FadeOut(canvasGroup, fadeOutDuration);

        OnCurrentFadeCompleted?.Invoke();
    }

    /// <summary>
    ///     Coroutine for a sequence of fade out and fade in animations with a gap in between.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to animate.</param>
    /// <param name="fadeInDuration">The duration of the fade in animation.</param>
    /// <param name="fadeOutDuration">The duration of the fade out animation.</param>
    /// <param name="gapDuration">The duration of the gap between the fade out and fade in animations.</param>
    private IEnumerator FadeOutFadeInWithGap(CanvasGroup canvasGroup, float fadeInDuration, float fadeOutDuration,
        float gapDuration)
    {
        yield return FadeOut(canvasGroup, fadeOutDuration);

        // Pause for a specified gap duration
        yield return new WaitForSeconds(gapDuration);

        yield return FadeIn(canvasGroup, fadeInDuration);

        OnCurrentFadeCompleted?.Invoke();
    }
}
