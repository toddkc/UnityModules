using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] int buildIndex = default;
    Text text;

    private string LevelKey()
    {
        return "scene_" + buildIndex + "_besttime";
    }

    private void Start()
    {
        text = GetComponent<Text>();
        if(text == null)
        {
            text = GetComponentInChildren<Text>();
        }
        if (text == null) return;

        if (PlayerPrefs.HasKey(LevelKey()))
        {
            float timer = PlayerPrefs.GetFloat(LevelKey());
            float minutes = Mathf.Floor(timer / 60);
            float seconds = timer % 60;
            text.text = "Best Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            text.text = "Best Time: --";
        }
    }
}
