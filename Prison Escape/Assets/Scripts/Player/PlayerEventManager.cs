using UnityEngine;

public partial class PlayerEventManager : MonoBehaviour
{
    void Awake()
    {
        AddTrapManager();
        AddSaveManager();
    }
}
