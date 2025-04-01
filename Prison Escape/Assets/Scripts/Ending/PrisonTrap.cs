using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PrisonTrap : MonoBehaviour
{
    [SerializeField] private GameObject prison;
    [SerializeField] private float openYPosition = 3.5f;
    [SerializeField] private float closeYPosition = 0.0f;
    [SerializeField] private float speed = 20.0f;
    
    [SerializeField] private AudioSource prisonSound;
    [SerializeField] private AudioClip closingClip;
    [SerializeField] private AudioClip closeClip;

    public bool isClosed { get; private set; }

    private void Start()
    {
        isClosed = false;
        
        prison.transform.localPosition = new(
            prison.transform.localPosition.x,
            openYPosition,
            prison.transform.localPosition.z);
    }

    public void Close()
    {
        StartCoroutine(CloseSequence());
    }

    private IEnumerator CloseSequence()
    {
        prisonSound.clip = closingClip;
        prisonSound.loop = true;
        prisonSound.Play();
        
        while (prison.transform.localPosition.y > closeYPosition)
        {
            prison.transform.Translate(speed * Time.deltaTime * Vector3.down);
            yield return null;
        }
        prisonSound.Stop();
        
        prison.transform.localPosition = new(
            prison.transform.localPosition.x,
            closeYPosition,
            prison.transform.localPosition.z);
        prisonSound.PlayOneShot(closeClip);
        isClosed = true;
    }
}
