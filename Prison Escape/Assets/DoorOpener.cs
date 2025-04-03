using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private List<Door> doors;
    [SerializeField] private KeyCode openKey;
    [SerializeField] private KeyCode closeKey;

    private void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            OpenDoor();
        }
        else if (Input.GetKeyDown(closeKey))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        foreach (Door door in doors)
        {
            door.Open();
        }
    }
    
    private void CloseDoor()
    {
        foreach (Door door in doors)
        {
            door.Close();
        }
    }
}
