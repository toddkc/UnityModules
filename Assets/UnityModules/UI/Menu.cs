using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text messageText = default;
    [SerializeField] private GameObject menuObject = default;
    [SerializeField] private GameObject continueButton = default;
    //[SerializeField] private string nextLevel = default;

    public static Menu instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowMenu()
    {
        menuObject.SetActive(true);
    }
    public void HideMenu()
    {
        menuObject.SetActive(false);
    }

    public void SetMessage(string message)
    {
        messageText.text = message;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowContinue()
    {
        continueButton.SetActive(true);
    }

    public void Continue()
    {
        //if (string.IsNullOrEmpty(nextLevel)) Restart();
        //else SceneManager.LoadScene(nextLevel);
        int buildindex = SceneManager.GetActiveScene().buildIndex;
        if(buildindex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(buildindex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
