using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Canvas loadingCanvas;
    public Text loadingText;
    public string[] tips;
    public Text tipText;

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
        loadingCanvas.gameObject.SetActive(true);
        loadingText.text = "Loading...";
        tipText.text = tips[Random.Range(0, tips.Length)];
    }

    public void LoadingComplete()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SoundManager.Play("maintheme");
            SoundManager.Stop("title");
        }
        loadingText.text = "Complete!";
        loadingCanvas.gameObject.SetActive(false);
    }
}
