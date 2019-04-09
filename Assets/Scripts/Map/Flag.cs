using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Flag : MonoBehaviour
{
    public string owner;

    private Sprite neutralFlagImage;
    private Sprite blueFlagImage;
    private Sprite redFlagImage;

    private float captureTime;

    private void Start()
    {
        captureTime = 10f;
        
        neutralFlagImage = Resources.Load<Sprite>("Images/circle_flag_icon");
        blueFlagImage = Resources.Load<Sprite>("Images/circle_blue_flag_icon");
        redFlagImage = Resources.Load<Sprite>("Images/circle_red_flag_icon");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = redFlagImage;
    }


    private void ChangeOwner(string team)
    {
        Sprite ownerSprite;
        if (team == "TeamBlue")
        {
            ownerSprite = blueFlagImage;
        }
        else if (team == "TeamRed")
        {
            ownerSprite = redFlagImage;
        }
        else
        {
            ownerSprite = neutralFlagImage;
        }

        this.gameObject.GetComponent<SpriteRenderer>().sprite = ownerSprite;
        owner = team;
    }
}