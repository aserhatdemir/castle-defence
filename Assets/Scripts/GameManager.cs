using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager
        instance; //Static instance of GameManager which allows it to be accessed by any other script.

    //private int level = 3;                                  //Current level number, expressed in game as "Day 1".
    public static bool GameIsOver;
    public GameObject gameOverUI;
    public PlayerStats playerStatsScript; //Store a reference to our WeaponManager.
    public WeaponManager weaponManagerScript; //Store a reference to our WeaponManager.

    //Awake is always called before any Start functions
    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //Get a component reference to the attached WeaponManager script
        weaponManagerScript = GetComponent<WeaponManager>();
        playerStatsScript = GetComponent<PlayerStats>();

        GameIsOver = false;
    }


    public void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}