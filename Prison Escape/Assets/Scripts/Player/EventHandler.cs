using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventHandler : MonoBehaviour
{
    // public event Action<float> OnInteractEvent;
    // public event Action<float> OnDropEvent;
    
    [SerializeField] private GameObject player; //이거는 플레이어 정보를 넘겨줄 필요가 있을 때에 필요해서
    [SerializeField] private PlayerController_TMP playerController;
    [SerializeField] private PlayerPickup playerPickup;
    private GameObject target;
    private IFocusable focusable = null;
    private IPickable pickable = null;
    private IUsable usable = null;

    private void Start()
    {
        CheckPlayer();
    }

    private void Update()
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            LeftClick();
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            RightClick();
        }
    }

    private void InvokeSwitch(GameObject target)
    {
        ActiveSwitch switchComponent = target.GetComponent<ActiveSwitch>();
        if (switchComponent == null)
        {
            Debug.Log("Switch component not found");
            switchComponent = target.AddComponent<ActiveSwitch>();
        }
                    
        switchComponent.TriggeerSwitch();
    }

    private void InvokeCauldron(GameObject target)
    {
        ActiveCauldron cauldronComponent = target.GetComponent<ActiveCauldron>();
        if (cauldronComponent == null)
        {
            Debug.Log("Cauldron component not found");
            cauldronComponent = target.AddComponent<ActiveCauldron>();
        }
        
        cauldronComponent.TriggerCauldron(player);
    }

    private void LeftClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            target = hit.collider.gameObject;
            
            focusable = target.GetComponent<IFocusable>();
            pickable = target.GetComponent<IPickable>();

            if (focusable != null)
            {
                focusable.Focus(player);
            }
            
            if (usable != null)
            {
                usable.Use(target);
            }
            
            if (pickable != null)
            {
                if (playerPickup.inHandItem != null)
                {
                    playerPickup.ChangeItem(target);
                }
                playerPickup.PickUp(pickable);
                usable = playerPickup.inHandItem.GetComponent<IUsable>();
                Debug.Log(usable);
            }
            
            if (target.CompareTag($"Switch"))
            {
                InvokeSwitch(target);
            }
            else if (target.CompareTag($"Cauldron"))
            {
                InvokeCauldron(target);
            }
            
        }
    }

    private void RightClick()
    {
        if (focusable != null)
        {
            focusable.UnFocus(player);
        }
        
    }

    private void CheckPlayer()
    {
        if (player == null)
        {
            Debug.Log("Player component not found");
            GameObject playerObj = GameObject.FindWithTag($"Player");
            
            if (playerObj != null)
            {
                player = playerObj;
            }
            
            else
            {
                Debug.LogError("플레이어가 없자나 태그 추가해");
                return;
            }
        }
    }
}
