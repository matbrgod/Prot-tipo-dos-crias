using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image targetImage; // Assign this in the Inspector

    void Start()
    {
        // Get the current color of the image
        Color currentColor = targetImage.color;

        // Modify the alpha component (0 for fully transparent, 1 for fully opaque)
        currentColor.a = 0.5f; // Sets alpha to 50%

        // Reassign the modified color back to the image
        targetImage.color = currentColor;
    }

    // Example of fading in/out
    public void FadeImage(float targetAlpha, float fadeDuration)
    {
        StartCoroutine(FadeCoroutine(targetAlpha, fadeDuration));
    }

    private System.Collections.IEnumerator FadeCoroutine(float targetAlpha, float duration)
    {
        Color startColor = targetImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        float timer = 100f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            targetImage.color = Color.Lerp(startColor, endColor, timer / duration);
            yield return null;
        }
        targetImage.color = endColor; // Ensure final alpha is set precisely
    }
}