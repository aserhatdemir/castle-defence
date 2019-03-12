using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public WeaponManager weaponManagerScript;                       //Store a reference to our WeaponManager.
    //private int level = 3;                                  //Current level number, expressed in game as "Day 1".

    //Awake is always called before any Start functions
    void Awake()
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
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached WeaponManager script
        weaponManagerScript = GetComponent<WeaponManager>();

        //Call the InitGame function to initialize the first level 
        //InitGame();
    }

    //Initializes the game for each level.
    //void InitGame()
    //{
    //    //Call the SetupScene function of the BoardManager script, pass it current level number.
    //    boardScript.SetupScene(level);

    //}



    //Update is called every frame.
    void Update()
    {

    }

}
