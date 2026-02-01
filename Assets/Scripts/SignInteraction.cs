using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SignInteraction : MonoBehaviour
{
    public Image signImage;
    public bool playerProx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerProx)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                signImage.enabled = !signImage.enabled;
            }
        }
        //Lets the user interact with the sign, but only if they are in proximity

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerProx = true;
            Debug.Log("Entered Zone");
            //Checks to see if the player entered the sign zone
        }
        
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerProx = false;
            Debug.Log("Exit Zone");
            signImage.enabled = false;
            //Checks to see if the player exited the sign zone
            //If so, turns off the image 
        }
    }
    
}
