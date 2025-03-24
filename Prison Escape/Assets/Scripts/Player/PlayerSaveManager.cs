using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    // ActiveSavepoint를 할당한 오브젝트가 여기에 들어갈 예정
    private GameObject savepointNow = null;
    
    // 이벤트 활성화
    private void OnEnable()
    {
        ActiveSavepoint.OnPlayerEnterSavepoint += TriggerSavepoint;
        ActiveTrap.OnPlayerRespawn += PlayerRespawn;
    }

    // 이벤트 비활성화
    private void OnDisable()
    {
        ActiveSavepoint.OnPlayerEnterSavepoint -= TriggerSavepoint;
        ActiveTrap.OnPlayerRespawn += PlayerRespawn;
    }

    
    /*
     * 플레이어가 ActiveSavepoint가 할당된 오브젝트에 다가가서
     * OnTriggerEnter가 활성화 되었을때에 해당 메소드가 실행
     */
    private void TriggerSavepoint(GameObject savepoint)
    {
        if (savepointNow == null)
        {
            Debug.LogWarning("Trigger save point is null");
            savepointNow = savepoint;
            savepoint.SetActive(false);
        }
        if (savepoint != null && savepointNow != savepoint)
        {
            Debug.LogWarning("Savepoint Changed");
            savepointNow.SetActive(true);
            savepointNow = savepoint;
            savepoint.SetActive(false);
        }
    }

    /*
     * 플레이어가 ActiveTrap 할당된 오브젝트에 다가가서
     * OnTriggerEnter가 활성화 되었을때에 해당 메소드가 실행
     */
    private void PlayerRespawn(GameObject player)
    {
        if (savepointNow == null)
        {
            Debug.LogWarning("SavepointNow가 설정되지 않음. 기본 위치로 리스폰 불가능!");
            return;
        }
        Debug.Log($"{player.name} respawn at {savepointNow.name}");
        
        Vector3 respawnPosition = new Vector3(savepointNow.transform.position.x, savepointNow.transform.position.y, savepointNow.transform.position.z);
        Debug.Log($"{respawnPosition}");
        
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            player.transform.position = respawnPosition;
            controller.enabled = true;
        }
    }
}
