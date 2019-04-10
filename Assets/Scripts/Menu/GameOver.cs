using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public Text totalTimeText;
    public Text winLoseText;
    public SceneFader sceneFader;

    private string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnEnable()
    {
        winLoseText.text = PlayerStats.YouWon ? "YOU WON!" : "YOU LOST!";
        roundsText.text = "Rounds: " + PlayerStats.Rounds;
        totalTimeText.text = "Time: " + $"{PlayerStats.TotalTime:00.0}";
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