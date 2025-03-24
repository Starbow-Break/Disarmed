using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveTrap : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject target;
    [SerializeField] private float trapTime = 0.5f;
    [SerializeField] private float waittime = 0.5f;
    public static event Action OnPlayerEnterTrap;
    public static event Action<GameObject> OnPlayerRespawn;
    
    private Vector3 targetPosition = new Vector3(0, 0.7f, 0);
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RaiseTrap(other.gameObject));
            OnPlayerEnterTrap?.Invoke();
            Debug.Log(other.gameObject);
        }
    }

    private IEnumerator RaiseTrap(GameObject player)
    {
        // 컨트롤러 비활성화 하고 활성화 할 동안 캐릭터 못움직이는거 구현 예정
        // 나중에 아래에서 활성화 할 예정, CharactorMove에서 비활성화 상태면 못움직이도록 예외처리 해야함
        PlayerMove move = player.GetComponent<PlayerMove>();
        if (move != null)
        {
            move.SetDirectionZero();
            move.enabled = false;
        }
        
        float elapsedTime = 0;
        Vector3 initialPosition = target.transform.localPosition;

        audioSource.Play();
        
        while (elapsedTime < trapTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / trapTime;
            target.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }
        
        target.transform.localPosition = targetPosition;
        
        yield return new WaitForSeconds(waittime);
        target.transform.localPosition = initialPosition;
        
        if (move != null)
        {
            move.enabled = true;
        }
        
        OnPlayerRespawn?.Invoke(player);
    }
}
