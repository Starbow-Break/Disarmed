using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorLocker : MonoBehaviour
{
    public static CursorLocker instance;
    public bool isCursorlocked { get; private set; }

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            LockCursor();
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDisable()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void LockCursor()
    {
        isCursorlocked = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
#if UNITY_EDITOR
        EditorWindow gameWindow = UnityEditor.EditorWindow.GetWindow(typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.GameView"));
        gameWindow.Focus();
        gameWindow.SendEvent(new Event
        {
            button = 0,
            clickCount = 1,
            type = EventType.MouseDown,
            mousePosition = gameWindow.rootVisualElement.contentRect.center
        });
#endif
    }

    public void UnlockCursor()
    {
        isCursorlocked = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}