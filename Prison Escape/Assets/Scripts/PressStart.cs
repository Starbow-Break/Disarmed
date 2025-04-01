using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    [SerializeField] private FadePanel fadePanel;
    [SerializeField] private float fadeDuration;
    
    private SceneLoader sceneLoader;
    
    public void StartButton(string sceneName)
    {
        CameraSwitcher.instance.SwitchCamera(
            cameraName: "Eye Camera",
            beforeSwitch: () => StartCoroutine(StartFadeIn()),
            afterSwitch: () => SceneManager.LoadScene(sceneName));
    }

    private IEnumerator StartFadeIn()
    {
        yield return fadePanel.FadeIn(fadeDuration);
    }
}
