using UnityEngine;
using UnityEngine.UI;
public class MaskPieceInteraction : MonoBehaviour 
{

    public Image maskImage;
    public bool playerProx;

    void Start()
    {
        if(playerProx)
        {
            maskImage.enabled = true;
        }
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerProx = true;
            Debug.Log("Entered Zone");
            //Checks to see if the player entered the sign zone
        }
    }
    
}

