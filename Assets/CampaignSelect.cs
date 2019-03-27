using UnityEngine;

public class CampaignSelect : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void PlayCampaing1()
    {
        sceneFader.FadeTo("Game");
    }

    public void ReturnMenu()
    {
        sceneFader.FadeTo("Menu");
    }
}
