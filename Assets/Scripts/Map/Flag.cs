using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Flag : MonoBehaviour
{
    public string owner;

    private Sprite neutralFlagImage;
    private Sprite blueFlagImage;
    private Sprite redFlagImage;

    private float captureTime;
    private float countdown;
    private SpriteRenderer flagSpriteRenderer;
    private List<GameObject> currentCollisionsList;

    private void Start()
    {
        neutralFlagImage = Resources.Load<Sprite>("Images/circle_flag_icon");
        blueFlagImage = Resources.Load<Sprite>("Images/circle_blue_flag_icon");
        redFlagImage = Resources.Load<Sprite>("Images/circle_red_flag_icon");
        
        captureTime = 2f;
        countdown = captureTime;
        flagSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        flagSpriteRenderer.sprite = neutralFlagImage;
        currentCollisionsList = new List <GameObject> ();
    }

    void Update()
    {
        if (countdown <= 0f)
        {
            ChangeOwner("TeamRed");
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    private void ChangeOwner(string team)
    {
        Sprite ownerSprite;
        switch (team)
        {
            case "TeamBlue":
                ownerSprite = blueFlagImage;
                break;
            case "TeamRed":
                ownerSprite = redFlagImage;
                break;
            default:
                ownerSprite = neutralFlagImage;
                break;
        }

        flagSpriteRenderer.sprite = ownerSprite;
        owner = team;
    }

    private void DecideOwner()
    {
        
    }
     
    void OnCollisionEnter (Collision col) 
    {
        currentCollisionsList.Add (col.gameObject);
        foreach (GameObject gObject in currentCollisionsList) 
        {
            Debug.Log(gObject);
        }
    }
 
    void OnCollisionExit (Collision col) 
    {
        currentCollisionsList.Remove (col.gameObject);
        foreach (GameObject gObject in currentCollisionsList) 
        {
            Debug.Log(gObject);
        }
    }
}