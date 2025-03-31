using UnityEngine;
using UnityEngine.InputSystem;

public class EventHandler : MonoBehaviour
{
    // public event Action<float> OnInteractEvent;
    // public event Action<float> OnDropEvent;
    
    [SerializeField] private GameObject player; //이거는 플레이어 정보를 넘겨줄 필요가 있을 때에 필요해서
    [SerializeField] private PlayerPickup playerPickup; // Player Pick Up
    [SerializeField] private GameObject playerUI;   // 플레이어 상호작용 UI
    [SerializeField, Min(1)] private float hitRange = 3.0f; // 상호작용 가능 범위
    [SerializeField] private LayerMask layerMask;   // 상호작용 가능한 레이어
    
    private GameObject target; // 에임에 들어온 오브젝트
    private IFocusable focusable = null;    // 현재 상호 작용중인 Focusable
    private IUsable usable = null;  // 현재 들고있는 Usable

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

    private void LeftClick()
    {
        // Focusable과 상호작용 중이라면 이후 작업을 하지 않는다.
        if (focusable != null)
        {
            return;
        }
        
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, hitRange, layerMask))
        {
            target = hit.collider.gameObject;
            Debug.Log(target.name);
            
            // 오브젝트의 타입 판별 (IPickable, IFocusable, IItemInteractable)
            IFocusable targetFocusable = target.GetComponent<IFocusable>();
            if(targetFocusable != null)
            {
                focusable = targetFocusable;
            }
            IPickable pickable = target.GetComponent<IPickable>();
            IItemInteractable itemInteractable = target.GetComponent<IItemInteractable>();
            
            // 상호작용하려는 오브젝트가 Focusable이면 포커싱한다.
            if (focusable != null)
            {
                playerUI.SetActive(false);
                focusable.Focus(player);
            }
            
            // 상호작용하려는 오브젝트가 IItemInteractable이면 들고 있는 아이템으로 오브젝트와 상호작용
            if (itemInteractable != null)
            {
                itemInteractable.InteractUseItem(gameObject, playerPickup.inHandItem);
            }
            
            // 상호작용하려는 오브젝트가 Pickable이면 집는다.
            if (pickable != null)
            {
                UsableIngredients ingredients = target.GetComponent<UsableIngredients>();
                
                // 기존 pickable, usable인 오브젝트에 반응할 것
                if (ingredients == null)
                {
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
