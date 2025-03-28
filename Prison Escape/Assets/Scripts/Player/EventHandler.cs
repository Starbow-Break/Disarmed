using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventHandler : MonoBehaviour
{
    // public event Action<float> OnInteractEvent;
    // public event Action<float> OnDropEvent;
    
    [SerializeField] private GameObject player; //이거는 플레이어 정보를 넘겨줄 필요가 있을 때에 필요해서
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerPickup playerPickup;
    [SerializeField] private GameObject playerUI;
    [SerializeField, Min(1)] private float hitRange = 3.0f;
    [SerializeField] private LayerMask layerMask;
    private GameObject target;
    private UsableIngredients ingredients = null;
    private IFocusable focusable = null;
    private IPickable pickable = null;
    private IUsable usable = null;

    private void Start()
    {
        CheckPlayer();
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
        
        if (focusable == null && Physics.Raycast(ray, out RaycastHit hit, hitRange, layerMask))
        {
            target = hit.collider.gameObject;
            Debug.Log(target.name);
            
            ingredients = target.GetComponent<UsableIngredients>();
            IFocusable focusableTemp = target.GetComponent<IFocusable>();
            if(focusableTemp != null)
            {
                focusable = focusableTemp;
            }
            pickable = target.GetComponent<IPickable>();

            if (focusable != null)
            {
                playerUI.SetActive(false);
                focusable.Focus(player);
            }
            
            if (usable != null)
            {
                usable.Use(gameObject, target);
            }
            else
            {
                IItemInteractable itemInteractable = target.GetComponent<IItemInteractable>();
                if (itemInteractable != null)
                {
                    itemInteractable.InteractUseItem(gameObject, null);
                }
            }
            
            if (pickable != null)
            {
                // 기존 pickable, usable인 오브젝트에 반응할 것
                if (ingredients == null)
                {
                    // 스크롤 끼리 교환을 구현하고 싶어서 냅둔 것 지울 수도 있음
                    if (playerPickup.inHandItem != null)
                    {
                        // playerPickup.ChangeItem(target);
                    }

                    playerPickup.PickUp(pickable);
                    usable = playerPickup.inHandItem.GetComponent<IUsable>();
                    Debug.Log(usable);
                }
                
                // 재료의 경우 복사해서 가져오고 싶어서 따로 뺌
                else
                {
                    if (playerPickup.inHandItem != null)
                    {
                        playerPickup.DestroyinHandItem();
                    }
                    playerPickup.CopyItem(target);
                    usable = playerPickup.inHandItem.GetComponent<IUsable>();
                    Debug.Log(usable);
                }
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
            playerUI.SetActive(true);
            focusable.UnFocus(player);
            focusable = null;
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

    public void SetNullUsable()
    {
        usable = null;
    }
}
