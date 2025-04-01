using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    
    public void LoadScene(string sceneName)
    {
        // StartCoroutine(FadeIn());
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Eye Camera",
            afterSwitch: () => StartGameScene());
            //afterSwitch: () => SceneManager.LoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        yield return fadePanel.FadeIn(fadeDuration);
    }

    // 이거는 게임 씬 안에서 시작할때 재생할거 여기서 테스트
    private void StartGameScene()
    {
        
    }
}
