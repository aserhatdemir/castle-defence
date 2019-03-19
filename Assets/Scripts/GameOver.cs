using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public Text totalTimeText;
    public Text winLoseText;

    private void OnEnable()
    {
        winLoseText.text = PlayerStats.YouWon ? "YOU WON!" : "YOU LOST!";
        roundsText.text = "Rounds: " + PlayerStats.Rounds;
        totalTimeText.text = "Time: " + string.Format("{0:00.0}", PlayerStats.TotalTime);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}