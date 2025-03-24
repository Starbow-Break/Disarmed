using UnityEngine;

public class StartWitchCollider : MonoBehaviour
{
    [SerializeField] StartWitch startWitch;
    private void OnTriggerEnter(Collider other)
    {
        startWitch.StartDialogue();
        gameObject.SetActive(false);
    }
}
