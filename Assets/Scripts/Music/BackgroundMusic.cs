using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance = null;
    
    public Slider volumeSlider;
    
    void Awake()
    {    
        //make this object a singleton.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume()
    {
        this.GetComponent<AudioSource>().volume = volumeSlider.value;
    }
}
 