using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject TransitionPanel;

   public void LoadScene(string sceneName)
    {
        
        StartCoroutine(FadeIn(sceneName));
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Transition Panel Code to not always be there.
    IEnumerator FadeIn(string sceneName)
    {
        TransitionPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
