using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [System.Serializable]
    public struct TeleportData
    {
        [field: SerializeField] 
        public KeyCode key { get; private set; }
        
        [field: SerializeField] 
        public Transform teleportPoint  { get; private set; }
    }

    [SerializeField] private List<TeleportData> teleportDatas;
    [SerializeField] private GameObject player;
    
    private void Update()
    {
        foreach (TeleportData data in teleportDatas)
        {
            if (Input.GetKeyDown(data.key))
            {
                Teleport(data.teleportPoint);
                return;
            }
        }
    }

    private void Teleport(Transform targetTransform)
    {
        CharacterController characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }
        
        player.transform.position = targetTransform.position;
        player.transform.rotation = targetTransform.rotation;
        
        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }
}
