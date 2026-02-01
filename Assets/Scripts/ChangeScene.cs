using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DelayedSceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadAfterDelay(20f)); // 3 second delay
    }

    IEnumerator LoadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("2026_GGJ_Programming");
    }
}
