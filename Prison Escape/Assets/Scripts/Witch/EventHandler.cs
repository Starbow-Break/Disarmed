using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private GameObject player; //이거는 플레이어 정보를 넘겨줄 필요가 있을 때에 필요해서
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject target = hit.collider.gameObject;
                // Debug.Log(target.tag);
                
                DetermineGameobject.GameObjectType(target);

                if (target.CompareTag($"Switch"))
                {
                    EnvokeSwitch(target);
                }
                else if (target.CompareTag($"Cauldron"))
                {
                    EnvokeCauldron(target);
                }
                
            }
        }
    }

    private void EnvokeSwitch(GameObject target)
    {
        ActiveSwitch switchComponent = target.GetComponent<ActiveSwitch>();
        if (switchComponent == null)
        {
            Debug.Log("Switch component not found");
            switchComponent = target.AddComponent<ActiveSwitch>();
        }
                    
        switchComponent.TriggeerSwitch();
    }

    private void EnvokeCauldron(GameObject target)
    {
        ActiveCauldron cauldronComponent = target.GetComponent<ActiveCauldron>();
        if (cauldronComponent == null)
        {
            Debug.Log("Cauldron component not found");
            cauldronComponent = target.AddComponent<ActiveCauldron>();
        }

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
        cauldronComponent.TriggerCauldron(player);
    }
}
