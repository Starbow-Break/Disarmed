using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource walkAudio;

    private void OnEnable()
    {
        PlayerMove.PlayerMoved += PlayWalkAudio;
    }

    private void OnDisable()
    {
        PlayerMove.PlayerMoved -= PlayWalkAudio;
    }

    private void PlayWalkAudio(bool isWalking)
    {
        if (!isWalking)
        {
            walkAudio.Stop();
            return;
        }
        
        walkAudio.Play();
    }

}
