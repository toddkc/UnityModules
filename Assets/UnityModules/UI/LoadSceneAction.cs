using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneAction : MonoBehaviour, IActionable
{
    [SerializeField] private int sceneIndex = default;

    public void Do(UIButton doButton)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
