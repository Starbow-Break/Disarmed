using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLoading : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private CameraMove cameraMove;
    private IEnumerator Start()
    {
        playerMove.enabled = false;
        cameraMove.enabled = false;
        
        yield return new WaitForSeconds(0.1f); // 이거를 이제 오프닝 씬 이후에 하도록 하면..?
        
        playerMove.enabled = true;
        cameraMove.enabled = true;
        
    }
}
