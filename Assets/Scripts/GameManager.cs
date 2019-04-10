using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    public static bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    
    public PlayerStats playerStatsScript; 
    public WeaponManager weaponManagerScript;
    public UIManager uiManager;

    public String deviceType;
    
    

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

        deviceType = DetectDeviceType();
    }

    private String DetectDeviceType()
    {
        String dType = "Desktop"; 
        switch (SystemInfo.deviceType)
        {
            case DeviceType.Desktop:
                dType = "Desktop";
                break;
            case DeviceType.Handheld:
                dType = "Handheld";
                break;
        }

        return dType;
    }


    public void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        
    }
}