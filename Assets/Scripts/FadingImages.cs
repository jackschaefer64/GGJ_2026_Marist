using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public CanvasGroup[] images; // Drag your CanvasGroups here
    public float fadeDuration = 1.0f;
    public float displayTime = 5.0f;

    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        foreach (CanvasGroup img in images)
        {
            // Fade In
            yield return StartCoroutine(Fade(img, 0, 1));
            // Wait
            yield return new WaitForSeconds(displayTime);
            // Fade Out
            yield return StartCoroutine(Fade(img, 1, 0));
        }
    }

    IEnumerator Fade(CanvasGroup cg, float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }
        cg.alpha = end;
    }
}

