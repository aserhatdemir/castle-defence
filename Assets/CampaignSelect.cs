using UnityEngine;

public class CampaignSelect : MonoBehaviour
{
    public SceneFader sceneFader;
    
    public void PlayCampaign1()
    {
        sceneFader.FadeTo("Campaign1");
    }
    
    public void PlayCampaign2()
    {
        sceneFader.FadeTo("Campaign2");
    }

    public void ReturnMenu()
    {
        sceneFader.FadeTo("Menu");
    }
}
