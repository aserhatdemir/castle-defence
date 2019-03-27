using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        Color tempColor = fadeImage.color;
        
        while (t > 0f)
        {
            t -= Time.deltaTime;
            tempColor.a = t;
            fadeImage.color = tempColor;
            yield return 0;   //wait a frame
        }
    }
    
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        Color tempColor = fadeImage.color;
        
        while (t < 1f)
        {
            t += Time.deltaTime;
            tempColor.a = t;
            fadeImage.color = tempColor;
            yield return 0;   //wait a frame
        }

        SceneManager.LoadScene(scene);
    }
}