using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLoading : MonoBehaviour
{
    public static PlayerLoading Instance { get; private set;}
    
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private CameraMove cameraMove;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    private void Start()
    {
        LoadDelay();
    }

    public static void LoadDelay()
    {
        Instance.StartCoroutine(Instance.LoadCoroutine());
    }

    public static void PlayerSetStop()
    {
        Instance.characterController.enabled = false;
        Instance.playerMove.enabled = false;
        Instance.cameraMove.enabled = false;
    }
    
    public static void PlayerSetStart()
    {
        Instance.characterController.enabled = true;
        Instance.playerMove.enabled = true;
        Instance.cameraMove.enabled = true;
    }

    private IEnumerator LoadCoroutine()
    {
        PlayerSetStop();
        
        yield return new WaitForSeconds(0.1f); // 이거를 이제 오프닝 씬 이후에 하도록 하면..?

        PlayerSetStart();
    }
}
