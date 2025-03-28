using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveTrap : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject target;
    [SerializeField] private float trapTime = 0.5f;
    [SerializeField] private float waittime = 0.5f;
    public static event Action<GameObject> OnPlayerTrapped;

    private Vector3 initialPosition;
    private static readonly Vector3 targetPosition = new Vector3(0, 0.7f, 0);

    private void Awake()
    {
        initialPosition = target.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TrapCoroutine(other.gameObject));
            audioSource.Play();
            DisableBoxCollider();
        }
    }

    private IEnumerator TrapCoroutine(GameObject player)
    {
        PlayerLoading.PlayerSetStop();
        {
            var elapsedTime = 0f;

            while (elapsedTime < trapTime)
            {
                elapsedTime += Time.deltaTime;
                var t = elapsedTime / trapTime;
                target.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
                yield return null;
            }
            yield return new WaitForSeconds(waittime);
            target.transform.localPosition = targetPosition;
            OnPlayerTrapped?.Invoke(player);
        }
        PlayerLoading.PlayerSetStart();
    }

    private void DisableBoxCollider()
    {
        boxCollider.enabled = false;
    }
}
