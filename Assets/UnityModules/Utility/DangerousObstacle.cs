using UnityEngine;

public class DangerousObstacle : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip = default;
    [SerializeField] private float volume = 1.0f;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            triggered = true;
            AudioPlayer.PlaySound(audioClip, transform.position, volume);
            //GameStateManager.ChangeState(GameState.PlayerDied);
        }
    }
}
