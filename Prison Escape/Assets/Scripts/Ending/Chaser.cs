using System.Collections;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField, Min(0.1f)] private float chaseTime = 1f;
    
    AudioSource audioSource;
    
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (startTransform != null)
        {
            transform.position = startTransform.position;
            transform.rotation = startTransform.rotation;
        }
    }
    
    public void Chase()
    {
        audioSource?.Play();
        if (startTransform != null && endTransform != null)
        {
            StartCoroutine(ChaseSequence());
        }
    }
    
    public void StopChase()
    {
        audioSource?.Stop();
        StopAllCoroutines();
    }

    private IEnumerator ChaseSequence()
    {
        float currentTime = 0f;
        while (currentTime < chaseTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, currentTime / chaseTime);
            transform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, currentTime / chaseTime);
        }
    }
}
