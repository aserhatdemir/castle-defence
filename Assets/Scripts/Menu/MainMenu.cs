using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void PlayGame()
    {
        sceneFader.FadeTo("Campaigns");
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
 