using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private InputActionReference pauseInput, continueInput;
    [SerializeField] private PlayerInput playerInput;

    public static bool isPaused { get; private set; } // 일시정지 상태

    private bool dirtyFlag;  // isPaused 요염 가능성
    private bool? beforeCursorLocked;

    private void OnEnable()
    {
        pauseInput.action.started += Pause;
        continueInput.action.started += Continue;
        beforeCursorLocked = null;
        SetPause(false);
    }
    
    private void OnDisable()
    {
        pauseInput.action.started -= Pause;
        continueInput.action.started -= Continue;
    }
    
    // 상태값이 오염됐다면 이에 맞춰 게임 상태를 변경해준다.
    void Update()
    {
        if (dirtyFlag)
        {
            UpdatePause();
        }
    }
    
    // 게임 일시정지
    private void Pause(InputAction.CallbackContext context)
    {
        SetPause(true);
    }

    private void Continue(InputAction.CallbackContext context)
    {
        SetPause(false);
    }
    
    // 일시정지 설정
    // 상태값만 변경하고 상태가 오염 됐음을 표시
    public void SetPause(bool value)
    {
        isPaused = value;
        dirtyFlag = true;
    }
    
    // 게임에 현재 일시정지 상태를 반영
    private void UpdatePause()
    {
        Time.timeScale = isPaused ? 0f : 1f;
        pauseUI.SetActive(isPaused);

        UpdateCursor();
        UpdateInputActionMap();
        
        dirtyFlag = false;
    }

    private void UpdateCursor()
    {
        if (isPaused)
        {
            beforeCursorLocked = CursorLocker.instance.isCursorlocked;
            Debug.Log(beforeCursorLocked);
            CursorLocker.instance.UnlockCursor();
        }
        else
        {
            if (beforeCursorLocked != null)
            {
                if (beforeCursorLocked == true)
                {
                    CursorLocker.instance.LockCursor();
                }
                else
                {
                    CursorLocker.instance.UnlockCursor();
                }
            }
        }
    }

    private void UpdateInputActionMap()
    {
        playerInput.SwitchCurrentActionMap(isPaused ? "UI" : "Player");
    }
}
