using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MaskPieceInteraction : MonoBehaviour 
{

    public Image maskImage;
    public bool playerProx;

    void Start()
    {
        
    }

    void Update()
    {
        if (playerProx)
        {
            maskImage.enabled = true;
            StartCoroutine(FadeInImage(maskImage, 1.5f));
        }
        //If the player is in the zone, enable the image. 
        //Then runs a method to fade in the image
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerProx = true;
            Debug.Log("Entered Zone");
            //Checks to see if the player entered the sign zone
        }
    }
    IEnumerator FadeInImage(Image img, float duration)
    {
        Color c = img.color;
        c.a = 0f;
        img.color = c;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = t / duration;
            img.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        //fade in colors of image
        img.color = new Color(c.r, c.g, c.b, 1f);
        
        
        yield return new WaitForSecondsRealtime(2f);
        //Waits for 2 seconds before fading out

        float startAlpha = img.color.a;

        float endAlpha = 0.0f;
        for (float time = 0.0f; time < duration; time += Time.deltaTime)
        {
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            img.color = new Color(img.color.r, img.color.g, img.color.b, newAlpha);
            yield return null; // Wait until the next frame
        }
        //Fades the image out
    }

}

