using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Rounds;
    public static float TotalTime;
    public static bool YouWon;

    public int startMoney = 100;

    private GameManager gameManager;


//    public int life = 10;
//    private float totalDamage;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        
        Money = startMoney;
        TotalTime = 0f;
        Rounds = 0;
        YouWon = false;
        gameManager.uiManager.UpdateMoneyTextUI();
        gameManager.uiManager.UpdateTotalTimeTextUI();
    }

    void Update()
    {
        if (GameManager.GameIsOver) return;
        TotalTime += Time.deltaTime;
        TotalTime = Mathf.Clamp(TotalTime, 0f, Mathf.Infinity);
        gameManager.uiManager.UpdateTotalTimeTextUI();
    }

    public void UpdateMoney(int m)
    {
        Money += m;
        gameManager.uiManager.UpdateMoneyTextUI();
    }
}