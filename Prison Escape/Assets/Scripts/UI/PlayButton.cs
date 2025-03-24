using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    
    // 플레이 버튼 클릭 시 게임 씬으로 이동
    public void ButtonOnClick()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
