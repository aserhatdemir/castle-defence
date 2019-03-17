using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Rounds;
    public static float TotalTime;
    public static bool YouWon;
    public Text moneyText;

    public int startMoney = 100;
    public Text totalTimeText;


//    public int life = 10;
//    private float totalDamage;

    // Start is called before the first frame update
    void Start()
    {
        Money = startMoney;
        moneyText.text = "$" + Money.ToString();
        TotalTime = 0f;
        Rounds = 0;
        YouWon = false;
    }

    void Update()
    {
        if (GameManager.GameIsOver) return;
        TotalTime += Time.deltaTime;
        TotalTime = Mathf.Clamp(TotalTime, 0f, Mathf.Infinity);
        totalTimeText.text = "Total Time: " + string.Format("{0:00.0}", TotalTime);
    }

    public void UpdateMoneyTextUI()
    {
        moneyText.text = "$" + Money.ToString();
    }
}