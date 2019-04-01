using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    public static bool GameIsOver;
    public GameObject gameOverUI;
    public PlayerStats playerStatsScript; 
    public WeaponManager weaponManagerScript;
    public UIManager uiManager;
    
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        weaponManagerScript = GetComponent<WeaponManager>();
        playerStatsScript = GetComponent<PlayerStats>();
        uiManager = GetComponent<UIManager>();
        GameIsOver = false;
    }


    public void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}