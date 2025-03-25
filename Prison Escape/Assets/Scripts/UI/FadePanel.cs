using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    [SerializeField] private Image image;

    void Start()
    {
        image.color = new Color(
            image.color.r,
            image.color.g,
            image.color.b,
            0f
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
            
            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
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
            
            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                Mathf.Lerp(0f, 1f, currentTime / duration)
            );
        }
    }
}
