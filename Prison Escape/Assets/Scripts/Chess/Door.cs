using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Open()
    {
        animator.SetTrigger("Open");
        audioSource.Play();
    }
}
