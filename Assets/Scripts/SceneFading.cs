using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneFading : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image faderImage;
    public float fadeDuration = 1.0f;
    private static SceneFading instance;

    void Awake()
    {
        // Make the fader persist across scenes
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            faderImage.color = new Color(0, 0, 0, 1); // Start fully black
            FadeIn();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(0f));
    }

    public void FadeOut(string sceneName)
    {
        
        StartCoroutine(FadeRoutine(1f, sceneName));
        
    }

    private IEnumerator FadeRoutine(float targetAlpha, string sceneName = "")
    {
        float startAlpha = faderImage.color.a;
        float timer = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            faderImage.color = new Color(faderImage.color.r, faderImage.color.g, faderImage.color.b, newAlpha);
            yield return null; // Wait for the end of the frame
        }

        faderImage.color = new Color(faderImage.color.r, faderImage.color.g, faderImage.color.b, targetAlpha);


        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
