using System;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    private GameObject savepointNow = null;

    private void OnEnable()
    {
        ActiveSavepoint.OnPlayerEnterSavepoint += TriggerSavepoint;
        ActiveTrap.OnPlayerTrapped += PlayerRespawn;
    }
    
    private void OnDisable()
    {
        ActiveSavepoint.OnPlayerEnterSavepoint -= TriggerSavepoint;
        ActiveTrap.OnPlayerTrapped += PlayerRespawn;
    }
    
    private void TriggerSavepoint(GameObject savepoint)
    {
        if (savepointNow == null)
        {
            savepointNow = savepoint;
            savepoint.SetActive(false);
        }
        if (savepoint != null && savepointNow != savepoint)
        {
            savepointNow.SetActive(true);
            savepointNow = savepoint;
            savepoint.SetActive(false);
        }
    }

    private void PlayerRespawn(GameObject player)
    {
        if (savepointNow == null)
        {
            Debug.LogWarning("SavepointNow가 설정되지 않음. 기본 위치로 리스폰 불가능!");
            return;
        }
        
        player.transform.position = savepointNow.transform.position;
        Debug.Log(player.transform.position);
    }
}
