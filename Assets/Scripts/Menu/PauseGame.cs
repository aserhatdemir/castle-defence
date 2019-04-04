using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public SceneFader sceneFader;

    private string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        
    }
    
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }


    public void Continue()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(sceneName);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo("Menu");
    }
}