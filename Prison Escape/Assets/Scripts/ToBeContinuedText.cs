using System.Collections;
using TMPro;
using UnityEngine;

public class ToBeContinuedText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.color = new Color(
            textMeshPro.color.r,
            textMeshPro.color.g,
            textMeshPro.color.b,
            0.0f
        );
    }
    
    public Coroutine FadeOut(float duration)
    {
        return StartCoroutine(FadeOutSequence(duration));
    }
    
    public Coroutine FadeIn(float duration)
    {
        return StartCoroutine(FadeInSequence(duration));
    }

    IEnumerator FadeOutSequence(float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            yield return null;
            
            currentTime += Time.deltaTime;
            
            textMeshPro.color = new Color(
                textMeshPro.color.r,
                textMeshPro.color.g,
                textMeshPro.color.b,
                Mathf.Lerp(1f, 0f, currentTime / duration)
            );
        }
    }

    IEnumerator FadeInSequence(float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            yield return null;
            
            currentTime += Time.deltaTime;
            
            textMeshPro.color = new Color(
                textMeshPro.color.r,
                textMeshPro.color.g,
                textMeshPro.color.b,
                Mathf.Lerp(0f, 1f, currentTime / duration)
            );
        }
    }
}
