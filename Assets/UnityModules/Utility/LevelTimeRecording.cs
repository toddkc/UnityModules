using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimeRecording : MonoBehaviour
{
    float timer = 0.0f;
    public bool isRunning { get; set; }

    public static LevelTimeRecording instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!isRunning) return;
        timer += Time.deltaTime;
    }

    private string LevelKey()
    {
        return "scene_" + SceneManager.GetActiveScene().buildIndex + "_besttime";
    }

    public void UpdateSavedTime()
    {
        if (PlayerPrefs.HasKey(LevelKey()))
        {
            float previousBest = PlayerPrefs.GetFloat(LevelKey());
            if (timer < previousBest || previousBest == 0.0f)
            {
                PlayerPrefs.SetFloat(LevelKey(), timer);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(LevelKey(), timer);
        }
    }
}
